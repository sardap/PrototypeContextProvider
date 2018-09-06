<!DOCTYPE html>
<script src="scripts/main.js"></script>
<html>
    <head>
  
    <meta charset="utf-8" />
    <title>Demo</title>
    </head>
    <body>
        <h1>Stration<h1>
        <?php
            $ch = curl_init();
            
            // Set query data here with the URL
            curl_setopt($ch, CURLOPT_URL, 'https://localhost:44320/api/values'); 
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            curl_setopt($ch, CURLOPT_TIMEOUT, 3);
            $content = trim(curl_exec($ch));
            curl_close($ch);
            print $content;
        ?>
    </body>
</html>