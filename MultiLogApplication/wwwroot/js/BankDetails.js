$(document).ready(function () {
    if ($('#ListBankDetail').length) {
        LoadActiveBanks();
    }
});

//Start Click Event
$(document).on('click', '#AddBankDetailModalBtn', function () {
    AddBankDetailFormValidationSingleton.getInstance();
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


//End Click Event


//Start Form Validation
var AddBankDetailFormfv;
var fv1;
var AddBankDetailFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddBankDetailForm');
        fv1 = FormValidation.formValidation(form, {
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
        return fv1;
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
//End Form Validation


//Start Function Region
function AddBankAccount() {
    var obj = {
        BankName: $('#AddBankDetailForm #BName').val(),
        AccountHolderName: $('#AddBankDetailForm #AHName').val(),
        AccountNumber: $('#AddBankDetailForm #ANumber').val(),
        IFSCCode: $('#AddBankDetailForm #IFSCCode').val(),
    }

    $.ajax({
        url: '/BankAccount/AddBankAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#AddBankDetailForm .close').trigger('click');
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


// End Function Region