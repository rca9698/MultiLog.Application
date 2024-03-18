var AddSiteFormfv;
var fv1;
var AddSiteFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('AddSiteModal');
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

function AddSite() {

    var formData = new FormData();
    formData.append("SiteName", $('#siteName').val());
    formData.append("SiteURL", $('#siteURL').val());
    formData.append("File", $("#files")[0].files[0]);

    $.ajax({
        url: '/Site/AddSite',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            if (result.returnStatus == 1) {
                alert(result.returnMessage);
            }
        }
    });
}




