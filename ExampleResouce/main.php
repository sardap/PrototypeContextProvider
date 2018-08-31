<!DOCTYPE html>
<html>
    <head>
  
    <meta charset="utf-8" />
    <title>PHP Application in Linux Subsystem for Windows</title>
    </head>
    <body>
        <h1>PHP Application in Linux Subsystem for Windows<h1>
        <?php
            //The URL that we want to GET.
            $url = 'https://localhost:44320/api/values';
            
            //Use file_get_contents to GET the URL in question.
            $contents = file_get_contents($url);
            
            //If $contents is not a boolean FALSE value.
            if($contents !== false){
                //Print out the contents.
                echo $contents;
            }        
        ?>
    </body>
</html>