var libraryModule;

var booksModule;
var journalsModule;
var newspapersModule;

var accountModule;
var sharedModule;

(function () {
    sharedModule = angular.module('sharedModule', []);  
    accountModule = angular.module('accountModule', ['sharedModule']);
    booksModule = angular.module('booksModule', ['sharedModule']);
    journalsModule = angular.module('journalsModule', ['sharedModule']);
    newspapersModule = angular.module('newspapersModule', ['sharedModule']);
    libraryModule = angular.module('libraryModule', ['booksModule', 'journalsModule', 'newspapersModule', 'accountModule', 'ngDialog', 'sharedModule', 'ngFileUpload']);    
})();


