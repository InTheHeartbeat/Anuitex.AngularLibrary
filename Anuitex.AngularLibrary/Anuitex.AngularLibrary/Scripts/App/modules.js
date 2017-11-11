var libraryModule;

var booksModule;
var journalsModule;
var newspapersModule;

var accountModule;
var sharedModule;

(function () {
    sharedModule = angular.module('sharedModule', []);
    sharedModule.factory('sharedService', function () {
        var data = {};

        data.CurrentUser = {};
        data.Nav = {};

        return data;
    });

    accountModule = angular.module('accountModule', ['sharedModule']);
    booksModule = angular.module('booksModule', ['sharedModule']);
    journalsModule = angular.module('journalsModule', ['sharedModule']);
    newspapersModule = angular.module('newspapersModule', ['sharedModule']);
    libraryModule = angular.module('libraryModule', ['booksModule', 'journalsModule', 'newspapersModule', 'accountModule', 'ngDialog', 'sharedModule']);

    libraryModule.controller('navController', function($scope, sharedService) {
        $scope.LibSections = ["Books", "Journals", "Newspapers"];
        $scope.FuncSections = ["ExportBooks"];
        $scope.CurrentNav = $scope.LibSections[0];
        $scope.applyNav = function(nav) {
            $scope.CurrentNav = nav;
        }

        sharedService.Nav = $scope;
    });

})();
