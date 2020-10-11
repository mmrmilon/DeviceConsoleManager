var app = angular.module("myApp", ["ngRoute", "datatables", 'toaster', 'ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider
        // .when("/", {
        //   template : "Dashboard"
        // })
        .when("/", {
            templateUrl: "/resources/views/Users/user-information.html",
            controller: "usercontroller"
        })
        .when("/Dashboard", {
            templateUrl: "/resources/views/Report/user-service-report.html",
            controller: "ReportController"
        })
        .when("/MyProfile", {
            templateUrl: "/resources/views/Users/my-profile.html",
            controller: "usercontroller"
        })
        .when("/UserAccount", {
            templateUrl: "/resources/views/Users/user-account.html",
            controller: "UserAccountController"

        })
        .when("/DeviceService", {
            templateUrl: "../resources/views/Users/user-group.html",
            controller: "usergroupcontroller"

        })
        .when("/UserRole", {
            templateUrl: "/resources/views/Users/user-role.html",
            controller: "userRoleController"
        })
        .when("/Chipset", {
            templateUrl: "/resources/views/Chipset/chipset.html",
            controller: "chipsetcontroller"
        })
        .when("/DeviceInformation", {
            templateUrl: "/resources/views/Devices/device-information.html",
            controller: "devicecontroller"
        })

        .when("/DeviceServiceConfiguration", {
            templateUrl: "/resources/views/Devices/device-service.html",
            controller: "deviceConfigGroupController"
        })
        .when("/DeviceSerialNumber", {
            templateUrl: "/resources/views/Devices/device-serial-number.html",
            controller: "deviceSerialNumberController"
        })
        .when("/AccountRechargeHistory", {
            templateUrl: "/resources/views/Users/user-account-recharge.html",
            controller: "AccountRechargeController"
        })
        .when("/DeviceLocation", {
            templateUrl: "/resources/views/Devices/device-locations.html",
            controller: "DeviceLocationController"
        })
        .when("/UserProfile", {
            templateUrl: "/resources/views/Users/user-profile.html",
            controller: "usercontroller"
        })
        .otherwise({
            template: "<h1>None</h1><p>Nothing has been selected</p>"
        });

});

app.service('BaseApiService', function () {
    this.baseUrl = function (x) {
        var fullPath = window.location.href;
        var parts = fullPath.split('/');
        return parts[0] + '//' + parts[2] + '/';
    };
});