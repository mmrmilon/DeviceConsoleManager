app.controller('AccountRechargeController', function ($scope, $http, BaseApiService, userServices, toaster, $window) {

    $scope.accountRecharge = {};
    $scope.isUpdate = false;

    $scope.OpenAddModal = function () {
        $scope.accountRecharge = {};
        $scope.isUpdate = false;
    };

    $scope.GetAll = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/AccountRechargeApi/GetAll'
        }).then(function successCallback(response) {
            $scope.accountRechargeHistoryList = response.data.data;
        });
    };

    $scope.Save = function (accountRecharge) {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/AccountRechargeApi/Save',
            data: accountRecharge
        }).then(function successCallback(response) {
            if (response.data.success === true) {
                toaster.pop('success', "Ok", "Saved Successfully");
                modalClose("AccountRechargeModal");
                $scope.GetAll();
            }
            else {
                toaster.pop('error', "Error", response.data.errorMessage.Message);
            }
        });
    };

    $scope.update = function () {
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/AccountRechargeApi/Update',
            data: $scope.accountRecharge
        }).then(function successCallback(response) {
            if (response.data.success === true) {
                toaster.pop('success', "Ok", "Updated Successfully");
                modalClose("AccountRechargeModal");
                $scope.isUpdate = false;
                $scope.GetAll();
            }
            else {
                toaster.pop('error', "Error", response.data.errorMessage.Message);
            }
        });
    };

    $scope.delete = function (id) {
        if ($window.confirm("Are you sure you want to delete?")) {
            $http({
                method: 'POST',
                url: BaseApiService.baseUrl() + 'api/AccountRechargeApi/Delete?id=' + id
            }).then(function successCallback(response) {
                if (response.data.success === true) {
                    toaster.pop('success', "Ok", "Deleted Successfully");
                    $scope.GetAll();
                }
                else {
                    toaster.pop('error', "Error", response.data.errorMessage.Message);
                }
            });
        }           
    };

    $scope.OpenAccountRechargeModal = function (data) {        
        $scope.isUpdate = true;
        $scope.accountRecharge = data.AccountRecharge;
        $('#AccountRechargeModal').modal('show');
    };

    $scope.GetCustomerList = function () {
        userServices.GetCustomerList().then(function (res) {
            $scope.Users = res.data;
            $scope.ResetInit = true;
        });
    };

    $scope.GetAllPaymentMethod = function () {
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/PaymentMethodApi/GetAll'
        }).then(function successCallback(response) {
            $scope.paymentMethodList = response.data.data;
        }, function errorCallback(response) {

        });
    };

    function modalClose(modalId) {
        $("#" + modalId).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove;
        $('.modal-backdrop').removeAttr('show');
    }
});