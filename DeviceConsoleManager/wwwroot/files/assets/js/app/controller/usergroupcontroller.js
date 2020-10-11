app.controller('usergroupcontroller', function ($scope, $http, BaseApiService, toaster) {

    $scope.userDevicelMapList = [];
    $scope.deviceMapList = [];
    

    $scope.LoadUserDeviceLists = function () {
        $scope.userDevicelMapList = [];
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserDeviceApi/GetAll'
        }).then(function successCallback(response) {
            response = response.data;
            if (response.success === true) {
                $scope.userDevicelMapList = response.data;
            }

        }, function errorCallback(response) {

        });
    };
    
    $scope.LoadUserDeviceListWithMapping = function (userId) {
        $scope.selectedUserId = userId;
        $scope.deviceMapList = [];
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserDeviceApi/GetUserDeviceMappingBy?userId=' + userId
        }).then(function successCallback(response) {
            response = response.data;
            if (response.success === true) {
                $scope.deviceListWithMappingRecords = response.data;
                //console.log(JSON.stringify($scope.deviceListWithMappingRecords));
                $scope.deviceListWithMappingRecords.forEach(function (obj) {
                    if (obj.UserGroup !== null) {
                        $scope.deviceMapList.push(obj.UserGroup);
                    }                    
                });
                $("#CustomerDeviceMappingModal").modal('show');
            }

        }, function errorCallback(response) {

        });
    };

    $scope.AddToDeviceMapList = function (data) {
        if (data.IsChecked) {
            var obj = {
                Id: $scope.deviceMapList.length + 1,
                Guid: '',
                UserId: data.UserId,
                DeviceId: data.DeviceInformation.Id,
                CreatedOn: '',
                UpdatedOn: '',
                IsActive: true
            };
            $scope.deviceMapList.push(obj);
        }
        else {
            var index = $scope.deviceMapList.findIndex(x => x.DeviceId === data.DeviceInformation.Id);
            //console.log('index: ' + index);
            $scope.deviceMapList.splice(index, 1);
        }
        //console.log(JSON.stringify($scope.deviceMapList));
    };

    $scope.SaveDeviceMaping = function (selectedUserId)
    {
        $scope.dataModel = [];
        $scope.deviceMapList.forEach(function (obj) {
            var item = {
                Guid: '',
                UserId: obj.UserId,
                DeviceId: obj.DeviceId,
                CreatedOn: '',
                UpdatedOn: '',
                IsActive: true
            };
            $scope.dataModel.push(item);
        });

        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserDeviceApi/SaveDeviceMaping?userId=' + selectedUserId,
            data: $scope.dataModel
        }).then(function successCallback(response) {
            if (response.data.success) {
                toaster.pop('success', "Message", response.data.successMessage);
                $scope.LoadUserDeviceLists();
            }
            else
                toaster.pop('error', "Message", response.data.successMessage);

            modalClose("CustomerDeviceMappingModal");
        }, function errorCallback(response) {
            console.log(response);
            toaster.pop('error', "Message", response.data.errorMessage);
        });
    };

    function modalClose(modalId) {
        $("#" + modalId).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove;
        $('.modal-backdrop').removeAttr('show');
    }
});