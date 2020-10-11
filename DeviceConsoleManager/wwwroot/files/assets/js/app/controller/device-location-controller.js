app.controller('DeviceLocationController', function ($scope, $http, BaseApiService, toaster ) {

    $scope.location = {};
    $scope.isUpdate = false;

    $scope.AddNewLocations = function () {
        $scope.isUpdate = false;
        $scope.location = {};
    };

    $scope.GetAll = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/DeviceLocationApi/GetAll'
        }).then(function successCallback(response) {
            console.log(response);
            $scope.locationlist = response.data.data;
        }, function errorCallback(response) {

        });
    };

    $scope.Save = function (location) {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/DeviceLocationApi/Save',
            data: location
        }).then(function successCallback(response) {
            modalClose("deviceLocationModal");
            $scope.GetAll();
            toaster.pop('success', "Ok", "Saved Successfully");
        }, function errorCallback(response) {
        });
    };

    $scope.update = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/DeviceLocationApi/Update',
            data: $scope.location
        }).then(function successCallback(response) {
            modalClose("deviceLocationModal");
            $scope.isUpdate = false;
            toaster.pop('success', "Ok", "Updated Successfully");
            $scope.GetAll();
        }, function errorCallback(response) {

        });
    };

    $scope.delete = function (id) {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/DeviceLocationApi/Delete?id=' + id
        }).then(function successCallback(response) {
            $scope.GetAll();
            toaster.pop('success', "Ok", "Deleted Successfully");
        }, function errorCallback(response) {

        });
    };

    $scope.OpenDeviceLoactionModal = function (data) {
        $scope.isUpdate = true;
        $scope.location = data;
        $('#deviceLocationModal').modal('show');
    };

    function modalClose(modalId) {
        $("#" + modalId).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove;
        $('.modal-backdrop').removeAttr('show');
    }

});