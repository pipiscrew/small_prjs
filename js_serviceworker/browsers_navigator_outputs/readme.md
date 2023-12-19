# browsers navigator outputs

Using `console.log(navigator)` to any browser you see what you share with pages.  

* `Blink` is engine by Google  
* `Gecko` is engine by Mozilla  
* `Goanna` is engine by Pale Moon ( fork of Gecko )

---

## [Beacon](https://github.com/imperviousinc/beacon) v100 (blink)
![image](assets/beacon.png)

## [Brave](https://github.com/brave/brave-browser) v159 (blink)
![image](assets/beacon.png)

## [Icecat](https://codeberg.org/chippy/icecat-for-windows) v78 (gecko)
![image](assets/icecat.png)

## [LibreWolf](https://codeberg.org/librewolf/source) v109 (gecko)
![image](assets/icecat.png)

## [Mercury](https://github.com/Alex313031/Mercury) v115 (gecko)
![image](assets/mercury.png)

## [Basilik](https://repo.palemoon.org/Basilisk-Dev/Basilisk) v20230912 (goanna)
![image](assets/basilisk.png)

## [Pale Moon](https://repo.palemoon.org/MoonchildProductions/Pale-Moon) v32.5.1 (goanna)
![image](assets/palemoon.png)

## [SRWare Iron](https://www.srware.net/iron/) v118 (blink)
![image](assets/srware.png)

## [Ungoogled](https://github.com/ungoogled-software/ungoogled-chromium) v91 (blink)
![image](assets/ungoogled.png)

only `Mercury` / `Basilik` / `Pale Moon` **not** expose the **service worker**. And interestingly we see all output 
```js
product: "Gecko"
``` 

because of javascript engine ;) 