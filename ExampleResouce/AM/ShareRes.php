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
</style>
<meta charset="utf-8" />
<title>Example Resouce</title>
</head>
<body>
    <?php
    
    if($shareTokenResult)
    {
        echo '
        <div class="center">
            <p>Entry policy in form box below</p>
            <div id="container"></div>
            <button class="btn btn-primary" onclick="setJsonResult()">Generate Json</button>
            </br>

            <p style="margin-top:50px">Generated Json</p>
            </br>
            <form method="post">
                <textarea id="json_result" cols="200" rows="12" name="comments" id="para1"></textarea><br>
                <input type="submit" name="someAction" value="SHARE" />
            </form>
        </div>
        ';

        '<button 
            class="btn btn-primary" onclick="alert(JSON.stringify(bf.getData(), null, 4))"
        >
        getData()</button>&nbsp;
        <button 
            class="btn btn-primary" onclick="if (bf.validate()) {alert(\'Validation succeeded\')}">validate()</button>';

        if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['comments']))
        {
            $data = $_POST['comments'];

            if($debug)
                echo "DATA: " . $data;

            $result = CreateAndApplyPolicy('localhost:44320', $shareTokken, $resID, $data);

            $newURL = 'ShareResultScreen.php?sec=' . $result . '&callback=' . urlencode($_GET['callback']);

            if(strpos($result, 'ERROR') !== true)
            {
                header('Location: '.$newURL);
            }
            else
            {
                echo 'RESULT: ' . $result;
            }

        }

        echo '</div>';
    }
    ?>
</body>
</html>
<script>

 function setJsonResult()
 {
    var jsonString = JSON.stringify(bf.getData(), null, 4)
    document.getElementById("json_result").value = jsonString;
 }

 var bf = brutusin["json-forms"].create({
  "$schema": "http://json-schema.   org/draft-03/schema#",
  "type": "object",
  "properties": {
    "Author": {
      "type": "string",
      "title": "Author",
      "default":"notpaul",
      "description": "Name of author"
    },
    "Proity": {
      "type": "integer",
      "title": "Priority",
      "minimum": 0,
      "maximum": 100000,
      "default":0,
      "description": "Priority"
    },
    "Decision": {
      "type": "string",
      "title": "Decision",
      "default":"garbage",
      "description": "Decision"
    },
    "DataConsumer": {
      "type": "object",
      "title": "DataConsumer",
      "description": "DataConsumer",
        "properties": {
            "Name": {
                "type": "string",
                "default":"paul",
                "title": "Name"
            },
            "Value": {
                "type": "string",
                "default":"pfsarda23@gmail.com",
                "title": "Value"
            }
        }
    },
    "PrivacyOblgations": {
      "type": "object",
      "title": "PrivacyOblgations",
      "description": "DataConsumer",
        "properties": {
            "Purpose": {
                "type": "string",
                "title": "Purpose",
                "default":"garbage"
            },
            "Granularity": {
                "type": "string",
                "title": "Granularity",
                "default":"garbage"
            },
            "Anonymisation": {
                "type": "string",
                "title": "Anonymisation",
                "default":"garbage"
            },
            "Notifaction": {
                "type": "string",
                "title": "Notification",
                "default":"garbage"
            },
            "Accounting": {
                "type": "string",
                "title": "Accounting",
                "default":"garbage"
            }
        }
    },
    "ResharingObligations": {
      "type": "object",
      "title": "Resharing",
      "description": "Resharing",
        "properties": {
            "CanShare": {
                "type": "boolean",
                "title": "Can Share",
                "default":true
            },
            "Cardinality": {
                "type": "integer",
                "title": "Cardinality",
                "default":10
            },
            "Recurring": {
                "type": "integer",
                "title": "Recurring",
                "default":10
            }
        }
    }
  }
});

var container = document.getElementById('container');
bf.render(container);

</script>
