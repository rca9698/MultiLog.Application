$(document).ready(function () {
    if ($('#listSites').attr('ViewType') == 'Admin') {
        LoadSites();
    }
    else if ($('#listSites').attr('ViewType') == 'MyIds') {
        LoadMySites();
    }
    else if ($('#listSites').attr('ViewType') == 'Ids') {
        LoadUserSites();
    }
});
//Start Click Events

$(document).on('click', '#AddSitesModalbtn', function () {
    AddSiteFormValidationSingleton.getInstance();
});

$(document).on('click', '.IDsList', function () {
    location.href = '/Site/Index?viewType=Ids'
});

$(document).on('click', '.MyIDsList', function () {
    location.href = '/Site/Index?viewType=MyIds'
});

$(document).on('click', '.idSwitch .tabSelection', function () {
    $('.idSwitch .tabSelection').removeClass('active');
    $(this).addClass('active');
    
    let url = $(this).attr('idType') == 'myIds'
        ? '/Site/GetUserListSiteById'
        : '/Site/GetUserListSites';

    $.ajax({
        url: url,
        type: 'POST',
        data: {},
        success: function (result) {
            $('#listSites').html(result);
        }
    });

});

$(document).on('click', '#updateSiteBtn', function () {
    let siteId = $(this).attr('siteId');
    let Name = $('.site_' + siteId + ' .siteName').html();
    let URL = $('.site_' + siteId + ' .siteURL').html();
    let iconSrc = $('.site_' + siteId + ' .siteIcon').attr('src');

    $('#UpdateSiteModal').attr('siteId', siteId);
    $('#UpdateSiteModalForm .siteName').val(Name);
    $('#UpdateSiteModalForm .siteURL').val(URL);
    $('#UpdateSiteModalForm .siteIcon').attr('siteIcon', iconSrc);
    UpdateSiteFormValidationSingleton.getInstance();
});

$(document).on('click', '#deleteSiteBtn', function () {
    let siteId = $(this).attr('siteId');
    $('#DeleteSiteConfirmbtn').attr('siteId', siteId);
});

$(document).on('click', '#DeleteSiteConfirmbtn', function () {
    DeleteSite($(this).attr('siteId'));
});

$(document).on('click', '.viewThisSiteDetails', function () {
    let siteId = $(this).attr('SiteId');
    $.ajax({
        url: '/Site/ViewThisSiteDetailsPV',
        type: 'POST',
        data: { siteId: siteId },
        success: function (result) {
            $('#listSites').hide();
            $('.idSwitch').hide();
            $('#SiteDetail').show();
            $('#SiteDetail').html(result);
        }
    });
});

$(document).on('click', '.BackBtnMyIds', function () {
    $('#listSites').show();
    $('.idSwitch').show();
    $('#SiteDetail').hide();
})

$(document).on('click', '#DepositeToAccountRequestCoinsBtn', function () {
    let siteId = $(this).attr('siteId');
    let Name = $('.site_' + siteId + ' .siteName').html();
    let URL = $('.site_' + siteId + ' .siteURL').html();
    let iconSrc = $('.site_' + siteId + ' .siteIcon').attr('src');

    $('#DepositeToAccountRequestModal').attr('siteId', siteId);
    $('#DepositeToAccountRequestModal .siteName').html(Name);
    $('#DepositeToAccountRequestModal .siteURL').html(URL);
    $('#DepositeToAccountRequestModal .siteIcon').attr('src', iconSrc);
    DepositeCoinsToAccountRequestFormValidationSingleton.getInstance();

})

$(document).on('click', '#withdrawFromAccountRequestCoinsBtn', function () {
    let siteId = $(this).attr('siteId');
    let Name = $('.site_' + siteId + ' .siteName').html();
    let URL = $('.site_' + siteId + ' .siteURL').html();
    let iconSrc = $('.site_' + siteId + ' .siteIcon').attr('src');

    $('#WithDrawToAccountRequestModal').attr('siteId', siteId);
    $('#WithDrawToAccountRequestModal .siteName').html(Name);
    $('#WithDrawToAccountRequestModal .siteURL').html(URL);
    $('#WithDrawToAccountRequestModal .siteIcon').attr('src', iconSrc);
    WithDrawFromAccountRequestFormValidationSingleton.getInstance();
})


//End Click Events


//Start function area
function AddSite() {
    var formData = new FormData();
    formData.append("SiteName", $('#AddSitesModal .siteName').val());
    formData.append("SiteURL", $('#AddSitesModal .siteURL').val());
    formData.append("File", $("#AddSitesModal .files")[0].files[0]);

    $.ajax({
        url: '/Site/AddSite',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            ToasteRMessage('#AddSitesModal');
        }
    });
}

function UpdateSite() {

    var formData = new FormData();
    formData.append("SiteId", $('#UpdateSiteModal').attr('siteId'));
    formData.append("SiteName", $('#UpdateSiteModal .siteName').val());
    formData.append("SiteURL", $('#UpdateSiteModal .siteURL').val());
    formData.append("ImageName", $('#UpdateSiteModal .siteIcon').attr('siteIcon'));
    formData.append("File", $("#UpdateSiteModal .files")[0].files[0]);

    $.ajax({
        url: '/Site/UpdateSite',
        type: 'POST',
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data
        success: function (result) {
            ToasteRMessage('#UpdateSiteModal');
        }
    });
}

function DepositeCoinsToAccountRequest() {
    
    var obj = {};
    obj.SiteId = $('#DepositeToAccountRequestModal').attr('siteId');
    obj.Coins = $('#DepositeToAccountRequestModal .Coins').val();

    $.ajax({
        url: '/Coin/AddCoinsToAccountRequest',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage('#DepositeToAccountRequestModal');
        }
    });
}

function WithdrawCoinsToAccountRequest() {
    var obj = {};
    obj.SiteId = $('#WithDrawToAccountRequestModal').attr('siteId');
    obj.Coins = $('#WithDrawToAccountRequestModal .Coins').val();

    $.ajax({
        url: '/Coin/WithDrawToAccountRequest',
        type: 'POST',
        data: obj,
        success: function (result) {
            ToasteRMessage('#WithDrawToAccountRequestModal');
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
            ToasteRMessage('#DeleteSiteModal');
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

function LoadUserSites() {
    let url = '/Site/GetUserListSites';

    $.ajax({
        url: url,
        type: 'POST',
        data: {},
        success: function (result) {
            $('#listSites').html(result);
        }
    });
    $('.newIdsClass').addClass('active');
}

function LoadMySites() {
    let url = '/Site/GetUserListSiteById';
    $('.myIdsClass').addClass('active');
    $.ajax({
        url: url,
        type: 'POST',
        data: {},
        success: function (result) {
            $('#listSites').html(result);
        }
    });
    
}

//End function area


//Start Form Validation

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
                },
                files: {
                    validators: {
                        callback: {
                            message: 'Please upload icon',
                            callback: function (value, validator, $field) {
                                if ($("#AddSitesModal .files")[0].files[0] == undefined)
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
                },
                files: {
                    validators: {
                        callback: {
                            message: 'Please upload icon',
                            callback: function (value, validator, $field) {
                                if ($("#UpdateSiteModal .files")[0].files[0] == undefined)
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


var DepositeCoinsToAccountFormfv;
var fv3;
var DepositeCoinsToAccountRequestFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('DepositeToAccountRequestModalForm');
        fv3 = FormValidation.formValidation(form, {
            fields: {
                depositeToAccountCoins: {
                    validators: {
                        notEmpty: {
                            message: 'Coins required'
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
            DepositeCoinsToAccountRequest();
        });
        return fv3;
    }
    return {
        getInstance: function () {
            if (DepositeCoinsToAccountFormfv) {
                DepositeCoinsToAccountFormfv.destroy();
            }
            DepositeCoinsToAccountFormfv = createInstance();
            return DepositeCoinsToAccountFormfv;
        }
    };
})();


var WithDrawFromAccountRequestFormfv;
var fv4;
var WithDrawFromAccountRequestFormValidationSingleton = (function () {
    function createInstance() {

        let form = document.getElementById('WithDrawToAccountRequestModalForm');
        fv4 = FormValidation.formValidation(form, {
            fields: {
                withdrawFromAccountCoins: {
                    validators: {
                        notEmpty: {
                            message: 'Coins required'
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
            WithdrawCoinsToAccountRequest();
        });
        return fv4;
    }
    return {
        getInstance: function () {
            if (WithDrawFromAccountRequestFormfv) {
                WithDrawFromAccountRequestFormfv.destroy();
            }
            WithDrawFromAccountRequestFormfv = createInstance();
            return WithDrawFromAccountRequestFormfv;
        }
    };
})();


//End Form Validation