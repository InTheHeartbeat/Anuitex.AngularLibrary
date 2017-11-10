libraryModule.controller('sellController', function ($scope, sellService, ngDialog) {

    $scope.Basket = {};

    function loadBasket() {
        sellService.getBasket().then(function(resp) {
            $scope.Basket = resp.data;
        });
    };

    loadBasket();

    $scope.addToBasket = function(type, item) {
        sellService.sellProduct({ Code: item.Id, type: type, count: 1 }).then(function () { loadBasket() });
    };
    $scope.removeFromBasket = function(id,type) {
        sellService.removeProductFromBasket({ ProductId: id, ProductType: type }).then(function() { loadBasket() });
    }

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

    $scope.applyBasket = function(id) {
        sellService.acceptSellOrder(id).then(function() {
            ngDialog.close('basketDialog');
            loadBasket();
        });
    } 
});