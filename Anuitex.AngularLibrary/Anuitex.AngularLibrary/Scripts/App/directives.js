booksModule.directive('books',
    function() {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/books.html',
            scope: false,
            replace: true
        }
    });

journalsModule.directive('journals',
    function () {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/journals.html',
            scope: false,
            replace: true
        }
    });

newspapersModule.directive('newspapers',
    function () {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/newspapers.html',
            scope: false,
            replace: true
        }
    });

libraryModule.directive('exportbooks',
    function () {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/export-books.html',
            scope: false,
            replace: true
        }
    });
libraryModule.directive('exportjournals',
    function () {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/export-journals.html',
            scope: false,
            replace: true
        }
    });
libraryModule.directive('exportnewspapers',
    function () {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/export-newspapers.html',
            scope: false,
            replace: true
        }
    });
libraryModule.directive('importresult',
    function () {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/importResult.html',
            scope: false,
            replace: true
        }
    });