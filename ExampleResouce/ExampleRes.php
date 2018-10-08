<?php
    session_start();

    require_once('ResFuncs.php');
    require_once('policyFunc.php');

    $resID = str_replace("/", "", $_SERVER['REQUEST_URI']);
    $phpExtPos = strpos($resID, ".php");
    $resID = substr($resID, 0, $phpExtPos);
    $resID = str_replace(".php", "", $resID);
    $resID = str_replace(".", "", $resID);

    $tokkenAuth = isset($_GET['auth']) || isset($_SESSION['auth']);

    $policyVaild = false;

    if(isset($_SESSION['email']))
    {
        echo '</br> EMIAL SET</br>';
        echo '</br> Auth: ' . $_SESSION['auth'] . '</br>';
        $apiKey = 'A39D69138C7C1729A02A4D8FC78B7BFEE261C047B11F6E7BBF76E07AB38DD1C87395BC5253426D6DD5E95678DE2E5AE0F22B5A705473E371D6724D363C5DE09EACE6332BB3419CE8A9030285D81D9CE44BA9C7EFDA40';
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
            echo '<table>
            <tr>
                <th>Subject</th> 
                <th>Mark</th>
                <th>Grade</th>
            </tr>
            <tr>
                <td>Research report A</td>
                <td>10000</td> 
                <td>VEHD</td> 
            </tr>
            <tr>
                <td>Paul is cool</td>
                <td>50</td> 
                <td>P</td> 
            </tr>
            <tr>
                <td>LSD</td>
                <td>0</td> 
                <td>F</td> 
            </tr>
            </table>';
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
                require_once('settings.php');

                $_SESSION['auth'] = $_GET['auth'];

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
            $apiKey = 'A39D69138C7C1729A02A4D8FC78B7BFEE261C047B11F6E7BBF76E07AB38DD1C87395BC5253426D6DD5E95678DE2E5AE0F22B5A705473E371D6724D363C5DE09EACE6332BB3419CE8A9030285D81D9CE44BA9C7EFDA40';
            require_once('ResFuncs.php');

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