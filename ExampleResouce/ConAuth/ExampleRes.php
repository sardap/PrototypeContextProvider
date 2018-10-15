<?php
    session_start();

    require_once('../ResFuncs.php');
    require_once('../policyFunc.php');

    $resID = str_replace("/", "", $_SERVER['REQUEST_URI']);
    $phpExtPos = strpos($resID, ".php");
    $resID = substr($resID, 0, $phpExtPos);
    $resID = str_replace(".php", "", $resID);
    $resID = str_replace(".", "", $resID);

    $tokkenAuth = isset($_GET['auth']) || isset($_SESSION['auth']);

    $policyVaild = false;
    
    $apiKey = 'FDBB583AEA18B2DA3142C2F894C0ED42D2074D1FA78CE0B1FFF29D2D740E7FAB48DC54258A4BA06DE6DBE677A1DA4CBB946A0169BEBDB5BC46CF83F3D2891AB352E2081EB0484E759192C3A4891D5F47292F87412864';
    echo 'API KEY: ' . $apiKey;
    
    if(isset($_SESSION['email']))
    {
        echo '</br> EMIAL SET</br>';
        echo '</br> Auth: ' . $_SESSION['auth'] . '</br>';
        echo 'API KEY: ' . $apiKey;
        $policyVaild = CheckPolicy('localhost:44320', $apiKey, $_SESSION['auth'], $resID, $_SESSION['email']) == 1 ? true : false;
    }

    echo '</br>POLICY VAILD:' . ($policyVaild ? 'TRUE' : 'FALSE') . '</br>';
    $auth = !(!isset($_SESSION['login']) && !($tokkenAuth && $policyVaild));
    echo 'TOKKEN: ' . ($tokkenAuth ? 'TRUE' : 'FALSE') . '</br>';
    echo 'AUTH: ' . ($auth ? 'TRUE' : 'FALSE') . '</br>';
?>
<!DOCTYPE html>
<script src="scripts/main.js"></script>
<html>
<head>

<meta charset="utf-8" />
<title>Example Resouce</title>
</head>
<body>
    <?php
        if($auth)
        {
            echo '</br><h>DATA RESOUCE START</h></br>';
            echo '</br><p id="dataRes"></p></br>';
            //echo '</br><iframe width="560" height="315" src="https://www.youtube.com/embed/live_stream?channel=UCaCByf9MOMDmalzR79tFtjw" frameborder="0" allowfullscreen></iframe></br>';
            echo '</br><h>DATA RESOUCE END</h></br></br>';
        }
        else
        {
            if(!$tokkenAuth)
            {
                $login_url = 'ExampleResLogin.php';
                echo '<a href=' . $login_url . '>Log in</a>';
            }
            else if(!$policyVaild)
            {
                require_once('../settings.php');

                $_SESSION['auth'] = $_GET['auth'];
                $_SESSION['api'] = $apiKey;

                $google_login_url = 'https://accounts.google.com/o/oauth2/v2/auth?scope=' . urlencode('https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.me') . '&redirect_uri=' . urlencode(CLIENT_REDIRECT_URL) . '&response_type=code&client_id=' . CLIENT_ID . '&access_type=online';
                echo '<a href=' . $google_login_url . '>Login with Google</a>';
            }
            else
            {
                echo '<p>you can\'t see this</p>';
            }
        }
    ?>
    
    <?php
        echo 'AUTHTOKKENUSED:' . ($tokkenAuth ? 1 : 0) . '</br>';

        if($auth && !$tokkenAuth)
        {
            $login_url = 'localhost\.php?resid=' . $resID;
            echo $resID . '</br>' . $login_url . '</br>';

            echo '
            <form action="ExampleRes.php" method="post">
                <input type="submit" name="someAction" value="SHARE" />
            </form>
            ';

        }
    ?>

    <?php
        if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['someAction']))
        {

            $shareToken = GetShareTokken('localhost:44320', $apiKey, $resID);

            $newURL = 'AM/ShareRes.php?shareToken=' 
                . $shareToken
                . '&resID=' . $resID;
            
            echo 'NEW URL:' . $newURL . '</br>';

            //header('Location: '.$newURL);
        }
    ?>

</body>
</html>