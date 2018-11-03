<?php
    session_start();

    $debug = false;

    require_once('AMFuncs.php');

    if (!isset($_GET['shareToken']))
    {
        echo 'Missing shareToken</br>';
    }
    if (!isset($_GET['resID']))
    {
        echo 'Missing resID</br>';
    }

    $shareTokken = $_GET['shareToken'];
    $resID = $_GET['resID'];

    $shareTokenResult = CheckShareTokken('localhost:44320', $shareTokken, $resID);
    
    if($debug)
    {
        echo 'SHARE TOKKEN REUSLT: ' . $shareTokenResult . '</br>';
    }
?>
<!DOCTYPE html>
<script src="scripts/main.js"></script>
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
input{
    margin: 50px;
}
textarea {
  width: 800px;
  height: 500px;
}
</style>
<meta charset="utf-8" />
<title>Example Resouce</title>
</head>
<body>
    <?php
    
    if($shareTokenResult)
    {
        echo '<div class="center">';
        echo '<p>Entry policy in text box below</p>';
        echo '
        <form method="post">
        <textarea cols="35" rows="12" name="comments" id="para1"></textarea><br>
        <input type="submit" name="someAction" value="SHARE" />
        </form>
        ';

        if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['comments']))
        {
            $data = $_POST['comments'];

            if($debug)
                echo "DATA: " . $data;

            $result = CreateAndApplyPolicy('localhost:44320', $shareTokken, $resID, $data);

            $text = strpos($result, 'ERROR:') !== false ? $result = 'URL: ' . $result  : $result ;

            if($debug)
                echo 'RESULT: ' . $text;

            echo 'Share Tokken: ' . $text;

            

            $newURL = urldecode($_GET['callback'])
                . '?auth=' . $text;

            header('Location: '.$newURL);
        }

        echo '</div>';
    }
    ?>
</body>
</html>