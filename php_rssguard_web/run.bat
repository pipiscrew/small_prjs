::Add this batch file and php near 'C:\Program Files\rssguard\data\database\local\' folder

::start the browser to locahost - https://stackoverflow.com/a/154090
start "" "C:\Program Files\+browsers\icecat\icecat.exe" http://localhost:8000

::start the php builtin developer server
X:\phpFE\php\php.exe -S localhost:8000 "C:\Program Files\rssguard\data\database\local\feed.php"

