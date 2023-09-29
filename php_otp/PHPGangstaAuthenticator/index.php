<?php
@session_start();

require_once('GoogleAuthenticator.php');

$ga = new PHPGangsta_GoogleAuthenticator();
$userSecret = $ga->createSecret();
echo "Secret is: ".$userSecret."\n\n";

// $qrCodeUrl = $ga->getQRCodeGoogleUrl('Blog', $secret);
// echo "Google Charts URL for the QR-Code: ".$qrCodeUrl."\n\n";

// $oneCode = $ga->getCode($secret);
// echo "Checking Code '$oneCode' and Secret '$secret':\n";



//used on validate.php
 $_SESSION['secret'] = $userSecret;


 //QRCode details
$companyName = "test";
$companyEmail = "test@test.met";



$qrCodeUrl = $ga->getQRCode2(
    $companyName,
    $companyEmail,
    $userSecret
);

//get short QRCODE
// $qrCodeUrl = $ga->getQRCode('Blog', $userSecret);
// echo $qrCodeUrl;

//generate QRCode by online API
// $qrCodeUrl = $ga->getQRCodeGoogleUrl('Blog', $userSecret);
// echo $qrCodeUrl ;

?>

<!-- https://davidshimjs.github.io/qrcodejs/ -->
<script src="qrcode.min.js"></script>


<body> 
<html>

</br>

<hr>

<form class="form-signin" method="GET" action="validate.php">
    <h2 class="form-signin-heading">Use any OTP service <br> scan the QR Code</h2>

    <!-- geneate the QRcode via JS -->
    <div id="qrcode"></div>

    <script type="text/javascript">
        new QRCode(document.getElementById("qrcode"), '<?= $qrCodeUrl ?>');
    </script>

    </br>
    <label for="passw2FA" class="sr-only">Answer</label>
    <input name="passw2FA" id="passw2FA" class="form-control" placeholder="Answer" required>

    <button class="btn btn-lg btn-primary btn-block" type="submit">Login 2FA</button>
</form>

</html>
</body> 
