libraryModule.controller('exportJournalsController', function ($scope, exportService) {

    $scope.ExportJournalsModel = {};    
       
    getExportableJournals();

    $scope.exportJournals = function() {
        exportService.exportJournals($scope.ExportJournalsModel).then(function (response) {                       
            saveFile(response.headers('Content-Disposition'),response.data);
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }

    function getExportableJournals() {
        exportService.getExportableJournals().then(function (response) {
            $scope.ExportJournalsModel = response.data;
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }
    function saveFile(disposition, data) {
        var filename = "";        
        if (disposition) {
            var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
            var matches = filenameRegex.exec(disposition);
            if (matches != null && matches[1]) {
                filename = matches[1].replace(/['"]/g, '');
            }
        }
        var a = document.createElement("a");
        document.body.appendChild(a);
        a.style = "display: none";
        a.href = window.URL.createObjectURL(new Blob([data], { type: "application/octet-stream" }));
        a.download = filename;
        a.click();
    }
});