<?php
    function CheckTokken($domain, $tokken)
    {
        $url = 'https://' . $domain . '/api/values/checkTokken/' . $tokken;
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
        return $data == 1 ? true : false;
    }
?>

