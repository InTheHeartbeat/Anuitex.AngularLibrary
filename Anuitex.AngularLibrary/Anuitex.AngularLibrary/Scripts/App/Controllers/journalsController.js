journalsModule.controller('journalsController', function ($scope, sharedService, journalsService, ngDialog) {

    $scope.IsEdit = false;
    $scope.IsAdmin = sharedService.CurrentUser.IsAdmin;    
    loadJournals();    

    $scope.$watch(function () { return sharedService.CurrentUser; }, function (newValue, oldValue) {
        if (newValue !== oldValue) $scope.IsAdmin = newValue.IsAdmin;
    });

    function loadJournals() {        
        var journals = journalsService.get();
        
        journals.then(function (bk) { $scope.Journals = bk.data;
        }, function (err) { alert('failture loading Journals ' + err); });
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
    $scope.tryEditJournal = function(journal) {
        var request = journalsService.put(journal);
        request.then(function(responce) {
            ngDialog.close('editJournalDialog');
            loadJournals();
        }, function (err) { alert(err.statusText); });
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
    $scope.tryAddJournal = function (journal) {
        var request = journalsService.post(journal);
        request.then(function (responce) {
            ngDialog.close('addJournalDialog');
            loadJournals();
        }, function (err) { alert(err.statusText); });        
    }

    $scope.delete = function(Id) {
        journalsService.delete(Id).then(function (responce) {            
            loadJournals();
        }, function (err) { alert(err.statusText); });
    }
});