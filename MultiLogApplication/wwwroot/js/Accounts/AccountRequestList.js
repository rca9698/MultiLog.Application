
$(document).ready(function () {
    if ($('#accountListing').attr('ViewType') == 'Accounts') {
        AccountList();
    }
    else if ($('#accountListing').attr('ViewType') == 'RejectedAcRequests') {
        RejectedRequestLists();
    }
    else if ($('#accountListing').attr('ViewType') == 'AcRequest') {
        AccountRequestList();
    }
});

//Start Click event
$(document).on('click', '#DeleteAccountRequestList', function () {
    var accountrequestid = $(this).attr('accountrequestid');
    $('.DeleteAccountRequestBtn').attr('accountrequestid', accountrequestid);
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
            ToasteRMessage(result,'#AddAccount')
        },
        error: function () {

        }
    })
});

$(document).on('click', '.DeleteAccountRequestBtn', function () {
    $(':input[type="submit"]').prop('disabled', true);
    var accountrequestid = $(this).attr('accountrequestid');
    var account = {
        AccountrequestId: accountrequestid
    }
    $.ajax({
        type: "Post",
        url: "/Account/DeleteAccountRequest",
        data: account,
        success: function (result) {
            ToasteRMessage(result,'.DeleteAccountRequestModel');
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

$(document).on('click', '#CreateIDModalBtn', function () {
    let accountRequestId = $(this).attr('accountRequestId');
    let userNumber = $('#accountRequest_' + accountRequestId).find('.userNumber').attr('userNumber');
    let userName = $('#accountRequest_' + accountRequestId).find('.userNumber').attr('userName');
    let siteName = $('#accountRequest_' + accountRequestId).find('.sitedetails').attr('siteName');
    let siteUrl = $('#accountRequest_' + accountRequestId).find('.sitedetails').attr('siteUrl');
    let iconPath = $('#accountRequest_' + accountRequestId).find('.sitedetails').attr('iconPath');
    CreateIDFormValidationSingleton.getInstance();
    //$.ajax({
    //    url: '/Account/AccountRequestDetails',
    //    type: 'POST',
    //    data: { AccountRequestId: AccountRequestId },
    //    success: function (result) {
            $('#CreateIDModalForm .siteIcon').attr('src', iconPath);
            $('#CreateIDModalForm .siteName').html(siteName);
            $('#CreateIDModalForm .siteURL').html(siteUrl);
            $('#CreateIDModalForm .Username').val(userName);
            $('#CreateIDModalForm').attr('accountRequestId', accountRequestId);
    //        CreateIDFormValidationSingleton.getInstance();
    //    }
    //});
});

// End Click Event

//Start Form
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
            $(':input[type="submit"]').prop('disabled', true);
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

var CreateIDFormfv;
var fv2;
var CreateIDFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('CreateIDModalForm');
        fv2 = FormValidation.formValidation(form, {
            fields: {
                Username: {
                    validators: {
                        notEmpty: {
                            message: 'User Name is required'
                        }
                    }
                },
                password: {
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

// End Form

//Start Function
function AddAccountRequest() {
    var obj = {
        UserName: $('#Username').val(),
        SiteId: $('#CreateIDRequestModal').attr('SiteId')
    }

    $.ajax({
        url: '/Account/AddAccountRequest',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage(result,'#CreateIDRequestModal');
        }
    });
}

function AddAccount() {
    var obj = {
        AccountRequestId: $('#CreateIDModalForm').attr('accountRequestId'),
        UserName: $('#CreateIDModalForm .Username').val(),
        Password: $('#CreateIDModalForm .password').val()
    }

    $.ajax({
        url: '/Account/AddAccount',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage(result,'#CreateIDModal');
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

//End Function
