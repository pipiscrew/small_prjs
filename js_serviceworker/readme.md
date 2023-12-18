# service_worker example

```js
//index.html
if ('serviceWorker' in navigator) {  //  if the browser supports Service Workers. 
  navigator.serviceWorker.register('/service_worker.js')
```

`navigator` is a global object in JavaScript that **provides information** about the browser environment. It represents the state and **identity** of the user agent (i.e., the web browser). The **navigator object** provides various properties and methods that allow you to **retrieve information** about the browser and control some aspects of its behavior.  

*by GPT*  
If you want to disable `navigator.serviceWorker` for all pages, there is no builtin method for Chrome (**Firefox** has a switch). You can create an addon where **background.js** :
```js
chrome.webNavigation.onCompleted.addListener(details => { //listen for the completion of page navigation 
  chrome.tabs.executeScript(details.tabId, { //inject a script into the page, setting navigator.serviceWorker to undefined
    code: `
      Object.defineProperty(navigator, 'serviceWorker', {
        value: undefined,
        writable: false,
        configurable: false
      });
    `
  });
});
```  


see how to **disable** service workers [here](https://www.pipiscrew.com/threads/webrowser-service-workers.99503/)