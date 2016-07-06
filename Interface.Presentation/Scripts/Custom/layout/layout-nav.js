'use strict'

var togglePositionCollapse = -10;

$(function () {

    $('.link[data-target-id="' + document.title + '"]').addClass('active');

    $("#imgInput").change(function () {
        readURL(this);
    });

    $('.navbar-toggle').click(function () {
        $('#navigation').toggleClass('toggle-table-cell').animate({ left: togglePositionCollapse }, 250, function () {
            togglePositionCollapse = togglePositionCollapse == -75 ? -10 : -75;
        });
    })


})

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgShow').attr('src', e.target.result);
            $('#imgPerfilShow').attr('src', e.target.result);
            $('#project-img').slideDown('slow');
        }

        reader.readAsDataURL(input.files[0]);
    }
}
