(function($) {

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
    var copyText = $(this).attr('data-value');

    var tempTextarea = $('<textarea>');
    $('body').append(tempTextarea);
    tempTextarea.val(copyText).select();
    document.execCommand('copy');
    tempTextarea.remove();
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
});
