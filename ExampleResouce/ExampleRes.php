<?php
    session_start();

    require_once('ResFuncs.php');

    $tokkenAuth = isset($_GET['auth']) && $tokkenAuth = CheckTokken('localhost:44320', $_GET['auth']);

    if(
        !isset($_SESSION['login']) &&
        !($tokkenAuth)
    ) {
        header('LOCATION:ExampleResLogin.php'); die();
    }
?>
<!DOCTYPE html>
<script src="scripts/main.js"></script>
<html>
<head>

<meta charset="utf-8" />
<title>Example Resouce</title>
</head>
<body>
    <table>
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
    </table>
    <?php
        $resID = str_replace("/", "", $_SERVER['REQUEST_URI']);
        $resID = str_replace(".php", "", $resID);
        $resID = str_replace(".", "", $resID);

        echo 'AUTHTOKKENUSED:' . ($tokkenAuth ? 1 : 0) . '</br>';

        if(!$tokkenAuth)
        {
            echo $resID . '</br>';
            $login_url = 'AddResouce.php?resid=' . $resID;
            echo '<a href="<?= $login_url ?>">Share</a>';
        }
    ?>

</body>
</html>