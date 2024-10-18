$(document).ready(function () {
    $('#search').keyup(function () {
        search_table($(this).val());

    });
    function search_table(value) {
        $('#documentacion tr').each(function () {
            var found = 'false';
            $(this).each(function () {
                if ($(this).text().toLowerCase().indexOf(value.toLowerCase()) >= 0) {
                    found = 'true';
                }
            });
            if (found == 'true') {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    }
});


$(document).ready(function () {
    $('.dataTables_filterinput[type="search"]').css(
        { 'width': '950px', 'display': 'inline-block' }
    );
});