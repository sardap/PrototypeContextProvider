<?php
    session_start();

    $debug = false;

    require_once('AMFuncs.php');

    if (!isset($_GET['targeturl']))
    {
        echo 'Missing targeturl</br>';
    }
    
    $targetURL = 'localhost' . urldecode($_GET['targeturl'])

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
    
    echo '
    <div style="margin-top:50px" class="center">
        <p>Share Tokken Created Sucesfully!</p></br>
        <p>' . $targetURL . '</p>
    </div>
    ';
    ?>
</body>
</html>