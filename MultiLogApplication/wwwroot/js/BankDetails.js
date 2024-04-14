$(document).ready(function () {
    if ($('#ListBankDetail').length) {
        if ($('#ListBankDetail').attr('ViewType') == 'adminBank') {
            $('.BankDataSwitch').hide();
            LoadAdminBanks();
        }
        else if ($('#ListBankDetail').attr('ViewType') == 'adminUpi') {
            $('.BankDataSwitch').hide();
            LoadAdminUpi();
        }
        else if ($('#ListBankDetail').attr('ViewType') == 'adminQR') {
            $('.BankDataSwitch').hide();
            LoadAdminQR();
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

$(document).on('click', '#AddAdminUpiDetailModalBtn', function () {
    AddAdminUpiFormValidationSingleton.getInstance();
});

$(document).on('click', '#AddAdminQRDetailModalBtn', function () {
    AddAdminQRFormValidationSingleton.getInstance();
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

$(document).on('click', '.MakeAdminBankDefault', function () {
    $('.MakeAdminBankDetailDefaultBtn').attr('bankId', $(this).attr('bankId'));
});

$(document).on('click', '.MakeAdminUpiDefault', function () {
    $('.MakeAdminUpiDetailDefaultBtn').attr('upiId', $(this).attr('upiId'));
});

$(document).on('click', '.MakeAdminUpiDetailDefaultBtn', function () {

    $.ajax({
        url: '/BankAccount/SetDefaultAdminUpiAccount',
        type: 'POST',
        data: { UpiId: $(this).attr('upiId') },
        success: function (result) {
            ToasteRMessage('');
        }
    });
});

$(document).on('click', '.DeleteAdminUpiAccount', function () {
    $('.DeleteAdminUpiDetailsBtn').attr('upiId', $(this).attr('upiId'));
});

$(document).on('click', '.DeleteAdminUpiDetailsBtn', function () {

    $.ajax({
        url: '/BankAccount/DeleteAdminUpiAccount',
        type: 'POST',
        data: { UpiId: $(this).attr('upiId') },
        success: function (result) {
            ToasteRMessage('');
        }
    });
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
            ToasteRMessage('');
        }
    });
});

$(document).on('click', '.MakeAdminBankDetailDefaultBtn', function () {
   
    $.ajax({
        url: '/BankAccount/SetDefaultAdminBankAccount',
        type: 'POST',
        data: { BankDetailID : $(this).attr('bankId') },
        success: function (result) {
            ToasteRMessage('');
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
            ToasteRMessage('');
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


var AddAdminUpiDetailFormfv;
var upifv;
var AddAdminUpiFormValidationSingleton = (function () {
    function createInstance() {
        let form = document.getElementById('AddAdminUpiDetailForm');
        upifv = FormValidation.formValidation(form, {
            fields: {
                UPI: {
                    validators: {
                        notEmpty: {
                            message: 'UPI is required'
                        }
                    }
                },
                UserName: {
                    validators: {
                        notEmpty: {
                            message: 'User Name is required'
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
            AddAdminUpiAccount();
        });
        return upifv;
    }
    return {
        getInstance: function () {
            if (AddAdminUpiDetailFormfv) {
                AddAdminUpiDetailFormfv.destroy();
            }
            AddAdminUpiDetailFormfv = createInstance();
            return AddAdminUpiDetailFormfv;
        }
    };
})();


var AddAdminQRDetailFormfv;
var QRfv;
var AddAdminQRFormValidationSingleton = (function () {
    function createInstance() {
        let form = document.getElementById('AddAdminQRDetailForm');
        QRfv = FormValidation.formValidation(form, {
            fields: {
                QRFile: {
                    validators: {
                        callback: {
                            message: 'Please upload QR Code',
                            callback: function (value, validator, $field) {
                                if ($("#AddAdminQRDetailForm .QRFile")[0].files[0] == undefined)
                                    return false;
                                return true;
                            }
                        }
                    }
                },
                QRUserName: {
                    validators: {
                        notEmpty: {
                            message: 'User Name is required'
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
            AddAdminQRAccount();
        });
        return QRfv;
    }
    return {
        getInstance: function () {
            if (AddAdminQRDetailFormfv) {
                AddAdminQRDetailFormfv.destroy();
            }
            AddAdminQRDetailFormfv = createInstance();
            return AddAdminQRDetailFormfv;
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
            ToasteRMessage('#AddBankDetailForm');
        }
    });
}

function AddAdminBankAccount() {
    var obj = {
        BankName: $('#AddAdminBankDetailForm .BName').val(),
        AccountHolderName: $('#AddAdminBankDetailForm .AHName').val(),
        AccountNumber: $('#AddAdminBankDetailForm .ANumber').val(),
        IFSCCode: $('#AddAdminBankDetailForm .IFSCCode').val()
    }

    $.ajax({
        url: '/BankAccount/AddUpdateAdminBankAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage('#AddAdminBankDetailForm');
        }
    });
}

function AddAdminUpiAccount() {
    var obj = {
        UpiId: $('#AddAdminUpiDetailForm .UPI').val(),
        UserName: $('#AddAdminUpiDetailForm .UserName').val()
    }

    $.ajax({
        url: '/BankAccount/AddUpdateAdminUpiAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage('#AddAdminUpiDetailForm');
        }
    });
}

function AddAdminQRAccount() {
    var formData = new FormData();
    formData.append("UserName", $('#AddAdminQRDetailForm .QRUserName').val());
    formData.append("File", $("#AddAdminQRDetailForm .QRFile")[0].files[0]);

    $.ajax({
        url: '/BankAccount/AddQRCode',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            ToasteRMessage('#AddAdminQRDetailForm');
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

function LoadAdminQR() {
    $.ajax({
        url: '/BankAccount/AdminQRDetails',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

function LoadAdminUpi() {
    $.ajax({
        url: '/BankAccount/AdminUpiAccounts',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

// End Function Region