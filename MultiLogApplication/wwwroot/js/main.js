var accountCoins = 0;

(function ($) {

	"use strict";

	var fullHeight = function() {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$('#sidebarCollapse').on('click', function () {
      $('#sidebar').toggleClass('active');
  });

})(jQuery);



const mobileScreen = window.matchMedia("(max-width: 990px )");
$(document).ready(function () {

    LoadSessionData();

    accountCoins = parseInt($('.coinsValidation').attr('data-coins'));
    $(".dashboard-nav-dropdown-toggle").click(function () {
        $(this).closest(".dashboard-nav-dropdown")
            .toggleClass("show")
            .find(".dashboard-nav-dropdown")
            .removeClass("show");
        $(this).parent()
            .siblings()
            .removeClass("show");
    });
    $(".menu-toggle").click(function () {
        if (mobileScreen.matches) {
            $(".dashboard-nav").toggleClass("mobile-show");
        } else {
            $(".dashboard").toggleClass("dashboard-compact");
        }
    });
});

$(document).on('click', '.CopyToClipboard', function () {
    var copyText = $(this).attr('copyData');
    const el = document.createElement('textarea');
    el.value = copyText;
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
    toastr.success('Copied the text:' + el.value);
});


$(document).click(function (e) {

    var container2 = $(".menu-toggle");
    if (container2.is(e.target) && container2.has(e.target).length > 0) {
        $(".dashboard-nav").addClass("mobile-show");
    }
    var container = $(".dashboard-nav");
    if (!container2.is(e.target) && container2.has(e.target).length === 0 && !container.is(e.target) && container.has(e.target).length === 0) {
        if (container.hasClass('mobile-show')) {
            $(".dashboard-nav").removeClass("mobile-show");
        }
    }

    $(document).on('click', '.closeMobileMenu', function (){
        if (container.hasClass('mobile-show')) {
            $(".dashboard-nav").removeClass("mobile-show");
        }
    });

});


function LoadSessionData() {
    $.ajax({
        url: '/User/GetUserById',
        type: 'POST',
        data: '',
        success: function (result) {
            if (result.returnStatus == 1) {
                $('.coinsValidation').attr('data-coins', result.returnVal.coins);
                accountCoins = parseInt($('.coinsValidation').attr('data-coins'));
            }
        }
    });
}


