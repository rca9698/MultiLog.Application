
$(document).ready(function () {
    if ($('#listUsers').length) {
        LoadUsers();
    }
});
 
$(document).on('click', '#deleteUserBtn', function () {
    $('#DeleteUserNumber').html($(this).attr('UserNumber'));
    $('#DeleteUserNumber').attr('UserId', ($(this).attr('userId')));
});

$(document).on('click', '#DeleteUserButton', function () {
    DeleteUser($('#DeleteUserNumber').attr('userid'));
});

$(document).on('click', '#LayoutAddUserBtn', function () {
    UserCreationFormValidationSingleton.getInstance();
});

$(document).on('click', '#ListCoinsBtn', function () {
    var obj = {
        UserId: $(this).attr('userId'),
        UserNumber: $(this).attr('UserNumber')
    }
    LoadCoinsHistory(obj);
});

var UserCreationFormfv;
var fv3;
var UserCreationFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddUserModalForm');
        fv3 = FormValidation.formValidation(form, {
            fields: {
                FName: {
                    validators: {
                        notEmpty: {
                            message: 'First Name is required'
                        }
                    }
                },
                LName: {
                    validators: {
                        notEmpty: {
                            message: 'Last Name is required'
                        }
                    }
                },
                MNumber: {
                    validators: {
                        notEmpty: {
                            message: 'Mobile Number is required'
                        }
                    }
                },
                UPassword: {
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
        FirstName: $('#AddUserModalForm .FName').val(),
        LastName: $('#AddUserModalForm .LName').val(),
        MobileNumber: $('#AddUserModalForm .MNumber').val(),
        Password: $('#AddUserModalForm .UPassword').val(),
        EmailId: ""
    }

    $.ajax({
        url: '/User/AddUser',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage('#AddUserModal');
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
            ToasteRMessage('#AddUserModal');
        }
    });
}

function LoadUsers() {
    $.ajax({
        url: '/User/GetUsers',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#listUsers').html(result);
        }
    });
}

function LoadCoinsHistory(obj) {

    $.ajax({
        url: '/Coin/GetTransaction',
        type: 'POST',
        data: obj,
        success: function (result) {
            $('#listUsers').html(result);
            $('#listUsers #coinHistoryUserid').html(obj.UserNumber)
        }
    });
}
