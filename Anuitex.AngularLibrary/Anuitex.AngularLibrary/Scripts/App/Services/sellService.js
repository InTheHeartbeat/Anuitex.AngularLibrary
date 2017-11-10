libraryModule.service('sellService', function($http) {

    this.getBasket = function() {
        return $http.get('api/Sell/GetBasket');
    }

    this.sellProduct = function(productData) {
        var request = $http({
            method: 'post',
            url: "api/Sell/SellProduct",
            data: productData
        });
        return request;
    }

    this.acceptSellOrder = function (orderId) {
        var request = $http({
            method: 'post',
            url: "api/Sell/AcceptSellOrder",
            data: { orderId: orderId }
        });
        return request;
    }
    
    this.removeProductFromBasket = function(removeProductFromBasketModel) {
        var request = $http({
            method: 'post',
            url: "api/Sell/RemoveProductFromBasket",
            data: removeProductFromBasketModel
        });
        return request;
    }    
});