<?php
    session_start();

    $debug = false;

    require_once('AMFuncs.php');

    if(isset($_SESSION['email']))
    {
        if($debug)
        {
            echo '</br> EMIAL SET</br>';
            echo '</br> Auth: ' . $_SESSION['auth'] . '</br>';
        }

        $secTokken = $_GET['sec'];
        $ident = $_SESSION['email'];
        
        $authTokken = CheckSecTokken('localhost:44320', $secTokken, $ident);

        $targetURL = 'localhost' . urldecode($_GET['callback']) . '?auth=' . $authTokken;

        echo 'TARGET URL: ' .  $targetURL . '</br>';
    }


?>
<!DOCTYPE html>
<link rel="stylesheet" href='https://cdn.jsdelivr.net/brutusin.json-forms/1.3.2/css/brutusin-json-forms.min.css'/>
<link rel="stylesheet" href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'/>
<script src="https://code.jquery.com/jquery-1.12.2.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
<script src="https://cdn.jsdelivr.net/brutusin.json-forms/1.3.2/js/brutusin-json-forms.min.js"></script>
<script src="https://cdn.jsdelivr.net/brutusin.json-forms/1.3.2/js/brutusin-json-forms-bootstrap.min.js"></script>
<html>
<head>
<style>
.center {
    margin: auto;
    width: 90%;
    border: 3px solid #D3D3D3;
    text-align: center;
    padding: 50px;
}
p{
    font-size: 30px;
}
a {
    font-size: 30px;
}

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
<meta charset="utf-8" />
<title>Example Resouce</title>
</head>
<body>
    <?php
    
    if(!isset($_SESSION['email']))
    {
        require_once('../settings.php');

        $_SESSION['callback'] = $_SERVER['REQUEST_URI'];

        $google_login_url = 'https://accounts.google.com/o/oauth2/v2/auth?scope=' . urlencode('https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.me') . '&redirect_uri=' . urlencode(CLIENT_REDIRECT_URL) . '&response_type=code&client_id=' . CLIENT_ID . '&access_type=online';        

        echo '<img src="images/stop.svg" alt="No entry" style="width:300px;height:300px;margin:50px;"></br>';

        echo '<a href=' . $google_login_url . ' class="loginBtn loginBtn--google button">Login with Google</a>';
    }

    ?>
</body>
</html>