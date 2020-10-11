app.controller('deviceSerialNumberController', function ($scope, BaseApiService, toaster, $http) {

    $scope.GetAllSerialNo = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/DeviceSerialNumberApi/GetAll',
        }).then(function successCallback(response) {
            console.log(response);
            $scope.columnlists = response.data.data;

        }, function errorCallback(response) {

        });

    }

    $scope.GenerateSerailNo = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/DeviceSerialNumberApi/GenerateRandomNumber',
        }).then(function successCallback(response) {
            $scope.GetAllSerialNo();
        }, function errorCallback(response) {

        });

    }


});