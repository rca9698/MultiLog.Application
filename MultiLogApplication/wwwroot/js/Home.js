$(document).ready(function () {
    if ($('#ListDashboardImgDetail').attr('ViewType') == 'AdminImg') {
        LoadDahboardImages();
    }
});

$(document).on('click', '#AddImgModalBtn', function () {
    AddImgFormValidationSingleton.getInstance();
})

$(document).on('click', '#ViewDashboardImgBtn', function () {
    var add = $(this).attr('dashboardPath');
    $('#ViewCoinRequestProof .paymentProof').attr('src', add);
});

$(document).on('click', '#DeleteDashboardImgBtn', function () {
    var docid = $(this).attr('docid');
    $('#DeleteDashboardImgConfirmbtn').attr('docid', docid);
});

$(document).on('click', '#DeleteDashboardImgConfirmbtn', function () {
    var docId = $(this).attr('docId');
    $.ajax({
        url: '/Home/DeleteDashboardImages',
        type: 'POST',
        data: { DocId: docId },
        success: function (result) {
            ToasteRMessage(result, '');
        }
    });
});


var AddImgFormfv;
var AddImgfv1;
var AddImgFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddImgModalForm');
        AddImgfv1 = FormValidation.formValidation(form, {
            fields: {
                ImgDisplayDate: {
                    validators: {
                        notEmpty: {
                            message: 'Display Date is required'
                        }
                    }
                },
                files: {
                    validators: {
                        callback: {
                            message: 'Please upload image',
                            callback: function (value, validator, $field) {
                                if ($("#AddImgModalForm .files")[0].files[0] == undefined)
                                    return false;
                                return true;
                            }
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
            AddDashBoardImg();
        });
        return AddImgfv1;
    }
    return {
        getInstance: function () {
            if (AddImgFormfv) {
                AddImgFormfv.destroy();
            }
            AddImgFormfv = createInstance();
            return AddImgFormfv;
        }
    };
})();



function AddDashBoardImg() {
    var formData = new FormData();
    formData.append("DisplayDate", $('#AddImgModal .DisplayDate').val());
    formData.append("File", $("#AddImgModal .files")[0].files[0]);

    $.ajax({
        url: '/Home/InsertDahboardImages',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            ToasteRMessage(result, '#AddImgModal');
        }
    });
}

function LoadDahboardImages() {
    $.ajax({
        url: '/Home/DashboardImages',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#ListDashboardImgDetail').html(result);
        }
    });
}


