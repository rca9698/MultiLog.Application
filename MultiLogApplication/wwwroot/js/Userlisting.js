$(document).on('click', '#deleteUserBtn', function () {
    $('#DeleteUserNumber').html($(this).attr('data-Number'));
    $('#DeleteUserNumber').attr('UserId', ($(this).attr('data-Number')));
});

$(document).on('click', '#DeleteUserButton', function () {
    DeleteUser($('#DeleteUserNumber').attr('userid'));
});

$(document).on('click', '#LayoutAddUserBtn', function () {
    UserCreationFormValidationSingleton.getInstance();
});

var UserCreationFormfv;
var fv3;
var UserCreationFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddUserModalForm');
        fv3 = FormValidation.formValidation(form, {
            fields: {
                userNumber: {
                    validators: {
                        notEmpty: {
                            message: 'User Number is required'
                        }
                    }
                },
                coins: {
                    validators: {
                        notEmpty: {
                            message: 'Coins Details are required'
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
            AddUser();
        });
        return fv3;
    }
    return {
        getInstance: function () {
            if (UserCreationFormfv) {
                UserCreationFormfv.destroy();
            }
            UserCreationFormfv = createInstance();
            return UserCreationFormfv;
        }
    };
})();

function AddUser() {
    var obj = {
        FirstName: $('#AddUserModalForm #FName').val(),
        LastName: $('#AddUserModalForm #LName').val(),
        MobileNumber: $('#AddUserModalForm #MNumber').val(),
        Password: $('#AddUserModalForm #UPassword').val(),
        EmailId: ""
    }

    $.ajax({
        url: '/User/AddUser',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                alert(result.returnMessage);
            }
        }
    });
}

function DeleteUser(id) {
    var obj = {
        UserId: id
    }

    $.ajax({
        url: '/User/DeleteUser',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                alert(result.returnMessage);
            }
        }
    });
}