
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
    $(':input[type="submit"]').prop('disabled', true);
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

$(document).on('click', '#depositeCoinsByUserIdBtn', function () {
    let coins = $(this).attr('coins');
    let userid = $(this).attr('userId');
    let usernumber = $(this).attr('UserNumber');
    $('#DepositCoinsByUserIdForm .existingCoins').html(coins);
    $('#DepositCoinsByUserIdForm .userNumber').val(usernumber);
    $('#DepositCoinsByUserIdForm').attr('userid', userid);
    DepositeCoinsByFormValidationSingleton.getInstance();
});

$(document).on('click', '#withdrawCoinsByUserIdBtn', function () {
    let coins = $(this).attr('coins');
    let userid = $(this).attr('userId');
    let usernumber = $(this).attr('UserNumber');
    $('#WithdrawCoinsUserIdForm .existingCoins').html(coins);
    $('#WithdrawCoinsUserIdForm .userNumber').val(usernumber);
    $('#WithdrawCoinsUserIdForm').attr('userid', userid);

    WithdrawCoinsByFormValidationSingleton.getInstance();
});


var DepositeCoinsByFormfv;
var DCfv;
var DepositeCoinsByFormValidationSingleton = (function () {
    function createInstance() {
        let form = document.getElementById('DepositCoinsByUserIdForm');
        DCfv = FormValidation.formValidation(form, {
            fields: {
                coins: {
                    validators: {
                        notEmpty: {
                            message: 'Coins Details are required'
                        },
                        callback: {
                            message: 'Requested coins should be more then 100',
                            callback: function (value, validator, $field) {
                                if ($('#DepositCoinsByUserIdForm .coins').val() != '' && $('#DepositCoinsByUserIdForm .coins').val() < parseInt(100))
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
            obj = {
                "UserId": $('#DepositCoinsByUserIdForm').attr('userid'),
                "Coins": $('#DepositCoinsByUserIdForm .Coins').val(),
            };
            depositeCoinsByUserid(obj);
        });
        return DCfv;
    }
    return {
        getInstance: function () {
            if (DepositeCoinsByFormfv) {
                DepositeCoinsByFormfv.destroy();
            }
            DepositeCoinsByFormfv = createInstance();
            return DepositeCoinsByFormfv;
        }
    };
})();

var WithdrawCoinsByFormfv;
var WCfv;
var WithdrawCoinsByFormValidationSingleton = (function () {
    function createInstance() {
        let form = document.getElementById('WithdrawCoinsUserIdForm');
        WCfv = FormValidation.formValidation(form, {
            fields: {
                coins: {
                    validators: {
                        notEmpty: {
                            message: 'Coins Details are required'
                        },
                        callback: {
                            message: 'Requested coins should be more then 100',
                            callback: function (value, validator, $field) {
                                if ($('#WithdrawCoinsUserIdForm .coins').val() != '' && $('#WithdrawCoinsUserIdForm .coins').val() < parseInt(100))
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
            obj = {
                "UserId": $('#WithdrawCoinsUserIdForm').attr('userid'),
                "Coins": $('#WithdrawCoinsUserIdForm .Coins').val(),
            };

            withdrawCoinsByUserid(obj);
        });
        return WCfv;
    }
    return {
        getInstance: function () {
            if (WithdrawCoinsByFormfv) {
                WithdrawCoinsByFormfv.destroy();
            }
            WithdrawCoinsByFormfv = createInstance();
            return WithdrawCoinsByFormfv;
        }
    };
})();


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
            $(':input[type="submit"]').prop('disabled', true);
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
            ToasteRMessage(result,'#AddUserModal');
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
            ToasteRMessage(result,'#AddUserModal');
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


function depositeCoinsByUserid(obj) {
    $.ajax({
        url: '/Coin/DepositeCoinsByUserid',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage(result, '#DepositCoinsByUserIdModal');
        }
    });
}

function withdrawCoinsByUserid(obj) {
    $.ajax({
        url: '/Coin/WithdrawCoinsByuserId',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage(result, '#WithdrawCoinsUserIdModal');
        }
    });
}
