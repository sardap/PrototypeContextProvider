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
        function CheckPolciy($domain, $apiKey, $resouceID)
        {
            $url = 'https://' . $domain . '/api/values/' . $apiKey . '/' . $resouceID;
            //Initialize cURL.
            $ch = curl_init();

            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
            //Set the URL that you want to GET by using the CURLOPT_URL option.
            curl_setopt($ch, CURLOPT_URL, $url);
            //Set CURLOPT_RETURNTRANSFER so that the content is returned as a variable.
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            //Set CURLOPT_FOLLOWLOCATION to true to follow redirects.
            curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true);
            //Execute the request.
            $data = curl_exec($ch);
            //Close the cURL handle.
            curl_close($ch);
            //Print the data out onto the page.
            return $data;
        }

        echo CheckPolciy('localhost:44320', '9A38075E807090757AAA40FF9470B499D63A56E01BB92F680E3EE09A25DE9D994AFB4E2B940A8197107A5D33C9CC364A5147D79F39C76439E90B4A3A224890C97807849810386707836B23B8C99FF5389AA94659792D', '1');
        
    ?>
    <?php
        require_once('settings.php');

        $login_url = 'https://accounts.google.com/o/oauth2/v2/auth?scope=' . urlencode('https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.me') . '&redirect_uri=' . urlencode(CLIENT_REDIRECT_URL) . '&response_type=code&client_id=' . CLIENT_ID . '&access_type=online';
    ?>

</body>
</html>