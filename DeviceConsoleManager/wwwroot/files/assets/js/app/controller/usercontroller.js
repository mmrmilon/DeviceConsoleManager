app.controller('usercontroller', function ($scope, userServices, $http, BaseApiService, $route, toaster, $location,$window) {

    $scope.user = {};
    $scope.frist_form = true;
    $scope.second_form = false;
    $scope.their_form = false;
    $scope.devicelists = [];
    $scope.init = true;
    $scope.isPasswordConfirm = true;
    $scope.rowIndex = 0;
    $scope.isAdminUser = false;
    $scope.isUpdateMode = false;
    $scope.ResetInit = true;
    $scope.title = 'Customer';
    $scope.isNumberValidate = true;
    $scope.reset = {};
	

    if (localStorage.getItem('sessionUserRole') === 'Operator')
        $scope.title = 'Customer';
    else if (localStorage.getItem('sessionUserRole') === 'Admin')
        $scope.title = 'Operator';
    else
        $scope.title = 'Admin';


    $scope.GetAllUserRoles = function () {
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserRoleApi/GetAllByRoleName?roleName=' + localStorage.getItem('sessionUserRole')
        }).then(function successCallback(response) {
            $scope.userRole = response.data.data;
            $scope.userRoleChange(localStorage.getItem('sessionUserRole'));
			
        }, function errorCallback(response) {

        });
    };

    $scope.GetAllUsers = function () {
        isUpdateMode = false;
        userServices.GetAllUsers(localStorage.getItem('sessionUserRole')).then(function (res) {
            $scope.columnlists = res.data;
            $scope.ResetInit = true;
        });
    };

    $scope.InitializeAddNewUserModel = function () {
        isUpdateMode = false;
        $scope.user = {};
        $scope.user.RoleId = $scope.userRole[0].Id;
        $scope.validation = {
            fristname: true,
            lastName: true,
            mobileNumber: true
        };
    };

    $scope.saveChanges = function () {
		
		var isValidUser = true;
	
		if($scope.user.FristName === "" || $scope.user.FristName === undefined){
			$scope.validation.fristname = false;
            isValidUser = false;
		}
		if($scope.user.LastName === "" || $scope.user.LastName === undefined){
			$scope.validation.lastName = false;
			isValidUser = false;
		}
		
		if($scope.user.EmailAddress === "" || $scope.user.EmailAddress === undefined){
            $scope.user.EmailAddress = "";
		}
		
		
		if($scope.user.MobileNumber === undefined){
			$scope.validation.mobileNumber = false;
			isValidUser = false;
		}else{
			if($scope.user.MobileNumber.toString().length !== 10){
				$scope.validation.mobileNumber = false;
				isValidUser = false;
			}
		}
		
        $scope.isPasswordConfirm = fnConfirmPassword($scope.user.Password, $scope.user.confirmPassword);

        var isValid = AdminUserCheck();


        if (isValid === true && isValidUser === true) {

            setDefaultPasswordForCustomer();

            $http({
                method: 'POST',
                url: BaseApiService.baseUrl() + 'api/UserApi/Save',
                data: $scope.user
            }).then(function successCallback(response) {
                data = response.data;
                if (data.success === true) {
                    $scope.GetAllUsers();
                    toaster.pop('success', "Ok", "Saved Successfully");
                    modalClose("exampleModal");
                    $scope.user = {};
                    $scope.init = true;
                }
                else {
                    toaster.pop('error', "Error", data.errorMessage);
                    modalClose("exampleModal");
                }

            }, function errorCallback(error, status) {
                toaster.pop('error', "Error", error);
            });

        } 
    };

    
    $scope.checkvalidation = function (level) {

        if (level === 'fristname') {
            if ($scope.user.FristName === "" || $scope.user.FristName === undefined) {
                $scope.validation.fristname = false;
            }
            else {
                $scope.validation.fristname = true;
            }
        }

        if (level === 'lastName') {
            if ($scope.user.LastName === "" || $scope.user.LastName === undefined) {
                $scope.validation.lastName = false;
            }
            else {
                $scope.validation.lastName = true;
            }
        }

        if (level === 'mobileNumber') {
            console.log('MobileNumber: ' + $scope.user.MobileNumber.length);
            if ($scope.user.MobileNumber.toString().length === 11) {
                $scope.validation.mobileNumber = true;
            }
            else {
                $scope.validation.mobileNumber = false;
            }
        }
    };
	
    $scope.deleteAUser = function (userId) {
        if ($window.confirm("Are you sure you want to delete?")) {
            $http({
                method: 'POST',
                url: BaseApiService.baseUrl() + 'api/UserApi/Delete?userId=' + userId,
            }).then(function successCallback(response) {
                if (response.data.success === true) {
                    toaster.pop('success', "Ok", "Deleted Successfully");
                    $scope.GetAllUsers();
                }
                else {
                    toaster.pop('error', "Error", response.data.errorMessage.Message);
                }
            });
        } 
    };

    /* update action */
    $scope.getUserInformationByUserId = function (userId) {
        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'getUserInformationByUserId?user_id=' + userId
        }).then(function successCallback(response) {
            $scope.user = response.data[0];
            console.log($scope.user);
            $scope.user.device = [];
            $scope.devicelists = [];
            angular.forEach(response.data, function (value, index) {
                $scope.devicelists.push(index);
                $scope.user.device.push(value.device_id);
            });
            $("#exampleModal").modal('show');
        }, function errorCallback(response) {

        });

    };

    $scope.openUpdateModal = function (user) {
        $scope.isUpdateMode = true;
        $scope.validation = {
            fristname: true,
            lastName: true,
            mobileNumber: true
        };
        var role = $scope.userRole.filter(obj => {
            return obj.RoleName === user.RoleName;
        });
        $scope.user = user;
        $scope.user.RoleId = role[0].Id;
        console.log($scope.user);
        $("#exampleModal").modal('show');
    };

    $scope.update = function (user) {
        $scope.isUpdateMode = true;
        $scope.mobileValidation($scope.user.MobileNumber, false);

            $http({
                method: 'POST',
                url: BaseApiService.baseUrl() + 'api/UserApi/Update',
                data: $scope.user
            }).then(function successCallback(response) {
                $scope.GetAllUsers();
                toaster.pop('success', "Ok", "Updated Successfully");
                modalClose("exampleModal");
            }, function errorCallback(response) {

            });
    };

    /* reset passoword */
    $scope.setResetPassword = function (Id) {
        $scope.UserId = Id;
    };

    $scope.resetPassword = function () {

        $scope.ResetInit = false;

        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserApi/ResetPassword?password=' + $scope.reset.Password + '&userId=' + $scope.UserId
        }).then(function successCallback(response) {
            toaster.pop('success', "Ok", "Password Reset Successfully!");
            modalClose("resetPassword");
        }, function errorCallback(response) {

        });
    };

    /* password streth and confirm passoword */
    $scope.confirmPassword = function (confirmPassword, password) {
        $scope.isPasswordConfirm = fnConfirmPassword(confirmPassword, password);
    };

    function fnConfirmPassword(password, confirmPassword) {
        if (angular.isUndefined(password) || angular.isUndefined(confirmPassword)) {
            return false;
        }
        if (password === confirmPassword)
            return true;
        else
            return false;
    }

    function setDefaultPasswordForCustomer() {

        if ($scope.isAdminUser === false) {
            $scope.user.Password = 12345678;
        }
    }

    function AdminUserCheck() {
        if ($scope.isAdminUser) {
            var isvalid = passwordStrenthCheck($scope.user.Password);
            if ($scope.isPasswordConfirm === true && isvalid === true) {
                return true;
            }
            else
                return false;
        }
        return true;
    }

    function passwordStrenthCheck(password) {
        if (password.length < 6) {
            $("#password-level").html("Password length must be greater then 6");
            return false;
        } else {
            return true;
        }

    }

    /* password streth and confirm passoword end*/
    $scope.mobileValidation = function (mobileNumber, init) {
        //#1 mobile number must be 11 number
        console.log("mobileNumber" + mobileNumber);
        console.log("init" + init);

        if (mobileNumber.length !== 11 && init === false) {
            $scope.isNumberValidate = false;
        }
        else {
            $scope.isNumberValidate = true;
        }
    };

    $scope.userRoleChange = function (roleName) {
		$scope.isPasswordConfirm = true;
		if (roleName === "Admin" || roleName === "SuperAdmin")
			$scope.isAdminUser = true;
		else
			$scope.isAdminUser = false;       
    };	
    $scope.ChangePassoword = function () {
        console.log('Calling...');
        $http({
            method: 'POST',
            url: BaseApiService.baseUrl() + 'api/UserApi/ResetPassword?password=' + $scope.reset.Password + '&userId=' + localStorage.getItem('sessionUserId')
        }).then(function successCallback(response) {
            toaster.pop('success', "Ok", "Password changed successfully!");
            localStorage.setItem('token', '');
            localStorage.setItem('sessionUserName', '');
            localStorage.setItem('sessionEmailAddress', '');
            localStorage.setItem('sessionUserRole', '');
            localStorage.setItem('sessionUserId', '');

            window.location.href = BaseApiService.baseUrl();
        }, function errorCallback(response) {
                toaster.pop('error', "Ok", "Password changed un-successful!");
        });
    };

    function modalClose(modalId) {
        $("#" + modalId).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove;
        $('.modal-backdrop').removeAttr('show');
    }

});

