<?php
    session_start();

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
    
    echo 'SHARE TOKKEN REUSLT: ' . $shareTokenResult . '</br>';
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
      if($shareTokenResult)
      {
          echo '
          <form method="post">
            <textarea cols="35" rows="12" name="comments" id="para1"></textarea><br>
            <input type="submit" name="someAction" value="SHARE" />
          </form>
          ';

          if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['comments']))
          {
            $data = $_POST['comments'];

            echo "DATA: " . $data;

            $result = CreateAndApplyPolicy('localhost:44320', $shareTokken, $resID, $data);

            $text = strpos($result, 'ERROR:') !== false ? $result = 'URL: ' . $result  : $result ;

            echo 'RESULT: ' . $text;
          }
  

      }
    ?>
</body>
</html>