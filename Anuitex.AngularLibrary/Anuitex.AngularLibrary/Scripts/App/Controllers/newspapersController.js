newspapersModule.controller('newspapersController', function ($scope, sharedService, newspapersService, ngDialog) {

    $scope.IsEdit = false;
    $scope.IsAdmin = sharedService.CurrentUser.IsAdmin;    
    loadNewspapers();    

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue) $scope.IsAdmin = newValue.IsAdmin;
    });

    function loadNewspapers() {        
        var newspapers = newspapersService.get();
        
        newspapers.then(function (bk) { $scope.Newspapers = bk.data;
        }, function (err) { alert('failture loading Newspapers ' + err); });
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
    $scope.tryEditNewspaper = function(newspaper) {
        var request = newspapersService.put(newspaper);
        request.then(function(responce) {
            ngDialog.close('editNewspaperDialog');
            loadNewspapers();
        }, function (err) { alert(err.statusText); });
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
    $scope.tryAddNewspaper = function (newspaper) {
        var request = newspapersService.post(newspaper);
        request.then(function (responce) {
            ngDialog.close('addNewspaperDialog');
            loadNewspapers();
        }, function (err) { alert(err.statusText); });        
    }

    $scope.delete = function(Id) {
        newspapersService.delete(Id).then(function (responce) {            
            loadNewspapers();
        }, function (err) { alert(err.statusText); });
    }

    $scope.export = function () {       
        sharedService.Nav.applyNav('ExportNewspapers');
    }
});