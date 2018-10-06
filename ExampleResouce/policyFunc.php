<?php
    function CheckPolciy($domain, $apiKey, $resouceID, $name)
    {
        echo 'NAME:' . $resouceID . '</br>';
        $url = 'https://' . $domain . '/api/values/' . $apiKey . '/' . $resouceID . '/' . $name;
        echo $url . '</br>';
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

    function httpPost($url, $data)
    {
        $array = json_decode($data, true); // decode json
        $data_string = json_encode($array);                                                                                   

        $ch = curl_init($url);
        curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($ch, CURLOPT_POSTFIELDS, $data_string);
        curl_setopt($ch, CURLOPT_HTTPHEADER, array(                                                                          
            'Content-Type: application/json',                                                                                
            'Content-Length: ' . strlen($data_string))                                                                       
        );                                                                                                                   
        $response = curl_exec($ch);
        if (curl_error($ch)) {
            throw new \Exception(curl_error($ch));
        }
        curl_close($ch);
    
        return $response;    
    }


    function AddPolicy($domain, $apiKey, $resouceID, $policy)
    {
        $url = 'https://' . $domain . '/api/values/' . $apiKey . '/' . $resouceID;
        echo $url;
        $result = httpPost($url, $policy);
        return $result;
    }
?>

