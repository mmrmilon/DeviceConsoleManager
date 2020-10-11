app.controller('PaymentMethodController', function($scope,$http,BaseApiService ) {

    $scope.paymentMethod = {};
    $scope.isUpdate = false;

    $scope.GetAll = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/PaymentMethodApi/GetAll'
        }).then(function successCallback(response) {
            console.log(response);
            $scope.locationlist = response.data.data;

        }, function errorCallback(response) {

        });
    };

    $scope.Save = function (paymentMethod) {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/PaymentMethodApi/Save',
            data: paymentMethod
        }).then(function successCallback(response) {
            $('#PaymentMethodModal').modal('hide');
            $scope.GetAll();
        }, function errorCallback(response) {
        });
    };

    $scope.update = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/PaymentMethodApi/Update',
            data: $scope.paymentMethod
        }).then(function successCallback(response) {
            $('#PaymentMethodModal').modal('hide');
            $scope.isUpdate = false;
        }, function errorCallback(response) {

        });
    };

    $scope.delete = function (id) {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/PaymentMethodApi/Delete?id=' + id
        }).then(function successCallback(response) {
            $scope.GetAllDevice();
        }, function errorCallback(response) {

        });
    };

    $scope.OpenPaymentMethodModal = function (data) {
        $scope.isUpdate = true;
        $scope.paymentMethod = data;
        $('#PaymentMethodModal').modal('show');
    };

});