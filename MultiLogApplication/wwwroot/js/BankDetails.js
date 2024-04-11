$(document).ready(function () {
    if ($('#ListBankDetail').length) {
        if ($('#ListBankDetail').attr('ViewType') == 'admin') {
            $('.BankDataSwitch').hide();
            LoadAdminBanks();
        }
        else {
            $('.BankDataSwitch').show();
            LoadActiveBanks();
        }
    }
});

//Start Click Event
$(document).on('click', '#AddAdminBankDetailModalBtn', function () {
    AddAdminBankDetailFormValidationSingleton.getInstance();
});
$(document).on('click', '#AddBankDetailModalBtn1', function () {
    AddBankDetailFormValidationSingleton.getInstance();
});
$(document).on('click', '.BankDataSwitch .tabSelection', function () {
    $('.BankDataSwitch .tabSelection').removeClass('active');
    $(this).addClass('active');

    if ($(this).attr('idType') == 'bankHistory') {
        LoadAccountsHistory();
    } else if ($(this).attr('idType') == 'deletedBank') {
        LoadDeletedBanks();
    } else if ($(this).attr('idType') == 'activeBank') {
        LoadActiveBanks();
    }
});

$(document).on('click', '.DeleteAdminBankAccount', function () {
    $('.DeleteAdminBankDetails').attr('bankId', $(this).attr('bankId'));
});

$(document).on('click', '.DeleteAdminBankDetails', function () {
    let obj = {
        BankId: $(this).attr('bankId')
    };
    $.ajax({
        url: '/BankAccount/DeleteAdminBankAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                location.reload();
            }
        }
    });
});


$(document).on('click', '.DeleteBankAccount', function () {
    $('.DeleteBankDetails').attr('bankId', $(this).attr('bankId'));
});

$(document).on('click', '.DeleteBankDetails', function () {
    let obj = {
        BankId: $(this).attr('bankId')
    };
    $.ajax({
        url: '/BankAccount/DeleteBankAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                location.reload();
            }
        }
    });
});



//End Click Event


//Start Form Validation
var AddBankDetailFormfv;
var bkfv1;
var AddBankDetailFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddBankDetailForm');
        bkfv1 = FormValidation.formValidation(form, {
            fields: {
                BName: {
                    validators: {
                        notEmpty: {
                            message: 'Bank Name is required'
                        }
                    }
                },
                AHName: {
                    validators: {
                        notEmpty: {
                            message: 'Account Holder Name is required'
                        }
                    }
                },
                ANumber: {
                    validators: {
                        notEmpty: {
                            message: 'Account Number is required'
                        }
                    }
                },
                IFSCCode: {
                    validators: {
                        notEmpty: {
                            message: 'IFSC Code is required'
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
            AddBankAccount();
        });
        return bkfv1;
    }
    return {
        getInstance: function () {
            if (AddBankDetailFormfv) {
                AddBankDetailFormfv.destroy();
            }
            AddBankDetailFormfv = createInstance();
            return AddBankDetailFormfv;
        }
    };
})();

var AddAdminBankDetailFormfv;
var bkfv2;
var AddAdminBankDetailFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddAdminBankDetailForm');
        bkfv2 = FormValidation.formValidation(form, {
            fields: {
                BName: {
                    validators: {
                        notEmpty: {
                            message: 'Bank Name is required'
                        }
                    }
                },
                AHName: {
                    validators: {
                        notEmpty: {
                            message: 'Account Holder Name is required'
                        }
                    }
                },
                ANumber: {
                    validators: {
                        notEmpty: {
                            message: 'Account Number is required'
                        }
                    }
                },
                IFSCCode: {
                    validators: {
                        notEmpty: {
                            message: 'IFSC Code is required'
                        }
                    }
                },
                UpiId: {
                    validators: {
                        notEmpty: {
                            message: 'UPI is required'
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
            AddAdminBankAccount();
        });
        return bkfv2;
    }
    return {
        getInstance: function () {
            if (AddAdminBankDetailFormfv) {
                AddAdminBankDetailFormfv.destroy();
            }
            AddAdminBankDetailFormfv = createInstance();
            return AddAdminBankDetailFormfv;
        }
    };
})();
//End Form Validation


//Start Function Region
function AddBankAccount() {
    var obj = {
        BankName: $('#AddBankDetailForm .BName').val(),
        AccountHolderName: $('#AddBankDetailForm .AHName').val(),
        AccountNumber: $('#AddBankDetailForm .ANumber').val(),
        IFSCCode: $('#AddBankDetailForm .IFSCCode').val(),
    }

    $.ajax({
        url: '/BankAccount/AddBankAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#AddBankDetailForm .close').trigger('click');
                location.reload();
            }
        }
    });
}

function AddAdminBankAccount() {
    var obj = {
        BankName: $('#AddAdminBankDetailForm .BName').val(),
        AccountHolderName: $('#AddAdminBankDetailForm .AHName').val(),
        AccountNumber: $('#AddAdminBankDetailForm .ANumber').val(),
        IFSCCode: $('#AddAdminBankDetailForm .IFSCCode').val(),
        UpiId: $('#AddAdminBankDetailForm .UpiId').val()
    }

    $.ajax({
        url: '/BankAccount/AddUpdateAdminBankAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#AddAdminBankDetailForm .close').trigger('click');
            }
        }
    });
}

function LoadActiveBanks() {
    $.ajax({
        url: '/BankAccount/ActiveBankAccounts',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

function LoadDeletedBanks() {
    $.ajax({
        url: '/BankAccount/DeletedBankAccounts',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

function LoadAccountsHistory() {
    $.ajax({
        url: '/BankAccount/BankAccountsHistory',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

function LoadAdminBanks() {
    $.ajax({
        url: '/BankAccount/AdminBankAccounts',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

// End Function Region