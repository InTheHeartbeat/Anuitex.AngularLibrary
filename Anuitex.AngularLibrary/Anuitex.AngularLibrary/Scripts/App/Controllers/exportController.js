libraryModule.controller('exportController', function ($scope, exportService) {

    $scope.ExportBooksModel = {};
    $scope.ExportableJournls = [];
    $scope.ExportableNewspapers = [];

    $scope.getExportableBooks = function () {
        exportService.getExportableBooks().then(function (response) { $scope.ExportBooksModel = response.data; });
    }
    $scope.getExportableJournals = function () {
        exportService.getExportableJournals().then(function (response) { $scope.ExportableJournals = response.data; });
    }
    $scope.getExportableNewspapers = function () {
        exportService.getExportableNewspapers().then(function (response) { $scope.ExportableNewspapers = response.data; });
    }

    $scope.getExportableBooks();

    $scope.exportBooks = function() {
        exportService.exportBooks($scope.ExportBooksModel).then(function (response) {
            console.log();
            var file = new Blob([response.data], { type: "application/octet-stream" });
            var fileURL = window.URL.createObjectURL(file);

            var filename = "";
            var disposition = response.headers('Content-Disposition');
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
            a.href = fileURL;
            a.download = filename;
            a.click();
        });
    }
});