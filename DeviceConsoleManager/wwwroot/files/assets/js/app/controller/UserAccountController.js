app.controller('UserAccountController', function ($scope, BaseApiService, toaster, $http, $window) {

    $scope.isUpdateMode = false;

    $scope.GetAllUsersAccount = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserAccountApi/GetAll',
        }).then(function successCallback(response) {
            $scope.userAccountLists = response.data.data;
        });
    };

    $scope.deleteAUserAccount = function (userAccount) {
        if ($window.confirm("Are you sure you want to delete?")) {
            $http({
                method: 'POST',
                url: BaseApiService.baseUrl() + 'api/UserAccountApi/Delete',
                data: userAccount
            }).then(function successCallback(response) {
                if (response.data.success === true) {
                    toaster.pop('success', "Ok", "Deleted Successfully");
                    $scope.GetAllUsersAccount();
                }
                else {
                    toaster.pop('error', "Error", response.data.errorMessage.Message);
                }
            });
        }
    };

    $scope.openUpdateModal = function (userAccount) {
        console.log(userAccount);
        $scope.userAccount = userAccount;
        $scope.isUpdateMode = true;
        $("#userAccountModal").modal('show');
    };

    $scope.update = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserAccountApi/Update',
            data: $scope.userAccount
        }).then(function successCallback(response) {
            if (response.data.success === true) {
                toaster.pop('success', "Ok", "Updated Successfully");
                modalClose("userAccountModal");
                $scope.GetAllUsersAccount();
            }
            else {
                toaster.pop('error', "Error", response.data.errorMessage.Message);
            }
        });
    };

    $scope.GetAllUserName = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserAccountApi/GetAllUsers'
        }).then(function successCallback(response) {
            $scope.Users = response.data.data;
            $scope.userAccount = {};
            $scope.userAccount.UserId = $scope.Users[0].Id;
            $scope.userAccount.AccountNumber = $scope.Users[0].MobileNumber;
        });
    };

    $scope.userschange = function (UserId) {
        var users = $scope.Users.filter(obj => {
            return obj.Id === UserId;
        });
        $scope.userAccount.AccountNumber = users[0].MobileNumber;
    };

    $scope.saveChanges = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserAccountApi/Save',
            data: $scope.userAccount
        }).then(function successCallback(response) {
            if (response.data.success === true) {
                toaster.pop('success', "Ok", "Saved Successfully");
                modalClose("userAccountModal");
                $scope.GetAllUsersAccount();
            }
            else {
                toaster.pop('error', "Error", response.data.errorMessage.Message);
            }
        });
    };

    function modalClose(modalId) {
        $("#" + modalId).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove;
        $('.modal-backdrop').removeAttr('show');
    }
});