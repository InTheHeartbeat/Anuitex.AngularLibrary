journalsModule.service('journalsService', function($http) {
    this.post = function(Journal) {
        var request = $http({
            method: 'post',
            url: "api/Journals",
            data: Journal
        });
        return request;
    }

    this.get = function() {
        return $http.get('api/Journals');
    }

    this.put = function(journal) {
        var request = $http({
            method: 'put',
            url: 'api/Journals',
            data: journal
        });
        return request;
    }

    this.delete = function(id) {
        var request = $http({
            method: "delete",
            url: "api/Journals/" + id
        });
        return request;
    }
});