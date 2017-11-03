booksModule.service('booksService', function($http) {
    this.post = function(Book) {
        var request = $http({
            method: 'post',
            url: "api/Books",
            data: Book
        });
        return request;
    }

    this.get = function() {
        return $http.get('api/Books');
    }

    this.put = function(Book) {
        var request = $http({
            method: 'put',
            url: 'api/Books',
            data: Book
        });
        return request;
    }

    this.delete = function(id) {
        var request = $http({
            method: "delete",
            url: "api/Books/" + id
        });
        return request;
    }
});