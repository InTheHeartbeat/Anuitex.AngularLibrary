var libraryModule;

var booksModule;
var accountModule;
(function () {
    booksModule = angular.module('booksModule', []);
    accountModule = angular.module('accountModule', []);
    libraryModule = angular.module('libraryModule', ['booksModule', 'accountModule', 'ngDialog']);
})();
