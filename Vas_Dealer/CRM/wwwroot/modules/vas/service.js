$(function () {
    $('table.data').DataTable({
        "processing": true,
        "searching": true,
        "language": {
            "search": "",
            "searchPlaceholder": "Tìm kiếm..."
        },
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 10
    });
});
