
$(document).on('click', '#DeleteAccountRequestList', function () {
    var userId = $(this).attr('data-userId');
    var siteId = $(this).attr('data-siteId');
    $('#DeleteAccountRequest').attr('data-userId', userId);
    $('#DeleteAccountRequest').attr('data-siteId', siteId);
});

$(document).on('click', '#AddAccontRequest', function () {
    var userId = $(this).attr('data-userId');
    var siteId = $(this).attr('data-siteId');
    $('#AddAccountRequest').attr('data-userId', userId);
    $('#AddAccountRequest').attr('data-siteId', siteId);
});

$(document).on('click', '#AddAccountRequest', function () {
    var userId = $(this).attr('data-userId');
    var siteId = $(this).attr('data-siteId');
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
    var userId = $(this).attr('data-userId');
    var siteId = $(this).attr('data-siteId');
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