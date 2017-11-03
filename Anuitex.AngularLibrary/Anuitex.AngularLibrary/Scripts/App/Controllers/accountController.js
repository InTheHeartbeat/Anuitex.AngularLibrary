accountModule.controller('accountController', function ($scope, accountService, ngDialog) {
    $scope.IsAuth = false;
    $scope.CurrentUser = null;

    loadCurrentUser();

    function loadCurrentUser() {
        var request = accountService.get();

        request.then(function (response) {
            $scope.IsAuth = !response.data.IsVisitor;            
            $scope.CurrentUser = response.data;            
            if (response.data.IsVisitor === true) {
                setCookie("AToken", "", -1);
                setCookie("VToken", response.data.Token, 1);
            }
            if (response.data.IsVisitor === false) {                
                setCookie("VToken", "", -1);
                setCookie("AToken", response.data.Token, 1);
            }            
        }, function (err) { alert('failture loading current user ' + err.error_description); });                



    }

    $scope.showSignInDialog = function () {        
        ngDialog.open({
            template: 'Content/Templates/Modal/signInDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom',
            scope: $scope
        });       
    }

    $scope.trySignIn = function (login, password) {
        
        var post = accountService.trySignIn(login, password);
        post.then(function (response) {
            ngDialog.close('ngdialog1');
            if (response.data.IsVisitor === false) {                
                setCookie("VToken", "", -1);
                setCookie("AToken", response.data.Token, 1);
            }            
            loadCurrentUser();            
        }, function (err) { alert(err.error_description); });
    }

    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }
});