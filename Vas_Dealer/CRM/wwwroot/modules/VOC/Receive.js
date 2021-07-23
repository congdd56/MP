$(function () {

    /**
    * Lưu thông tin khách hàng
    * */
    SaveReceiveEvent();
    /**
     * Thay đổi mục đích
     * */
    ChangePurposeEvent();
});

/**
 * Lưu thông tin khách hàng
 * */
function SaveReceiveEvent() {
    $('button.add-info-receive').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        /**
        * Lấy thông tin tiếp nhận sự vụ
        * */
        var objTicket = GetObjectReveive();
        /**
         * Validate trước khi lưu tiếp nhận
         * @param {any} item
         */
        if (!ValidateReveive(objTicket)) {
            return;
        }

        SaveReceive($this, objTicket);
    });
}

/**
 * Lấy thông tin tiếp nhận sự vụ
 * */
function GetObjectReveive() {
    var obj = new Object();
    obj.TicketId = $('#txtRTicketId').val().trim();
    obj.CallerPhone = $('#txtRCallerPhone').val().trim();
    obj.CallerEmail = $('#txtREmail').val().trim();

    obj.TicketChannel = $('#ddlRChanel').val();
    obj.TicketSource = $('#ddlRSource').val();
    obj.TicketPurpose = $('#ddlRPurpose').val();
    obj.TicketArea = $('#ddlRCustomerArea').val();
    obj.TicketProvince = $('#ddlRCustomerProvince').val();
    obj.TicketDistrict = $('#ddlRCustomerDistrict').val();
    obj.TicketAddress = $('#txtRCustomerAddress').val().trim();

    //Customer
    obj.ConfirmCustomerCode = $('#txtRIStoreCode').val().trim();
    obj.ConfirmCustomerCodeId = $('#hddRCustomerCode').val().trim();
    //Store
    obj.ConfirmStoreCode = $('#txtRStoreCode').val().trim();
    obj.ConfirmStoreCodeId = $('#hddRStoreCode').val().trim();

    obj.ProductBrand = $('#ddlRBrandProduct').val();
    obj.ProductType = $('#ddlRProductType').val();
    obj.ProductModel = $('select#ddlRProductModel').val() ? $('select#ddlRProductModel').select2('data')[0].text : "";
    obj.ProductModel = obj.ProductModel === '-Chọn-' ? '' : obj.ProductModel;
    obj.ProductModel2 = $('select#ddlRProductModel2').val() ? $('select#ddlRProductModel2').select2('data')[0].text : "";
    obj.ProductModel2 = obj.ProductModel2 === '-Chọn-' ? '' : obj.ProductModel2;
    obj.ProductSeri2 = $('#txtRProductSeri2').val().trim();
    obj.ProductCode = $('#txtRProductCode').val().trim();
    obj.ProductCode2 = $('select#ddlRProductModel2').val() == '-Chọn-' ? '' : $('select#ddlRProductModel2').val();
    obj.ProductColor = $('#ddlRProductColor').val();
    obj.ProductColor = obj.ProductColor === '-Chọn-' ? '' : obj.ProductColor;

    obj.ProductSeri = $('#txtRProductSeriBH').val().trim();
    obj.ProductSeriCheckContext = $('i.icheck-found-item').attr('data-original-title');
    obj.ProductExpiredDateStr = $('#txtRPRoductExpiredDate').val().trim();
    obj.ProductHadDocument = $('#chkRHasDocument').iCheck('update')[0].checked;
    obj.ProductPurchaseCheckContent = $('i.icheck-document-found-item').attr('data-original-title');
    obj.ProductPurchaseDateStr = $('#txtRProductBuyDate').val().trim();
    obj.ProductDocType = $('#ddlRDocutmentType').val();
    obj.ProductOtherDocType = $('#ddlROtherDocutmentType').val();
    obj.ProductDocNum = $('#txtRInvoiceNumber').val().trim();
    obj.ProductStatus = $('#ddlRProductStatus').val();//Tình trạng hàng
    obj.CurrentTicketStatus = $('#ddlRCurrentTicketStatus').val();//Hiện trạng xử lý
    obj.ProductCrashStatus = $('#ddlRCrashStatus').val();//Tình trạng hư hỏng
    obj.ReceiveContent = $('#txtRCustomerDiscus').val().trim();//Nội dung trao đổi với KH
    obj.TicketAttach = $('#hddTicketTemplate').val();

    obj.MPLoadElSerial = $('#hdCheckWaranty').val();

    return obj;
}

/**
 * Validate trước khi lưu tiếp nhận
 * @param {any} obj
 */
function ValidateReveive(obj) {
    //Mục đích
    if (!obj.TicketPurpose) {
        toastr.warning('Vui lòng chọn mục đích', 'Cảnh báo');
        return false;
    }
    //Mã khách hàng
    if (!obj.ConfirmCustomerCode) {
        toastr.warning('Vui lòng chọn thông tin khách hàng', 'Cảnh báo');
        return false;
    }
    //Kênh tiếp nhận
    if (!obj.TicketChannel) {
        toastr.warning('Vui lòng chọn kênh tiếp nhận', 'Cảnh báo');
        return false;
    }
    //Nguồn nhận
    if (!obj.TicketSource) {
        toastr.warning('Vui lòng chọn nguồn nhận', 'Cảnh báo');
        return false;
    }
    //Nội dung trao đổi với KH
    if (!obj.ReceiveContent) {
        toastr.warning('Vui lòng nhập nội dung trao đổi với khách hàng', 'Cảnh báo');
        return false;
    }

    //Nếu không phải mục đích vận hành thì validate theo DPL
    if (obj.TicketPurpose.indexOf('1016_') == -1) {
        //Loại ngành hàng
        if (!obj.ProductType) {
            toastr.warning('Vui lòng chọn loại ngành hàng', 'Cảnh báo');
            return false;
        }
        //Model
        if (!obj.ProductModel) {
            toastr.warning('Vui lòng chọn model sản phẩm', 'Cảnh báo');
            return false;
        }
        //Kiểm tra nhập chứng từ
        if (obj.ProductHadDocument) {
            if (!obj.ProductPurchaseDateStr) {
                toastr.warning('Vui lòng nhập thời gian mua hàng', 'Cảnh báo');
                return false;
            }
            if (!obj.ProductDocType) {
                toastr.warning('Vui lòng chọn loại chứng từ', 'Cảnh báo');
                return false;
            }
            if (obj.ProductDocType === '16_Khac' && !obj.ProductOtherDocType) {
                toastr.warning('Vui lòng chọn loại chứng từ khác', 'Cảnh báo');
                return false;
            }
            if (obj.ProductDocType !== '16_Khac' && !obj.ProductDocNum) {
                toastr.warning('Vui lòng nhập số hóa đơn', 'Cảnh báo');
                return false;
            }
            if (!pond1.getFile()) {
                toastr.warning('Vui lòng tải file hóa đơn', 'Cảnh báo');
                return false;
            }
        }
        //Tình trạng hàng
        if (!obj.ProductStatus) {
            toastr.warning('Vui lòng chọn tình trạng hàng', 'Cảnh báo');
            return false;
        }
        //Hiện trạng xử lý
        if (!obj.CurrentTicketStatus) {
            toastr.warning('Vui lòng chọn hiện trạng xử lý', 'Cảnh báo');
            return false;
        }

        return true;
    }
    return true;

}

/**
 * Lưu thông tin tiếp nhận
 * @param {any} control
 * @param {any} item
 */
function SaveReceive(control, item) {
    $.confirm({
        title: 'Xác nhận!',
        content: 'Đồng ý lưu thông tin tiếp nhận?',
        buttons: {
            confirm: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                keys: ['enter'],
                action: function () {
                    var itemDisableds = [];
                    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
                    mylop.start();
                    $.ajax({
                        type: "POST",
                        url: '/receive/SaveReceive',
                        dataType: "json",
                        data: item,
                        success: function (msg) {
                            if (msg.value.Value) {//Trả về khi tạo mới
                                $.confirm({
                                    title: '<i class="fa fa-question-o text-red"></i> Thông báo',
                                    content: msg.value.Message,
                                    type: 'success',
                                    typeAnimated: true,
                                    buttons: {
                                        tryAgain: {
                                            text: 'OK',
                                            btnClass: 'btn-success',
                                            action: function () {
                                                location.href = "/receive/" + msg.value.Value;
                                            }
                                        }
                                    }
                                });
                            } else {
                                toastr.info(msg.value.Message, 'Thông báo');
                            }
                        },
                        complete: function (msg) {
                            mylop.stop();
                        }
                    });
                }
            },
            Hủy: {
                text: 'Hủy',
                btnClass: 'btn-warning',
                keys: ['esc'],
            }
        }
    });
}

/**
 * Thay đổi mục đích
 * */
function ChangePurposeEvent() {
    $('select[id="ddlRPurpose"]').change(function (e) {
        e.preventDefault();
        if ($(this).val() && $(this).val().indexOf('1016_') == -1) {
            $('span.text-red.optional').show();
        } else {
            $('span.text-red.optional').hide();
        }

    });
}