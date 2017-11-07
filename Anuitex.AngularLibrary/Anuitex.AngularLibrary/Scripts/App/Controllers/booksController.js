booksModule.controller('booksController', function ($scope, sharedService, booksService) {

    $scope.IsEdit = false;
    $scope.IsAdmin = false;    

    loadBooks();    

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue) $scope.IsAdmin = newValue.IsAdmin;        
    });

    function loadBooks() {
        var books = booksService.get();

        books.then(function (bk) { $scope.Books = bk.data; }, function (err) { alert('failture loading Books ' + err); });
    }

    $scope.save = function() {
        var book = {
            Id: $scope.Id,
            Title: $scope.Title,
            Author: $scope.Author,
            Genre: $scope.Genre,
            Amount: $scope.Amount,
            Pages: $scope.Pages,
            Price: $scope.Price,
            Year: $scope.Year,
            PhotoId: $scope.PhotoId
        };

        if (book.Id === 0 || book.Id === null) {
            var post = booksService.post(book);
            post.then(function(bk) { $scope.Id = bk.data.Id }, function(err) { alert(err); });
        }
    }
    $scope.delete = function(Id) {
        booksService.delete(Id);
    }

});