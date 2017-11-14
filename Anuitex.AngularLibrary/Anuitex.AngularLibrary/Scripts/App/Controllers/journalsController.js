journalsModule.controller('journalsController', function ($scope, sharedService, journalsService, ngDialog) {

    $scope.IsEdit = false;
    $scope.IsAdmin = sharedService.CurrentUser.IsAdmin;    

    loadJournals();

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue) {$scope.IsAdmin = newValue.IsAdmin;}
    });    

    $scope.export = function () {
        sharedService.Nav.applyNav('ExportJournals');
    }
    $scope.showEditJournalDialog = function (journal) {        
        $scope.Current = journal;
        $scope.Current.IsEdit = true;
        ngDialog.open({
            name: 'editJournalDialog',
            template: 'Content/Templates/Modal/editJournalDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-form',
            scope: $scope
        });        
    }    
    $scope.showAddJournalDialog = function () {
        $scope.Current = {};
        $scope.Current.IsEdit = false;
        ngDialog.open({
            name: 'addJournalDialog',
            template: 'Content/Templates/Modal/editJournalDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-form',
            scope: $scope 
        }); 
    }

    $scope.tryEditJournal = function (journal) {
        journalsService.put(journal).then(function (response) {
                ngDialog.close('editJournalDialog');
                loadJournals();
            },
            function (err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
    $scope.tryAddJournal = function (journal) {
        journalsService.post(journal).then(function(response) {
                ngDialog.close('addJournalDialog');
                loadJournals();
            },
            function(err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
    $scope.delete = function (Id) {
        journalsService.delete(Id).then(function (response) {
            loadJournals();
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }
    
    function loadJournals() {
        journalsService.get().then(function (bk) {
                $scope.Journals = bk.data;
            },
            function (err) {
                alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
                console.log(err);
            });
    }
});