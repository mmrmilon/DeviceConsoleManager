app.controller('deviceConfigGroupController', function ($scope, BaseApiService, toaster,$http) {
    $scope.serviceConfigList = [];

    $scope.GetServiceConfigurationList = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/ServiceConfigurationApi/GetAll'
        }).then(function successCallback(response) {
            $scope.serviceConfigList = response.data.data;
            console.log($scope.serviceConfigList);

        }, function errorCallback(response) {

        });
    };

    $scope.OpenServiceConfigModal = function (data) {
        console.log(JSON.stringify(data));
        $scope.DeviceName = data.DeviceInformation.DeviceName;
        $scope.LocationName = data.DeviceLocation.LocationName;
        $scope.ServiceConfigDataModel = {
            Id: data.ServiceConfiguration === null? 0:data.ServiceConfiguration.Id,
            DeviceId: data.DeviceInformation.Id,
            Unit: data.ServiceConfiguration === null ? 0 : data.ServiceConfiguration.Unit,
            UnitRate: data.ServiceConfiguration === null ? 0 : data.ServiceConfiguration.UnitRate,
            IsActive: data.ServiceConfiguration === null ? false : data.ServiceConfiguration.IsActive
        };
        
        $("#ServiceConfigModal").modal('show');
    };

    $scope.UpdateServiceConfig = function () {
        if ($scope.ServiceConfigDataModel.Unit === '' || $scope.ServiceConfigDataModel.UnitRate === '') {
            toaster.pop('danger', "error", "Unit and Unit Rate can not be emety!");
        }
        else if (parseInt($scope.ServiceConfigDataModel.Unit) < 0) {
            toaster.pop('danger', "error", "Unit must be in greater than zero (0).");
        }
        else {
            $http({
                method: 'Post',
                url: BaseApiService.baseUrl() + 'api/ServiceConfigurationApi/UpdateServiceConfiguration',
                data: $scope.ServiceConfigDataModel
            }).then(function successCallback(response) {
                if (response.data.success) {
                    toaster.pop('success', "Message", response.data.successMessage);
                    $scope.GetServiceConfigurationList();
                }
                else
                    toaster.pop('error', "Message", response.data.successMessage);

                modalClose("ServiceConfigModal");
                
            }, function errorCallback(response) {

            });
        }
    };

    function modalClose(modalId) {
        $("#" + modalId).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove;
        $('.modal-backdrop').removeAttr('show');
    }
});