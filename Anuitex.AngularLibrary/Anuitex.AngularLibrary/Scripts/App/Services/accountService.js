accountModule.service('accountService', function ($http) {  
    this.get = function () {
        return $http.get('api/Account/GetCurrentUser');
    }   

    this.trySignIn = function(login, password) {              
        var request = $http({
            method: 'post',
            url: "api/Account/TrySignIn",
            data: { login: login, password: password }
        });
        return request;
    }

    this.signOut = function() {
        var request = $http.get('api/Account/SignOut');
        return request;
    }

    this.trySignUp = function(login, password) {
        var request = $http({
            method: 'post',
            url: "api/Account/TrySignUp",
            data: { login: login, password: password }
        });
        return request;
    }
});