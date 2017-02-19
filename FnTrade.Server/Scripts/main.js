$(function () {
    var uri = 'api/cars';

    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('<tr><td>' + (key + 1) + '</td><td>' + item.Symbol + '</td><td>' + item.Qty + '</td></tr>')
                    .appendTo($('#cars tbody'));
            });
        });
});
