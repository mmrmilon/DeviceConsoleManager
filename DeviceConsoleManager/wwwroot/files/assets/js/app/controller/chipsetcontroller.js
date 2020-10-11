app.controller('chipsetcontroller', function ($scope, $http, BaseApiService, $filter, toaster, $window) {
    $scope.usercard = {};
    $scope.GetUserCards = function () {
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserCard/GetAll'
        }).then(function successCallback(response) {
            data = response.data;
            if (data.success === true) {
                $scope.columnlists = data.data;
            }
        }, function errorCallback(error, status) {

        });
    };    

    $scope.genarateRandomNumber = function () {    
        GetPrefix().then(function (res) {
            var frist_part = Math.floor(1000 + Math.random() * 9000), second_part = Math.floor(1000 + Math.random() * 9000), third_part = Math.floor(1000 + Math.random() * 9000), last_part = Math.floor(1000 + Math.random() * 9000);
            $scope.usercard.ChipCardNo = frist_part + '' + second_part + '' + third_part + '' + res.data;
        });
    };

    $scope.GetAllUsers = function () {
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserCard/GetAllUsers'
        }).then(function successCallback(response) {
            //console.log(response);
            $scope.Users = response.data.data;
            $scope.usercard.UserId = response.data.data.length > 0 ? response.data.data[0].Id : 0;                
        }, function errorCallback(error, status) {

        });
    };

    $scope.saveChanges = function () {
        var date = $scope.usercard.ExpairDate;
        date = date.getMonth() + 1 + '/' + date.getDate() + '/' + date.getFullYear();
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserCard/WriteToCard?date=' + date,
            data: $scope.usercard
        }).then(function successCallback(response) {
            console.log(response);
            var data = response.data;
            if (data.success === true) {
                toaster.pop('success', "Ok", 'Saved Successfully');
                modalClose("userChipModal");
                $scope.GetUserCards();
            }
            else {
                toaster.pop('error', "Error", data.errorMessage.Message);
            }
        }, function errorCallback(error, status) {
           toaster.pop('success', "Ok", "Saved Successfully");
        });
    };

    $scope.deleteSmartCard = function (chipCardId) {
        if ($window.confirm("Are you sure you want to delete?")) {
            $http({
                method: 'POST',
                url: BaseApiService.baseUrl() + 'api/UserCard/Delete?chipCardId=' + chipCardId
            }).then(function successCallback(response) {
                if (response.data.success === true) {
                    toaster.pop('success', "Ok", "Deleted Successfully");
                    $scope.GetUserCards();
                }
                else {
                    toaster.pop('error', "Error", response.data.errorMessage.Message);
                }
            }, function errorCallback(response) {

            });
        }       
    };

    $scope.openUpdateModal = function (data) {
        data.Card.ExpairDate = new Date(data.Card.ExpairDate);
        $scope.chipCardInfo = data;
        console.log($scope.chipCardInfo);
        $("#userChipUpdateModal").modal('show');
    };

    $scope.update = function (data) {
        data.Card.ExpairDate = $filter('date')(new Date(data.Card.ExpairDate), 'yyyy-MM-dd');
        //console.log(data.Card);
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserCard/Update',
            data: data.Card
        }).then(function successCallback(response) {
            if (response.data.success === true) {
                toaster.pop('success', "Ok", "Updated Successfully");
                modalClose("userChipUpdateModal");
                $scope.GetUserCards();
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

    function GetPrefix() {
        var promise = $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserCard/GetPrefix'
        }).then(function successCallback(response) {
            data = response.data;
            if (data.success === true) {
                return data;
            }
        }, function errorCallback(error, status) {

        });
        return promise;
    }
});