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

$(document).on('click', '#loginNotification', function () {
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

var MobileFormfv;
var fv1;
var MobileFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('MobileModalForm');
        fv1 = FormValidation.formValidation(form, {
            fields: {
                userNumber: {
                    validators: {
                        notEmpty: {
                            message: 'User Number required'
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
            PasswordFormValidationSingleton.getInstance();
            $('#OtpPasswordModalForm').show();
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


var SignupFormfv;
var fv3;
var signupFormValidationSingleton = (function () {

    function createInstance() {
        let form = document.getElementById('signupCred');
        fv3 = FormValidation.formValidation(form, {
            fields: {
                signupUserName: {
                    validators: {
                        notEmpty: {
                            message: 'Email is required'
                        }
                    }
                },
                signupPassowrd: {
                    validators: {
                        notEmpty: {
                            message: 'Password is required'
                        }
                    }
                },
                SignupConfirmPassword: {
                    validators: {
                        callback: {
                            message: "Confirm password should be same as password",
                            callback: function (input) {
                                var signupPassowrd = $('#signupPassowrd').val();
                                var SignupConfirmPassword = $('#SignupConfirmPassword').val();
                                if (signupPassowrd == SignupConfirmPassword) {
                                    return true;
                                }
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
            var signup = {}
            login.UserName = $('#UserName');
            login.Password = $('#Password');
            $.ajax({
                type: "Post",
                url: "/LoginSignup/Login",
                data: login,
                success: function () {
                    Location.re
                },
                error: function () {

                }
            });
        });
        return fv3;
    }
    return {
        getInstance: function () {
            if (SignupFormfv) {
                SignupFormfv.destroy();
            }
            SignupFormfv = createInstance();
            return SignupFormfv;
        }
    };
})();

function Login() {
    var login = {}
    login.UserNumber = $('#MobileModalForm .userNumber').val();
    login.Password = $('#OtpPasswordModalForm .password').val();
    $.ajax({
        type: "Post",
        url: "/LoginSignup/Login",
        data: login,
        success: function (result) {
            if (result.returnStatus == 1) {
                location.reload();
            }
            else {
                toastr.warning(result.returnMessage);
            }
        },
        error: function () {

        }
    });
};

function SignUp() {

};


