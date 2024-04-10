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


