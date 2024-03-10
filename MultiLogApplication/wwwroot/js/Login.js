const loginText = document.querySelector(".title-text .login");
const loginForm = document.querySelector("form.login");
const loginBtn = document.querySelector("label.login");
const signupBtn = document.querySelector("label.signup");
const signupLink = document.querySelector("form .signup-link a");
signupBtn.onclick = (() => {
    loginForm.style.marginLeft = "-50%";
    loginText.style.marginLeft = "-50%";
});

loginBtn.onclick = (() => {
    loginForm.style.marginLeft = "0%";
    loginText.style.marginLeft = "0%";
});

$(document).ready(function () {
    LoginFormValidationSingleton.getInstance();
    signupFormValidationSingleton.getInstance();
})

var LoginFormfv;
var fv1;
var LoginFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('loginwithCred');
        fv1 = FormValidation.formValidation(form, {
            fields: {
                UserName: {
                    validators: {
                        notEmpty: {
                            message: 'User Name required'
                        }
                    }
                },
                Password: {
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
            debugger;
            window.location.href = "/User/ViewPanel";

        });
        return fv1;
    }
    return {
        getInstance: function () {
            if (LoginFormfv) {
                LoginFormfv.destroy();
            }
            LoginFormfv = createInstance();
            return LoginFormfv;
        }
    };
})();

var SignupFormfv;
var fv2;
var signupFormValidationSingleton = (function () {

    function createInstance() {
        let form = document.getElementById('signupCred');
        fv2 = FormValidation.formValidation(form, {
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
        return fv1;
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
    login.UserName = $('#UserName');
    login.Password = $('#Password');
    $.ajax({
        type: "Post",
        url: "/LoginSignup/Signup",
        data: login,
        success: function(){
            
        },
        error: function(){

        }
    });
};

function SignUp() {

};


