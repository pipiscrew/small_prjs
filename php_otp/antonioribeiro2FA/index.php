<?php
@session_start();

require_once('Google2FA.php');

$google2fa = new Google2FA();

$userSecret = $google2fa->generateSecretKey();
$_SESSION['secret'] = $userSecret;

print "Please enter the following secret into your phone:" . PHP_EOL .  $userSecret . PHP_EOL;

$companyName = "test";
$companyEmail = "test@test.met";

$qrCodeUrl = $google2fa->getQRCodeUrl(
    $companyName,
    $companyEmail,
    $userSecret
);

echo $qrCodeUrl ;

// Use your own QR Code generator to generate a data URL:
// $google2fa_url = custom_generate_qrcode_url($qrCodeUrl);

?>

<!-- https://davidshimjs.github.io/qrcodejs/ -->
<script src="qrcode.min.js"></script>


<body> 
<html>

<hr>

<form class="form-signin" method="GET" action="validate.php">
    <h2 class="form-signin-heading">Use any OTP service <br> scan the QR Code</h2>

    <!-- geneate the QRcode via JS -->
    <div id="qrcode"></div>

    <script type="text/javascript">
        new QRCode(document.getElementById("qrcode"), '<?= $qrCodeUrl ?>');
    </script>

    </br>
    <label for="umail" class="sr-only">Answer</label>
    <input name="passw2FA" id="passw2FA" class="form-control" placeholder="Answer" required>

    <button class="btn btn-lg btn-primary btn-block" type="submit">Login 2FA</button>
</form>

</html>
</body> 
