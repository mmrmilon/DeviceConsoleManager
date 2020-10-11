app.service('userServices', function ($http, BaseApiService) {
    this.GetAllUsers = function (userRole) {
        var url = BaseApiService.baseUrl() + 'api/UserApi/GetAll?roleName=' + userRole;

        var promise = $http.get(url).then(function (res) {
            var userRole = res.data;
            return userRole;
        });
        return promise;
    };

    this.GetAllUsersLists = function () {
        var url = BaseApiService.baseUrl() + 'api/UserApi/GetAllListOfUsers';
        var promise = $http.get(url).then(function (res) {
            var users = res.data;
            return users;
        });
        return promise;
    };

    this.GetCustomerList = function () {
        var url = BaseApiService.baseUrl() + 'api/UserApi/GetCustomerList';
        var promise = $http.get(url).then(function (res) {
            var users = res.data;
            return users;
        });
        return promise;
    };

    this.GetDeviceInfoBy = function (userId) {
        var url = BaseApiService.baseUrl() + 'api/UserDeviceApi/GetUserDeviceBy?userId=' + userId;
        var promise = $http.get(url).then(function (res) {
            return res.data;
        });
        return promise;
    };
});