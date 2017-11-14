newspapersModule.controller('newspapersController', function ($scope, sharedService, newspapersService, ngDialog) {

    $scope.IsEdit = false;
    $scope.IsAdmin = sharedService.CurrentUser.IsAdmin;    
    loadNewspapers();    

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue) $scope.IsAdmin = newValue.IsAdmin;
    });
    
    $scope.export = function () {
        sharedService.Nav.applyNav('ExportNewspapers');
    }
    $scope.showEditNewspaperDialog = function (newspaper) {        
        $scope.Current = newspaper;
        $scope.Current.IsEdit = true;
        ngDialog.open({
            name: 'editNewspaperDialog',
            template: 'Content/Templates/Modal/editNewspaperDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-form',
            scope: $scope
        });        
    }
    $scope.showAddNewspaperDialog = function () {
        $scope.Current = {};
        $scope.Current.IsEdit = false;
        ngDialog.open({
            name: 'addNewspaperDialog',
            template: 'Content/Templates/Modal/editNewspaperDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-form',
            scope: $scope
        });
    }
    
    $scope.tryEditNewspaper = function(newspaper) {
        newspapersService.put(newspaper).then(function(response) {
                ngDialog.close('editNewspaperDialog');
                loadNewspapers();
            },
            function(err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
    $scope.tryAddNewspaper = function (newspaper) {
        newspapersService.post(newspaper).then(function (response) {
            ngDialog.close('addNewspaperDialog');
            loadNewspapers();
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }
    $scope.delete = function(Id) {
        newspapersService.delete(Id).then(function (response) {            
            loadNewspapers();
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }
  
    function loadNewspapers() {
        newspapersService.get().then(function (bk) {
                $scope.Newspapers = bk.data;
            },
            function (err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
});