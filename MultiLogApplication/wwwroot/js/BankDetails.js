$(document).ready(function () {
    debugger;
    if ($('#ListBankDetail').length) {
        LoadBanks();
    }
});


function LoadBanks() {
    $.ajax({
        url: '/BankAccount/ViewPanel',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListBankDetail').html(result);
        }
    });
}

