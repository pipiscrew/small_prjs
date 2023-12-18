//https://worker-playground.glitch.me/
//this is a modified version (allows to play when navigation.serviceWorker is not available).

var swReg = null;
var useSw = null;

//console.log(navigator.serviceWorker);

if (navigator.serviceWorker)
	console.log("navigator.serviceWorker -- is not null");

if ('serviceWorker' in navigator) {
	navigator.serviceWorker.register("sw.js").then((reg) => {
	  swReg = reg;
	  useSw = reg.active || reg.waiting || reg.installing;
	});
}

var taDedicated = document.getElementById("dedicated");
var dedicatedWorker = new Worker("dedicated.js");
dedicatedWorker.addEventListener("message", function(evt) {
  let { result } = evt.data;
  taDedicated.value = result;
});

var sharedWorker;
var taShared = document.getElementById("shared");
if ("SharedWorker" in window) {
  sharedWorker = new SharedWorker("shared.js");
  sharedWorker.port.addEventListener("message", function(evt) {
    let { result } = evt.data;
    taShared.value = result;
  });
  sharedWorker.port.start();
} else {
  taShared.value = "Not supported by this browser.";
}

if ('serviceWorker' in navigator) {
	var taService = document.getElementById("service");
	var serviceWorker;
	navigator.serviceWorker.addEventListener("message", function(evt) {
	  var { result } = evt.data;
	  taService.value = result;
	});
}

var curGeneration = 1;
function evalInAll(str) {
  var payload = {
    generation: curGeneration++,
    str
  };
  
  dedicatedWorker.postMessage(payload);
  if (sharedWorker) {
    sharedWorker.port.postMessage(payload);
  }
  if (useSw) {
    useSw.postMessage(payload);
  } else {
	  if ('serviceWorker' in navigator) {
		navigator.serviceWorker.ready.then((reg) => {
		  reg.active.postMessage(payload);
		});
	  }
  }
}

var taInput = document.getElementById("inp");
taInput.addEventListener("keyup", function(evt) {
  evalInAll(taInput.value);
});