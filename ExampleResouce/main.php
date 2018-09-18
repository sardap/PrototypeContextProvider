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
        require_once('settings.php');

        $login_url = 'https://accounts.google.com/o/oauth2/v2/auth?scope=' . urlencode('https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.me') . '&redirect_uri=' . urlencode(CLIENT_REDIRECT_URL) . '&response_type=code&client_id=' . CLIENT_ID . '&access_type=online';
    ?>
	
    <a href="<?= $login_url ?>">Login with Google</a>
    

</body>
</html>