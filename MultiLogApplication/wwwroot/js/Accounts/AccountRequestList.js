
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

$(document).on('click', '#AddAccountRequest', function () {
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