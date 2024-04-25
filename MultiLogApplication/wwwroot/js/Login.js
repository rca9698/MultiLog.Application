//const loginText = document.querySelector(".title-text .login");
//const loginForm = document.querySelector("form.login");
//const loginBtn = document.querySelector("label.login");
//const signupBtn = document.querySelector("label.signup");
//const signupLink = document.querySelector("form .signup-link a");
//signupBtn.onclick = (() => {
//    loginForm.style.marginLeft = "-50%";
//    loginText.style.marginLeft = "-50%";
//});

//loginBtn.onclick = (() => {
//    loginForm.style.marginLeft = "0%";
//    loginText.style.marginLeft = "0%";
//});

//$(document).ready(function () {
//    LoginFormValidationSingleton.getInstance();
//    signupFormValidationSingleton.getInstance();
//})

$(document).on('click', '#loginbtn', function () {
    MobileFormValidationSingleton.getInstance();
    $('#OtpPasswordModalForm').hide();
    $('#MobileModalForm').show();
});
$(document).on('click', '.LogoutBtnSubmit', function () {
    location.href = '/LoginSignup/SignOut';
});

$(document).on('click', '.loginNotification', function () {
    toastr.warning('Please login to application!!!');
})

//$('.viewPassword').mousedown(function () {
//    $('.password').attr('type', 'text');
//}).mouseup(function () {
//    $('.password').attr('type', 'password');
//}).mouseout(function () {
//    $('.password').attr('type', 'password');
//});

$(document).on('click', '.viewPassword', function () {
    if ($('.password').attr('type') === 'text') {
        $('.password').attr('type', 'password');
        $(this).find('i').removeClass('bi-eye-slash-fill').addClass('bi-eye-fill');
    } else {
        $('.password').attr('type', 'text');
        $(this).find('i').removeClass('bi-eye-fill').addClass('bi-eye-slash-fill');
    }
});

function LoadPassword() {
    $('.otpFormGroup').hide();
    $('.passwordFromGroup').show();

    fv2.disableValidator('otp');
    fv2.enableValidator('password');
}

function LoadOTP() {
    $('.otpFormGroup').show();
    $('.passwordFromGroup').hide();

    fv2.disableValidator('password');
    fv2.enableValidator('otp');
}

$(document).on('click', '.viewotp', function () {
    if ($('#OtpPasswordModalForm .password').attr('type') === 'text') {
        $('#OtpPasswordModalForm .password').attr('type', 'password');
        $(this).find('i').removeClass('bi-eye-slash-fill').addClass('bi-eye-fill');
    } else {
        $('#OtpPasswordModalForm .password').attr('type', 'text');
        $(this).find('i').removeClass('bi-eye-fill').addClass('bi-eye-slash-fill');
    }
});



var MobileFormfv;
var fv1;
var MobileFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('MobileModalForm');
        fv1 = FormValidation.formValidation(form, {
            fields: {
                userNumber: {
                    validators: {
                        //notEmpty: {
                        //    message: 'Mobile Number required'
                        //},
                        callback: {
                            message: 'Mobile Number should be 10 digit',
                            callback: function (value, validator, $field) {
                                if ($('#MobileModalForm .userNumber').val().length != 10)
                                    return false;
                                return true;
                            }
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap3: new FormValidation.plugins.Bootstrap3(),
                submitButton: new FormValidation.plugins.SubmitButton(),
                icon: new FormValidation.plugins.Icon({
                    valid: 'fas fa-check',
                    invalid: 'fa fa-times',
                    validating: 'fa fa-refresh'
                }),
            }
        }).on('core.form.valid', function () {
            SendOtpToMobile();
            PasswordFormValidationSingleton.getInstance();
            $('#OtpPasswordModalForm').show();
            $('.otpFormGroup').show();
            $('.passwordFromGroup').hide();

            fv2.disableValidator('password');
            fv2.enableValidator('otp');

            $('#MobileModalForm').hide();
        });
        return fv1;
    }
    return {
        getInstance: function () {
            if (MobileFormfv) {
                MobileFormfv.destroy();
            }
            MobileFormfv = createInstance();
            return MobileFormfv;
        }
    };
})();


var PasswordFormfv;
var fv2;
var PasswordFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('OtpPasswordModalForm');
        fv2 = FormValidation.formValidation(form, {
            fields: {
                password: {
                    validators: {
                        notEmpty: {
                            message: 'Password is required'
                        }
                    }
                },
                otp: {
                    validators: {
                        //notEmpty: {
                        //    message: 'Otp is required'
                        //},
                        callback: {
                            message: 'OTP should be 6 digit',
                            callback: function (value, validator, $field) {
                                if ($('#OtpPasswordModalForm .otp').val().length != 6)
                                    return false;
                                return true;
                            }
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap3: new FormValidation.plugins.Bootstrap3(),
                submitButton: new FormValidation.plugins.SubmitButton(),
                icon: new FormValidation.plugins.Icon({
                    valid: 'fas fa-check',
                    invalid: 'fa fa-times',
                    validating: 'fa fa-refresh'
                }),
            }
        }).on('core.form.valid', function () {
            Login();
        });
        return fv2;
    }
    return {
        getInstance: function () {
            if (PasswordFormfv) {
                PasswordFormfv.destroy();
            }
            PasswordFormfv = createInstance();
            return PasswordFormfv;
        }
    };
})();

function SendOtpToMobile(){
    var MobileNumber = $('#MobileModalForm .userNumber').val();
    $.ajax({
        type: "Post",
        url: "/LoginSignup/SendOTP",
        data: { MobileNumber: MobileNumber },
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
            } else {
                toastr.warning(result.returnMessage);
            }
        },
        error: function () {
            toastr.warning('error while sending OTP!!')
        }
    });
}

function Login() {
    var login = {}
    login.UserNumber = $('#MobileModalForm .userNumber').val();
    login.Password = $('#OtpPasswordModalForm .password').val();
    login.otp = $('#OtpPasswordModalForm .otp').val();
    $.ajax({
        type: "Post",
        url: "/LoginSignup/Login",
        data: login,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                location.href = result.returnVal;
            } else {
                toastr.warning(result.returnMessage);
            }
        },
        error: function () {

        }
    });
};



