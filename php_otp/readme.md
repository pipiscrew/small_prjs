# OTP

Each **folder** is separate **sample** project, using the following libraries :   

* [PHPGangsta/GoogleAuthenticator](https://github.com/PHPGangsta/GoogleAuthenticator) (only SHA1)
  * can use [QRCode Generator API](https://goqr.me/api/)
* [antonioribeiro/google2fa](https://github.com/antonioribeiro/google2fa) (has more settings compare PHPGangsta)
  * in this sample the library has been monolith (all PHP files merged to one)

> both projects using [QRCodeJS](https://davidshimjs.github.io/qrcodejs/), to generate QRCode. Otherwise in PHP, needed to enable a PHP extension.  

other libraries :  
* [RobThree/TwoFactorAuth](https://github.com/RobThree/TwoFactorAuth)
* (minimal) [Adminer - OTP](https://www.adminer.org/sk/plugins/otp/) or at [vrana repo](https://github.com/vrana/adminer/blob/master/plugins/login-otp.php).  

---

Always use online server, make sure your server-time is [NTP-synced](https://en.wikipedia.org/wiki/Network_Time_Protocol)! Depending on the $discrepancy allowed your time cannot drift too much from the users time!, otherwise will fail always. [src](https://github.com/RobThree/TwoFactorAuth/blob/master/demo/demo.php)  

---

To  generate OTP use the original [Redhat.FreeOTP](https://freeotp.github.io/).  

There are two standards :  
* HOTP (HMAC-Based One-Time Password) 
* TOTP (Time-Based One-Time Password) 

both methods for generating one-time passwords used in two-factor authentication (2FA) and other security applications. They share some similarities but differ in how they generate these temporary passwords.  

* **HOTP** (HMAC-Based One-Time Password):
  * `Algorithm`: HOTP is based on the HMAC-SHA-1 (Hash-based Message Authentication Code using Secure Hash Algorithm 1) algorithm. However, more secure hash algorithms like HMAC-SHA-256 or HMAC-SHA-512 can also be used.
  * `Counter-Based`: HOTP relies on a counter value that increments with each authentication attempt. The counter value is used as input to the HMAC function along with a secret key to generate the OTP.
  * `Time-Independence`: HOTP does not depend on the current time, making it immune to time-related attacks but requiring synchronization between the server and the client's counter.  

* **TOTP** (Time-Based One-Time Password):
  * `Algorithm`: TOTP also uses HMAC, but it's time-based rather than counter-based. It's typically based on a time interval, usually 30 seconds.
  * `Time-Based`: TOTP generates OTPs based on the current time, usually in 30-second intervals. Both the client and server must have synchronized clocks.
  * `Counter Independence`: TOTP doesn't rely on a counter, making it more convenient for systems where clock synchronization is feasible.
