<?php
    session_start();

    // Holds the Google application Client Id, Client Secret and Redirect Url
    require_once('settings.php');

    // Holds the various APIs involved as a PHP class. Download this class at the end of the tutorial
    require_once('google-login-api.php');

    // Google passes a parameter 'code' in the Redirect Url
    if(isset($_GET['code'])) {
        try {
            $gapi = new GoogleLoginApi();
            
            // Get the access token 
            $data = $gapi->GetAccessToken(CLIENT_ID, CLIENT_REDIRECT_URL, CLIENT_SECRET, $_GET['code']);

            // Access Tokem
            $access_token = $data['access_token'];
            
            // Get user information
            $user_info = $gapi->GetUserProfileInfo($access_token);
          
            echo '<pre>';print_r($user_info); echo '</pre>';

            // Now that the user is logged in you may want to start some session variables
            $_SESSION['logged_in'] = 1;

            // You may now want to redirect the user to the home page of your website
            // header('Location: home.php');

            $_SESSION['email'] = $user_info[emails][0][value];
            header('Location: ' . $_SESSION['callback']);
        }
        catch(Exception $e) {
            echo $e->getMessage();
            exit();
        }

        // Make auth tokken for server

        $identValue = $user_info[emails][0][value];
    }
?>
