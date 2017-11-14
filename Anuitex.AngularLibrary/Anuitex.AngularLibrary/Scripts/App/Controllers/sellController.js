libraryModule.controller('sellController', function ($scope, sellService, ngDialog) {

    $scope.Basket = {};
    
    loadBasket();
   
    $scope.showBasket = function() {
        sellService.getBasket().then(function(resp) {
            $scope.Basket = resp.data;
            console.log(resp);
            ngDialog.open({
                name: 'basketDialog',
                template: 'Content/Templates/Modal/basketDialogTemplate.html',
                className: 'ngdialog-theme-flat ngdialog-theme-custom ng-dialog-basket',
                scope: $scope
            });
        });
    }
    
    $scope.addToBasket = function (type, item) {
        sellService.sellProduct({ Code: item.Id, type: type, count: 1 }).then(function () { loadBasket() }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    };
    $scope.removeFromBasket = function (id, type) {
        sellService.removeProductFromBasket({ ProductId: id, ProductType: type }).then(function () { loadBasket() }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }
    $scope.applyBasket = function(id) {
        sellService.acceptSellOrder(id).then(function() {
            ngDialog.close('basketDialog');
            loadBasket();
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    }

    function loadBasket() {
        sellService.getBasket().then(function (resp) {
            $scope.Basket = resp.data;
        }, function (err) {
            alert(err.statusCode + " " + err.statusText + " " + err.statusMessage);
            console.log(err);
        });
    };
});