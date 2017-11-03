booksModule.directive('books',
    function() {
        return {
            restrict: 'E',
            templateUrl: 'Content/Templates/books.html',
            scope: false,
            replace: true
        }
    });