$(document).ready(function () {
    if ($('#listPassbookHistory').attr('ViewType') == 'Client') {
        LoadPassbookHistory();
    }
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


