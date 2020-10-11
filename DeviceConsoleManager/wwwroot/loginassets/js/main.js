var input
(function ($) {
    "use strict";

    $(".wrap-input100").focusout();
    /*==================================================================
    [ Focus Contact2 ]*/
    $('.input100').each(function () {
        $(this).on('blur', function () {
            if ($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        });
    });
  
  
    /*==================================================================
    [ Validate ]*/
    input = $('.validate-input .input100');

  

    $('.validate-form .input100').each(function(){
        $(this).focus(function(){
           hideValidate(this);
        });
    });
    
    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }
    

})(jQuery);


function logins(){
    var check = true;

    for(var i=0; i<input.length; i++) {
        if(validate(input[i]) == false){
            showValidate(input[i]);
            check=false;
        }
    }
    if(check)
        login();
}

function login(){
    makelogin().then(function (response) {

        var data = response;

        if (data.success === true) {
            localStorage.setItem('token', data.token);
            localStorage.setItem('sessionUserName', data.sessionUser.UserName);
            localStorage.setItem('sessionEmailAddress', data.sessionUser.EmailAddress);
            localStorage.setItem('sessionUserId', data.sessionUser.UserId);
             localStorage.setItem('sessionUserRole', data.sessionUser.UserRole);
            window.location.href = baseApiURL() + '/home/home';
        } else {
            showErrorMessage();
        }
    });
}

function makelogin(){
    
    var userName = $("input[name=email]").val();
    var password = $("input[name=pass]").val();
    var url = baseApiURL() + 'api/LogInApi/Login?email=' + userName + '&password=' + password;

    var promise = $.post(url,function(data){
            return data;
    }).fail(function(response) {
        showErrorMessage();
    });

    return promise;
}


function logout(){
    var csrf_token = $('meta[name="csrf-token"]').attr('content');
    var url  = baseApiURL()+'logout';

    var data = { 
        _token: csrf_token
    }
    var promise = $.post(url, data, function (data) {
        window.location.href = baseApiURL();
        distroytoken();
    });

}


function showErrorMessage(){
    $('#wrong-password').show();
    $('#wrong-password').delay(2000).fadeOut('slow');
}    

function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}
function setToken(token,expiration){
    localStorage.setItem('token',token);
}
function distroytoken(){
    localStorage.removeItem('token');
}

function getToken(){
    return  'Bearer '+ localStorage.getItem('token');
}

function baseApiURL() {
    var fullPath = window.location.href;
    var parts = fullPath.split('/');
    return parts[0] + '//' + parts[2] + '/';
}

function validate (input) {
    if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
        if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
            return false;
        }
    }
    else {
        if($(input).val().trim() == ''){
            return false;
        }
    }
}
