$(document).ready(function () {
    if ($('#ListCoinsDetail').length) {
        if ($('#ListCoinsDetail').attr('ViewType') == 'Deposite') {
            GetDepositCoinsRequest();
        }
        else if ($('#ListCoinsDetail').attr('ViewType') == 'Withdraw') {
            GetWithdrawCoinsRequest();
        }
        else {
            LoadCoinsHistory();
        }
    }
});

$(document).on('click', '.PaymentModeTypes', function () {
    $('#PaymentModeList').hide();
    $('.PaymentModeTypesDetail').hide();
    $('.PaymentModeTypesDetailList').show();
    $('#proofUpload').show();
    var id = $(this).attr('id');
    $('#' + id + 'Detail').show();
});

$(document).on('click', '#closePaymentModesModal', function () {
    $('#PaymentModesModal').modal('hide');
});

$(document).on('click', '#depositeCoinsBtn', function () {
    $('#DepositCoinsForm #userNumber').val($(this).attr('UserNumber'));
    $('#DepositCoinsForm').attr('UserId', ($(this).attr('userId')));
    $('#DepositCoinsForm').attr('coinRequestID', ($(this).attr('coinRequestId')));
    AddCoinsFormValidationSingleton.getInstance();
});

$(document).on('click', '#withdrawCoinsBtn', function () {
    $('#WithdrawCoinsForm #userNumber').val($(this).attr('UserNumber'));
    $('#WithdrawCoinsForm').attr('UserId', ($(this).attr('userId')));
    $('#WithdrawCoinsForm').attr('coinRequestID', ($(this).attr('coinRequestId')));
    WithdrawCoinsFormValidationSingleton.getInstance();
});

$(document).on('click', '#DepositCoinsRequestModalBtn', function () {
    AddCoinsRequestFormValidationSingleton.getInstance();
});

$(document).on('click', '#DesitCoins', function () {

    if ($("#PaymentModesModal #files")[0].files[0] == undefined) {
        toastr.warning('Please upload payment proof!!!');
        return;
    }

    var formData = new FormData();
    formData.append("Coins", $('#DepositCoinsRequestModal #coins').val());
    formData.append("File", $("#PaymentModesModal #files")[0].files[0]);

    $.ajax({
        url: '/Coin/AddCoinsRequest',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#closePaymentModesModal').trigger('click');
            }
        }
    });
});

var AddCoinsFormfv;
var fv1;
var AddCoinsFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('DepositCoinsForm');
        fv1 = FormValidation.formValidation(form, {
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
            DepositCoins();
        });
        return fv1;
    }
    return {
        getInstance: function () {
            if (AddCoinsFormfv) {
                AddCoinsFormfv.destroy();
            }
            AddCoinsFormfv = createInstance();
            return AddCoinsFormfv;
        }
    };
})();

function DepositCoins() {
    var obj = {
        UserId: $('#DepositCoinsForm').attr('userId'),
        Coins: $('#DepositCoinsForm #Coins').val(),
        CoinRequestID: $('#DepositCoinsForm').attr('coinRequestID')
    }

    $.ajax({
        url: '/Coin/AddCoins',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#DepositCoinsModal .close').trigger('click');
            }
        }
    });
}


var WithdrawCoinsFormfv;
var fv2;
var WithdrawCoinsFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('WithdrawCoinsForm');
        fv2 = FormValidation.formValidation(form, {
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
            WithdrawCoins();
        });
        return fv2;
    }
    return {
        getInstance: function () {
            if (WithdrawCoinsFormfv) {
                WithdrawCoinsFormfv.destroy();
            }
            WithdrawCoinsFormfv = createInstance();
            return WithdrawCoinsFormfv;
        }
    };
})();

function WithdrawCoins() {
    var obj = {
        UserId: $('#WithdrawCoinsForm').attr('userId'),
        Coins: $('#WithdrawCoinsForm #Coins').val(),
        CoinRequestID: $('#WithdrawCoinsForm').attr('coinRequestID')
    }

    $.ajax({
        url: '/Coin/DeleteCoins',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#WithdrawCoinsModal .close').trigger('click');
            }
        }
    });
}

function GetDepositCoinsRequest() {
    $.ajax({
        url: '/Coin/GetDepositCoinsRequest',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListCoinsDetail').html(result);
        }
    });
}

function GetWithdrawCoinsRequest() {
    $.ajax({
        url: '/Coin/GetWithdrawCoinsRequest',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListCoinsDetail').html(result);
        }
    });
}

var AddCoinsRequestFormfv;
var fv3;
var AddCoinsRequestFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('DepositCoinsRequestModalForm');
        fv3 = FormValidation.formValidation(form, {
            fields: {
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
            $('#depositAmount').html($('#DepositCoinsRequestModalForm #coins').val());
            $('#PaymentModeList').show()
            $('#PaymentModesModal').modal('show');
            $('.PaymentModeTypesDetailList').hide();
            $('#DepositCoinsRequestModal .close').trigger('click');
        });
        return fv3;
    }
    return {
        getInstance: function () {
            if (AddCoinsRequestFormfv) {
                AddCoinsRequestFormfv.destroy();
            }
            AddCoinsRequestFormfv = createInstance();
            return AddCoinsRequestFormfv;
        }
    };
})();





