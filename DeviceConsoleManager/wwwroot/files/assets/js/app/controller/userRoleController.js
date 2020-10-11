app.controller('userRoleController', function ($scope, userServices, $http, BaseApiService, toaster  ) {

    $scope.isUpdate = false;

    $scope.role = {};

    $scope.loadUserRole = function () {

        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserRoleApi/GetAll'
        }).then(function successCallback(response) {
            data = response.data;
            if (data.success === true) {
                $scope.columnlists = data.data;
            }
            else {
                toaster.pop('error', "Error", data.errorMessage);
                modalClose("exampleModal");
            }

        }, function errorCallback(error, status) {

        });

    };
    
    $scope.delete = function (role) {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserRoleApi/Delete?userId=' + role.Id
        }).then(function successCallback(response) {
            $scope.loadUserRole();
            toaster.pop('success', "Ok", "Deleted Successfully");
        }, function errorCallback(response) {
        });
    };

    $scope.getEditOption = function (role) {
        $scope.role = role;
        $scope.isUpdate = true;
        $('#userRoleModal').modal('show');

    };

    $scope.Save = function(role){
        role.is_active = true;
        $http({
           method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserRoleApi/Save',
           data : role
         }).then(function successCallback(response) {
             $('#userRoleModal').modal('hide');
             $scope.loadUserRole();
             toaster.pop('success', "Ok", "Saved Successfully");

           }, function errorCallback(response) {
        });
   }

    $scope.update = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserRoleApi/Update',
            data: $scope.role
        }).then(function successCallback(response) {
            $('#userRoleModal').modal('hide');
            $scope.isUpdate = false;
        }, function errorCallback(response) {

        });
    };

});
    
    