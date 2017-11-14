libraryModule.service('importService', function($http) {

    this.confirmImport = function (model) {                
        return $http.post("api/Import/ConfirmImport", model);
    } 
});