'use strict'

$(function () {
    $('.delete').each(function () {
        var self = $(this);

        self.popover({
            live: true,
            placement: 'left',
            html: 'true',
            title: 'Sure?',
            content: 'Are you sure to delete this application?' + '<a href="/Application/DeleteApp/' + self.attr('data-delete-id') + '">Yes</a>'
        });
    })
});