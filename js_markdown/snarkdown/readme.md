# developit/snarkdown

* [developit/snarkdown](https://github.com/developit/snarkdown) original repo
* [developit demo](http://jsfiddle.net/developit/828w6t1x/)
* [this repo - demo](https://htmlpreview.github.io/?https://github.com/pipiscrew/small_prjs/blob/master/js_markdown/snarkdown/index.html)  

[JS version](https://github.com/commit-intl/micro-down/blob/master/src/index.js) cloned - August 31 2020  

con :  
the only problem with his implementation is that not accepts **nested lists**. 

mod :
* add naked HTML, to achieve nested list (as suggested by SephReed [here](https://github.com/developit/snarkdown/issues/104#issuecomment-1075658898))  
* merge blockquote CSS by [docsify](https://docsify.js.org/)
* remove `export` from JS `function parse`

make sure you will check his other project [preact](https://github.com/preactjs/preact) - Fast `3kB React alternative`.