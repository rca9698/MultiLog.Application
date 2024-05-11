

$(document).on('click', '#PasswordChangeModalBtn', function () {
    ChangePasswordFormValidationSingleton.getInstance();
});

var ChangeIDPasswordFormfv;
var CPfv1;
var ChangePasswordFormValidationSingleton = (function () {
    function createInstance() {
        let form = document.getElementById('PasswordChangeForm');
        CPfv1 = FormValidation.formValidation(form, {
            fields: {
                CPassword: {
                    validators: {
                        notEmpty: {
                            message: 'Current Password is required'
                        }
                    }
                },
                NPassword: {
                    validators: {
                        notEmpty: {
                            message: 'New Password is required'
                        }
                    }
                },
                CNPassword: {
                    validators: {
                        notEmpty: {
                            message: 'Confirm Password is required'
                        },
                        callback: {
                            message: 'Entered password should be same as New Password!',
                            callback: function (value, validator, $field) {
                                if ($("#PasswordChangeForm .NPassword").val() != $("#PasswordChangeForm .CNPassword").val())
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
            $(':input[type="submit"]').prop('disabled', true);
            ChangePassword();
        });
        return CPfv1;
    }
    return {
        getInstance: function () {
            if (ChangeIDPasswordFormfv) {
                ChangeIDPasswordFormfv.destroy();
            }
            ChangeIDPasswordFormfv = createInstance();
            return ChangeIDPasswordFormfv;
        }
    };
})();


function ChangePassword() {
    var obj = {
        CurrentPassword: $('#PasswordChangeForm .CPassword').val(),
        ChangePassword: $('#PasswordChangeForm .NPassword').val(),
        ConfirmPassword: $('#PasswordChangeForm .CNPassword').val(),
    }

    $.ajax({
        url: '/Profile/ChangePassword',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage(result, '#PasswordChangeForm');
        }
    });
}

