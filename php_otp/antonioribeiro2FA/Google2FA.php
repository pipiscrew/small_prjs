<?php

// //SecretKeyTooShort.php
interface SecretKeyTooShort extends Throwable
{
}

// //InvalidCharacters.php
interface InvalidCharacters extends Throwable
{
}


// //InvalidAlgorithm.php
interface InvalidAlgorithm extends Throwable
{
}


// //IncompatibleWithGoogleAuthenticator.php
interface IncompatibleWithGoogleAuthenticator extends Throwable
{
}

// //Google2FA.php
interface Google2FAb extends Throwable
{
}

// //Google2FAException.php
class Google2FAException extends Exception implements Google2FAb
{
}

// //InvalidAlgorithmException.php
class InvalidCharactersException extends Google2FAException implements Google2FAb, InvalidCharacters
{
    protected $message = 'Invalid characters in the base32 string.';
}

// //IncompatibleWithGoogleAuthenticatorException.php
class IncompatibleWithGoogleAuthenticatorException extends Google2FAException implements Google2FAb, IncompatibleWithGoogleAuthenticator
{
    protected $message = 'This secret key is not compatible with Google Authenticator.';
}

// //SecretKeyTooShortException.php
class SecretKeyTooShortException extends Google2FAException implements Google2FAb, SecretKeyTooShort
{
    protected $message = 'Secret key is too short. Must be at least 16 base32 characters';
}

// //InvalidAlgorithmException.php
class InvalidAlgorithmException extends Google2FAException implements Google2FAb, InvalidAlgorithm
{
    protected $message = 'Invalid HMAC algorithm.';
}

abstract class Base32 implements EncoderInterface
{
    /**
     * Decode a Base32-encoded string into raw binary
     *
     * @param string $encodedString
     * @param bool $strictPadding
     * @return string
     */
    public static function decode(string $encodedString, bool $strictPadding = false): string
    {
        return static::doDecode($encodedString, false, $strictPadding);
    }

    /**
     * Decode an uppercase Base32-encoded string into raw binary
     *
     * @param string $src
     * @param bool $strictPadding
     * @return string
     */
    public static function decodeUpper(string $src, bool $strictPadding = false): string
    {
        return static::doDecode($src, true, $strictPadding);
    }

    /**
     * Encode into Base32 (RFC 4648)
     *
     * @param string $binString
     * @return string
     * @throws TypeError
     */
    public static function encode(string $binString): string
    {
        return static::doEncode($binString, false, true);
    }
    /**
     * Encode into Base32 (RFC 4648)
     *
     * @param string $src
     * @return string
     * @throws TypeError
     */
    public static function encodeUnpadded(string $src): string
    {
        return static::doEncode($src, false, false);
    }

    /**
     * Encode into uppercase Base32 (RFC 4648)
     *
     * @param string $src
     * @return string
     * @throws TypeError
     */
    public static function encodeUpper(string $src): string
    {
        return static::doEncode($src, true, true);
    }

    /**
     * Encode into uppercase Base32 (RFC 4648)
     *
     * @param string $src
     * @return string
     * @throws TypeError
     */
    public static function encodeUpperUnpadded(string $src): string
    {
        return static::doEncode($src, true, false);
    }

    /**
     * Uses bitwise operators instead of table-lookups to turn 5-bit integers
     * into 8-bit integers.
     *
     * @param int $src
     * @return int
     */
    protected static function decode5Bits(int $src): int
    {
        $ret = -1;

        // if ($src > 96 && $src < 123) $ret += $src - 97 + 1; // -64
        $ret += (((0x60 - $src) & ($src - 0x7b)) >> 8) & ($src - 96);

        // if ($src > 0x31 && $src < 0x38) $ret += $src - 24 + 1; // -23
        $ret += (((0x31 - $src) & ($src - 0x38)) >> 8) & ($src - 23);

        return $ret;
    }

    /**
     * Uses bitwise operators instead of table-lookups to turn 5-bit integers
     * into 8-bit integers.
     *
     * Uppercase variant.
     *
     * @param int $src
     * @return int
     */
    protected static function decode5BitsUpper(int $src): int
    {
        $ret = -1;

        // if ($src > 64 && $src < 91) $ret += $src - 65 + 1; // -64
        $ret += (((0x40 - $src) & ($src - 0x5b)) >> 8) & ($src - 64);

        // if ($src > 0x31 && $src < 0x38) $ret += $src - 24 + 1; // -23
        $ret += (((0x31 - $src) & ($src - 0x38)) >> 8) & ($src - 23);

        return $ret;
    }

    /**
     * Uses bitwise operators instead of table-lookups to turn 8-bit integers
     * into 5-bit integers.
     *
     * @param int $src
     * @return string
     */
    protected static function encode5Bits(int $src): string
    {
        $diff = 0x61;

        // if ($src > 25) $ret -= 72;
        $diff -= ((25 - $src) >> 8) & 73;

        return \pack('C', $src + $diff);
    }

    /**
     * Uses bitwise operators instead of table-lookups to turn 8-bit integers
     * into 5-bit integers.
     *
     * Uppercase variant.
     *
     * @param int $src
     * @return string
     */
    protected static function encode5BitsUpper(int $src): string
    {
        $diff = 0x41;

        // if ($src > 25) $ret -= 40;
        $diff -= ((25 - $src) >> 8) & 41;

        return \pack('C', $src + $diff);
    }

    /**
     * @param string $encodedString
     * @param bool $upper
     * @return string
     */
    public static function decodeNoPadding(string $encodedString, bool $upper = false): string
    {
        $srcLen = Binary::safeStrlen($encodedString);
        if ($srcLen === 0) {
            return '';
        }
        if (($srcLen & 7) === 0) {
            for ($j = 0; $j < 7 && $j < $srcLen; ++$j) {
                if ($encodedString[$srcLen - $j - 1] === '=') {
                    throw new InvalidArgumentException(
                        "decodeNoPadding() doesn't tolerate padding"
                    );
                }
            }
        }
        return static::doDecode(
            $encodedString,
            $upper,
            true
        );
    }

    /**
     * Base32 decoding
     *
     * @param string $src
     * @param bool $upper
     * @param bool $strictPadding
     * @return string
     *
     * @throws TypeError
     * @psalm-suppress RedundantCondition
     */
    protected static function doDecode(
        string $src,
        bool $upper = false,
        bool $strictPadding = false
    ): string {
        // We do this to reduce code duplication:
        $method = $upper
            ? 'decode5BitsUpper'
            : 'decode5Bits';

        // Remove padding
        $srcLen = Binary::safeStrlen($src);
        if ($srcLen === 0) {
            return '';
        }
        if ($strictPadding) {
            if (($srcLen & 7) === 0) {
                for ($j = 0; $j < 7; ++$j) {
                    if ($src[$srcLen - 1] === '=') {
                        $srcLen--;
                    } else {
                        break;
                    }
                }
            }
            if (($srcLen & 7) === 1) {
                throw new RangeException(
                    'Incorrect padding'
                );
            }
        } else {
            $src = \rtrim($src, '=');
            $srcLen = Binary::safeStrlen($src);
        }

        $err = 0;
        $dest = '';
        // Main loop (no padding):
        for ($i = 0; $i + 8 <= $srcLen; $i += 8) {
            /** @var array<int, int> $chunk */
            $chunk = \unpack('C*', Binary::safeSubstr($src, $i, 8));
            /** @var int $c0 */
            $c0 = static::$method($chunk[1]);
            /** @var int $c1 */
            $c1 = static::$method($chunk[2]);
            /** @var int $c2 */
            $c2 = static::$method($chunk[3]);
            /** @var int $c3 */
            $c3 = static::$method($chunk[4]);
            /** @var int $c4 */
            $c4 = static::$method($chunk[5]);
            /** @var int $c5 */
            $c5 = static::$method($chunk[6]);
            /** @var int $c6 */
            $c6 = static::$method($chunk[7]);
            /** @var int $c7 */
            $c7 = static::$method($chunk[8]);

            $dest .= \pack(
                'CCCCC',
                (($c0 << 3) | ($c1 >> 2)             ) & 0xff,
                (($c1 << 6) | ($c2 << 1) | ($c3 >> 4)) & 0xff,
                (($c3 << 4) | ($c4 >> 1)             ) & 0xff,
                (($c4 << 7) | ($c5 << 2) | ($c6 >> 3)) & 0xff,
                (($c6 << 5) | ($c7     )             ) & 0xff
            );
            $err |= ($c0 | $c1 | $c2 | $c3 | $c4 | $c5 | $c6 | $c7) >> 8;
        }
        // The last chunk, which may have padding:
        if ($i < $srcLen) {
            /** @var array<int, int> $chunk */
            $chunk = \unpack('C*', Binary::safeSubstr($src, $i, $srcLen - $i));
            /** @var int $c0 */
            $c0 = static::$method($chunk[1]);

            if ($i + 6 < $srcLen) {
                /** @var int $c1 */
                $c1 = static::$method($chunk[2]);
                /** @var int $c2 */
                $c2 = static::$method($chunk[3]);
                /** @var int $c3 */
                $c3 = static::$method($chunk[4]);
                /** @var int $c4 */
                $c4 = static::$method($chunk[5]);
                /** @var int $c5 */
                $c5 = static::$method($chunk[6]);
                /** @var int $c6 */
                $c6 = static::$method($chunk[7]);

                $dest .= \pack(
                    'CCCC',
                    (($c0 << 3) | ($c1 >> 2)             ) & 0xff,
                    (($c1 << 6) | ($c2 << 1) | ($c3 >> 4)) & 0xff,
                    (($c3 << 4) | ($c4 >> 1)             ) & 0xff,
                    (($c4 << 7) | ($c5 << 2) | ($c6 >> 3)) & 0xff
                );
                $err |= ($c0 | $c1 | $c2 | $c3 | $c4 | $c5 | $c6) >> 8;
                if ($strictPadding) {
                    $err |= ($c6 << 5) & 0xff;
                }
            } elseif ($i + 5 < $srcLen) {
                /** @var int $c1 */
                $c1 = static::$method($chunk[2]);
                /** @var int $c2 */
                $c2 = static::$method($chunk[3]);
                /** @var int $c3 */
                $c3 = static::$method($chunk[4]);
                /** @var int $c4 */
                $c4 = static::$method($chunk[5]);
                /** @var int $c5 */
                $c5 = static::$method($chunk[6]);

                $dest .= \pack(
                    'CCCC',
                    (($c0 << 3) | ($c1 >> 2)             ) & 0xff,
                    (($c1 << 6) | ($c2 << 1) | ($c3 >> 4)) & 0xff,
                    (($c3 << 4) | ($c4 >> 1)             ) & 0xff,
                    (($c4 << 7) | ($c5 << 2)             ) & 0xff
                );
                $err |= ($c0 | $c1 | $c2 | $c3 | $c4 | $c5) >> 8;
            } elseif ($i + 4 < $srcLen) {
                /** @var int $c1 */
                $c1 = static::$method($chunk[2]);
                /** @var int $c2 */
                $c2 = static::$method($chunk[3]);
                /** @var int $c3 */
                $c3 = static::$method($chunk[4]);
                /** @var int $c4 */
                $c4 = static::$method($chunk[5]);

                $dest .= \pack(
                    'CCC',
                    (($c0 << 3) | ($c1 >> 2)             ) & 0xff,
                    (($c1 << 6) | ($c2 << 1) | ($c3 >> 4)) & 0xff,
                    (($c3 << 4) | ($c4 >> 1)             ) & 0xff
                );
                $err |= ($c0 | $c1 | $c2 | $c3 | $c4) >> 8;
                if ($strictPadding) {
                    $err |= ($c4 << 7) & 0xff;
                }
            } elseif ($i + 3 < $srcLen) {
                /** @var int $c1 */
                $c1 = static::$method($chunk[2]);
                /** @var int $c2 */
                $c2 = static::$method($chunk[3]);
                /** @var int $c3 */
                $c3 = static::$method($chunk[4]);

                $dest .= \pack(
                    'CC',
                    (($c0 << 3) | ($c1 >> 2)             ) & 0xff,
                    (($c1 << 6) | ($c2 << 1) | ($c3 >> 4)) & 0xff
                );
                $err |= ($c0 | $c1 | $c2 | $c3) >> 8;
                if ($strictPadding) {
                    $err |= ($c3 << 4) & 0xff;
                }
            } elseif ($i + 2 < $srcLen) {
                /** @var int $c1 */
                $c1 = static::$method($chunk[2]);
                /** @var int $c2 */
                $c2 = static::$method($chunk[3]);

                $dest .= \pack(
                    'CC',
                    (($c0 << 3) | ($c1 >> 2)             ) & 0xff,
                    (($c1 << 6) | ($c2 << 1)             ) & 0xff
                );
                $err |= ($c0 | $c1 | $c2) >> 8;
                if ($strictPadding) {
                    $err |= ($c2 << 6) & 0xff;
                }
            } elseif ($i + 1 < $srcLen) {
                /** @var int $c1 */
                $c1 = static::$method($chunk[2]);

                $dest .= \pack(
                    'C',
                    (($c0 << 3) | ($c1 >> 2)             ) & 0xff
                );
                $err |= ($c0 | $c1) >> 8;
                if ($strictPadding) {
                    $err |= ($c1 << 6) & 0xff;
                }
            } else {
                $dest .= \pack(
                    'C',
                    (($c0 << 3)                          ) & 0xff
                );
                $err |= ($c0) >> 8;
            }
        }
        $check = ($err === 0);
        if (!$check) {
            throw new RangeException(
                'Base32::doDecode() only expects characters in the correct base32 alphabet'
            );
        }
        return $dest;
    }

    /**
     * Base32 Encoding
     *
     * @param string $src
     * @param bool $upper
     * @param bool $pad
     * @return string
     * @throws TypeError
     */
    protected static function doEncode(string $src, bool $upper = false, $pad = true): string
    {
        // We do this to reduce code duplication:
        $method = $upper
            ? 'encode5BitsUpper'
            : 'encode5Bits';
        
        $dest = '';
        $srcLen = Binary::safeStrlen($src);

        // Main loop (no padding):
        for ($i = 0; $i + 5 <= $srcLen; $i += 5) {
            /** @var array<int, int> $chunk */
            $chunk = \unpack('C*', Binary::safeSubstr($src, $i, 5));
            $b0 = $chunk[1];
            $b1 = $chunk[2];
            $b2 = $chunk[3];
            $b3 = $chunk[4];
            $b4 = $chunk[5];
            $dest .=
                static::$method(              ($b0 >> 3)  & 31) .
                static::$method((($b0 << 2) | ($b1 >> 6)) & 31) .
                static::$method((($b1 >> 1)             ) & 31) .
                static::$method((($b1 << 4) | ($b2 >> 4)) & 31) .
                static::$method((($b2 << 1) | ($b3 >> 7)) & 31) .
                static::$method((($b3 >> 2)             ) & 31) .
                static::$method((($b3 << 3) | ($b4 >> 5)) & 31) .
                static::$method(  $b4                     & 31);
        }
        // The last chunk, which may have padding:
        if ($i < $srcLen) {
            /** @var array<int, int> $chunk */
            $chunk = \unpack('C*', Binary::safeSubstr($src, $i, $srcLen - $i));
            $b0 = $chunk[1];
            if ($i + 3 < $srcLen) {
                $b1 = $chunk[2];
                $b2 = $chunk[3];
                $b3 = $chunk[4];
                $dest .=
                    static::$method(              ($b0 >> 3)  & 31) .
                    static::$method((($b0 << 2) | ($b1 >> 6)) & 31) .
                    static::$method((($b1 >> 1)             ) & 31) .
                    static::$method((($b1 << 4) | ($b2 >> 4)) & 31) .
                    static::$method((($b2 << 1) | ($b3 >> 7)) & 31) .
                    static::$method((($b3 >> 2)             ) & 31) .
                    static::$method((($b3 << 3)             ) & 31);
                if ($pad) {
                    $dest .= '=';
                }
            } elseif ($i + 2 < $srcLen) {
                $b1 = $chunk[2];
                $b2 = $chunk[3];
                $dest .=
                    static::$method(              ($b0 >> 3)  & 31) .
                    static::$method((($b0 << 2) | ($b1 >> 6)) & 31) .
                    static::$method((($b1 >> 1)             ) & 31) .
                    static::$method((($b1 << 4) | ($b2 >> 4)) & 31) .
                    static::$method((($b2 << 1)             ) & 31);
                if ($pad) {
                    $dest .= '===';
                }
            } elseif ($i + 1 < $srcLen) {
                $b1 = $chunk[2];
                $dest .=
                    static::$method(              ($b0 >> 3)  & 31) .
                    static::$method((($b0 << 2) | ($b1 >> 6)) & 31) .
                    static::$method((($b1 >> 1)             ) & 31) .
                    static::$method((($b1 << 4)             ) & 31);
                if ($pad) {
                    $dest .= '====';
                }
            } else {
                $dest .=
                    static::$method(              ($b0 >> 3)  & 31) .
                    static::$method( ($b0 << 2)               & 31);
                if ($pad) {
                    $dest .= '======';
                }
            }
        }
        return $dest;
    }
}

abstract class Binary
{
    /**
     * Safe string length
     *
     * @ref mbstring.func_overload
     *
     * @param string $str
     * @return int
     */
    public static function safeStrlen(string $str): int
    {
        if (\function_exists('mb_strlen')) {
            // mb_strlen in PHP 7.x can return false.
            /** @psalm-suppress RedundantCast */
            return (int) \mb_strlen($str, '8bit');
        } else {
            return \strlen($str);
        }
    }

    /**
     * Safe substring
     *
     * @ref mbstring.func_overload
     *
     * @staticvar boolean $exists
     * @param string $str
     * @param int $start
     * @param ?int $length
     * @return string
     *
     * @throws TypeError
     */
    public static function safeSubstr(
        string $str,
        int $start = 0,
        $length = null
    ): string {
        if ($length === 0) {
            return '';
        }
        if (\function_exists('mb_substr')) {
            return \mb_substr($str, $start, $length, '8bit');
        }
        // Unlike mb_substr(), substr() doesn't accept NULL for length
        if ($length !== null) {
            return \substr($str, $start, $length);
        } else {
            return \substr($str, $start);
        }
    }
}

interface EncoderInterface
{
    /**
     * Convert a binary string into a hexadecimal string without cache-timing
     * leaks
     *
     * @param string $binString (raw binary)
     * @return string
     */
    public static function encode(string $binString): string;

    /**
     * Convert a binary string into a hexadecimal string without cache-timing
     * leaks
     *
     * @param string $encodedString
     * @param bool $strictPadding Error on invalid padding
     * @return string (raw binary)
     */
    public static function decode(string $encodedString, bool $strictPadding = false): string;
}

trait Base32b
{
    /**
     * Enforce Google Authenticator compatibility.
     */
    protected $enforceGoogleAuthenticatorCompatibility = true;

    /**
     * Calculate char count bits.
     *
     * @param string $b32
     *
     * @return int
     */
    protected function charCountBits($b32)
    {
        return strlen($b32) * 8;
    }

    /**
     * Generate a digit secret key in base32 format.
     *
     * @param int    $length
     * @param string $prefix
     *
     * @throws \Exception
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return string
     */
    public function generateBase32RandomKey($length = 16, $prefix = '')
    {
        $secret = $prefix ? $this->toBase32($prefix) : '';

        $secret = $this->strPadBase32($secret, $length);

        $this->validateSecret($secret);

        return $secret;
    }

    /**
     * Decodes a base32 string into a binary string.
     *
     * @param string $b32
     *
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return string
     */
    public function base32Decode($b32)
    {
        $b32 = strtoupper($b32);

        $this->validateSecret($b32);

        return Base32::decodeUpper($b32);
    }

    /**
     * Check if the string length is power of two.
     *
     * @param string $b32
     *
     * @return bool
     */
    protected function isCharCountNotAPowerOfTwo($b32)
    {
        return (strlen($b32) & (strlen($b32) - 1)) !== 0;
    }

    /**
     * Pad string with random base 32 chars.
     *
     * @param string $string
     * @param int    $length
     *
     * @throws \Exception
     *
     * @return string
     */
    private function strPadBase32($string, $length)
    {
        for ($i = 0; $i < $length; $i++) {
            $string .= substr(
                Constants::VALID_FOR_B32_SCRAMBLED,
                $this->getRandomNumber(),
                1
            );
        }

        return $string;
    }

    /**
     * Encode a string to Base32.
     *
     * @param string $string
     *
     * @return string
     */
    public function toBase32($string)
    {
        $encoded = ParagonieBase32::encodeUpper($string);

        return str_replace('=', '', $encoded);
    }

    /**
     * Get a random number.
     *
     * @param int $from
     * @param int $to
     *
     * @throws \Exception
     *
     * @return int
     */
    protected function getRandomNumber($from = 0, $to = 31)
    {
        return random_int($from, $to);
    }

    /**
     * Validate the secret.
     *
     * @param string $b32
     *
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     */
    protected function validateSecret($b32)
    {
        $this->checkForValidCharacters($b32);

        $this->checkGoogleAuthenticatorCompatibility($b32);

        $this->checkIsBigEnough($b32);
    }

    /**
     * Check if the secret key is compatible with Google Authenticator.
     *
     * @param string $b32
     *
     * @throws IncompatibleWithGoogleAuthenticatorException
     */
    protected function checkGoogleAuthenticatorCompatibility($b32)
    {
        if (
            $this->enforceGoogleAuthenticatorCompatibility &&
            $this->isCharCountNotAPowerOfTwo($b32) // Google Authenticator requires it to be a power of 2 base32 length string
        ) {
            throw new IncompatibleWithGoogleAuthenticatorException();
        }
    }

    /**
     * Check if all secret key characters are valid.
     *
     * @param string $b32
     *
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     */
    protected function checkForValidCharacters($b32)
    {
        if (
            preg_replace('/[^'.Constants::VALID_FOR_B32.']/', '', $b32) !==
            $b32
        ) {
            throw new InvalidCharactersException();
        }
    }

    /**
     * Check if secret key length is big enough.
     *
     * @param string $b32
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     */
    protected function checkIsBigEnough($b32)
    {
        // Minimum = 128 bits
        // Recommended = 160 bits
        // Compatible with Google Authenticator = 256 bits

        if (
            $this->charCountBits($b32) < 128
        ) {
            throw new SecretKeyTooShortException();
        }
    }
}

class Constants
{
    /**
     * Characters valid for Base 32.
     */
    const VALID_FOR_B32 = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ234567';

    /**
     * Characters valid for Base 32, scrambled.
     */
    const VALID_FOR_B32_SCRAMBLED = '234567QWERTYUIOPASDFGHJKLZXCVBNM';

    /**
     * SHA1 algorithm.
     */
    const SHA1 = 'sha1';

    /**
     * SHA256 algorithm.
     */
    const SHA256 = 'sha256';

    /**
     * SHA512 algorithm.
     */
    const SHA512 = 'sha512';
}

trait QRCode
{
    /**
     * Creates a QR code url.
     *
     * @param string $company
     * @param string $holder
     * @param string $secret
     *
     * @return string
     */
    public function getQRCodeUrl($company, $holder, $secret)
    {
        return 'otpauth://totp/'.
            rawurlencode($company).
            ':'.
            rawurlencode($holder).
            '?secret='.
            $secret.
            '&issuer='.
            rawurlencode($company).
            '&algorithm='.
            rawurlencode(strtoupper($this->getAlgorithm())).
            '&digits='.
            rawurlencode(strtoupper((string) $this->getOneTimePasswordLength())).
            '&period='.
            rawurlencode(strtoupper((string) $this->getKeyRegeneration())).
            '';
    }
}

class Google2FA
{
    use QRCode;
    use Base32b;

    /**
     * Algorithm.
     *
     * @var string
     */
    protected $algorithm = Constants::SHA1;

    /**
     * Length of the Token generated.
     *
     * @var int
     */
    protected $oneTimePasswordLength = 6;

    /**
     * Interval between key regeneration.
     *
     * @var int
     */
    protected $keyRegeneration = 30;

    /**
     * Secret.
     *
     * @var string
     */
    protected $secret;

    /**
     * Window.
     *
     * @var int
     */
    protected $window = 1; // Keys will be valid for 60 seconds

    /**
     * Find a valid One Time Password.
     *
     * @param string   $secret
     * @param string   $key
     * @param int|null $window
     * @param int      $startingTimestamp
     * @param int      $timestamp
     * @param int|null $oldTimestamp
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return bool|int
     */
    public function findValidOTP(
        $secret,
        $key,
        $window,
        $startingTimestamp,
        $timestamp,
        $oldTimestamp = null
    ) {
        for (;
            $startingTimestamp <= $timestamp + $this->getWindow($window);
            $startingTimestamp++
        ) {
            if (
                hash_equals($this->oathTotp($secret, $startingTimestamp), $key)
            ) {
                return is_null($oldTimestamp)
                    ? true
                    : $startingTimestamp;
            }
        }

        return false;
    }

    /**
     * Generate the HMAC OTP.
     *
     * @param string $secret
     * @param int    $counter
     *
     * @return string
     */
    protected function generateHotp($secret, $counter)
    {
        return hash_hmac(
            $this->getAlgorithm(),
            pack('N*', 0, $counter), // Counter must be 64-bit int
            $secret,
            true
        );
    }

    /**
     * Generate a digit secret key in base32 format.
     *
     * @param int    $length
     * @param string $prefix
     *
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     *
     * @return string
     */
    public function generateSecretKey($length = 16, $prefix = '')
    {
        return $this->generateBase32RandomKey($length, $prefix);
    }

    /**
     * Get the current one time password for a key.
     *
     * @param string $secret
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return string
     */
    public function getCurrentOtp($secret)
    {
        return $this->oathTotp($secret, $this->getTimestamp());
    }

    /**
     * Get the HMAC algorithm.
     *
     * @return string
     */
    public function getAlgorithm()
    {
        return $this->algorithm;
    }

    /**
     * Get key regeneration.
     *
     * @return int
     */
    public function getKeyRegeneration()
    {
        return $this->keyRegeneration;
    }

    /**
     * Get OTP length.
     *
     * @return int
     */
    public function getOneTimePasswordLength()
    {
        return $this->oneTimePasswordLength;
    }

    /**
     * Get secret.
     *
     * @param string|null $secret
     *
     * @return string
     */
    public function getSecret($secret = null)
    {
        return is_null($secret) ? $this->secret : $secret;
    }

    /**
     * Returns the current Unix Timestamp divided by the $keyRegeneration
     * period.
     *
     * @return int
     **/
    public function getTimestamp()
    {
        return (int) floor(microtime(true) / $this->keyRegeneration);
    }

    /**
     * Get a list of valid HMAC algorithms.
     *
     * @return array
     */
    protected function getValidAlgorithms()
    {
        return [
            Constants::SHA1,
            Constants::SHA256,
            Constants::SHA512,
        ];
    }

    /**
     * Get the OTP window.
     *
     * @param null|int $window
     *
     * @return int
     */
    public function getWindow($window = null)
    {
        return is_null($window) ? $this->window : $window;
    }

    /**
     * Make a window based starting timestamp.
     *
     * @param int|null $window
     * @param int      $timestamp
     * @param int|null $oldTimestamp
     *
     * @return mixed
     */
    private function makeStartingTimestamp($window, $timestamp, $oldTimestamp = null)
    {
        return is_null($oldTimestamp)
            ? $timestamp - $this->getWindow($window)
            : max($timestamp - $this->getWindow($window), $oldTimestamp + 1);
    }

    /**
     * Get/use a starting timestamp for key verification.
     *
     * @param string|int|null $timestamp
     *
     * @return int
     */
    protected function makeTimestamp($timestamp = null)
    {
        if (is_null($timestamp)) {
            return $this->getTimestamp();
        }

        return (int) $timestamp;
    }

    /**
     * Takes the secret key and the timestamp and returns the one time
     * password.
     *
     * @param string $secret  Secret key in binary form.
     * @param int    $counter Timestamp as returned by getTimestamp.
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return string
     */
    public function oathTotp($secret, $counter)
    {
        if (strlen($secret) < 8) {
            throw new SecretKeyTooShortException();
        }

        $secret = $this->base32Decode($this->getSecret($secret));

        return str_pad(
            $this->oathTruncate($this->generateHotp($secret, $counter)),
            $this->getOneTimePasswordLength(),
            '0',
            STR_PAD_LEFT
        );
    }

    /**
     * Extracts the OTP from the SHA1 hash.
     *
     * @param string $hash
     *
     * @return string
     **/
    public function oathTruncate($hash)
    {
        $offset = ord($hash[strlen($hash) - 1]) & 0xF;

        $temp = unpack('N', substr($hash, $offset, 4));

        $temp = $temp[1] & 0x7FFFFFFF;

        return substr(
            (string) $temp,
            -$this->getOneTimePasswordLength()
        );
    }

    /**
     * Remove invalid chars from a base 32 string.
     *
     * @param string $string
     *
     * @return string|null
     */
    public function removeInvalidChars($string)
    {
        return preg_replace(
            '/[^'.Constants::VALID_FOR_B32.']/',
            '',
            $string
        );
    }

    /**
     * Setter for the enforce Google Authenticator compatibility property.
     *
     * @param mixed $enforceGoogleAuthenticatorCompatibility
     *
     * @return $this
     */
    public function setEnforceGoogleAuthenticatorCompatibility(
        $enforceGoogleAuthenticatorCompatibility
    ) {
        $this->enforceGoogleAuthenticatorCompatibility = $enforceGoogleAuthenticatorCompatibility;

        return $this;
    }

    /**
     * Set the HMAC hashing algorithm.
     *
     * @param mixed $algorithm
     *
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidAlgorithmException
     *
     * @return \PragmaRX\Google2FA\Google2FA
     */
    public function setAlgorithm($algorithm)
    {
        // Default to SHA1 HMAC algorithm
        if (!in_array($algorithm, $this->getValidAlgorithms())) {
            throw new InvalidAlgorithmException();
        }

        $this->algorithm = $algorithm;

        return $this;
    }

    /**
     * Set key regeneration.
     *
     * @param mixed $keyRegeneration
     */
    public function setKeyRegeneration($keyRegeneration)
    {
        $this->keyRegeneration = $keyRegeneration;
    }

    /**
     * Set OTP length.
     *
     * @param mixed $oneTimePasswordLength
     */
    public function setOneTimePasswordLength($oneTimePasswordLength)
    {
        $this->oneTimePasswordLength = $oneTimePasswordLength;
    }

    /**
     * Set secret.
     *
     * @param mixed $secret
     */
    public function setSecret($secret)
    {
        $this->secret = $secret;
    }

    /**
     * Set the OTP window.
     *
     * @param mixed $window
     */
    public function setWindow($window)
    {
        $this->window = $window;
    }

    /**
     * Verifies a user inputted key against the current timestamp. Checks $window
     * keys either side of the timestamp.
     *
     * @param string   $key          User specified key
     * @param string   $secret
     * @param null|int $window
     * @param null|int $timestamp
     * @param null|int $oldTimestamp
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return bool|int
     */
    public function verify(
        $key,
        $secret,
        $window = null,
        $timestamp = null,
        $oldTimestamp = null
    ) {
        return $this->verifyKey(
            $secret,
            $key,
            $window,
            $timestamp,
            $oldTimestamp
        );
    }

    /**
     * Verifies a user inputted key against the current timestamp. Checks $window
     * keys either side of the timestamp.
     *
     * @param string   $secret
     * @param string   $key          User specified key
     * @param int|null $window
     * @param null|int $timestamp
     * @param null|int $oldTimestamp
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return bool|int
     */
    public function verifyKey(
        $secret,
        $key,
        $window = null,
        $timestamp = null,
        $oldTimestamp = null
    ) {
        $timestamp = $this->makeTimestamp($timestamp);

        return $this->findValidOTP(
            $secret,
            $key,
            $window,
            $this->makeStartingTimestamp($window, $timestamp, $oldTimestamp),
            $timestamp,
            $oldTimestamp
        );
    }

    /**
     * Verifies a user inputted key against the current timestamp. Checks $window
     * keys either side of the timestamp, but ensures that the given key is newer than
     * the given oldTimestamp. Useful if you need to ensure that a single key cannot
     * be used twice.
     *
     * @param string   $secret
     * @param string   $key          User specified key
     * @param int|null $oldTimestamp The timestamp from the last verified key
     * @param int|null $window
     * @param int|null $timestamp
     *
     * @throws \PragmaRX\Google2FA\Exceptions\SecretKeyTooShortException
     * @throws \PragmaRX\Google2FA\Exceptions\InvalidCharactersException
     * @throws \PragmaRX\Google2FA\Exceptions\IncompatibleWithGoogleAuthenticatorException
     *
     * @return bool|int
     */
    public function verifyKeyNewer(
        $secret,
        $key,
        $oldTimestamp,
        $window = null,
        $timestamp = null
    ) {
        return $this->verifyKey(
            $secret,
            $key,
            $window,
            $timestamp,
            $oldTimestamp
        );
    }
}
