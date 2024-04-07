
$(document).ready(function () {
    if ($('#accountListing').attr('ViewType') == 'Accounts') {
        AccountList();
    }
    else if ($('#accountListing').attr('ViewType') == 'RejectedAcRequests') {
        RejectedRequestLists();
    }
    else {
        AccountRequestList();
    }
});


$(document).on('click', '#DeleteAccountRequestList', function () {
    var userId = $(this).attr('userId');
    var siteId = $(this).attr('siteId');
    $('#DeleteAccountRequest').attr('userId', userId);
    $('#DeleteAccountRequest').attr('siteId', siteId);
});

$(document).on('click', '#AddAccontRequest', function () {
    var userId = $(this).attr('userId');
    var siteId = $(this).attr('siteId');
    $('#AddAccountRequest').attr('userId', userId);
    $('#AddAccountRequest').attr('siteId', siteId);
});

$(document).on('click', '#AddAccount', function () {
    var userId = $(this).attr('userId');
    var siteId = $(this).attr('siteId');
    var account = {
        SiteID: siteId,
        UserId: userId,
        UserName: $('#UserName').val(),
        Password: $('#Password').val()
    }
    $.ajax({
        type: "Post",
        url: "/Account/AddAccount",
        data: account,
        success: function (result) {
            if (result.returnStatus == 1) {

            }
            else {

            }
        },
        error: function () {

        }
    })
});

$(document).on('click', '#DeleteAccountRequest', function () {
    var userId = $(this).attr('userId');
    var siteId = $(this).attr('siteId');
    var account = {
        SiteID: siteId,
        UserId: userId
    }
    $.ajax({
        type: "Post",
        url: "/Account/DeleteAccount",
        data: account,
        success: function(result) {
            if (result.returnStatus == 1) {

            }
            else {

            }
        },
        error: function() {
            
        }
    })
});

$(document).on('click', '#CreateIDRequestModalBtn', function () {
    let siteId = $(this).attr('siteId');
    $('#CreateIDRequestModal').attr('siteId', siteId);

    let Name = $('.site_' + siteId + ' .siteName').html();
    let URL = $('.site_' + siteId + ' .siteURL').html();
    let iconSrc = $('.site_' + siteId + ' .siteIcon').attr('src');

    $('#CreateIDRequestModal').attr('siteId', siteId);
    $('#CreateIDRequestModal .siteName').html(Name);
    $('#CreateIDRequestModal .siteURL').html(URL);
    $('#CreateIDRequestModal .siteIcon').attr('src', iconSrc);

    CreateIDRequestFormValidationSingleton.getInstance();
});


var CreateIDRequestFormfv;
var fv1;
var CreateIDRequestFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('CreateIDRequestModalForm');
        fv1 = FormValidation.formValidation(form, {
            fields: {
                Username: {
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
            AddAccountRequest();
        });
        return fv1;
    }
    return {
        getInstance: function () {
            if (CreateIDRequestFormfv) {
                CreateIDRequestFormfv.destroy();
            }
            CreateIDRequestFormfv = createInstance();
            return CreateIDRequestFormfv;
        }
    };
})();

function AddAccountRequest() {
    debugger;
    var obj = {
        UserName: $('#Username').val(),
        SiteId: $('#CreateIDRequestModal').attr('SiteId')
    }

    $.ajax({
        url: '/Account/AddAccountRequest',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#CreateIDRequestModal .close').trigger('click');
            }
        }
    });
}

$(document).on('click', '#CreateIDModalBtn', function () {
    let AccountRequestId = $(this).attr('accountRequestId');
    let iconPath = $(this).attr('iconpath'); 

    $.ajax({
        url: '/Account/AccountRequestDetails',
        type: 'POST',
        data: { AccountRequestId: AccountRequestId },
        success: function (result) {
            $('#CreateIDModalForm .logoicon').attr('src', iconPath);
            $('#CreateIDModalForm .SiteName').html(result.returnVal.siteName);
            $('#CreateIDModalForm .SiteURL').html(result.returnVal.siteURL);
            $('#CreateIDModalForm').attr('accountRequestId', AccountRequestId);

            CreateIDFormValidationSingleton.getInstance();
        }
    });
});


var CreateIDFormfv;
var fv2;
var CreateIDFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('CreateIDModalForm');
        fv2 = FormValidation.formValidation(form, {
            fields: {
                UserName: {
                    validators: {
                        notEmpty: {
                            message: 'User Name is required'
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
            AddAccount();
        });
        return fv2;
    }
    return {
        getInstance: function () {
            if (CreateIDFormfv) {
                CreateIDFormfv.destroy();
            }
            CreateIDFormfv = createInstance();
            return CreateIDFormfv;
        }
    };
})();

function AddAccount() {
    var obj = {
        AccountRequestId: $('#CreateIDModalForm').attr('accountRequestId'),
        UserName: $('#CreateIDModalForm #Username').val(),
        Password: $('#CreateIDModalForm #password').val()
    }

    $.ajax({
        url: '/Account/AddAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#CreateIDModal .close').trigger('click');
            }
        }
    });
}

function AccountRequestList() {
    $.ajax({
        url: '/Account/AccountRequestList',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#accountListing').html(result);
        }
    });
}

function AccountList() {
    $.ajax({
        url: '/Account/AccountList',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#accountListing').html(result);
        }
    });
}

function RejectedRequestLists() {
    $.ajax({
        url: '/Account/RejectedRequestLists',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#accountListing').html(result);
        }
    });
}

