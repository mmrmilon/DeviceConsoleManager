app.controller('topNavBarController', function ($scope, BaseApiService, toaster, $http) {

    $("#SessionUserName").html(localStorage.getItem('sessionUserName')
        + ' (' + localStorage.getItem('sessionUserRole') + ')');

    
    $scope.logout = function () {

        localStorage.setItem('token', '');
        localStorage.setItem('sessionUserName', '');
        localStorage.setItem('sessionEmailAddress', '');
        localStorage.setItem('sessionUserRole', '');
        localStorage.setItem('sessionUserId', '');

        window.location.href = BaseApiService.baseUrl();
    };

});