
$(function () {
    $('table#tbl-doc-search, table#tbl-customer-search,table#tblRole').DataTable({
        "retrieve": true,
        "searching": true,
        "processing": true,
        "info": false,
        "ordering": false,
        "lengthChange": false,
        "pageLength": 5,
        language: {
            paginate: { previous: '<', next: '>' },
            search: ''
        }
    });

    SearchDocEvent();
    SearchCustomerEvent();
});


var tblDoc, tblSearch;
function SearchDocEvent() {
    $('button.btn-doc-search').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var obj = new Object();
        obj.FromDate = $('#txtDocFromDate').val();
        obj.ToDate = $('#txtDocToDate').val();
        if (!obj.FromDate && !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Cảnh báo');
            return;
        }
        if ($("#txtTicketID").val() == null) {
            obj.code = "";
        } else { obj.code = $("#txtTicketID").val(); };

        if (tblDoc) tblDoc.destroy();
        $.ajax({
            type: "POST",
            url: '/manager/GetCreateDocRequestLog',
            contentType: 'application/json-patch+json',
            dataType: "json",
            data: JSON.stringify(obj),
            success: function (msg) {
                console.log(msg);
                tblDoc = $('table.tbl-doc-search').DataTable({
                    "data": msg,
                    "columns": [
                        {
                            "data": "STT",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "TicketId",
                            "className": 'vertical-middle',
                            "render": function (data, type, row, meta) {
                                console.log(data);
                                if (type === 'display') {
                                    data = '<a  target="_blank" class="badge bg-yellow" href="/receive/' + data + '">' + data + '</a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "CreatedBy",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "CreatedDate",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "IsSuccess",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "RequestContent",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "ResponseContent",
                            "className": 'vertical-middle'
                        }
                    ],
                    "retrieve": true,
                    "searching": true,
                    "processing": true,
                    "info": false,
                    "ordering": false,
                    "lengthChange": false,
                    "pageLength": 5,
                    language: {
                        paginate: { previous: '<', next: '>' },
                        search: ''
                    }
                });
            },
            complete: function (msg) {
                console.log(msg);
            }
        });

    });
}

function SearchCustomerEvent() {
    $('button.btn-customer-search').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var obj = new Object();
        obj.FromDate = $('#txtCustomerFromDate').val();
        obj.ToDate = $('#txtCustomerToDate').val();
        if (!obj.FromDate && !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Cảnh báo');
            return;
        }
        if ($("#txtCustomer").val() == null) {
            obj.Customer = "";
        } else { obj.Costomer = $("#txtCustomer").val(); };

        if (tblSearch) tblSearch.destroy();
        $.ajax({
            type: "POST",
            url: '/manager/GetCreateCustomerRequestLog',
            dataType: "json",
            contentType: 'application/json-patch+json',
            data: JSON.stringify(obj),
            success: function (msg) {
                tblSearch = $('table.tbl-customer-search').DataTable({
                    "data": msg,
                    "columns": [
                        {
                            "data": "STT",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "CustomerCode",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "CreatedBy",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "CreatedDateStr",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "CompletedDateStr",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "IsSuccess",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "RequestContent",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "ResponseContent",
                            "className": 'vertical-middle'
                        }
                    ],
                    "retrieve": true,
                    "searching": true,
                    "processing": true,
                    "info": false,
                    "ordering": false,
                    "lengthChange": false,
                    "pageLength": 5,
                    language: {
                        paginate: { previous: '<', next: '>' },
                        search: ''
                    }
                });
            },
            complete: function (msg) {
                console.log(msg);
            }
        });

    });
}