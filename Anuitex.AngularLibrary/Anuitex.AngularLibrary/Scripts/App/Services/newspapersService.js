newspapersModule.service('newspapersService', function($http) {
    this.post = function(Newspaper) {
        var request = $http({
            method: 'post',
            url: "api/Newspapers",
            data: Newspaper
        });
        return request;
    }

    this.get = function() {
        return $http.get('api/Newspapers');
    }

    this.put = function(newspaper) {
        var request = $http({
            method: 'put',
            url: 'api/Newspapers',
            data: newspaper
        });
        return request;
    }

    this.delete = function(id) {
        var request = $http({
            method: "delete",
            url: "api/Newspapers/" + id
        });
        return request;
    }
});