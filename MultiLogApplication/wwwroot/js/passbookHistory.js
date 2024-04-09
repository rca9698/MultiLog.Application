$(document).ready(function () {
    if ($('#listPassbookHistory').attr('ViewType') == 'Client') {
        LoadPassbookHistory();
    }
});

$(document).on('click', '#PassbookHistory .backbtnNavDesign', function () {
    $('#listPassbookHistory').show();
    $('.tabSelection').show();
    $('#PassbookHistory').hide();
});

$(document).on('click', '.viewPassbookHistoryDetails', function () {
    let siteIcon = $(this).find('.siteIcon').attr('src');
    let activity = $(this).find('.activityClass').html();
    let siteUrl = $(this).find('.siteUrl').html();
    let siteUserName = $(this).find('.siteUserName').html();
    let createdOn = $(this).find('.createdOn').html();
    let passbookId = $(this).attr('passbookId');
    let coins = $(this).attr('Coins');
    let paidCoins = $(this).attr('PaidCoins');

    $.ajax({
        url: '/Passbook/PassbookHistory',
        type: 'POST',
        data: { },
        success: function (result) {
            $('#listPassbookHistory').hide();
            $('#PassbookHistory').show();
            $('#PassbookHistory').html(result); 
            $('.tabSelection').hide();

            $('#PassbookHistory .siteIcon').attr('src', siteIcon);
            $('#PassbookHistory .activityClass').html(activity);
            $('#PassbookHistory .siteURL').html(siteUrl);
            $('#PassbookHistory .siteUserName').html(siteUserName);
            $('#PassbookHistory .createdOn').html(createdOn);
            $('#PassbookHistory .passbookId').html(passbookId);
            $('#PassbookHistory .coins').html(coins);
            $('#PassbookHistory .paidCoins').html(paidCoins);
        }
    });
});


function LoadPassbookHistory() {
    $.ajax({
        url: '/Passbook/ViewPanel',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#listPassbookHistory').html(result);
        }
    });
}


