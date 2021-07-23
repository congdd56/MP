
$(function () {
    $('table#tblRole').DataTable({
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
}); 

//Hiện thị danh sách Account theo nhóm Điện thoại viên

$('body').on('click', 'td.detailAccountByRole', function (e) {

    if ($.fn.dataTable.isDataTable('#tableDetailGroup')) {
        $('#tableDetailGroup').DataTable().destroy()
    }
    $("#tableDetailGroup").show();
    //$("#divGroup").hide();
    var $this = $(this);
    var itemDisableds = [$this];
    var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        //url: "/Role/GetInfoUserByRoleId?Id=" + $this.attr('uid') + "",
        url: "/Manager/GetInfoGroupByRoleId?Id=" + $this.attr('uid') + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var item = msg.value.userByRole;
            var html = "";
            for (var i = 0; i < item.length; i++) {
                html += "<tr><td>" + (i + 1) + "</td>";
                html += "<td>" + item[i].Name + "</td>";
                html += "<td>" + item[i].FullName + "</td>";
                html += "<td>" + item[i].Email + "</td>";
                html += "</tr>";
            }
            $("#tbdDetailAccountByGroup").html(html);
            $('#tableDetailGroup').DataTable({
                "retrieve": true,
                "processing": true,
                "info": false,
                "lengthChange": false,
                language: {
                    paginate: { previous: '<', next: '>' },
                    search: ''
                }
            });
            $(".detail-user-role").show();
        },
        error: function (msg) {
            console.log(msg);
            toastr.warning("Quá trình lấy dữ liệu nhóm điện thoại viên bị lỗi", "Thông báo");
        },
        complete: function (msg) {
            mylop.stop();
        }
    });

});

// Chỉnh sửa Nhóm điện thoại viên
$('body').on('click', '.editGroup', function (e) {
    $(".add").hide();
    $(".update").show();
    $(".pass").hide();
    var $this = $(this);
    id = $this.attr('uid');
    $.confirm({
        title: '<i class="fa fa-user text-red"></i> Cập nhật Nhóm',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/CIC/GroupModel.html',
        buttons: {
            formSubmit: {
                text: 'Cập nhật',
                btnClass: 'btn-blue btn-add-account',
                action: function () {
                    var name = $("#txtNameGroupModal").val();
                    if (name == "") { toastr.warning("Nhập tên nhóm điện thoại viên", "Thông báo"); return false; }

                    var obj = new Object();
                    obj.NameGroup = name;
                    var GroupId = $this.attr('uid');
                    obj.GroupId = GroupId;
                    if ($("#ddGroup").val() == null) {
                        obj.MP_Account = "";
                    } else { obj.MP_Account = $("#ddGroup").val().join(); }
                    $.ajax({
                        type: "POST",
                        url: "/Manager/UpdateGroup",
                        contentType: 'application/json',
                        data: JSON.stringify(obj),
                        success: function (msg) {

                            console.log(msg);
                            toastr.success("Cập nhât thành công", "Thông báo");
                            $("#hidRole").val('');
                            setTimeout(location.reload.bind(location), 2000);
                        },
                        error: function (data) {
                            return false;
                        }
                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('#txtUserLogin').bind('keyup paste', function () {
                this.value = this.value.replace(/[^0-z_]/g, '');
            });
            $(".add").hide();
            $.ajax({
                type: "POST",
                url: "/Manager/GetInfoGroupByRoleId?Id=" + $this.attr('uid') + "",
                contentType: "application/json; charset=utf-8",
                dataType: "json", success: function (msg) {
                    var html = "";
                    var item = msg.value.lstUser;
                    for (var i = 0; i < item.length; i++) {
                        html += "<option value='" + item[i].Id + "'>" + item[i].Name + " " + item[i].FullName + "</option>";
                    }
                    $("#ddGroup").html(html);
                    var userByRole = msg.value.userByRole;
                    for (var i = 0; i < userByRole.length; i++) {
                        $("#ddGroup option[value='" + userByRole[i].Id + "']").prop("selected", true);
                    }

                    $("#txtNameGroupModal").val($("." + ($this.attr('uid'))).text());

                }
            });
        }
    });

});

//Sự kiện xóa Nhóm
$('body').on('click', 'span.deleteGroup', function (e) {
    var $this = $(this);
    var id = $this.attr('uid');
    $.confirm({
        title: 'Xóa nhóm điện thoại viên',
        content: 'Bạn có muốn xóa nhóm này không?',
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/Manager/DeleteGroup?Id=" + id + "",
                        //dataType: "json",
                        success: function (msg) {
                            console.log(msg);
                            toastr.success("Xóa nhóm thành công", "Thông báo");
                            setTimeout(location.reload.bind(location), 2000);
                            return false;
                        }
                    });
                }
            },
            cancel:
            {
                text: 'Đóng',
                function() {
                },
            },
        }

    });

});

//Thêm mới Nhóm
$('body').on('click', '#btnAddGroupModal', function (e) {
    $.confirm({
        title: '<i class="fa fa-mortar-board text-green"></i>Thêm mới Nhóm',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-6 col-md-offset-3',
        content: 'url:/HtmlModel/CIC/GroupModel.html',
        buttons: {
            formSubmit: {
                text: 'Thêm mới',
                btnClass: 'btn-blue btn-add-role',
                action: function () {
                    var name = $("#txtNameGroupModal").val();
                    if (name == "") { toastr.warning("Nhập tên nhóm điện thoại viên", "Thông báo"); return false; }
                    var obj = new Object();
                    obj.NameGroup = name;
                    if ($("#ddGroup").val() == null) {
                        obj.MP_Account = "";
                    } else { obj.MP_Account = $("#ddGroup").val().join(); }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/Manager/AddGroup",
                        //dataType: "json",
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            console.log(msg);
                            toastr.success("Thêm mới thành công", "Thông báo");
                            setTimeout(location.reload.bind(location), 2000);
                            return false;
                        },

                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('#txtUserLogin').bind('keyup paste', function () {
                this.value = this.value.replace(/[^0-z_]/g, '');
            });
            $(".add").hide();
            $.ajax({
                type: "POST",
                url: "/Manager/GetInfoGroupByRoleId",
                contentType: "application/json; charset=utf-8",
                dataType: "json", success: function (msg) {
                    var html = "";
                    var item = msg.value.lstUser;
                    for (var i = 0; i < item.length; i++) {
                        html += "<option value='" + item[i].Id + "'>" + item[i].Name + " " + item[i].FullName + "</option>";
                    }
                    $("#ddGroup").html(html);
                }
            });
        }
    });
});


