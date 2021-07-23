$(function () {
    /**
     * Thiết lập cây danh mục
     * */
    bindDataToTree();
    /**
     * bind lại event cho các nút trên lưới cattype
     * 
     * */
    bindEvent4toolsInCatypeGrid();
    /**
     * bind lại event cho các nút trên lưới category
     * 
     * */
    bindEvent4toolsInCategoryGrid();
    /**
     * Sự kiện hủy cập nhật, quay lại màn hình thêm mới danh mục
     * */
    bindEventCancelUpdateCatType();
    /**
    * Sự kiện cập nhật CatTypeName
    * */
    bindEventUpdateCatTypeName();
    /**
     * Sự kiện nhập tên danh mục
     * */
    bindEventtxtName();
    /**
     * Sự kiện thêm mới CatType
     * */
    bindEventAddNewCatType();
    /**
     * Sự kiện thêm mới Category
     * */
    bindEventAddNewCategory();
    //bindEventSlectNode();
    //Xóa danh mục cha
    $('tbody#tbdCatType i.fa-trash-o').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var code = $this.attr('code');
        var name = $this.attr('name');
        if (code === '' || name === '') return;
        var objs = new Object();
        objs.CatTypeCode = code;

        $.confirm({
            title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
            content: 'Đồng ý xóa danh mục: <code>' + name + '</code>',
            type: 'red',
            buttons: {
                confirm:
                {
                    text: "Xác nhận",
                    btnClass: "btn-blue",
                    action: function () {
                        $.ajax({
                            type: "POST",
                            url: "/api/Category/DeleteCateType",
                            data: objs,
                            dataType: "json",
                            success: function (msg) {
                                if (msg.status === 'notfound') toastr.warning("không tìm thấy danh mục", "Thông báo");
                                else if (msg.status === 'ok') {
                                    Refresh();
                                    toastr.success("Xóa danh mục thành công", "Thông báo");
                                    $('#ajax').jstree("destroy").empty();
                                    bindDataToTree();
                                }
                                else console.log(msg);
                            }
                        });
                    }
                },
                cancel: {
                    text: "Hủy bỏ",
                    btnClass: "btn-default",
                    action: function (e) {
                    }
                }
            }
        });

    });

});
var idCategory;
/**
 * Thiết lập cây danh mục
 * */
var tblCatType; var tblCategory;
function bindDataToTree() {
    $('#ajax').jstree({
        'core': {
            'themes': {
                'name': 'proton',
                'responsive': true
            },
            'data': {
                "type": "GET",
                "url": "/Manager/Category/GetTree",
                "dataType": "json"
            }
        }
    });
    bindEventLoadTree();
    bindEventSlectNode();
}
function bindEventLoadTree() {
    $('#ajax').bind('loaded.jstree', function (node, ref) {
        var allNode = $('#ajax').jstree(true).get_json('#', { flat: true });
        var dataineed = [];
        $.each(allNode, function (idx, val) {
            if (val.data.CatTypeCode) dataineed.push(val.data);
        });
        if (tblCatType) {
            tblCatType.destroy();
            tblCatType = undefined;
            $('#tbdCatType').empty();
        }
        bindDataTotblCatType(dataineed);

    }).jstree();
}
function bindEventSlectNode() {
    $("#ajax").bind("select_node.jstree", function (evt, val) {
        var item = val.node.data;
        if (item && item.CatTypeId) {
            $('.category-panel').show();
            $('.cattype-panel').hide();
            $('code#lblTitle').html(item.CatTypeName + ' (' + item.CatTypeId + ')');
            $('#txtCatCode').val(item.CatTypeId);
            $("#txtCatName").val('');
            changeStatus(val.node.id, 'disable');
            idCategory = item.CatTypeId;
            $.ajax({
                type: "GET",
                url: "/Manager/Category/GetCategoryByCatType/" + item.CatTypeId,
                dataType: "json",
                success: function (msg) {
                    if (msg) {
                        bindDataTotblCategory(msg);
                        $('#btnAddCategory').attr('typeCode', item.CatTypeId).show();
                        $('#btnUpdateCategory').attr('typeCode', item.CatTypeId);
                        $('#btnUpdateCategory, #btnCancelUpdate').hide();
                    }
                }, complete: function () {
                    changeStatus(val.node.id, 'enable');
                }
            });
        } else {
            $('.category-panel').hide();
            $('.cattype-panel').show();
        }
    });
}

/**
 * Sự kiện thêm mới Category
 * */
function bindEventAddNewCategory() {
    $('#btnAddCategory').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var catTypeCode = $this.attr('typeCode');
        var name = $('#txtCatName').val();
        var code = $('#txtCatCode').val();
        var description = $('#txtCatDescription').val().trim();
        if (catTypeCode === '' || code === '' || name === '') {
            toastr.info("Yêu cầu nhập đầy đủ thông tin", "Thông báo");
            return;
        }
        if (!ValidateCatCode(code)) {
            toastr.warning("Sai định dạng mã danh mục");
            return;
        }
        var objs = new Object();
        objs.CatTypeId = catTypeCode;
        objs.Name = name;
        objs.Code = code;
        objs.ExpandProperties = description;

        $.confirm({
            title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
            content: 'Đồng ý thêm danh mục: <code>' + name + '</code>',
            type: 'red',
            buttons: {
                confirm: {
                    text: "Xác nhận",
                    btnClass: "btn-blue",
                    action: function () {
                        var itemDisableds = [$this];
                        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
                        mylop.start();
                        $.ajax({
                            type: "POST",
                            url: "/Manager/Category/Insert",
                            data: objs,
                            dataType: "json",
                            success: function (msg) {
                                Refresh();
                                $("#txtCatCode").val('');
                                $("#txtCatName").val('');
                                toastr.success("Thêm mới danh mục thành công", "Thông báo");
                                LoadListCategory(catTypeCode);
                            }, complete: function () {
                                mylop.stop();
                            }
                        });
                    }
                },
                cancel: {
                    text: "Hủy bỏ",
                    btnClass: "btn-default",
                    action: function (e) {
                    }
                }
            }
        });


    });
    $('#btnUpdateCategory').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var catTypeCode = $this.attr('typeCode');
        var name = $('#txtCatName').val();
        var code = $('#txtCatCode').val();
        var des = $('#txtCatDescription').val().trim();
        if (catTypeCode === '' || code === '' || name === '') {
            toastr.info("Yêu cầu nhập đầy đủ thông tin", "Thông báo");
            return;
        }
        var objs = new Object();
        objs.Id = catId;
        objs.Name = name;
        objs.Code = code;
        objs.ExpandProperties = des;
        $.confirm({
            title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
            content: 'Đồng ý cập nhật danh mục: <code>' + name + '</code>',
            type: 'red',
            buttons: {
                confirm:
                {
                    text: "Xác nhận",
                    btnClass: "btn-blue",
                    action: function () {
                        var itemDisableds = [$this];
                        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
                        mylop.start();
                        $.ajax({
                            type: "POST",
                            url: "/Manager/Category/Update",
                            data: objs,
                            dataType: "json",
                            success: function (msg) {
                                Refresh();
                                $("#txtCatName").val('');
                                $("#txtCatCode").val('');
                                $("#txtCatDescription").val('');
                                toastr.success("Cập nhật danh mục thành công", "Thông báo");
                                LoadListCategory(code);
                            },
                            complete: function () {
                                mylop.stop();
                            }
                        });
                    }
                },
                cancel: {
                    text: "Hủy bỏ",
                    btnClass: "btn-default",
                    action: function (e) {
                    }
                }
            }
        });
    });
}

/**
 * bind dữ liệu vào lưới category
 * @param {any} item item
 */
function bindDataTotblCategory(item) {
    if (tblCategory) {
        tblCategory.destroy();
        tblCategory = undefined;
        $('#tbdCategory').empty();
    }
    for (var i = 0; i < item.length; i++) {
        cleanObject(item[i]);
        var htm = '<tr><td>' + (i + 1) + '</td><td>' + item[i].Name + '</td><td>' + item[i].Code + '</td>' +
            '<td><i class="fa fa-edit fa-lg text-green mp-pointer-st" name="' + item[i].Name + '" des="' + item[i].ExpandProperties + '" code="' + item[i].Code + '" catid="' + item[i].Id + '" title="Edit"></i></td>' +
            '<td><i class="fa fa-trash-o fa-lg text-red mp-pointer-st" name="' + item[i].Name + '" code="' + item[i].Code + '" catid="' + item[i].Id + '" title="Delete"></i></td></tr>';
        $('#tbdCategory').append(htm);
    }
    tblCategory = $('table.tblCategory').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "pageLength": 10
    });
}


/**
 * bind dữ liệu vào lưới cattype
 * @param {any} item item
 */
function bindDataTotblCatType(item) {
    if (tblCatType) {
        tblCatType.destroy();
        tblCatType = undefined;
        $('#tbdCatType').empty();
    }
    $('#ddlPhanNhom').select2('destroy').empty();
    for (var i = 0; i < item.length; i++) {
        cleanObject(item[i]);
        var htm = '<tr><td>' + (i + 1) + '</td><td>' + item[i].CatTypeCode + '</td><td>' + item[i].CatTypeName + '</td>' +
            '<td><i class="fa fa-edit fa-lg text-green mp-pointer-st" name="' + item[i].CatTypeName + '" code="' + item[i].CatTypeCode + '" title="Edit"></i></td>' +
            '<td><i class="fa fa-trash-o fa-lg text-red mp-pointer-st" name="' + item[i].CatTypeName + '" code="' + item[i].CatTypeCode + '" title="Delete"></i></td></tr>';
        $('#tbdCatType').append(htm);
        //Comboxbox
        $('#ddlPhanNhom').append('<option value="' + item[i].CatTypeCode + '">' + item[i].CatTypeName + '</option>');

    }
    tblCatType = $('table.tblCatType').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "pageLength": 10
    });
    $('#ddlPhanNhom').prepend('<option selected="selected" value = "" > -Chọn -</option > ');
    $('#ddlPhanNhom').select2();
}
var catId = "";
/**
 * bind lại event cho các nút trên lưới cattype
 * 
 * */
function bindEvent4toolsInCatypeGrid() {
    $('table.tblCatType').on('draw.dt', function () {
        //Cập nhật CatTypeName
        $('table.tblCatType i.fa-edit').click(function (e) {
            e.preventDefault();
            var $this = $(this);
            var code = $this.attr('code');
            var name = $this.attr('name');
            if (code === '' || name === '') return;
            $('.PhanNhom-panel').hide();

            $('#txtName').val(name);
            $('div.cattype-panel .update').html('Cập nhật: <code>' + name + '</code>').show();
            $('div.cattype-panel .add').hide();
            $('button#btnAddNewCatType').hide();
            $('button#btnUpdateCatType').attr('code', code).show();
            $('button#btnCancelUpdateCatType').show();
        });
        //Xóa danh mục
        $('table.tblCatType i.fa-trash-o').click(function (e) {
            e.preventDefault();
            var $this = $(this);
            var code = $this.attr('code');
            var name = $this.attr('name');
            if (code === '' || name === '') return;
            var objs = new Object();
            objs.CatTypeCode = code;

            $.confirm({
                title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
                content: 'Đồng ý xóa danh mục: <code>' + name + '</code>',
                type: 'red',
                buttons: {
                    confirm:
                    {
                        text: "Xác nhận",
                        btnClass: "btn-blue",
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "/api/Category/DeleteCateType",
                                data: objs,
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.status === 'notfound') toastr.info("Không tìm thấy tên danh mục", "Thông báo");
                                    else if (msg.status === 'ok') {
                                        Refresh();
                                        toastr.success("Xóa danh mục thành công", "Thông báo");
                                        $('#ajax').jstree("destroy").empty();
                                        LoadListCategory(catTypeCode);
                                    }
                                    else console.log(msg);
                                }
                            });
                        }
                    },
                    cancel: {
                        text: "Hủy bỏ",
                        btnClass: "btn-default",
                        action: function (e) {
                        }
                    }
                }
            });

        });
    });
}

/**
 * bind lại event cho các nút trên lưới category
 * 
 * */
function bindEvent4toolsInCategoryGrid() {

    $('table.tblCategory').on('draw.dt', function () {
        //Cập nhật CatTypeName
        $('tbody#tbdCategory i.fa-edit').click(function (e) {
            e.preventDefault();
            var $this = $(this);
            var code = $this.attr('code');
            var name = $this.attr('name');
            var des = $this.attr('des');
            catId = $this.attr('catid');
            if (code === '' || name === '') return;
            $('#txtName').val(name);
            $('div#lblTitle').html('<code>' + name + '</code>').show();
            $('#btnAddCategory').hide();
            $('#btnUpdateCategory, #btnCancelUpdateCategory').show();
            $('#txtCatCode').val(code);
            $('#txtCatName').val(name);
            $('#txtCatDescription').val(des);


            $('div.cattype-panel .add').hide();
            $('button#btnAddNewCatType').hide();
            $('button#btnUpdateCatType').attr('code', code).show();
            $('button#btnCancelUpdateCatType').show();
        });
        //Xóa danh mục con
        $('tbody#tbdCategory i.fa-trash-o').click(function (e) {
            e.preventDefault();
            var $this = $(this);
            var catid = $this.attr('catid');
            var name = $this.attr('name');
            if (catid === '' || name === '') return;
            $.confirm({
                title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
                content: 'Đồng ý xóa danh mục: <code>' + name + '</code>',
                type: 'red',
                buttons: {
                    confirm:
                    {
                        text: "Xác nhận",
                        btnClass: "btn-blue",
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "/Manager/Category/Delete/" + catid,
                                dataType: "json",
                                success: function (msg) {
                                    toastr.success("Xóa danh mục thành công", "Thông báo");
                                }
                            });
                        }
                    },
                    cancel: {
                        text: "Hủy bỏ",
                        btnClass: "btn-default"
                    }
                }
            });

        });
    });
}

/**
 * Sự kiện hủy cập nhật, quay lại màn hình thêm mới danh mục
 * */
function bindEventCancelUpdateCatType() {
    $('button#btnCancelUpdateCatType').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        $('#txtName').val('');
        $('div.cattype-panel .update').hide();
        $('div.cattype-panel .add').show();
        $('button#btnAddNewCatType').show();
        $('button#btnUpdateCatType').attr('code', '').hide();
        $('button#btnCancelUpdateCatType').hide();
        $('.PhanNhom-panel').show();
    });
}

function bindEventCancelUpdateCategory() {
    $('button#CancelUpdateCategory').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        $('#txtName').val('');
        $('div.cattype-panel .update').hide();
        $('div.cattype-panel .add').show();
        $('button#btnAddNewCatType').show();
        $('button#btnUpdateCatType').attr('code', '').hide();
        $('button#btnCancelUpdateCatType').hide();
        $('.PhanNhom-panel').show();
    });
}
/**
 * Sự kiện thêm mới CatType
 * */
function bindEventAddNewCatType() {
    $('button#btnAddNewCatType').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var name = $('#txtName').val();
        var code = $('#ddlPhanNhom').val();
        if (name === '') {
            toastr.info("Vui lòng nhập đủ thông tin", "Thông báo");
            return;
        }
        var objs = new Object();
        objs.Name = name;
        objs.grou = code;

        $.confirm({
            title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
            content: 'Đồng ý thêm danh mục: <code>' + name + '</code>',
            type: 'red',
            buttons: {
                confirm:
                {
                    text: "Xác nhận",
                    btnClass: "btn-blue",
                    action: function () {
                        var itemDisableds = [$this];
                        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
                        mylop.start();
                        $.ajax({
                            type: "POST",
                            url: "/Manager/Category/AddCatType",
                            data: objs,
                            dataType: "json",
                            success: function (msg) {
                                Refresh();
                                toastr.success("Thêm mới thành công", "Thông báo");
                                $('#ajax').jstree("destroy").empty();
                                bindDataToTree();
                            },
                            error: function (msg) {
                            },
                            complete: function () {
                                mylop.stop();
                            }
                        });
                    }
                },
                cancel: {
                    text: "Hủy bỏ",
                    btnClass: "btn-default",
                    action: function (e) {
                    }
                }
            }
        });
    });
}

/**
 * Sự kiện cập nhật CatTypeName
 * */
function bindEventUpdateCatTypeName() {
    $('button#btnUpdateCatType').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var name = $('#txtName').val();
        if (name && name === '') {
            toastr.info("Nhập loại danh mục", "Thông báo");
            return;
        }
        var code = $this.attr('code');
        if (code === '' || name === '') return;

        var objs = new Object();
        objs.CatTypeName = name;
        objs.CatTypeCode = code;

        $.confirm({
            title: '<i class="fa fa-question-circle fa-lg text-red"></i> Yêu cầu xác nhận',
            content: 'Đồng ý thay đổi thông tin danh mục?',
            type: 'red',
            buttons: {
                confirm:
                {
                    text: "Xác nhận",
                    btnClass: "btn-blue",
                    action: function () {
                        var itemDisableds = [$this];
                        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
                        mylop.start();
                        $.ajax({
                            type: "POST",
                            url: "/api/Category/UpdateCateTypeName",
                            data: objs,
                            dataType: "json",
                            success: function (msg) {
                                if (msg.status === 'exits') toastr.warning("Trùng tên danh mục", "Thông báo");
                                else if (msg.status === 'notfound') toastr.info("Không tìm thấy loại danh mục", "Thông báo");
                                else if (msg.status === 'ok') {
                                    toastr.success("Cập nhật thành công", "Thông báo");
                                    Refresh();
                                    $('div.cattype-panel .update').html('Cập nhật: <code>' + name + '</code>').hide();
                                    $('div.cattype-panel .add').show();
                                    $('button#btnAddNewCatType').show();
                                    $('.PhanNhom-panel').show();
                                    $('button#btnUpdateCatType').hide();
                                    $('button#btnCancelUpdateCatType').hide();
                                    $('#ajax').jstree("destroy").empty();
                                    bindDataToTree();
                                }
                                else console.log(msg);
                                //changeStatus(val.node.id, 'enable');
                            }, complete: function () {
                                mylop.stop();
                            }
                        });
                    }
                },
                cancel: {
                    text: "Hủy bỏ",
                    btnClass: "btn-default",
                    action: function (e) {
                    }
                }
            }
        });
    });
}

/**
 * Sự kiện nhập tên danh mục
 * */
function bindEventtxtName() {
    $('#txtName').change(function (e) {
        e.preventDefault();
        $('code.lbCatTypeHelper').hide();
    });
}

/**
 * enable/disable node trên cây danh mục
 * @param {any} node_id item
 * @param {any} changeTo item
 */
function changeStatus(node_id, changeTo) {
    var node = $("#ajax").jstree().get_node(node_id);
    if (changeTo === 'enable') {
        $("#ajax").jstree().enable_node(node);
        node.children.forEach(function (child_id) {
            changeStatus(child_id, changeTo);
        });
    } else {
        $("#ajax").jstree().disable_node(node);
        node.children.forEach(function (child_id) {
            changeStatus(child_id, changeTo);
        });
    }
}
function Refresh() {
    $("input#txtName").val('');
    $('div.cattype-panel .update').html('Cập nhật: <code>' + name + '</code>').hide();
    $('div.cattype-panel .add').show();
    $('button#btnAddNewCatType').show();
    $('.PhanNhom-panel').show();
    $('button#btnUpdateCatType').hide();
    $('button#btnCancelUpdateCatType').hide();
}
function LoadListCategory(cattypecode) {
    $.ajax({
        type: "GET",
        url: "/Manager/Category/GetCategoryByCatType/" + cattypecode,
        dataType: "json",
        success: function (msg) {
            if (msg) {
                bindDataTotblCategory(msg);
                $('#btnAddCategory').attr('typeCode', cattypecode).show();
                $('#btnUpdateCategory').attr('typeCode', cattypecode);
                $('#btnUpdateCategory, #btnCancelUpdate').hide();
            }
        }
    });
}

function ValidateCatCode(CatCode) {
    if (CatCode.length >= 1 && CatCode.length <= 30) {
        var re = /^[a-zA-Z0-9]+[\w\-\._~:/?#[\]@!^\$&'%\(\)\*\+,;=.]+$/gm;
        return re.test(CatCode);
    }
    return false;
}