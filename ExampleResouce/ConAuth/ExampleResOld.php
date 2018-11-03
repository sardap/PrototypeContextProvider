<?php
    session_start();

    require_once('../ResFuncs.php');
    require_once('../policyFunc.php');

    $debug = false;

    $resID = str_replace("/", "", $_SERVER['REQUEST_URI']);
    $phpExtPos = strpos($resID, ".php");
    $resID = substr($resID, 0, $phpExtPos);
    $resID = str_replace(".php", "", $resID);
    $resID = str_replace(".", "", $resID);

    $tokkenAuth = isset($_GET['auth']) || isset($_SESSION['auth']);

    $policyVaild = false;
    
    $apiKey = 'FDBB583AEA18B2DA3142C2F894C0ED42D2074D1FA78CE0B1FFF29D2D740E7FAB48DC54258A4BA06DE6DBE677A1DA4CBB946A0169BEBDB5BC46CF83F3D2891AB352E2081EB0484E759192C3A4891D5F47292F87412864';

    if($debug)
        echo 'API KEY: ' . $apiKey;
    
    if(isset($_SESSION['email']))
    {
        if($debug)
            echo '</br> EMIAL SET</br>';
        $auth = isset($_SESSION['auth']) ? $_SESSION['auth'] : $_GET['auth'];
        str_replace("\n", "", $auth);

        if($debug)
        {
            echo '</br><p id="auth">'.$auth.'</p></br>';
            echo 'API KEY: ' . $apiKey;
        }
    
        $policyVaild = CheckPolicy('localhost:44320', $apiKey, $auth, $resID, $_SESSION['email']) == 1 ? true : false;
    }

    $auth = !(!isset($_SESSION['login']) && !($tokkenAuth && $policyVaild));

    if($debug)
    {
        echo '</br>POLICY VAILD:' . ($policyVaild ? 'TRUE' : 'FALSE') . '</br>';
        echo 'TOKKEN: ' . ($tokkenAuth ? 'TRUE' : 'FALSE') . '</br>';
        echo 'AUTH: ' . ($auth ? 'TRUE' : 'FALSE') . '</br>';
    }
?>
<!DOCTYPE html>
<script src="main.js"></script>
<html>
<head>
<meta charset="utf-8" />

<meta name="google-signin-scope" content="profile email">
<meta name="google-signin-client_id" content="YOUR_CLIENT_ID.apps.googleusercontent.com">
<script src="https://apis.google.com/js/platform.js" async defer></script>

<title>Example Resouce</title>
<style>

.center {
    margin: auto;
    width: 90%;
    border: 3px solid #D3D3D3;
    text-align: center;
    padding: 50px;
}
a {
    font-size: 30px;
}

body { padding: 2em; }


/* Shared */
.loginBtn {
  box-sizing: border-box;
  position: relative;
  /* width: 13em;  - apply for fixed size */
  margin: 0.2em;
  padding: 0 15px 0 46px;
  border: none;
  text-align: left;
  line-height: 34px;
  white-space: nowrap;
  border-radius: 0.2em;
  font-size: 16px;
  color: #FFF;
}
.loginBtn:before {
  content: "";
  box-sizing: border-box;
  position: absolute;
  top: 0;
  left: 0;
  width: 34px;
  height: 100%;
}
.loginBtn:focus {
  outline: none;
}
.loginBtn:active {
  box-shadow: inset 0 0 0 32px rgba(0,0,0,0.1);
}


/* Facebook */
.loginBtn--facebook {
  background-color: #4C69BA;
  background-image: linear-gradient(#4C69BA, #3B55A0);
  /*font-family: "Helvetica neue", Helvetica Neue, Helvetica, Arial, sans-serif;*/
  text-shadow: 0 -1px 0 #354C8C;
}
.loginBtn--facebook:before {
  border-right: #364e92 1px solid;
  background: url('https://s3-us-west-2.amazonaws.com/s.cdpn.io/14082/icon_facebook.png') 6px 6px no-repeat;
}
.loginBtn--facebook:hover,
.loginBtn--facebook:focus {
  background-color: #5B7BD5;
  background-image: linear-gradient(#5B7BD5, #4864B1);
}


/* Google */
.loginBtn--google {
  /*font-family: "Roboto", Roboto, arial, sans-serif;*/
  background: #DD4B39;
}
.loginBtn--google:before {
  border-right: #BB3F30 1px solid;
  background: url('https://s3-us-west-2.amazonaws.com/s.cdpn.io/14082/icon_google.png') 6px 6px no-repeat;
}
.loginBtn--google:hover,
.loginBtn--google:focus {
  background: #E74B37;
}

a.button {
    -webkit-appearance: button;
    -moz-appearance: button;
    appearance: button;

    text-decoration: none;
    color: initial;
}
</style>
</head>
<title>Example Resouce</title>
</head>
<body>
    <?php
        echo '<div class="center">';
        if($auth)
        {
            if($debug)
                echo '</br><h>DATA RESOUCE START</h></br>';
    
            echo '</br><iframe id="datares" width="560" height="315" src="https://www.youtube.com/embed/live_stream?channel=UCaCByf9MOMDmalzR79tFtjw" frameborder="0" allowfullscreen></iframe></br>';
    
            if($debug)
                echo '</br><h>DATA RESOUCE END</h></br></br>';
        }
        else
        {
            if(!$tokkenAuth)
            {
                echo '<img src="images/stop.svg" alt="No entry" style="width:300px;height:300px;margin:50px;"></br>';
                $login_url = 'ExampleResLogin.php';
                echo '<a href=' . $login_url . '>Log in</a>';
            }
            else if(!$policyVaild)
            {
                require_once('../settings.php');

                if(isset($_GET['auth']))
                    $_SESSION['auth'] = $_GET['auth'];

                $_SESSION['api'] = $apiKey;

                echo '<img src="images/stop.svg" alt="No entry" style="width:300px;height:300px;margin:50px;"></br>';

                $google_login_url = 'https://accounts.google.com/o/oauth2/v2/auth?scope=' . urlencode('https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.me') . '&redirect_uri=' . urlencode(CLIENT_REDIRECT_URL) . '&response_type=code&client_id=' . CLIENT_ID . '&access_type=online';
                echo '<a href=' . $google_login_url . ' class="loginBtn loginBtn--google button">Login with Google</a>';
            }
            else
            {
                echo '<p>you can\'t see this</p>';
            }
        }
    ?>
    
    <?php
        if($debug)
            echo 'AUTHTOKKENUSED:' . ($tokkenAuth ? 1 : 0) . '</br>';

        if($auth && !$tokkenAuth)
        {
            $login_url = 'localhost\.php?resid=' . $resID;

            if($debug)
                echo $resID . '</br>' . $login_url . '</br>';

            echo '
            <form action="ExampleRes.php" method="post" style="margin:50px;">
                <input type="submit" name="someAction" value="SHARE" />
            </form>
            ';

        }
    ?>

    <?php
        if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['someAction']))
        {

            $shareToken = GetShareTokken('localhost:44320', $apiKey, $resID);

            $encodedURL = rawurlencode($_SERVER['REQUEST_URI']);
            
            $newURL = 'AM/ShareRes.php?shareToken=' 
                . $shareToken
                . '&resID=' . $resID
                . '&callback=' . $encodedURL;

            $newURL = 'http://localhost/myphp/' . $newURL;
        
            echo '<a href=' . $newURL . '>Click here to share</a>';

            header('Location: '.$newURL);
        }
        echo '</div>';
    ?>

</body>
</html>