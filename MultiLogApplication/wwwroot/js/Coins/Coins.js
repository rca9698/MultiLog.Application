$(document).ready(function () {
    if ($('#ListCoinsDetail').length) {
        if ($('#ListCoinsDetail').attr('ViewType') == 'Deposite') {
            GetDepositCoinsRequest();
        }
        else if ($('#ListCoinsDetail').attr('ViewType') == 'Withdraw') {
            GetWithdrawCoinsRequest();
        }
        else if ($('#ListCoinsDetail').attr('ViewType') == 'DepositeToSite') {
            GetDepositCoinsToAccountRequest();
        }
        else if ($('#ListCoinsDetail').attr('ViewType') == 'WithdrawFromSite') {
            GetWithdrawCoinsToAccountRequest();
        }
        else {
            LoadCoinsHistory();
        }
    }
});

//Start click event region
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

$(document).on('click', '#WithdrawCoinsRequestModalBtn', function () {

    var obj = {
        UserId: $(this).attr('userId'),
    }

    $.ajax({
        url: '/BankAccount/GetBankAccounts',
        type: 'POST',
        data: obj,
        success: function (result) {
            bankDropDown(result);
            SetBankdetails(result);
        }
    });

    WithDrawCoinsRequestFormValidationSingleton.getInstance();
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


$(document).on('click', '#DepositeCoinsToAccountBtn', function () {
    let coinsRequestID = $(this).attr('coinRequestID');
    $.ajax({
        url: '/Coin/GetCoinsFromAccountRequestById',
        type: 'POST',
        data: { CoinsRequestId: coinsRequestID, CoinType: 0 },
        success: function (result) {
            if (result.returnStatus == 1) {
                $('#DepositeCoinsToAccountModal').attr('coinsRequestId', result.returnVal.coinsRequestId);
                $('#DepositeCoinsToAccountModal').attr('userId', result.returnVal.userId);
                $('#DepositeCoinsToAccountModal').attr('siteId', result.returnVal.siteId);
                $('#DepositeCoinsToAccountModal .siteName').html(result.returnVal.siteName);
                $('#DepositeCoinsToAccountModal .siteURL').html(result.returnVal.siteURL);
                $('#DepositeCoinsToAccountModal .siteIcon').attr('siteIcon', '');
            }
          }
    });
    DepositeCoinsToAccountFormValidationSingleton.getInstance();

})

$(document).on('click', '#withdrawCoinsFromAccountBtn', function () {
    let coinsRequestID = $(this).attr('coinRequestID');
    $.ajax({
        url: '/Coin/GetCoinsFromAccountRequestById',
        type: 'POST',
        data: { CoinsRequestId: coinsRequestID, CoinType: 1 },
        success: function (result) {
            if (result.returnStatus == 1) {
                $('#withdrawCoinsFromAccountModal').attr('coinsRequestId', result.returnVal.coinsRequestId);
                $('#withdrawCoinsFromAccountModal').attr('userId', result.returnVal.userId);
                $('#withdrawCoinsFromAccountModal').attr('siteId', result.returnVal.siteId);
                $('#withdrawCoinsFromAccountModal .siteName').html(result.returnVal.siteName);
                $('#withdrawCoinsFromAccountModal .siteURL').html(result.returnVal.siteURL);
                $('#withdrawCoinsFromAccountModal .siteIcon').attr('siteIcon', '');
            }
        }
    });
    WithdrawCoinsFromAccountFormValidationSingleton.getInstance();
})


//End click event region

//Start function region

function DepositCoins() {
    var obj = {
        UserId: $('#DepositCoinsForm').attr('userId'),
        Coins: $('#DepositCoinsForm #Coins').val(),
        CoinsRequestId: $('#DepositCoinsForm').attr('coinRequestID')
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

function WithdrawCoins() {
    var obj = {
        UserId: $('#WithdrawCoinsForm').attr('userId'),
        Coins: $('#WithdrawCoinsForm #Coins').val(),
        CoinsRequestId: $('#WithdrawCoinsForm').attr('coinRequestID')
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

function GetDepositCoinsToAccountRequest() {
    $.ajax({
        url: '/Coin/GetDepositCoinsToAccountRequest',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListCoinsDetail').html(result);
        }
    });
}

function GetWithdrawCoinsToAccountRequest() {
    $.ajax({
        url: '/Coin/GetWithdrawCoinsFromAccountRequest',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListCoinsDetail').html(result);
        }
    });
}

function bankDropDown(result) {
    $('.bankListDropdown select').empty();
    var dropDownData = `<option disabled="" selected=""> CHANGE BANK </option>`;
    if (result.returnList) {
        result.returnList.forEach(function (item) {
            dropDownData += `<option value="${item.bankAccountDetailID}">${item.bankName}</option>`
        });
    }

    if (result.returnVal) {
        $('.bankListDropdown option').attr('selected', false);
        $('.bankListDropdown option[value=' + result.returnVal.bankAccountDetailID + ']').attr('selected', true);
    }

    $('.bankListDropdown select').append(dropDownData);
}

function SetBankdetails(result) {
    $('#BankTrDetail').empty();
    if (result.returnVal) {
        $('#WithDrawCoinsBankDetail').html(
            `<div class="d-flex flex-column col-12">
          <div class="row col-12">
              <div class="col-6"> Bank Name </div>
              <div class="col-6" style="text-align: end;"> ${result.returnVal.bankName} </div>
          </div>
          <div class="nav-item-divider-small"></div>
          <div class="row col-12">
              <div class="col-6"> Account Holder Name </div>
              <div class="col-6" style="text-align: end;"> ${result.returnVal.accountHolderName} </div>
          </div>
          <div class="nav-item-divider-small"></div>
          <div class="row col-12">
              <div class="col-6"> Account Number </div>
              <div class="col-6" style="text-align: end;"> ${result.returnVal.accountNumber} </div>
          </div>
          <div class="nav-item-divider-small"></div>
          <div class="row col-12">
              <div class="col-6"> IFSC Code </div>
              <div class="col-6" style="text-align: end;"> ${result.returnVal.ifscCode}  </div>
          </div>
          <div class="nav-item-divider-small"></div>
      </div>`)
    }
}

function ChangeWithDrawBank() {

    let selectedValue = $('.bankListDropdown').find(":selected").val()
    SetDefaultBank(selectedValue);
}

function SetDefaultBank(selectedValue) {
    $.ajax({
        url: '/BankAccount/SetDefaultBankAccount',
        type: 'POST',
        data: { BankDetailID: selectedValue },
        success: function (result) {
            SetBankdetails(result);
        }
    });
}

function WithDrawCoinsRequest() {
    var obj = {
        UserId: $('#WithdrawCoinsRequestModalForm').attr('userId'),
        Coins: $('#WithdrawCoinsRequestModalForm #coins').val()
    }

    $.ajax({
        url: '/Coin/WithDrawCoinsRequest',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#WithdrawCoinsRequestModal .close').trigger('click');
            }
        }
    });
}

function LoadAdminBankDetails() {
    $.ajax({
        url: '/BankAccount/GetBankUPIDetails',
        type: 'POST',
        data: '',
        success: function (result) {
            if (result.returnStatus == 1) {
                $('.PaymentModeTypesDetailList #BankTrDetail .bankName').html(result.returnVal.bankName);
                $('.PaymentModeTypesDetailList #BankTrDetail .bankNameCopy').attr('copydata', result.returnVal.bankName);
                $('.PaymentModeTypesDetailList #BankTrDetail .accountHolderName').html(result.returnVal.accountHolderName);
                $('.PaymentModeTypesDetailList #BankTrDetail .accountHolderNameCopy').attr('copydata', result.returnVal.accountHolderName);
                $('.PaymentModeTypesDetailList #BankTrDetail .accountNumber').html(result.returnVal.accountNumber);
                $('.PaymentModeTypesDetailList #BankTrDetail .accountNumberCopy').attr('copydata', result.returnVal.accountNumber);
                $('.PaymentModeTypesDetailList #BankTrDetail .IFSCCode').html(result.returnVal.ifscCode);
                $('.PaymentModeTypesDetailList #BankTrDetail .IFSCCodeCopy').attr('copydata', result.returnVal.ifscCode);
                $('.PaymentModeTypesDetailList #PhonePeDetail .upiCode').html(result.returnVal.upiId);
                $('.PaymentModeTypesDetailList #PhonePeDetail .upiCodeCopy').attr('copydata', result.returnVal.upiId);
            }
        }
    });
}

function DepositeCoinsToAccount() {
    var obj = {
        UserId: $('#DepositeCoinsToAccountModal').attr('userId'),
        SiteId: $('#DepositeCoinsToAccountModal').attr('siteId'),
        Coins: $('#DepositeCoinsToAccountModal .Coins').val(),
        CoinType: 0,
        CoinsRequestId: $('#DepositeCoinsToAccountModal').attr('coinsRequestId')
    }

    $.ajax({
        url: '/Coin/UpdateCoinsToAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#DepositeCoinsToAccountModal .close').trigger('click');
                GetDepositCoinsToAccountRequest();
            }
        }
    });
}

function WithdrawCoinsFromAccount() {
    var obj = {
        UserId: $('#withdrawCoinsFromAccountModal').attr('userId'),
        SiteId: $('#withdrawCoinsFromAccountModal').attr('siteId'),
        Coins: $('#withdrawCoinsFromAccountModal .Coins').val(),
        CoinType: 1,
        CoinsRequestId: $('#withdrawCoinsFromAccountModal').attr('coinsRequestId')
    }

    $.ajax({
        url: '/Coin/UpdateCoinsToAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#withdrawCoinsFromAccountModal .close').trigger('click');
                GetWithdrawCoinsToAccountRequest();
            }
        }
    });
}
//End function region

//Start form region

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
            LoadAdminBankDetails();
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

var WithDrawCoinsRequestFormfv;
var fv4;
var WithDrawCoinsRequestFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('WithdrawCoinsRequestModalForm');
        fv4 = FormValidation.formValidation(form, {
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
            WithDrawCoinsRequest();
        });
        return fv4;
    }
    return {
        getInstance: function () {
            if (WithDrawCoinsRequestFormfv) {
                WithDrawCoinsRequestFormfv.destroy();
            }
            WithDrawCoinsRequestFormfv = createInstance();
            return WithDrawCoinsRequestFormfv;
        }
    };
})();

var DepositeCoinsToAccountFormfv;
var fv5;
var DepositeCoinsToAccountFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('DepositeCoinsToAccountModalForm');
        fv5 = FormValidation.formValidation(form, {
            fields: {
                Coins: {
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
            DepositeCoinsToAccount();
        });
        return fv5;
    }
    return {
        getInstance: function () {
            if (DepositeCoinsToAccountFormfv) {
                DepositeCoinsToAccountFormfv.destroy();
            }
            DepositeCoinsToAccountFormfv = createInstance();
            return DepositeCoinsToAccountFormfv;
        }
    };
})();

var WithdrawCoinsFromAccountFormfv;
var fv6;
var WithdrawCoinsFromAccountFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('WithDrawCoinsFromAccountModalForm');
        fv6 = FormValidation.formValidation(form, {
            fields: {
                Coins: {
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
            WithdrawCoinsFromAccount();
        });
        return fv6;
    }
    return {
        getInstance: function () {
            if (WithdrawCoinsFromAccountFormfv) {
                WithdrawCoinsFromAccountFormfv.destroy();
            }
            WithdrawCoinsFromAccountFormfv = createInstance();
            return WithdrawCoinsFromAccountFormfv;
        }
    };
})();


//End form area

