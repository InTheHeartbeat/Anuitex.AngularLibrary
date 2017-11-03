accountModule.service('accountService', function ($http) {  
    this.get = function () {
        return $http.get('api/Account/GetCurrentUser');
    }

    var loginData = {login: "", password: ""}

    this.trySignIn = function(login, password) {
        loginData.login = login;
        loginData.password = password;
        var request = $http({
            method: 'post',
            url: "api/Account/TrySignIn",
            data: loginData
        });
        return request;
    }
});