$(function () {
    /**
        * Sự kiện chọn khu vực ở thông tin sự vụ
        * */
    $('select#ddlAreas').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        $('select#ddlDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
        SelectCustomerArea(item, null);
    });

    /**
    * Sự kiện chọn tỉnh thành ở thông tin sự vụ
    * */
    $('select#ddlProvince').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        SelectCustomerProvince(item, null);
    });

    /**
     * Lấy danh sách khách hàng
     * */
    LoadData();
    $('button.search').click(function (e) {
        e.preventDefault();
        LoadData();
    });


    // Add event listener for opening and closing details
    $('table.customer tbody').on('click', 'i.edit-customer', function (e) {
        var tr = $(this).closest('tr');
        var row = tableCustomer.row(tr);
        console.log(row.data().Id);
        PopUpdateCustomer(row.data().Id);
    });


});

/**
 * Lấy danh sách khách hàng
 * */
var tableCustomer;
function LoadData() {
    var obj = new Object();
    obj.Type = $('#ddlType').val();
    obj.Code = $('#txtCustomerCode').val().trim();
    obj.Area = $('#ddlAreas').val();
    obj.Province = $('#ddlProvince').val();
    obj.District = $('#ddlDistrict').val();
    if (tableCustomer) {
        tableCustomer.destroy();
        $('table.customer tbody').empty();
    }

    var itemDisableds = [];
    var mylop = new myMpLoop($('button.search'), 'Đang xử lý', $('button.search').html(), itemDisableds);
    mylop.start();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/manager/SearchCustomer',
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (msg) {
            var textEdit = msg.allowEdit ? '<i class="fa fa-edit fa-lg edit-customer text-info mp-pointer-st"></i>' : '';
            tableCustomer = $('table.customer').DataTable({
                "processing": true,
                "searching": false,
                "data": msg.value,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "RetailCMTyple_Des",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "NumSeqRetailCM",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "RetailCMName",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "PHONE",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "SEGMENTID_Name",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "SUBSEGMENTID_name",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "SALESDISTRICTID_Name",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "STREET",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "className": 'details-control text-center vertical-middle',
                        "orderable": false,
                        "data": null,
                        "defaultContent": textEdit
                    }
                ],
                "order": [[0, 'asc']],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 10
            });


        },
        complete: function () {
            mylop.stop();
        }
    });




}


/**
 * Popup chỉnh sửa thông tin khách hàng, cửa hàng đại lý
 * @param {any} id
 */
function PopUpdateCustomer(id) {
    var popUpdate = $.confirm({
        title: '<i class="fa fa-bank text-green"></i> Cập nhật thông tin khách hàng, cửa hàng',
        type: 'blue',
        id: 'confirmUpdateAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/DPL/EditCustomer.html',
        buttons: {
            formSubmit: {
                text: 'Cập nhật',
                btnClass: 'btn-blue btn-flat btn-edit-dpl-customer',
                action: function () {
                    var obj = new Object();
                    obj.id = id;
                    obj.NumSeqRetailCM = $('#txtRPopCreateStoreCode').val();
                    obj.PHONE = $('#txtRPopCreateStorePhone').val();
                    obj.SEGMENTID = $('#ddlRPopCreateStoreArea').val() === '-Chọn-' ? '' : $('#ddlRPopCreateStoreArea').val();
                    obj.STREET = $('#txtRPopCreateStoreAddress').val().trim();
                    obj.SUBSEGMENTID = $('#ddlRPopCreateStoreProvince').val() === '-Chọn-' ? '' : $('#ddlRPopCreateStoreProvince').val();
                    obj.RetailCMName = $('#txtRPopCreateStoreName').val().trim();
                    obj.SALESDISTRICTID = $('#ddlRPopCreateStoreDistrict').val() === '-Chọn-' ? '' : $('#ddlRPopCreateStoreDistrict').val();
                    obj.RetailCMTyple = $('#ddlRPopCreateStoreType').val();
                    obj.ContactName = $('#txtRPopCreateContactName').val();
                    if (!obj.NumSeqRetailCM || !obj.PHONE || !obj.SEGMENTID || !obj.STREET || !obj.SUBSEGMENTID
                        || !obj.RetailCMName || !obj.SALESDISTRICTID || !obj.RetailCMTyple) {
                        toastr.warning('Vui lòng nhập đầy đủ thông tin', 'Cảnh báo');
                        return false;
                    }

                    var itemDisableds = [$('button.btn-edit-dpl-customer')];
                    var mylop = new myMpLoop($('button.btn-edit-dpl-customer'), 'Đang xử lý', $('button.btn-edit-dpl-customer').html(), itemDisableds);
                    mylop.start();

                    $.ajax({
                        type: "POST",
                        url: "/Manager/UpdateCustomer",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            switch (msg.status) {
                                case "ok":
                                    //Đóng popup
                                    popUpdate.close();

                                    toastr.options.progressBar = true;
                                    toastr.info("Cập nhật khách hàng thành công.", 'Thông báo');
                                    setTimeout(function () {
                                        location.reload();
                                    }, 3200);
                                    break;
                                case "notok":
                                    toastr.warning("Không thể cập nhật khách hàng trên core. Vui lòng liên hệ quản trị.", "Thông báo");
                                    return false;
                                    break;
                                default:
                                    toastr.warning("Tiến trình bị lỗi", "Cảnh báo");
                                    return false;
                                    break;
                            }
                            return false;


                        },
                        complete: function () {
                            mylop.stop();
                        }
                    });
                    return false;
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            //Lấy thông tin khách hàng theo id
            $.ajax({
                type: "GET",
                url: '/manager/GetCustomerById/' + id,
                dataType: "json",
                success: function (msg) {
                    console.log(msg);
                    $('#ddlRPopCreateStoreType').attr('disabled', 'true');
                    $('#ddlRPopCreateStoreType').val(msg.RetailCMTyple).trigger('change.select2');
                    $('#txtRPopCreateStoreCode').val(msg.NumSeqRetailCM);
                    $('#txtRPopCreateStoreName').val(msg.RetailCMName);
                    $('#txtRPopCreateStorePhone').val(msg.PHONE);
                    $('#txtRPopCreateContactName').val(msg.ContactName);
                    $('#txtRPopCreateStoreAddress').val(msg.STREET);

                    //Lấy thông tin khu vực, tỉnh, huyện
                    $.ajax({
                        type: "GET",
                        url: '/receive/GetAllArea',
                        dataType: "json",
                        success: function (msgArea) {
                            $('select#ddlRPopCreateStoreArea').select2({
                                data: msgArea,
                                dropdownParent: $('.jconfirm-no-transition')
                            }).val(msg.SEGMENTID).trigger('change.select2');

                            SelectCreateStoreArea(msg.SEGMENTID, msg.SUBSEGMENTID, msg.SALESDISTRICTID);

                            /**
                             * Lấy thông tin tỉnh thành theo khu vực trong popup tạo mới KH, đại lý
                             * @param {any} area
                             */
                            $('select#ddlRPopCreateStoreArea').change(function (e) {
                                e.preventDefault();
                                $('#ddlRPopCreateStoreProvince').val('').change();
                                var value = $(this).val();
                                SelectCreateStoreArea(value, null, null);
                            });
                        }
                    });
                }
            });
        }
    });
}


/**
 * Lấy thông tin tỉnh thành theo khu vực ở thông tin sự vụ
 * @param {any} area
 * @param {any} selectedProvince
 */
function SelectCustomerArea(area, selectedProvince) {
    $.ajax({
        type: "GET",
        url: '/receive/getprovincebyarea/' + area + "/" + selectedProvince,
        dataType: "json",
        success: function (msg) {
            $('select#ddlProvince').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlProvince').select2({ data: msg });
        }
    });
}


/**
 * Lấy thông tin huyện theo tỉnh của thông tin sự vụ
 * @param {any} province
 * @param {any} selectedDistrict
 */
function SelectCustomerProvince(province, selectedDistrict) {
    $.ajax({
        type: "GET",
        url: '/receive/getdistrictbyprovince/' + province + "/" + selectedDistrict,
        dataType: "json",
        success: function (msg) {
            $('select#ddlDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlDistrict').select2({ data: msg });
        }
    });
}


/**
 * Lấy thông tin tỉnh thành theo khu vực trong popup tạo mới KH, đại lý
 * @param {any} area
 * @param {any} selectedProvince
 * @param {any} selectedDistrict
 */
function SelectCreateStoreArea(area, selectedProvince, selectedDistrict) {
    $.ajax({
        type: "GET",
        url: '/receive/getprovincebyarea/' + area + "/" + selectedProvince,
        dataType: "json",
        success: function (msg) {
            $('select#ddlRPopCreateStoreProvince').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRPopCreateStoreProvince').select2({
                data: msg,
                dropdownParent: $('.jconfirm-no-transition')
            });

            SelectCreateStoreProvince(selectedProvince, selectedDistrict ? selectedDistrict : null);

            $('select#ddlRPopCreateStoreProvince').change(function (e) {
                e.preventDefault();
                var value = $(this).val();
                /**
                 * Lấy thông tin huyện theo tỉnh cho popup tạo mới KH
                 * @param {any} province
                 * @param {any} selectedDistrict
                */
                SelectCreateStoreProvince(value, null);
            });
        }
    });
}


/**
 * Lấy thông tin huyện theo tỉnh cho popup tạo mới KH
 * @param {any} province
 * @param {any} selectedDistrict
 */
function SelectCreateStoreProvince(province, selectedDistrict) {
    $.ajax({
        type: "GET",
        url: '/receive/getdistrictbyprovince/' + province + "/" + selectedDistrict,
        dataType: "json",
        success: function (msg) {
            $('select#ddlRPopCreateStoreDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRPopCreateStoreDistrict').select2({
                data: msg,
                dropdownParent: $('.jconfirm-no-transition')
            });
        }
    });
}