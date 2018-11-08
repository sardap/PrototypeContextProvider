<?php
    session_start();

    require_once('../ResFuncs.php');
    require_once('../policyFunc.php');

    $resID = str_replace("/", "", $_SERVER['REQUEST_URI']);
    $phpExtPos = strpos($resID, ".php");
    $resID = substr($resID, 0, $phpExtPos);
    $resID = str_replace(".php", "", $resID);
    $resID = str_replace(".", "", $resID);

    $authTokkenUsed = isset($_GET['auth']);

    $policyVaild = false;

    $debug = isset($_GET['debug']) && $_GET['debug'] == 1;

    if($authTokkenUsed)
    {
        $authTokkenVaild = CheckAuthTokken('localhost:44320', $_GET['auth'], $resID);

        if($debug)
            echo 'AUTH TOKKEN RESULT: ' . $authTokkenVaild . '</br>';
    }

    if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['someAction']))
    {
        ?>
        <style type="text/css">
        #shareButton {
            display:none;
        }
        </style>
        <?php

        $apiKey = 'FDBB583AEA18B2DA3142C2F894C0ED42D2074D1FA78CE0B1FFF29D2D740E7FAB48DC54258A4BA06DE6DBE677A1DA4CBB946A0169BEBDB5BC46CF83F3D2891AB352E2081EB0484E759192C3A4891D5F47292F87412864';

        //$resID = rawurlencode($_SERVER['REQUEST_URI']);

        $shareToken = GetShareTokken('localhost:44320', $apiKey, $resID);

        $encodedURL = rawurlencode($_SERVER['REQUEST_URI']);

        $newURL = 'AM/ShareRes.php?shareToken=' 
            . $shareToken
            . '&resID=' . $resID
            . '&callback=' . $encodedURL;

        $newURL = 'http://localhost/myphp/' . $newURL;

        echo '</br>' . $newURL;
    
        header('Location: '.$newURL);
    }

    $auth = !(!isset($_SESSION['login']) && !($authTokkenUsed && $authTokkenVaild == 1));
    if($debug)
    {
        echo '</br>POLICY VAILD:' . ($policyVaild ? 'TRUE' : 'FALSE') . '</br>';
        echo 'TOKKEN: ' . ($tokkenAuth ? 'TRUE' : 'FALSE') . '</br>';
        echo 'AUTH: ' . ($auth ? 'TRUE' : 'FALSE') . '</br>';
    }
?>
<!DOCTYPE html>
<script>
var interval = null;
var url_string = window.location.href;
var url = new URL(url_string);
var authTokken = url.searchParams.get("auth");
var ready = false;

function httpGetAsync(theUrl, callback)
{
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function() { 
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", theUrl, true); // true for asynchronous 
    xmlHttp.send(null);
}

function Check(responseText)
{
    var result = false;

    if(responseText == "1")
    {
        result = true;
    }
    else if(responseText == "0")
    {
        result = false;
    }

    var elem = document.getElementById('datares');
    var revokeMessage = document.getElementById('revokeMessage');
    if(!result)
    {
        elem.style.display = 'none';
        revokeMessage.style.display = "";
    }
    else
    {
        elem.style.display = "";
        revokeMessage.style.display = "none";
    }

    ready = true;
}

function SetUpResponse(responseText)
{
    if(interval == null)
    {
        interval = parseInt(responseText);
        window.setInterval( function(){
            if(ready)
            {
                var url = "https://localhost:44320/api/values/CheckCon/" + authTokken; 
                httpGetAsync(url, Check)
                ready = false;
            }
          },
        interval)  
    }

    ready = true;
}


function SetUp()
{
    var url = "https://localhost:44320/api/values/CheckCon/GetInterval/" + authTokken; 
    httpGetAsync(url, SetUpResponse);
}

if(authTokken != null)
    SetUp();
</script>
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

a.button {
    -webkit-appearance: button;
    -moz-appearance: button;
    appearance: button;

    text-decoration: none;
    color: initial;
}
</style>
</head>
<body>
    <?php
        if($authTokkenUsed && $debug)
            echo '<p id="auth">' . $_GET['auth'] . '</p>';

        echo '<div class="center">';
        if($auth)
        {
            echo '</br><iframe id="datares" width="1500" height="700" src="https://www.youtube.com/embed/live_stream?channel=UCaCByf9MOMDmalzR79tFtjw" frameborder="0" allowfullscreen></iframe>';            
            echo '<p id="revokeMessage" style="display:none" >You have been Revoked access</p></br>';
        }
        else
        {
            if(!$authTokkenUsed)
            {
                echo '<img src="images/stop.svg" alt="No entry" style="width:300px;height:300px;"></br>';

                $login_url = 'ExampleResLogin.php';
                echo '<a href=' . $login_url . '>Log in</a>';
            }
            else
            {
                echo '<p>you can\'t see this</p>';
            }
        }
    ?>
    
    <?php
        if($debug)
        {
            echo 'AUTHTOKKENUSED:' . ($tokkenAuth ? 1 : 0) . '</br>';
        }

        if($auth && !$authTokkenUsed)
        {
            $login_url = 'localhost\.php?resid=' . $resID;

            if($debug)
                echo $resID . '</br>' . $login_url . '</br>';

            echo '
            <form action="ExampleRes.php" method="post" id="shareButton">
                <input type="submit" name="someAction" value="SHARE" />
            </form>
            ';

        }
    ?>

    <?php
        echo '</div>';
    ?>

</body>
</html>