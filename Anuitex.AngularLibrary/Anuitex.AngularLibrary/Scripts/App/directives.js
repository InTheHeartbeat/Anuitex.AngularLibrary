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