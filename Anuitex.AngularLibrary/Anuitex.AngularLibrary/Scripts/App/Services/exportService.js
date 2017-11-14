libraryModule.service('exportService', function($http) {

    this.getExportableBooks = function() {
        return $http.get('api/Export/GetExportableBooks');
    }
    this.getExportableJournals = function () {
        return $http.get('api/Export/GetExportableJournals');
    }
    this.getExportableNewspapers = function () {
        return $http.get('api/Export/GetExportableNewspapers');
    }

    this.exportBooks = function(exportable) {
        var request = $http({
            method: 'post',
            url: "api/Export/TryExportBooks",
            data: exportable
        });
        return request;    
    }
    this.exportJournals = function (exportable) {
        var request = $http({
            method: 'post',
            url: "api/Export/TryExportJournals",
            data: exportable
        });
        return request;
    }
    this.exportNewspapers = function (exportable) {
        var request = $http({
            method: 'post',
            url: "api/Export/TryExportNewspapers",
            data: exportable
        });
        return request;
    }
});