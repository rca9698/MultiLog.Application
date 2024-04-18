$(document).ready(function () {
    let viewType = $('#listPassbookHistory').attr('ViewType');
    if (viewType != null && viewType != undefined) {
        if (viewType == 'Client') {
            //whole account history
            LoadPassbookHistory();
        }
        else if (viewType.startsWith('account_')) {
            //site related passbook
            var str = $('#listPassbookHistory').attr('ViewType');
            let siteid = str.replace('account_', '');
            LoadAccountPassbookHistory(siteid);
        }
    }
});

$(document).on('click', '#PassbookHistory .backbtnNavDesign', function () {
    $('#listPassbookHistory').show();
    $('.tabSelection').show();
    $('#PassbookHistory').hide();
});

$(document).on('click', '.viewPassbookHistoryDetails', function () {
    let siteIcon = $(this).find('.siteIcon').attr('src');
    let passbookId = $(this).attr('passbookId');
    let obj = {};
    obj.PassbookId = passbookId;

    $.ajax({
        url: '/Passbook/PassbookHistory',
        type: 'POST',
        data: { obj: obj },
        success: function (result) {
            $('#listPassbookHistory').hide();
            $('#PassbookHistory').show();
            $('#PassbookHistory').html(result);
            $('.tabSelection').hide();

            $('#PassbookHistory .siteIcon').attr('src', siteIcon);
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


function LoadAccountPassbookHistory(siteId) {
    $.ajax({
        url: '/Passbook/LoadAccountPassbook',
        type: 'POST',
        data: { SiteId: siteId },
        success: function (result) {
            $('#listPassbookHistory').html(result);
        }
    });
}

