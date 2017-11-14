libraryModule.controller('importController', function ($scope, importService, Upload, ngDialog, sharedService) {

    $scope.ImportModel = {};

    $scope.showUploadDialog = function () {
        ngDialog.open({
            name: 'uploadDialog',
            template: 'Content/Templates/Modal/importDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom',
            scope: $scope
        });
    }

    $scope.uploadImportFile = function(file) {
        $scope.f = file;        
        if (file) {
            file.upload = Upload.upload({
                url: 'api/Import/UploadImportFile',
                data: { file: file }
            });

            file.upload.then(function(response) {
                    file.result = response.data;
                    $scope.ImportModel = response.data;
                    ngDialog.close('uploadDialog');
                    sharedService.Nav.applyNav("ImportResult");
                },
                function(err) {
                    console.log(err);
                },
                function(evt) {
                    file.progress = Math.min(100,
                        parseInt(100.0 *
                            evt.loaded /
                            evt.total));
                });
        }
    }

    $scope.confirmImport = function() {
        importService.confirmImport($scope.ImportModel).then(function () { sharedService.Nav.applyNav("Books"); });        
    }
});