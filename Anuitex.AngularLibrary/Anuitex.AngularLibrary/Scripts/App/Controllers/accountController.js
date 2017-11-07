accountModule.controller('accountController', function ($scope, accountService, sharedService, ngDialog) {

    $scope.IsAuth = false;
    $scope.CurrentUser = null;
    $scope.isSignInBtnDisabled = false;

    loadCurrentUser();

    function loadCurrentUser() {
        var request = accountService.get();

        request.then(function (response) {
            
            $scope.IsAuth = !response.data.IsVisitor;            
            $scope.CurrentUser = response.data;
            sharedService.CurrentUser = response.data;            

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
            name: 'signInDialog',
            template: 'Content/Templates/Modal/signInDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom',
            scope: $scope
        });       
    }

    $scope.trySignIn = function(login, password) {
        //$scope.isSignInBtnDisabled = true;
        var post = accountService.trySignIn(login, password);
        post.then(function(response) {
                
                if (response.data.IsVisitor === false) {
                    setCookie("VToken", "", -1);
                    setCookie("AToken", response.data.Token, 1);
                }
                ngDialog.close('signInDialog');
                loadCurrentUser();
            },
            function(err) { alert(err.statusText); });
    };

    $scope.showSignOutDialog = function() {
        ngDialog.open({
            name:'signOutDialog',
            template: 'Content/Templates/Modal/signOutDialogTemplate.html',
            className: 'ngdialog-theme-flat ngdialog-theme-custom',
            scope: $scope
        });
    };

    $scope.signOut = function () {        
        var request = accountService.signOut();
        request.then(function (responce) { setCookie("AToken", "", -1);
            loadCurrentUser();
            ngDialog.close('signOutDialog');
        }, function (err) { alert(err.statusText); });
        
    }

    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }
});