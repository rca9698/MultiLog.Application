

$(document).ready(function () {
    if ($('#listSites').length)
    {
        LoadSites();
    }
});

$(document).on('click', '#AddSitesModalbtn', function () {
    AddSiteFormValidationSingleton.getInstance();
});

$(document).on('click', '#updateSite', function () {
    let siteId = $(this).attr('data-siteId');
    let Name = $('#site_' + siteId + ' .siteName').html();
    let URL = $('#site_' + siteId + ' .siteURL').html();
    $('#UpdateSiteModal').attr('data-siteId', siteId);
    $('#UpdateSiteModalForm #siteName').val(Name);
    $('#UpdateSiteModalForm #siteURL').val(URL);
    UpdateSiteFormValidationSingleton.getInstance();
});

$(document).on('click', '#deleteSite', function () {
    let siteId = $(this).attr('data-siteId');
    $('#DeleteSitebtn').attr('data-siteId', siteId);
});

$(document).on('click', '#DeleteSitebtn', function () {
    DeleteSite($(this).attr('data-siteId'));
});

var AddSiteFormfv;
var fv1;
var AddSiteFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddSiteModalForm');
        fv1 = FormValidation.formValidation(form, {
            fields: {
                siteName: {
                    validators: {
                        notEmpty: {
                            message: 'Site Name required'
                        }
                    }
                },
                siteURL: {
                    validators: {
                        notEmpty: {
                            message: 'Site URL is required'
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
            AddSite();
        });
        return fv1;
    }
    return {
        getInstance: function () {
            if (AddSiteFormfv) {
                AddSiteFormfv.destroy();
            }
            AddSiteFormfv = createInstance();
            return AddSiteFormfv;
        }
    };
})();


var UpdateSiteFormfv;
var fv2;
var UpdateSiteFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('UpdateSiteModalForm');
        fv2 = FormValidation.formValidation(form, {
            fields: {
                siteName: {
                    validators: {
                        notEmpty: {
                            message: 'Site Name required'
                        }
                    }
                },
                siteURL: {
                    validators: {
                        notEmpty: {
                            message: 'Site URL is required'
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
            UpdateSite();
        });
        return fv2;
    }
    return {
        getInstance: function () {
            if (UpdateSiteFormfv) {
                UpdateSiteFormfv.destroy();
            }
            UpdateSiteFormfv = createInstance();
            return UpdateSiteFormfv;
        }
    };
})();

function AddSite() {

    var formData = new FormData();
    formData.append("SiteName", $('#AddSitesModal #siteName').val());
    formData.append("SiteURL", $('#AddSitesModal #siteURL').val());
    formData.append("File", $("#AddSitesModal #files")[0].files[0]);

    $.ajax({
        url: '/Site/AddSite',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#AddSitesModal .close').trigger('click');
            }
        }
    });
}

function UpdateSite() {

    var formData = new FormData();
    formData.append("SiteId", $('#UpdateSiteModal').attr('data-siteId'));
    formData.append("SiteName", $('#UpdateSiteModal #siteName').val());
    formData.append("SiteURL", $('#UpdateSiteModal #siteURL').val());
    formData.append("File", $("#UpdateSiteModal #files")[0].files[0]);

    $.ajax({
        url: '/Site/AddSite',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#UpdateSiteModal .close').trigger('click');
            }
        }
    });
}

function DeleteSite(id) {
    var obj = {
        SiteId: id
    }
    $.ajax({
        url: '/Site/DeleteSite',
        type: 'POST',
        data: obj,
        success: function (result) {
            if (result.returnStatus == 1) {
                toastr.success(result.returnMessage);
                $('#DeleteSiteModal .close').trigger('click');
            }
        }
    });
}

function LoadSites() {
    $.ajax({
        url: '/Site/Getsites',
        type: 'POST',
        data: '',
        success: function (result) {
            $('#listSites').html(result);
        }
    });
}
