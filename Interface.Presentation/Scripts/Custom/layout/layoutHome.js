$(function () {

    $('.nav-link[data-target-id="' + document.title + '"]').addClass('active');

    var $information = $('#informations');

    if ($information.height() > 0) {
        $information.css('display', 'block');
    }

    $("#imgInput").change(function () {
        readURL(this);
    });

})

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgShow').attr('src', e.target.result);
            $('#project-img').slideDown('slow');
        }

        reader.readAsDataURL(input.files[0]);
    }
}


jQuery(function ($) {

    //Ajax contact
    var form = $('.contact-form');
    form.submit(function () {
        $this = $(this);
        $.post($(this).attr('action'), function (data) {
            $this.prev().text(data.message).fadeIn().delay(3000).fadeOut();
        }, 'json');
        return false;
    });

    //Goto Top
    $('.gototop').click(function (event) {
        event.preventDefault();
        $('html, body').animate({
            scrollTop: 0
        }, 500);
    });
    //End goto top		

});