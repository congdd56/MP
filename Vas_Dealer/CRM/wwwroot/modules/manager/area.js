$(function () {
    /**
     * Load danh sách khu vực
     * */
    LoadAreaData();

    if (location.hash) {
        $('.nav-tabs a[href="' + location.hash + '"]').tab('show');
    }
});

/**
 * Load danh sách khu vực
 * */
function LoadAreaData() {
    $.ajax({
        type: "GET",
        url: '/Manager/Area/AllArea',
        dataType: "json",
        success: function (msg) {
            var table = $('table#tblArea').DataTable({
                "data": msg,
                "columns": [
                    {
                        "data": "STT",
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Area",
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "TeamLeaderStr",
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            var htmlData = '';
                            if (type === 'display') {
                                var item = data.split(',');
                                if (item && item.length > 0) {
                                    for (var i = 0; i < item.length; i++) {
                                        htmlData += '<span class="badge bg-green">' + item[i] + '</span>';
                                    }
                                }
                            }
                            return htmlData;
                        }
                    },
                    {
                        "data": "UseNameStr",
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            var htmlData = '';
                            if (type === 'display') {
                                var item = data.split(',');
                                if (item && item.length > 0) {
                                    for (var i = 0; i < item.length; i++) {
                                        htmlData += '<span class="badge bg-yellow">' + item[i] + '</span>';
                                    }
                                }
                            }
                            return htmlData;
                        }
                    },
                    {
                        "className": 'details-control-area text-center vertical-middle',
                        "orderable": false,
                        "data": null,
                        "defaultContent": '<i class="fa fa-edit fa-lg text-primary mp-pointer-st pointer mx-1" alt="change" title="Sửa khu vực"></i>'
                    }
                ],
                "retrieve": true,
                "searching": false,
                "processing": true,
                "info": false,
                "ordering": false,
                "lengthChange": false,
                "pageLength": 10,
                language: {
                    paginate: { previous: '<', next: '>' },
                    search: ''
                }
            });

            // Add event listener for opening and closing details
            $('table#tblArea tbody').on('click', 'td.details-control-area > i', function (e) {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                var itemAccount = row.data();
                console.log(itemAccount);
                var popQuestion = $.confirm({
                    title: '<i class="fa fa-list-ol text-green"></i> Cập nhật khu vực',
                    type: 'red',
                    id: 'confirmDetailsHandlerArea',
                    columnClass: 'col-md-8',
                    content: 'url:/HtmlModel/DPL/AreaManager.html',
                    buttons: {
                        chosen: {
                            text: 'Xác nhận',
                            btnClass: 'bg-green btn-flat btn-update-accountarea-popup',
                            action: function (e) {
                                var obj = new Object();
                                obj.AreaId = $('button.btn-update-accountarea-popup').data('area');
                                obj.TeamLeader = $('#ddlPopTeamleader').val();
                                obj.UseName = $('#ddlPopAccountArea').val();
                                $.confirm({
                                    title: '<i class="fa fa-question-o text-red"></i> Xác nhận',
                                    type: 'red',
                                    content: 'Xác nhận cập nhật khu vực?',
                                    buttons: {
                                        chosen: {
                                            text: 'Xác nhận',
                                            btnClass: 'bg-green btn-flat',
                                            action: function (e) {
                                                var itemDisableds = [$('button.btn-update-accountarea-popup')];
                                                var mylop = new myMpLoop($('button.btn-update-accountarea-popup'), 'Đang xử lý', $('button.btn-update-accountarea-popup').html(), itemDisableds);
                                                mylop.start();
                                                $.ajax({
                                                    type: "POST",
                                                    url: '/Manager/Area/UpdateAccountArea',
                                                    contentType: "application/json charset=utf-8",
                                                    dataType: "json",
                                                    data: JSON.stringify(obj),
                                                    success: function (msg) {
                                                        console.log(msg);
                                                        debugger;
                                                        if (!msg.status) {
                                                            toastr.warning(msg.value, 'Cảnh báo');
                                                        } else {
                                                            toastr.info(msg.value, 'Thông báo');
                                                            window.location = '/manager#panel-area'
                                                            location.reload();
                                                        }

                                                    },
                                                    complete: function (msg) {
                                                        console.log(msg);
                                                        mylop.stop();
                                                    }
                                                });
                                            }
                                        },
                                        cancel: {
                                            text: 'Hủy',
                                            keys: ['esc']
                                        }
                                    }
                                });
                                return false;
                            }
                        },
                        cancel: {
                            text: 'Đóng',
                            keys: ['esc']
                        }
                    },
                    onContentReady: function () {
                        $(".jconfirm-content").css("overflow", "hidden");
                        $('#txtPopAreaName').val(itemAccount.Area);
                        $('button.btn-update-accountarea-popup').attr('data-area', itemAccount.AreaId);
                        $.ajax({
                            type: "GET",
                            url: '/Manager/Area/GetSelectedDataAreaTeamleaderUser/' + itemAccount.AreaId,
                            dataType: "json",
                            success: function (msg) {
                                $('select#ddlPopTeamleader').select2({
                                    data: msg.itemLeader,
                                    dropdownParent: $('.jconfirm-no-transition'),
                                    placeholder: 'Chọn tài khoản'
                                }).trigger('change.select2');
                                $('select#ddlPopAccountArea').select2({
                                    data: msg.itemAccount,
                                    dropdownParent: $('.jconfirm-no-transition'),
                                    placeholder: 'Chọn tài khoản'
                                }).trigger('change.select2');
                            },
                            complete: function (msg) {
                                console.log(msg);
                            }
                        });
                    }
                });



            });
        }
    });
}