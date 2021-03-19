const pageSize = 12;
$(document).ready(function () {

    $('.filmsList').on('click', '.film',  function () {
        window.location = '/Films/Details?id='+$(this).attr('id');
    });

    $('.filmsList').on('click', '.toPage', function () {
        FilmsLoad($(this).data("page"), pageSize);
    });

});

function FilmsLoad(page, count) {
    $.ajax({
        type: 'GET',
        url: '/Films/Index',
        data: { page: page, count: count },
        async: true,
        success: function (data) {
            $('.filmsList').html(data);
            localStorage.setItem('page', page);
        },
        error: function (response) {
            $('.filmsList').html(response);
        }
    });
}