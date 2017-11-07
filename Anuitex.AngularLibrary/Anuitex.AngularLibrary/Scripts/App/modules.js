var libraryModule;

var booksModule;
var accountModule;

var sharedModule;

(function () {
    sharedModule = angular.module('sharedModule', []);
    sharedModule.factory('sharedService', function () {
        var data = {};

        data.CurrentUser = {};
      
        return data;
    });

    accountModule = angular.module('accountModule', ['sharedModule']);
    booksModule = angular.module('booksModule', ['sharedModule']);
    libraryModule = angular.module('libraryModule', ['booksModule', 'accountModule', 'ngDialog']);    
})();
