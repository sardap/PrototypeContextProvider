<?php
    session_start();

    require_once('AMFuncs.php');

    if (!isset($_GET['shareToken']))
    {
        echo 'Missing shareToken</br>'
    }
    if (!isset($_GET['resID']))
    {
        echo 'Missing resID</br>'
    }

    $shareToken = 

    $tokkenAuth = isset($_GET['auth']) && $tokkenAuth = CheckTokken('localhost:44320', $_GET['auth'], $resID);
    $apiKey = '9A38075E807090757AAA40FF9470B499D63A56E01BB92F680E3EE09A25DE9D994AFB4E2B940A8197107A5D33C9CC364A5147D79F39C76439E90B4A3A224890C97807849810386707836B23B8C99FF5389AA94659792D';

    $policyVaild = false;

    if(isset($_SESSION['email']))
    {
        $policyVaild = CheckPolciy('localhost:44320', $apiKey, $resID, $_SESSION['email']);
    }

    echo '</br>POLICY VAILD:' . $policyVaild . '</br>';
    $auth = !(!isset($_SESSION['login']) && !($tokkenAuth && $policyVaild));
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
            $login_url = 'AddResouce.php?resid=' . $resID;
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
            $apiKey = '9A38075E807090757AAA40FF9470B499D63A56E01BB92F680E3EE09A25DE9D994AFB4E2B940A8197107A5D33C9CC364A5147D79F39C76439E90B4A3A224890C97807849810386707836B23B8C99FF5389AA94659792D';
            require_once('ResFuncs.php');
            $newAuthTokken = CreateTokken('localhost:44320', $apiKey, $resID);
            echo 'url:http://localhost/myphp/ExampleRes.php?auth=' . $newAuthTokken;
        }
    ?>

</body>
</html>