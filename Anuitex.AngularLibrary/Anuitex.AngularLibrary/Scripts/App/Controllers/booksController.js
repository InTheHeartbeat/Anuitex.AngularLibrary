booksModule.controller('booksController', function ($scope, sharedService, booksService, ngDialog) {

    $scope.IsEdit = false;
    $scope.IsAdmin = sharedService.CurrentUser.IsAdmin;

    loadBooks();    

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue){ $scope.IsAdmin = newValue.IsAdmin;}        
    });
        
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
    $scope.export = function() {        
        sharedService.Nav.applyNav('ExportBooks');
    }

    $scope.tryEditBook = function (book) {
        booksService.put(book).then(function (response) {
                ngDialog.close('editBookDialog');
                loadBooks();
            },
            function (err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
    $scope.tryAddBook = function (book) {
        booksService.post(book).then(function (response) {
                ngDialog.close('addBookDialog');
                loadBooks();
            },
            function (err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
    $scope.delete = function (Id) {
        booksService.delete(Id).then(function (response) {
            loadBooks();
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }

    function loadBooks() {
        booksService.get().then(function (bk) { $scope.Books = bk.data; },
            function (err) {
                alert('failture loading Books ' + err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
});