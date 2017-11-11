booksModule.controller('booksController', function ($scope, sharedService, booksService, ngDialog) {

    $scope.IsEdit = false;
    $scope.IsAdmin = sharedService.CurrentUser.IsAdmin;

    loadBooks();    

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue) $scope.IsAdmin = newValue.IsAdmin;        
    });

    function loadBooks() {
        var books = booksService.get();
        
        books.then(function (bk) { $scope.Books = bk.data; }, function (err) { alert('failture loading Books ' + err); });
    }

    $scope.export = function() {
        console.log(sharedService.Nav);
        sharedService.Nav.applyNav('ExportBooks');
    }

    $scope.showEditBookDialog = function (book) {        
        $scope.Current = book;
        $scope.Current.IsEdit = true;
        ngDialog.open({
            name: 'editBookDialog',
            template: 'Content/Templates/Modal/editBookDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-form',
            scope: $scope
        });        
    }
    $scope.tryEditBook = function(book) {
        var request = booksService.put(book);
        request.then(function(responce) {
            ngDialog.close('editBookDialog');
            loadBooks();
        }, function (err) { alert(err.statusText); });
    }

    $scope.showAddBookDialog = function () {
        $scope.Current = {};
        $scope.Current.IsEdit = false;
        ngDialog.open({
            name: 'addBookDialog',
            template: 'Content/Templates/Modal/editBookDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-form',
            scope: $scope 
        }); 
    }

    $scope.tryAddBook = function (book) {
        var request = booksService.post(book);
        request.then(function (responce) {
            ngDialog.close('addBookDialog');
            loadBooks();
        }, function (err) { alert(err.statusText); });        
    }


    $scope.delete = function(Id) {
        booksService.delete(Id).then(function (responce) {            
            loadBooks();
        }, function (err) { alert(err.statusText); });
    }

});