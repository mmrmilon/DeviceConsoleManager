app.controller('sideNavBarController', function ($scope, BaseApiService, toaster, $http) {

    var userRole = localStorage.getItem('sessionUserRole');
    var SUPERADMIN = "SuperAdmin", ADMIN = "Admin", CUSTOMER = "Customer", OPARETOR = "Operator";

    $scope.Init = function () {
        loadNavBarByUserRole();
    };
    
    function loadNavBarByUserRole() {

        $(".manu-options").hide();
        $("#CustomerInformation").hide();

        if (userRole === SUPERADMIN) {
            SupperAdminShow();
        }
        else if (userRole === ADMIN) {
            AdminShow();
        }
        else if (userRole === CUSTOMER) {
            CustomerShow();
        }
        else if (userRole === OPARETOR) {
            OparetorShow();
        }
    }

    function SupperAdminShow() {
        $("#UserSettings").html('Admin Settings');
        $("#UserInfromation").show();
        $("#UserInfromation > a > span").html('Admin Information');
        $("#UserRole").show();
        $("#UserRole > a > span").html('App User Role');
        $("#DeviceInformation").show();
        $("#ServiceConfiguration").show();
        $("#DeviceSerialNumber").show();
    }

    function AdminShow() {
        $("#UserSettings").html('Oparetor Settings');
        $("#UserInfromation").show();
        $("#UserInfromation > a > span").html('Oparetor Information');
        $("#UserRole").show();
        $("#UserRole > a > span").html('App User Role');
        $("#UserAccount").show();
        $("#UserAccount > a > span").html('Customer Account');
        $("#DeviceInformation").show();
        $("#UserDeviceServices").show();
        $("#DeviceLocations").show();
        $("#ServiceConfiguration").show();
        $("#PaymentMethods").hide();
    }

    function OparetorShow() {
        $("#UserInfromation").show();
        $("#UserSettings").html('Customer Settings');
        $("#UserInfromation > a > span").html('Customer Information');
        $("#UserAccount > a > span").html('Customer Account');
        $("#UserAccount").show();
        $("#ChipsetInformation").show();
        $("#AccountRechargeHistory").show();
        $("#DeviceInformation").show();
        $("#UserDeviceServices").show();
    }
});