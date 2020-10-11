app.controller('devicecontroller', function($scope,deviceServices,$http,BaseApiService ) {

    $scope.device = {};

    $scope.GetAllDevice = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/DeviceApi/GetAll',
        }).then(function successCallback(response) {
            console.log(response);
            $scope.columnlists = response.data.data;
        }, function errorCallback(response) {

        });

    }
    $scope.DeviceInitModal = function () {
        $scope.device = {};
        $scope.device.DeviceLocationId = $scope.deviceLoadtion[0].Id;
        $scope.GetDeviceTokenNumber();

    }

    $scope.GetDeviceTokenNumber = function () {

        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/DeviceSerialNumberApi/GetTopSerialNo',
        }).then(function successCallback(response) {
            $scope.device.DeviceCode = response.data.data;

        }, function errorCallback(response) {

        });

    }

    $scope.isUpdate = false;

    $scope.delete = function(id){
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/DeviceApi/Delete?pDeviceId='+id
        }).then(function successCallback(response) {
            $scope.GetAllDevice();
            }, function errorCallback(response) {
            
        });
    }

    $scope.getEditOption = function (data) {
        $scope.isUpdate = true;
        $scope.device = data;
        $('#deviceInfomationModal').modal('show');
    }

    $scope.genarateRandomNumber = function () {
        $scope.device = {};
        var frist_part = Math.floor(1000 + Math.random() * 9000), second_part = Math.floor(1000 + Math.random() * 9000), third_part = Math.floor(1000 + Math.random() * 9000), last_part = Math.floor(1000 + Math.random() * 9000);
        $scope.device.DeivceToken = frist_part + '' + second_part + '' + third_part + '' + last_part;

    };

    $scope.Save = function (device) {
        $http({
           method: 'POST',
            url: BaseApiService.baseUrl() + 'api/DeviceApi/Save',
           data : device
         }).then(function successCallback(response) {
             $('#deviceInfomationModal').modal('hide'); 
             $scope.GetAllDevice();
           }, function errorCallback(response) {
        });
   }

   $scope.update = function(){
       $http({
           method: 'POST',
           url: BaseApiService.baseUrl() + 'api/DeviceApi/Update',
           data : $scope.device
         }).then(function successCallback(response) {
               $('#deviceInfomationModal').modal('hide');               
                 $scope.isUpdate = false; 
                 $scope.GetAllDevice();
           }, function errorCallback(response) {
             
        });
    }


    $scope.GetAllLocations = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/DeviceLocationApi/GetAll'
        }).then(function successCallback(response) {
            $scope.deviceLoadtion = response.data.data;
            $scope.device.DeviceLocationId = $scope.deviceLoadtion[0].Id;

        }, function errorCallback(response) {

        });
    };

  

   



});