FilePond.registerPlugin(

    // encodes the file as base64 data
    FilePondPluginFileEncode,

    // validates the size of the file
    FilePondPluginFileValidateSize,

    FilePondPluginFileValidateType,

    // corrects mobile image orientation
    FilePondPluginImageExifOrientation,

    // previews dropped images
    FilePondPluginImagePreview,

    //FilePondPluginImageOverlay,

    FilePondPluginGetFile
);
// Select the file input and use create() to turn it into a pond
const pond1 = FilePond.create(
    document.querySelector('input[name="filepond-invoice"]'), {
    labelIdle: '<i class="fa fa-link text-blue"></i> File hóa đơn <span class="filepond--label-action">chọn..</span>',
    acceptedFileTypes: ['image/png', 'image/jpg', 'image/jpeg', 'application/pdf'],
    allowFileTypeValidation: true,
    imagePreviewHeight: 80,
    allowDownloadByUrl: true,
    beforeRemoveFile: function (e) {
        return confirm('Xác nhận xóa file hóa đơn?\r\n***Bạn không thể lấy lại');
    },
    labelFileTypeNotAllowed: 'Sai đinh dạng, yêu cầu file ảnh, PDF',
    onaddfilestart: function (e) {
        $('label.lb-invoice').css('display', 'block');
    },
    onremovefile: function (e) {
        $('label.lb-invoice').css('display', 'none');
    },
    server: {
        process: (fieldName, file, metadata, load, error, progress, abort, transfer, options) => {
            const formData = new FormData();
            formData.append('files', file);
            formData.append('tempfolder', $('input#hddTicketTemplate').val());
            formData.append('type', "1");

            const request = new XMLHttpRequest();
            request.open('POST', '/Receive/UploadFile');
            request.upload.onprogress = (e) => {
                progress(e.lengthComputable, e.loaded, e.total);
            };
            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    load(request.responseText);
                }
                else {
                    error('oh no');
                }
            };
            request.send(formData);
            return {
                abort: () => {
                    request.abort();
                    abort();
                }
            };
        },
        load: '/Receive/LoadUploadFile/',
        remove: (source, load, error) => {
            const request = new XMLHttpRequest();
            request.open('GET', '/Receive/RemoveUploadFile/' + source);
            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    load();
                    toastr.info('Xóa file thành công', 'Thông báo');
                }
                else {
                    toastr.warning('Xóa file không thành công', 'Thông báo');
                }
            };
            request.send();

        }
    }
});


const pond2 = FilePond.create(
    document.querySelector('input[name="filepond-tem-sp"]'), {
    labelIdle: '<i class="fa fa-link text-blue"></i> File mẫu sản phẩm <span class="filepond--label-action">chọn..</span>',
    acceptedFileTypes: ['image/png', 'image/jpg', 'image/jpeg', 'application/pdf'],
    allowFileTypeValidation: true,
    imagePreviewHeight: 80,
    allowDownloadByUrl: true,
    beforeRemoveFile: function (e) {
        return confirm('Xác nhận xóa file hóa đơn?');
    },
    labelFileTypeNotAllowed: 'Sai đinh dạng, yêu cầu file ảnh, PDF',
    //fileValidateTypeDetectType: (source, type) => new Promise((resolve, reject) => {
    //    resolve(type);
    //    console.log(type);
    //    console.log(source);
    //}),
    onaddfilestart: function (e) {
        $('label.lb-tem-sp').css('display', 'block');
    },
    onremovefile: function (e) {
        $('label.lb-tem-sp').css('display', 'none');
    },
    server: {
        process: (fieldName, file, metadata, load, error, progress, abort, transfer, options) => {
            const formData = new FormData();
            formData.append('files', file);
            formData.append('tempfolder', $('input#hddTicketTemplate').val());
            formData.append('type', "2");

            const request = new XMLHttpRequest();
            request.open('POST', '/Receive/UploadFile');
            request.upload.onprogress = (e) => {
                progress(e.lengthComputable, e.loaded, e.total);
            };
            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    load(request.responseText);
                }
                else {
                    error('oh no');
                }
            };
            request.send(formData);
            return {
                abort: () => {
                    request.abort();
                    abort();
                }
            };
        },
        load: '/Receive/LoadUploadFile/',
        remove: (source, load, error) => {
            const request = new XMLHttpRequest();
            request.open('GET', '/Receive/RemoveUploadFile/' + source);
            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    load();
                    toastr.info('Xóa file thành công', 'Thông báo');
                }
                else {
                    toastr.warning('Xóa file không thành công', 'Thông báo');
                }
            };
            request.send();

        }
    }
});

const pond3 = FilePond.create(
    document.querySelector('input[name="filepond-tt-hh"]'), {
    labelIdle: '<i class="fa fa-link text-blue"></i> File tình trạng <span class="filepond--label-action">chọn..</span>',
    acceptedFileTypes: ['image/png', 'image/jpg', 'image/jpeg', 'application/pdf'],
    allowFileTypeValidation: true,
    imagePreviewHeight: 80,
    allowDownloadByUrl: true,
    beforeRemoveFile: function (e) {
        return confirm('Xác nhận xóa tình trạng hàng hóa?');
    },
    labelFileTypeNotAllowed: 'Sai đinh dạng, yêu cầu file ảnh, PDF',
    onaddfilestart: function (e) {
        $('label.lb-tt-hh').css('display', 'block');
    },
    onremovefile: function (e) {
        $('label.lb-tt-hh').css('display', 'none');
    },
    server: {
        process: (fieldName, file, metadata, load, error, progress, abort, transfer, options) => {
            const formData = new FormData();
            formData.append('files', file);
            formData.append('tempfolder', $('input#hddTicketTemplate').val());
            formData.append('type', "3");

            const request = new XMLHttpRequest();
            request.open('POST', '/Receive/UploadFile');
            request.upload.onprogress = (e) => {
                progress(e.lengthComputable, e.loaded, e.total);
            };
            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    load(request.responseText);
                }
                else {
                    error('oh no');
                }
            };
            request.send(formData);
            return {
                abort: () => {
                    request.abort();
                    abort();
                }
            };
        },
        load: '/Receive/LoadUploadFile/',
        remove: (source, load, error) => {
            const request = new XMLHttpRequest();
            request.open('GET', '/Receive/RemoveUploadFile/' + source);
            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    load();
                    toastr.info('Xóa file thành công', 'Thông báo');
                }
                else {
                    toastr.warning('Xóa file không thành công', 'Thông báo');
                }
            };
            request.send();

        }
    }
});


var itemSpecialdisplay = [];
$(function () {
    if (location.hash) {
        $('.nav-tabs a[href="' + location.hash + '"]').tab('show');
    }

    var itemFileInvoice = JSON.parse($('#hddFileAttachInvoice').val());
    var itemFileTemp = JSON.parse($('#hddFileAttachTemp').val());
    var itemFileTTHH = JSON.parse($('#hddFileAttachTTHH').val());

    if (itemFileInvoice) {
        pond1.files = [
            {
                source: itemFileInvoice.TempFolder + "|1",
                options: {
                    type: 'local'
                }
            }
        ]
    }

    if (itemFileTemp) {
        pond2.files = [
            {
                source: itemFileTemp.TempFolder + "|2",
                options: {
                    type: 'local'
                }
            }
        ]
    }

    if (itemFileTTHH) {
        pond3.files = [
            {
                source: itemFileTTHH.TempFolder + "|3",
                options: {
                    type: 'local'
                }
            }
        ]
    }


    //Specialdisplay 
    if ($('#hddRProductType').val()) {
        itemSpecialdisplay = $('#hddRProductType').val().split(',');
    }
    /**
    * popup hiển thị thông tin chi tiết của tình trạng hư hỏng
    * */
    GuideCrashStatus();
    /**
    * Popup tra cứu danh mục mã lỗi tab Tư vấn
    * */
    GuideDiscusErrorCategory();
    /**
     * Popup tra cứu danh mục mã lỗi tab Kỹ thuật
     * */
    //GuideTechErrorCategory();
    /**
     * Popup tra cứu danh mục mã lỗi tab Trạm xử lý
     * */
    GuideHandlerErrorCategory();
    /**
    * popup hiển thị thông tin chi tiết của tình trạng hư hỏng
    * */
    SearchCustomer();
    /**
     * popup hiển thị thông tin chi tiết của trạm
     * */
    SearchStationInfo();


    /**
    * Sự kiện chọn khu vực
    * */
    $('select#ddlRStoreArea').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        $('select#ddlRStoreDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
        SelectStoreArea(item, null);
    });
    /**
    * Sự kiện chọn khu vực ở thông tin sự vụ
    * */
    $('select#ddlRCustomerArea').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        $('select#ddlRCustomerDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
        SelectCustomerArea(item, null);
    });
    /**
    * Sự kiện chọn tỉnh thành ở cửa hàng
    * */
    $('select#ddlRStoreProvince').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        SelectStoreProvince(item, null);
    });
    /**
    * Sự kiện chọn tỉnh thành ở thông tin sự vụ
    * */
    $('select#ddlRCustomerProvince').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        SelectCustomerProvince(item, null);
    });
    /**
    * Sự kiện enter Tên cửa hàng, đại lý
    * */
    $('#txtRStoreName').keyup(function (e) {
        e.preventDefault();
        if (e.which === 13) {
            var valueSearch = $('input#txtRStoreName').val();
            if (valueSearch) valueSearch = valueSearch.trim();
            PopupSearchCustomer(valueSearch);
        }
    });

    /**
    * Sự kiện chọn loại nghành hàng
    * */
    $('select#ddlRProductType').change(function (e) {
        e.preventDefault();

        $('#txtRProductCode, #txtRProductCapacity').val('');
        $('#ddlRProductColor').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });

        var item = $(this).val();
        if (itemSpecialdisplay.includes(item)) {
            $('div.panel-RProductModel2, div.panel-RProductSeri2').show();
        } else {
            $('div.panel-RProductModel2, div.panel-RProductSeri2').hide();
        }

        selectProductType(item, null);
        //Tình trạng hư hỏng
        selectReceiveCrashStatus(item, null);
    });

    /**
    * Sự kiện chọn loại chứng từ
    * */
    $('select#ddlRDocutmentType').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        if (item === '16_Khac') {//Chọn loại chứng từ khác thì hiện thị chọn loại chứng từ khác
            $('div.panel-otherdocutmenttype').show();
        } else {
            $('div.panel-otherdocutmenttype').hide();
            $('#ddlROtherDocutmentType').val('').trigger('change.select2');
        }
    });

    /**
    * Sự kiện chọn model
    * */
    $('select#ddlRProductModel').change(function (e) {
        e.preventDefault();
        var item = $(this).val();
        if (item == '-Chọn-') item = '';
        selectProductModel(item, null);
    });

    $('input[type=radio][name="chosen-document"]').on('ifChanged', function (event) {
        var $this = $(this);
        var item = $this.is(':checked');
        if (item) {
            if ($this.val() == 1) {
                $('div.panel-document').show();
                if ($('select#ddlRDocutmentType').val() === '16_Khac')
                    $('div.panel-otherdocutmenttype').show();
                else
                    $('div.panel-otherdocutmenttype').hide();

                $('#txtRProductBuyDate').attr('disabled', false);
                pond1.disabled = false;
                //pond2.disabled = false;
                pond3.disabled = false;
            } else {
                $('div.panel-document, div.panel-otherdocutmenttype').hide();
                $('#txtRProductBuyDate').attr('disabled', true);
                pond1.disabled = true;
                //pond2.disabled = true;
                pond3.disabled = true;
            }
        }
    });


    /**
     * Kiểm tra bảo hành khi nhập số seri
     * */
    CheckSeriWarranty();

    /**
     * Kiểm tra bảo hành khi nhập chứng từ (Ngày mua hàng)
     * */
    CheckDocumentWarranty();

    //Gen datatable lịch sử sụ vụ thông tin sản phẩm theo số điện thoại
    tbl_tab_his_phone_seri = $('#tbl-tab-his-phone-seri').DataTable({
        "retrieve": true,
        "searching": false,
        "processing": true,
        "info": false,
        "ordering": true,
        "lengthChange": false,
        "pageLength": 5,
        language: {
            paginate: { previous: '<', next: '>' },
            search: ''
        }
    });
    $('table#tbl-list-discus').DataTable({
        "retrieve": true,
        "searching": true,
        "processing": true,
        "info": false,
        "ordering": true,
        "lengthChange": false,
        "pageLength": 5,
        language: {
            paginate: { previous: '<', next: '>' },
            search: ''
        }
    });
    /**
     * Tìm kiếm lịch sử theo số điện thoại
     * */
    SearchTicketHisPhoneEvent();
    /**
     * Tìm kiếm lịch sử theo model và seri
     * */
    SearchTicketHisModelSeriEvent();
    /**
     * Khi nhập tìm kiếm ở model cho việc tìm lịch sử sự vụ
     * */
    LoadModelForSearchHistory();
    /**
     * Lấy danh sách lưới danh sách kỹ thuật
     * */
    BindDataForListTech();
    /**
     * Lấy danh sách lưới danh sách trạm xử lý
     * */
    BindDataForListStation();
    /**
     * Lấy danh sách lưới danh sách trạm xử lý
     * */
    BindDataForListCS();
});

/**
 * Lấy danh sách lưới danh sách kỹ thuật
 * */
function BindDataForListTech() {
    $.ajax({
        type: "GET",
        url: '/receive/GetListTech',
        dataType: "json",
        success: function (msg) {
            var tableListTech = $('table#tbl-list-tech').DataTable({
                "processing": true,
                "searching": true,
                "data": msg,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": true,
                        "searchable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<span style="display: inline; white-space: nowrap;">' + data + '&nbsp;<i class="fa fa-plus-circle fa-lg text-green details-plus mp-pointer-st"></i></span>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "TicketId",
                        "orderable": true,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                if (row.IsManager)
                                    data = '<div><label><input data-ticket="' + data + '" type="checkbox" name="chosen-tech-manager" class="flat-red"> ' + data + '</label>' +
                                        '<a style="float: right" data-toggle="tooltip" data-placement="top" title="Chi tiết sự vụ: ' + data + '" target="_blank" href="/receive/' + data + '#tech"><i class="fa fa-edit fa-lg"></i></a></div>';
                                else
                                    data = '<a target="_blank" class="badge bg-yellow" href="/receive/' + data + '#tech">' + data + '</a>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "Model",
                        "orderable": true,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Color",
                        "orderable": true,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Serial",
                        "orderable": true,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "CrashStatus_Code",
                        "orderable": true,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<i class="text-info" data-toggle="tooltip" data-placement="top" title="' + row.CrashStatus_Name + '">' + row.CrashStatus_Code + '</i>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "R_DiscusContent",
                        "orderable": false,
                        "searchable": true,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<div class="crop-text-230"><span data-toggle="tooltip"data-placement="top" title="' + data + '">' + data + '</span></div>'
                            }
                            return data;
                        }
                    },
                    {
                        "data": "D_DiscusContent",
                        "orderable": false,
                        "searchable": true,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<div class="crop-text-230"><span data-toggle="tooltip"data-placement="top" title="' + data + '">' + data + '</span></div>'
                            }
                            return data;
                        }
                    },
                    {
                        "data": "Status",
                        "orderable": true,
                        "searchable": true,
                        "className": "text-center",
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<span class="' + (row.Status == "New" ? "badge bg-green" : "badge bg-info") + '">' + data + '</span>';
                            }
                            return data;
                        }
                    }
                ],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 6
            });

            // Add event listener for opening and closing details
            $('table#tbl-list-tech tbody').on('change', 'input[name="chosen-tech-manager"]', function (e) {
                var ticketSelect = [];
                $('input[name="chosen-tech-manager"]').each(function (s, i) {
                    if ($(i).is(':checked')) {
                        ticketSelect.push($(i).data('ticket'));
                    }
                });
                if (ticketSelect.length > 0) {//Ẩn đi ticketid và tranfer TL
                    $('div.panel-multi-option-change-tech').hide();
                } else {
                    $('div.panel-multi-option-change-tech').show();
                }
            });

            var detailRows = [];

            $('table#tbl-list-tech tbody').on('click', 'tr td i.details-plus', function () {
                var tr = $(this).closest('tr');
                var row = tableListTech.row(tr);
                var idx = $.inArray(tr.attr('id'), detailRows);

                if (row.child.isShown()) {
                    tr.removeClass('details');
                    row.child.hide();

                    // Remove from the 'open' array
                    detailRows.splice(idx, 1);
                }
                else {
                    tr.addClass('details');
                    row.child(formatTechDetail(row.data())).show();

                    // Add to the 'open' array
                    if (idx === -1) {
                        detailRows.push(tr.attr('id'));
                    }
                }
            });

            // On each draw, loop over the `detailRows` array and show any child rows
            tableListTech.on('draw', function () {
                $.each(detailRows, function (i, id) {
                    $('#' + id + ' td i.details-plus').trigger('click');
                });
            });


        }
    });
}

/**
 * Định dạng detail cho table kỹ thuật
 * @param {any} d
 */
function formatTechDetail(d) {
    return 'Mã khách hàng: <b>' + d.CustomerCode + '</b><br>' +
        'Họ tên khách hàng: <b>' + d.CustomerName + '</b><br>' +
        'Số điện thoại: <b>' + d.CustomerPhone + '</b>';
}

/**
 * Lấy danh sách lưới danh sách trạm xử lý
 * */
function BindDataForListStation() {
    //$.ajax({
    //    type: "GET",
    //    url: '/receive/GetListStation',
    //    dataType: "json",
    //    success: function (msg) {
    //        table = $('table#tbl-list-station').DataTable({
    //            "processing": true,
    //            "searching": true,
    //            "data": msg,
    //            "columns": [
    //                {
    //                    "data": "TicketId",
    //                    "orderable": true,
    //                    "searchable": false,
    //                    "className": 'vertical-middle',
    //                    "render": function (data, type, row, meta) {
    //                        if (type === 'display') {
    //                            data = meta.row;
    //                        }
    //                        return data;
    //                    }
    //                },
    //                {
    //                    "data": "TicketId",
    //                    "orderable": true,
    //                    "className": 'vertical-middle',
    //                    "render": function (data, type, row, meta) {
    //                        if (type === 'display') {
    //                            data = '<a target="_blank" class="badge bg-yellow" href="/receive/' + data + '#handler">' + data + '</a>';
    //                        }
    //                        return data;
    //                    }
    //                },
    //                {
    //                    "data": "Model",
    //                    "orderable": true,
    //                    "className": 'vertical-middle'
    //                },
    //                {
    //                    "data": "Color",
    //                    "orderable": true,
    //                    "className": 'vertical-middle'
    //                },
    //                {
    //                    "data": "Serial",
    //                    "orderable": true,
    //                    "className": 'vertical-middle'
    //                },
    //                {
    //                    "data": "CrashStatus_Code",
    //                    "orderable": true,
    //                    "className": 'vertical-middle',
    //                    "render": function (data, type, row, meta) {
    //                        if (type === 'display') {
    //                            data = '<i class="text-info" data-toggle="tooltip" data-placement="top" title="' + row.CrashStatus_Name + '">' + row.CrashStatus_Code + '</i>';
    //                        }
    //                        return data;
    //                    }
    //                },
    //                {
    //                    "data": "R_DiscusContent",
    //                    "orderable": false,
    //                    "className": 'vertical-middle',
    //                    "render": function (data, type, row, meta) {
    //                        if (type === 'display') {
    //                            data = '<div class="crop-text-230"><span data-toggle="tooltip"data-placement="top" title="' + data + '">' + data + '</span></div>'
    //                        }
    //                        return data;
    //                    }
    //                },
    //                {
    //                    "data": "D_DiscusContent",
    //                    "orderable": false,
    //                    "searchable": true,
    //                    "className": 'vertical-middle',
    //                    "render": function (data, type, row, meta) {
    //                        if (type === 'display') {
    //                            data = '<div class="crop-text-230"><span data-toggle="tooltip"data-placement="top" title="' + data + '">' + data + '</span></div>'
    //                        }
    //                        return data;
    //                    }
    //                },
    //                {
    //                    "data": "Status",
    //                    "orderable": true,
    //                    "searchable": true,
    //                    "className": "text-center vertical-middle",
    //                    "render": function (data, type, row, meta) {
    //                        if (type === 'display') {
    //                            data = '<span class="' + (row.Status == "New" ? "badge bg-green" : "badge bg-info") + '">' + data + '</span>';
    //                        }
    //                        return data;
    //                    }
    //                }
    //            ],
    //            "bLengthChange": false,
    //            "bInfo": false,
    //            "iDisplayLength": 6
    //        });
    //    }
    //});


    table = $('table#tbl-list-station').DataTable({
        "proccessing": true,
        "serverSide": true,
        "searching": true,
        "ajax": {
            url: "/receive/GetListStation",
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Search..."
        },
        "columns": [
            {
                "data": "STT",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "TicketId",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<a target="_blank" class="badge bg-yellow" href="/receive/' + data + '#handler">' + data + '</a>';
                    }
                    return data;
                }
            },
            {
                "data": "Model",
                "orderable": false,
                "className": 'vertical-middle'
            },
            {
                "data": "Color",
                "orderable": false,
                "className": 'vertical-middle'
            },
            {
                "data": "Serial",
                "orderable": false,
                "className": 'vertical-middle'
            },
            {
                "data": "CrashStatus_Code",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<i class="text-info" data-toggle="tooltip" data-placement="top" title="' + row.CrashStatus_Name + '">' + row.CrashStatus_Code + '</i>';
                    }
                    return data;
                }
            },
            {
                "data": "R_DiscusContent",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<div class="crop-text-230"><span data-toggle="tooltip"data-placement="top" title="' + data + '">' + data + '</span></div>'
                    }
                    return data;
                }
            },
            {
                "data": "D_DiscusContent",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<div class="crop-text-230"><span data-toggle="tooltip"data-placement="top" title="' + data + '">' + data + '</span></div>'
                    }
                    return data;
                }
            },
            {
                "data": "Status",
                "orderable": false,
                "searchable": true,
                "className": "text-center vertical-middle",
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="' + (row.Status == "New" ? "badge bg-green" : "badge bg-info") + '">' + data + '</span>';
                    }
                    return data;
                }
            }
        ],
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 6,
        "initComplete": function (settings, json) {
            var api = this.api();
            var textBox = $('#tbl-list-station_filter label input');
            textBox.unbind();
            textBox.bind('keyup input', function (e) {
                if (e.keyCode == 8 && !textBox.val() || e.keyCode == 46 && !textBox.val()) {
                } else if (e.keyCode == 13 || !textBox.val()) {
                    api.search(this.value).draw();
                }
            });
        }
    });
}


/**
 * Lấy danh sách lưới danh sách trạm xử lý
 * */
function BindDataForListCS() {
    $.ajax({
        type: "GET",
        url: '/receive/GetListCS',
        dataType: "json",
        success: function (msg) {
            table = $('table#tbl-list-cs').DataTable({
                "processing": true,
                "searching": true,
                "data": msg.data,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "TicketId",
                        "orderable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<a target="_blank" class="badge bg-yellow" href="/receive/' + data + '#care">' + data + '</a>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "TakeCareBy",
                        "orderable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                //data = row.TakeCareBy_FullName ? (data + " - " + row.TakeCareBy_FullName) : data;
                                data = '<span class="text-info font-bold" data-toggle="tooltip" data-placement="top" title="' + row.TakeCareBy_FullName + '" data-original-title="' + row.TakeCareBy_FullName + '">';
                                if (row.SignalStatus) data += '<i class="fa fa-circle text-success"></i> ';
                                else data += '<i class="fa fa-circle text-gray"></i> ';
                                data += row.TakeCareBy + '</span>';

                            }
                            return data;
                        }
                    },
                    {
                        "data": "Area",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Model",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Color",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Serial",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "ErrorCode",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<i class="text-info" data-toggle="tooltip" data-placement="top" title="' + row.ErrorName + '">' + data + '</i>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "CodePKTTTKT",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "CloseTicket",
                        "orderable": false,
                        "visible": msg.SoClosed,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Status",
                        "orderable": false,
                        "searchable": false,
                        "className": "text-center vertical-middle",
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<span class="badge bg-green">' + data + '</span>';
                            }
                            return data;
                        }
                    }
                ],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 6
            });
        }
    });
}


var tbl_tab_his_phone_seri, tbl_tab_his_seri_seri;
/**
 * Tìm kiếm lịch sử theo số điện thoại
 * */
function SearchTicketHisPhoneEvent() {
    $('button.btn-SearchHisPhone').click(function (e) {
        e.preventDefault();
        var phone = $('#txtSearchHisPhone').val().trim();
        if (!phone) return;

        tbl_tab_his_phone_seri.destroy();
        $.ajax({
            type: "GET",
            url: '/receive/GetHisTicketsByPhone/' + phone,
            dataType: "json",
            success: function (msg) {
                tbl_tab_his_phone_seri = $('table#tbl-tab-his-phone-seri').DataTable({
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
                                if (type === 'display') {
                                    data = '<a  target="_blank" class="badge bg-yellow" href="/receive/' + data + '">' + data + '</a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "Model",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "Color",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "Serial",
                            "className": 'vertical-middle'
                        }
                    ],
                    "retrieve": true,
                    "searching": false,
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
            }
        });
    });
}

/**
 * Tìm kiếm lịch sử theo model và seri
 * */
function SearchTicketHisModelSeriEvent() {
    $('button.btn-SearchHisSeri').click(function (e) {
        e.preventDefault();
        var model = $('#ddlDD212Status').val();
        var seri = $('#txtSearchHisSeri').val().trim();
        if (!model || !seri) return;
        if (tbl_tab_his_seri_seri) tbl_tab_his_seri_seri.destroy();
        $.ajax({
            type: "GET",
            url: '/receive/GetHisTicketsBySeriModel/' + model + '/' + seri,
            dataType: "json",
            success: function (msg) {
                tbl_tab_his_seri_seri = $('table#tbl-tab-his-seri-seri').DataTable({
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
                                if (type === 'display') {
                                    data = '<a  target="_blank" class="badge bg-yellow" href="/receive/' + data + '">' + data + '</a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "Model",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "Color",
                            "className": 'vertical-middle'
                        },
                        {
                            "data": "Serial",
                            "className": 'vertical-middle'
                        }
                    ],
                    "retrieve": true,
                    "searching": false,
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
            }
        });
    });
}

/**
 * Khi nhập tìm kiếm ở model cho việc tìm lịch sử sự vụ
 * */
function LoadModelForSearchHistory() {
    $('#ddlDD212Status').select2({
        ajax: {
            delay: 250,
            url: '/receive/GetModelForSearchHistory',
            data: function (params) {
                var query = {
                    model: params.term,
                    page: params.page || 1
                }

                // Query parameters will be ?search=[term]&page=[page]
                return query;
            },
            dataType: 'json'
        }
    });
}


/**
 * Popup tra cứu khách bảo hành, cửa hàng
 * */
function GuideCrashStatus() {
    $('a.detail-crash-status').click(function (e) {
        $.confirm({
            title: '<i class="fa fa-user text-red"></i> Chi tiết tình trạng hư hỏng',
            type: 'blue',
            id: 'confirmDetailsCrashStatus',
            columnClass: 'col-md-12',
            content: 'url:/HtmlModel/DPL/GuideCrashStatus.html',
            buttons: {
                chosen: {
                    text: 'Lựa chọn',
                    btnClass: 'bg-green btn-flat btn-chosen-status',
                    action: function (e) {
                        if (e.el.data('category')) {
                            $('select#ddlRCrashStatus').val(e.el.data('category')).trigger('change.select2');
                            toastr.info('Lựa chọn tình trạng hư hỏng, thành công', 'Thông báo');
                        } else {
                            toastr.warning('Vui lòng chọn tình trạng hư hỏng', 'cảnh bảo');
                            return false;
                        }
                    }
                },
                cancel: {
                    text: 'Đóng',
                    keys: ['esc']
                }
            },
            onContentReady: function () {
                $(".jconfirm-content").css("overflow", "hidden");
                $.ajax({
                    type: "GET",
                    url: '/receive/allcrashstatus',
                    dataType: "json",
                    success: function (msg) {
                        table = $('table#tbl-detail-crash-status').DataTable({
                            "data": msg,
                            "columns": [
                                {
                                    "data": "STT",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "ItemGroupCode",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "ItemGroupName",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Category",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "StatusName",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Description",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Note",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "className": 'details-control text-center vertical-middle',
                                    "orderable": false,
                                    "data": null,
                                    "defaultContent": '<div style="height: 20px;width: 20px;"><input type="radio" name="chosen-crash" class="flat-red"></div>'
                                }
                            ],
                            "order": [[0, 'asc']],
                            "bLengthChange": false,
                            "bInfo": false,
                            "iDisplayLength": 8,
                        });

                        // Add event listener for opening and closing details
                        $('table#tbl-detail-crash-status tbody').on('change', 'input[name="chosen-crash"]', function (e) {
                            var tr = $(this).closest('tr');
                            $('table#tbl-detail-crash-status tbody tr').removeClass('bgg-info')
                            var check = $(this).is(':checked');
                            if (check) {
                                tr.addClass('bgg-info');
                                var row = table.row(tr);
                                $('button.btn-chosen-status').attr('data-category', row.data().Category);
                            }
                        });
                    },
                    complete: function (msg) {
                    }
                });
            }
        });
    });
}

/**
 * Popup tra cứu danh mục mã lỗi tab Tư vấn
 * */
function GuideDiscusErrorCategory() {
    $('a.detail-error-category').click(function (e) {
        $.confirm({
            title: '<i class="fa fa-list-ol text-green"></i> Chi tiết danh mục mã lỗi',
            type: 'blue',
            id: 'confirmDetailsErrorCategory',
            columnClass: 'col-md-12',
            content: 'url:/HtmlModel/DPL/GuideErrorCategory.html',
            buttons: {
                chosen: {
                    text: 'Lựa chọn',
                    btnClass: 'bg-green btn-flat btn-chosen-error',
                    action: function (e) {
                        if (e.el.data('category')) {
                            $('select#ddlDErrorCategory').val(e.el.data('category')).trigger('change.select2');
                            toastr.info('Lựa chọn tình mã lỗi, thành công', 'Thông báo');
                        } else {
                            toastr.warning('Vui lòng chọn tình mã lỗi', 'cảnh bảo');
                            return false;
                        }
                    }
                },
                cancel: {
                    text: 'Đóng',
                    keys: ['esc']
                }
            },
            onContentReady: function () {
                $(".jconfirm-content").css("overflow", "hidden");
                $.ajax({
                    type: "GET",
                    url: '/receive/allcrashstatus',
                    dataType: "json",
                    success: function (msg) {
                        table = $('table#tbl-detail-error-category').DataTable({
                            "data": msg,
                            "columns": [
                                {
                                    "data": "STT",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "ItemGroupCode",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "ItemGroupName",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Category",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "StatusName",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Description",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Note",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "className": 'details-control text-center vertical-middle',
                                    "orderable": false,
                                    "data": null,
                                    "defaultContent": '<div style="height: 20px;width: 20px;"><input type="radio" name="chosen-error" class="flat-red"></div>'
                                }
                            ],
                            "order": [[0, 'asc']],
                            "bLengthChange": false,
                            "bInfo": false,
                            "iDisplayLength": 6,
                        });

                        // Add event listener for opening and closing details
                        $('table#tbl-detail-error-category tbody').on('change', 'input[name="chosen-error"]', function (e) {
                            var tr = $(this).closest('tr');
                            $('table#tbl-detail-error-category tbody tr').removeClass('bgg-info')
                            var check = $(this).is(':checked');
                            if (check) {
                                tr.addClass('bgg-info');
                                var row = table.row(tr);
                                $('button.btn-chosen-error').attr('data-category', row.data().Category);
                            }
                        });
                    },
                    complete: function (msg) {
                    }
                });
            }
        });
    });
}

/**
 * Popup tra cứu danh mục mã lỗi tab Trạm xử lý
 * */
function GuideHandlerErrorCategory() {
    $('a.detail-handler-error-category').click(function (e) {
        $.confirm({
            title: '<i class="fa fa-list-ol text-green"></i> Chi tiết danh mục mã lỗi',
            type: 'blue',
            id: 'confirmDetailsHandlerErrorCategory',
            columnClass: 'col-md-12',
            content: 'url:/HtmlModel/DPL/GuideHandlerErrorCategory.html',
            buttons: {
                chosen: {
                    text: 'Lựa chọn',
                    btnClass: 'bg-green btn-flat btn-chosen-handler-error',
                    action: function (e) {
                        if (e.el.data('category')) {
                            $('select#ddlHErrorCategory').val(e.el.data('category')).trigger('change.select2');
                            toastr.info('Lựa chọn mã lỗi, thành công', 'Thông báo');
                        } else {
                            toastr.warning('Vui lòng chọn mã lỗi', 'cảnh bảo');
                            return false;
                        }
                    }
                },
                cancel: {
                    text: 'Đóng',
                    keys: ['esc']
                }
            },
            onContentReady: function () {
                $(".jconfirm-content").css("overflow", "hidden");
                $.ajax({
                    type: "GET",
                    url: '/receive/allcrashstatus',
                    dataType: "json",
                    success: function (msg) {
                        table = $('table#tbl-detail-handler-error-category').DataTable({
                            "data": msg,
                            "columns": [
                                {
                                    "data": "STT",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "ItemGroupCode",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "ItemGroupName",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Category",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "StatusName",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Description",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "data": "Note",
                                    "className": 'vertical-middle'
                                },
                                {
                                    "className": 'details-control text-center vertical-middle',
                                    "orderable": false,
                                    "data": null,
                                    "defaultContent": '<div style="height: 20px;width: 20px;"><input type="radio" name="chosen-error" class="flat-red"></div>'
                                }
                            ],
                            "order": [[0, 'asc']],
                            "bLengthChange": false,
                            "bInfo": false,
                            "iDisplayLength": 6,
                        });

                        // Add event listener for opening and closing details
                        $('table#tbl-detail-handler-error-category tbody').on('change', 'input[name="chosen-handler-error"]', function (e) {
                            var tr = $(this).closest('tr');
                            $('table#tbl-detail-handler-error-category tbody tr').removeClass('bgg-info')
                            var check = $(this).is(':checked');
                            if (check) {
                                tr.addClass('bgg-info');
                                var row = table.row(tr);
                                $('button.btn-chosen-handler-error').attr('data-category', row.data().Category);
                            }
                        });
                    },
                    complete: function (msg) {
                    }
                });
            }
        });
    });
}

/**
 * popup hiển thị thông tin chi tiết của tình trạng hư hỏng
 * */
function SearchCustomer() {
    $('span.voc-seach-customer a, button.voc-seach-customer').click(function (e) {
        var valueSearch = ''; var isCustomer = false;
        if ($(e.currentTarget).is(":button.voc-seach-customer.customer")) {
            valueSearch = $('input#txtRIStoreName').val();
            if (valueSearch) valueSearch = valueSearch.trim();
            isCustomer = true;
        } else if ($(e.currentTarget).is(":button.voc-seach-customer.store")) {
            valueSearch = $('input#txtRStoreName').val();
            if (valueSearch) valueSearch = valueSearch.trim();
        }

        PopupSearchCustomer(valueSearch, isCustomer);
    });

}

/**
 * Popup tạo mới thông tin khách hàng, cửa hàng đại lý
 * @param {any} searchPopup
 * @param {any} isCustomer
 */
function PopCreateCustomer(searchPopup, isCustomer) {
    $('button.btn-pop-add-customer').click(function (e) {
        e.preventDefault();
        searchPopup.close();
        var popCreate = $.confirm({
            title: '<i class="fa fa-bank text-green"></i> Tạo mới thông tin khách hàng, cửa hàng',
            type: 'blue',
            id: 'confirmAddAccount',
            columnClass: 'col-md-8 col-md-offset-2',
            content: 'url:/HtmlModel/DPL/AddCustomer.html',
            buttons: {
                formSubmit: {
                    text: 'Tạo mới',
                    btnClass: 'btn-blue btn-flat btn-add-dpl-customer',
                    action: function () {
                        var obj = new Object();
                        obj.NumSeqRetailCM = $('#ddlRPopCreateStoreType').val();
                        obj.PHONE = $('#txtRPopCreateStorePhone').val();
                        obj.SEGMENTID = $('#ddlRPopCreateStoreArea').val();
                        obj.STREET = $('#txtRPopCreateStoreAddress').val();
                        obj.SUBSEGMENTID = $('#ddlRPopCreateStoreProvince').val();
                        obj.RetailCMName = $('#txtRPopCreateStoreName').val();
                        obj.SALESDISTRICTID = $('#ddlRPopCreateStoreDistrict').val();
                        obj.RetailCMTyple = $('#ddlRPopCreateStoreType').val();
                        obj.ContactName = $('#txtRPopCreateContactName').val();
                        if (!obj.NumSeqRetailCM || !obj.PHONE || !obj.SEGMENTID || !obj.STREET || !obj.SUBSEGMENTID
                            || !obj.RetailCMName || !obj.SALESDISTRICTID || !obj.RetailCMTyple) {
                            toastr.warning('Vui lòng nhập đầy đủ thông tin', 'Cảnh báo');
                            return false;
                        }

                        var itemDisableds = [$('button.btn-add-dpl-customer')];
                        var mylop = new myMpLoop($('button.btn-add-dpl-customer'), 'Đang xử lý', $('button.btn-add-dpl-customer').html(), itemDisableds);
                        mylop.start();

                        $.ajax({
                            type: "POST",
                            url: "/Receive/CreateCustomer",
                            dataType: "json",
                            data: obj,
                            success: function (msg) {
                                //Đóng popup
                                popCreate.close();

                                toastr.info("Tạo mới khách hàng thành công.", 'Thông báo');

                                if (obj.RetailCMTyple === '0') {//Khách
                                    $('#hddRCustomerCode').val(msg.value.Id);

                                    $('#txtRIStoreCode').val(msg.value.NumSeqRetailCM);
                                    $('#txtRIStoreName').val(msg.value.RetailCMName);

                                } else {
                                    $('#hddRStoreCode').val(msg.value.Id);

                                    $('#txtRStoreCode').val(msg.value.NumSeqRetailCM);
                                    $('#txtRStoreName').val(msg.value.RetailCMName);

                                    $('#txtRStorePhone').val(msg.value.PHONE);
                                    $('#txtRStoreContact').val(msg.value.ContactName);
                                    $('#ddlRStoreType').val(msg.value.RetailCMTyple).trigger('change.select2');

                                    $('#ddlRStoreArea').val(msg.value.SEGMENTID).trigger('change.select2');
                                    SelectStoreArea(msg.value.SEGMENTID, msg.value.SUBSEGMENTID);
                                    SelectStoreProvince(msg.value.SUBSEGMENTID, msg.value.SALESDISTRICTID);

                                    $('#txtRStoreAddress').val(msg.value.STREET);
                                }
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
                //Lấy thông tin khu vực, tỉnh, huyện
                $.ajax({
                    type: "GET",
                    url: '/receive/GetAllArea',
                    dataType: "json",
                    success: function (msg) {
                        $('select#ddlRPopCreateStoreArea').select2({
                            data: msg,
                            dropdownParent: $('.jconfirm-no-transition')
                        }).trigger('change.select2');
                        /**
                         * Lấy thông tin tỉnh thành theo khu vực trong popup tạo mới KH, đại lý
                         * @param {any} area
                         */
                        $('select#ddlRPopCreateStoreArea').change(function (e) {
                            e.preventDefault();
                            $('#ddlRPopCreateStoreProvince').val('').change();
                            var value = $(this).val();
                            SelectCreateStoreArea(value, null);
                        });
                    }
                });
                //Lấy thông tin loại khách hàng
                $.ajax({
                    type: "GET",
                    url: '/receive/GetAllCustomerType',
                    dataType: "json",
                    success: function (msg) {
                        $('select#ddlRPopCreateStoreType').select2({
                            data: msg,
                            dropdownParent: $('.jconfirm-no-transition')
                        }).trigger('change.select2');
                        $('select#ddlRPopCreateStoreType').val(isCustomer ? "0" : "1").trigger('change.select2');
                    }
                });
            }
        });
    });
}

/**
 * popup hiển thị thông tin chi tiết của trạm
 * */
function SearchStationInfo() {
    $('a.detail-tech-station-info, a.detail-handler-station-info').click(function (e) {
        var valueSearch = '';
        var isTech = false;
        if ($(e.currentTarget).data("tab") == 'tech') {
            if ($('select#ddlTStationInfo').select2('data'))
                valueSearch = $('select#ddlTStationInfo').select2('data')[0].text;
            isTech = true;
        } else {
            if ($('select#ddlHStationInfo').select2('data'))
                valueSearch = $('select#ddlHStationInfo').select2('data')[0].text;
        }

        if (valueSearch) valueSearch = (valueSearch.trim() === '-Chọn-' ? '' : valueSearch.trim());
        PopupSearchStationInfo(valueSearch, isTech);

    });

}

/**
 * Popup tìm kiếm khách hàng
 * @param {any} valueSearch
 * @param {any} isCustomer
 */
function PopupSearchCustomer(valueSearch, isCustomer) {
    var itemaddAccount = $.confirm({
        title: '<i class="fa fa-user text-red"></i> Danh sách khách hàng',
        type: 'blue',
        id: 'confirmSearchCustomer',
        columnClass: 'col-md-12',
        content: 'url:/HtmlModel/DPL/SearchCustomer.html',
        buttons: {
            chosen: {
                text: 'Lựa chọn',
                btnClass: 'bg-green btn-flat btn-chosen-seach-customer',
                action: function (e) {
                    if (e.el.data('category')) {
                        var item = e.el.data('category');
                        if (isCustomer) {
                            $('#hddRCustomerCode').val(item.Id);

                            $('#txtRIStoreCode').val(item.NumSeqRetailCM);
                            $('#txtRIStoreName').val(item.RetailCMName);
                            $('#txtRCustomerPhone').val(item.PHONE);

                            toastr.info('Lựa chọn khách hàng thành công', 'Thông báo');
                        } else {
                            $('#hddRStoreCode').val(item.Id);

                            $('#txtRStoreCode, #txtRStoreCode1').val(item.NumSeqRetailCM);
                            $('#txtRStoreName, #txtRStoreName1').val(item.RetailCMName);

                            $('#txtRStorePhone').val(item.PHONE);
                            $('#txtRStoreContact').val(item.ContactName);
                            $('#ddlRStoreType').val(item.RetailCMTyple).trigger('change.select2');

                            $('#ddlRStoreArea').val(item.SEGMENTID).trigger('change.select2');
                            SelectStoreArea(item.SEGMENTID, item.SUBSEGMENTID);
                            SelectStoreProvince(item.SUBSEGMENTID, item.SALESDISTRICTID);

                            $('#txtRStoreAddress').val(item.STREET);

                            toastr.info('Lựa chọn cửa hàng thành công', 'Thông báo');
                        }
                    } else {
                        toastr.warning('Vui lòng chọn khách hàng', 'cảnh bảo');
                        return false;
                    }
                }
            },
            cancel: {
                text: 'Đóng',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('input.search-value').val(valueSearch);
            $.ajax({
                type: "GET",
                url: '/receive/allcustomer/' + isCustomer + '?searchValue=' + valueSearch,
                dataType: "json",
                success: function (msg) {
                    table = $('table#tbl-search-customer').DataTable({
                        "processing": true,
                        "searching": false,
                        "data": msg,
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
                                "defaultContent": '<div style="height: 20px;width: 20px;"><input type="radio" name="chosen-customer" class="flat-red"></div>'
                            }
                        ],
                        "order": [[0, 'asc']],
                        "bLengthChange": false,
                        "bInfo": false,
                        "iDisplayLength": 6
                    });

                    // Add event listener for opening and closing details
                    $('table#tbl-search-customer tbody').on('change', 'input[name="chosen-customer"]', function (e) {
                        var tr = $(this).closest('tr');
                        $('table#tbl-search-customer tbody tr').removeClass('bgg-info')
                        var check = $(this).is(':checked');
                        if (check) {
                            tr.addClass('bgg-info');
                            var row = table.row(tr);
                            $('button.btn-chosen-seach-customer').attr('data-category', JSON.stringify(row.data()));
                        }
                    });
                    $('button.btn-search-value').click(function (e) {
                        e.preventDefault();
                        var value = $('input.search-value').val();

                        /**
                         * Lấy thông tin cho tìm kiếm khách hàng
                         * @param {any} value
                         */
                        PostSearchCustomer(value, isCustomer);

                    });


                    $('input.search-value').keyup(function (e) {
                        e.preventDefault();
                        if (e.which === 13) {
                            var value = $('input.search-value').val();
                            /**
                              * Lấy thông tin cho tìm kiếm khách hàng
                              * @param {any} value
                              */
                            PostSearchCustomer(value, isCustomer);
                        }
                    });

                }
            });

            /**
             * Popup tạo mới thông tin khách hàng, cửa hàng đại lý
             * */
            PopCreateCustomer(itemaddAccount, isCustomer);
        }
    });
}

/**
 *  Popup tìm kiếm thông tin trạm
 * @param {any} valueSearch
 * @param {any} isTech phân biệt tab tư vấn, trạm xử lý
 */
var tableStation;
function PopupSearchStationInfo(valueSearch, isTech) {
    $.confirm({
        title: '<i class="fa fa-user text-red"></i> Danh sách trạm',
        type: 'blue',
        id: 'confirmSearchStation',
        columnClass: 'col-md-12',
        content: 'url:/HtmlModel/DPL/SearchStation.html',
        buttons: {
            chosen: {
                text: 'Lựa chọn',
                btnClass: 'bg-green btn-flat btn-chosen-seach-station',
                action: function (e) {
                    if (e.el.data('category')) {
                        var item = e.el.data('category');
                        if (isTech)
                            $('select#ddlTStationInfo').val(item.AccountNum).trigger('change.select2');
                        else
                            $('select#ddlHStationInfo').val(item.AccountNum).trigger('change.select2');
                        toastr.info('Lựa chọn trạm thành công', 'Thông báo');
                    } else {
                        toastr.warning('Vui lòng chọn trạm', 'cảnh bảo');
                        return false;
                    }
                }
            },
            cancel: {
                text: 'Đóng',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('input.search-value').val(valueSearch);
            GetDataSearchStation(valueSearch);

            $('button.btn-search-value').click(function (e) {
                e.preventDefault();
                var value = $('input.search-value').val();

                /**
                 * Lấy thông tin cho tìm kiếm trạm
                 * @param {any} value
                 */
                GetDataSearchStation(value);

            });
            $('input.search-value').keyup(function (e) {
                e.preventDefault();
                if (e.which === 13) {
                    var value = $('input.search-value').val();
                    /**
                      * Lấy thông tin cho tìm kiếm trạm
                      * @param {any} value
                      */
                    GetDataSearchStation(value);
                }
            });
        }
    });
}

/**
 * Lấy dữ liệu cho bảng tìm kiếm trạm
 * */
function GetDataSearchStation(valueSearch) {
    $.ajax({
        type: "GET",
        url: '/receive/allstation?searchValue=' + valueSearch,
        dataType: "json",
        success: function (msg) {
            if (tableStation) {
                tableStation.destroy();
            }
            tableStation = $('table#tbl-search-station').DataTable({
                "processing": true,
                "searching": false,
                "data": msg,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "AccountNum",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Employeeresponsible",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Name",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Address",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "mail",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "phone",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "className": 'details-control text-center vertical-middle',
                        "orderable": false,
                        "data": null,
                        "defaultContent": '<div style="height: 20px;width: 20px;"><input type="radio" name="chosen-station" class="flat-red"></div>'
                    }
                ],
                "order": [[0, 'asc']],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 6
            });

            // Add event listener for opening and closing details
            $('table#tbl-search-station tbody').on('change', 'input[name="chosen-station"]', function (e) {
                var tr = $(this).closest('tr');
                $('table#tbl-search-station tbody tr').removeClass('bgg-info')
                var check = $(this).is(':checked');
                if (check) {
                    tr.addClass('bgg-info');
                    var row = table.row(tr);
                    $('button.btn-chosen-seach-station').attr('data-category', JSON.stringify(row.data()));
                }
            });

        }
    });
}


/**
 * Lấy thông tin cho tìm kiếm khách hàng
 * @param {any} value
 * @param {any} isCustomer
 */
function PostSearchCustomer(value, isCustomer) {
    $.ajax({
        type: "GET",
        url: '/receive/allcustomer/' + isCustomer + '?searchValue=' + value,
        dataType: "json",
        success: function (msg) {
            table.destroy();
            table = $('table#tbl-search-customer').DataTable({
                "processing": true,
                "searching": false,
                "data": msg,
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
                        "defaultContent": '<div style="height: 20px;width: 20px;"><input type="radio" name="chosen-customer" class="flat-red"></div>'
                    }
                ],
                "order": [[0, 'asc']],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 8
            });

            // Add event listener for opening and closing details
            $('table#tbl-search-customer tbody').on('change', 'input[name="chosen-customer"]', function (e) {
                var tr = $(this).closest('tr');
                $('table#tbl-search-customer tbody tr').removeClass('bgg-info')
                var check = $(this).is(':checked');
                if (check) {
                    tr.addClass('bgg-info');
                    var row = table.row(tr);
                    $('button.btn-chosen-seach-customer').attr('data-category', JSON.stringify(row.data()));
                }
            });
        }
    });
}


/**
 * Lấy thông tin tỉnh thành theo khu vực
 * @param {any} area
 * @param {any} selectedProvince
 */
function SelectStoreArea(area, selectedProvince) {
    $.ajax({
        type: "GET",
        url: '/receive/getprovincebyarea/' + area + "/" + selectedProvince,
        dataType: "json",
        success: function (msg) {
            $('select#ddlRStoreProvince').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRStoreProvince').select2({ data: msg });
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
            $('select#ddlRCustomerProvince').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRCustomerProvince').select2({ data: msg });
        }
    });
}

/**
 * Lấy thông tin tỉnh thành theo khu vực trong popup tạo mới KH, đại lý
 * @param {any} area
 * @param {any} selectedProvince
 */
function SelectCreateStoreArea(area, selectedProvince) {
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
/**
 * Lấy thông tin huyện theo tỉnh
 * @param {any} province
 * @param {any} selectedDistrict
 */
function SelectStoreProvince(province, selectedDistrict) {
    if (!selectedDistrict) selectedDistrict = null;
    $.ajax({
        type: "GET",
        url: '/receive/getdistrictbyprovince/' + province + "/" + selectedDistrict,
        dataType: "json",
        success: function (msg) {
            $('select#ddlRStoreDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRStoreDistrict').select2({ data: msg });
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
            $('select#ddlRCustomerDistrict').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRCustomerDistrict').select2({ data: msg });
        }
    });
}


/**
 * Lấy data cho model khi chọn loại nghành hàng
 * @param {any} productType
 * @param {any} selectedModel
 */
function selectProductType(productType, selectedModel) {
    if (!productType) {
        $('select#ddlRProductModel').html('').select2({ data: [{ id: '', text: '-Chọn-' }] }).trigger('change.select2');
        $('select#ddlRProductModel2').html('').select2({ data: [{ id: '', text: '-Chọn-' }] }).trigger('change.select2');
        return;
    }
    $.ajax({
        type: "GET",
        url: '/receive/getmodelbyproducttype/' + productType + "/" + selectedModel,
        dataType: "json",
        success: function (msg) {
            $('select#ddlRProductModel').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRProductModel').select2({ data: msg });

            $('select#ddlRProductModel2').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRProductModel2').select2({ data: msg });
        }
    });
}

/**
 * Lấy data cho tình trạng hư hỏng khi chọn loại nghành hàng
 * @param {any} productType
 * @param {any} selectedModel
 */
function selectReceiveCrashStatus(productType, selectedModel) {
    if (!productType) {
        $('select#ddlRCrashStatus').html('').select2({ data: [{ id: '', text: '-Chọn-' }] }).trigger('change.select2');
        return;
    }
    $.ajax({
        type: "GET",
        url: '/receive/getcrashstatusbyproducttype/' + productType + "/" + selectedModel,
        dataType: "json",
        success: function (msg) {
            $('select#ddlRCrashStatus').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRCrashStatus').select2({ data: msg });
        }
    });
}



/**
 * Lấy data cho Mã hàng hóa, Màu, Dung tích/Công suất khi chọn loại nghành hàng, gán selected color
 * @param {any} productType
 * @param {any} selectedColor
 */
function selectProductModel(model, selectedColor) {
    $('#hdCheckWaranty').val('');
    $('i.icheck-found-item, i.icheck-document-found-item').hide().attr('data-original-title', '');
    if (!model) {
        $('#txtRProductCapacity').val('');
        $('#txtRProductCode').val('');
        $('select#ddlRProductColor').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
        return;
    }

    $.ajax({
        type: "GET",
        url: '/receive/getproductdetailbymodel/' + model + "/" + selectedColor,
        dataType: "json",
        success: function (msg) {
            $('#txtRProductCapacity').val(msg.ProductCapacity);
            $('#txtRProductCode').val(model);
            $('select#ddlRProductColor').html('').select2({ data: [{ id: '', text: '-Chọn-' }] });
            $('select#ddlRProductColor').select2({ data: msg.Select2Data });
            if (msg.Select2Data && msg.Select2Data.length == 1) {
                $('select#ddlRProductColor').val(msg.Select2Data[0].id).trigger('change.select2');
            }
        }
    });
}

/**
 * Kiểm tra bảo hành khi nhập số seri
 * */
function CheckSeriWarranty() {
    $('button.btn-check-seri-warranty').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var seri = $('input#txtRProductSeriBH').val();

        $('i.icheck-document-found-item').hide().attr('data-original-title', '');

        if (!seri) {
            toastr.warning('Vui lòng nhập seri bảo hành', 'Thông báo');
            return;
        }
        var item = $('#ddlRProductModel').val();
        if (!item || item === '-Chọn-') {
            toastr.warning('Vui lòng nhập vui lòng chọn model', 'Thông báo');
            return;
        }
        var model = $('select#ddlRProductModel').select2('data')[0].text;

        if (seri) {
            seri = seri.trim();
            var itemDisableds = [];
            var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
            mylop.start();

            var obj = new Object();
            obj.Model = model;
            obj.Item = item;
            obj.Serial = seri;
            $.ajax({
                type: "POST",
                contentType: 'application/json',
                url: '/receive/CheckMPLoadElSerial',
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (msg) {
                    msg.Item = item;
                    msg.Serial = seri;
                    msg.Model = model;
                    msg.RequestDoc = false;

                    $('#hdCheckWaranty').val(JSON.stringify(msg));
                    $('#hddRPINVOICEDATE').val(msg.INVOICEDATE);

                    if (msg.Found) {
                        if (msg.IsExpired) {
                            toastr.warning(msg.Message, 'Cảnh báo');
                        } else {
                            //checkOpenUploadTemp = true;
                            //pond2.disabled = false;
                            toastr.info(msg.Message, 'Thông báo');
                        }

                        if (msg.IsFound_MPLoadEWarrantyView) {
                            //Nếu tìm thấy ở MPLoadEWarrantyView thì không hiển thị ngày lên mà yêu cầu khách hàng khai báo
                            $('#txtRProductBuyDate').val('');
                            $('#hddRPRoductExpiredDate').val(msg.ExpiredDate);
                            $('i.icheck-found-item').show().attr('data-original-title', "Tìm thấy ở bước 2 với serial: " + seri + ", Mã SP: " + item + ", Model: " + model + ", " + msg.Message);
                            //checkOpenUploadTemp = false;
                            //pond2.disabled = true;
                        } else {
                            $('#txtRProductBuyDate').val(msg.PurchaseDate);
                            $('#txtRPRoductExpiredDate').val(msg.ExpiredDate);
                            $('i.icheck-found-item').show().attr('data-original-title', "Tìm thấy ở bước 1 với serial: " + seri + ", Mã SP: " + item + ", Model: " + model + ", " + msg.Message);
                        }
                    } else {
                        toastr.info(msg.Message, 'Thông báo');
                        $('i.icheck-found-item').show().attr('data-original-title', msg.Message);
                    }
                },
                complete: function (msg) {
                    mylop.stop();
                }
            });
        }
    });
}

/**
 * Chỉ chạy được với định dạng dd/MM/yyyy
 * @param {any} dateString
 */
function CheckThanCurrentDate(dateString) {
    debugger;
    var dateParts = dateString.split("/");
    var date = new Date();
    // month is 0-based, that's why we need dataParts[1] - 1
    var yourDate = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
    var myDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    if (yourDate > myDate) {
        return true;
    }
    return false;
}

/**
 * Kiểm tra bảo hành khi nhập chứng từ (Ngày mua hàng)
 * */
//var checkOpenUploadTemp = false;
function CheckDocumentWarranty() {
    $('button.btn-check-date-warranty').click(function (e) {
        e.preventDefault();

        var itemCheckSeri = $('#hdCheckWaranty').val();
        if (!itemCheckSeri) {
            toastr.warning('Không tìm thấy thông tin sản phẩm, vui lòng kiểm tra lại tên sản phẩm và serial sản phẩm', 'Cảnh báo');
            return;
        }

        if ($("#txtRProductBuyDate").val() && CheckThanCurrentDate($("#txtRProductBuyDate").val().trim())) {
            toastr.warning('Ngày mua hàng không được lớn hơn ngày hiện tại', 'Cảnh báo');
            return;
        }

        var itemCheck = JSON.parse(itemCheckSeri);
        if (itemCheck) {//Đã check
            if (itemCheck.Found) {//Đã check và tìm thấy thông tin sản phẩm
                if (!itemCheck.RequiredDoc) {
                    if (!itemCheck.IsExpired) {
                        //checkOpenUploadTemp = true;
                        toastr.info('Sản phẩm còn bảo hành, không cần kiểm tra ngày mua hàng', 'Thông báo');
                        return;
                    } else {
                        toastr.info('Sản phẩm đã hết bảo hành, không cần kiểm tra ngày mua hàng', 'Thông báo');
                        return;
                    }
                }

            } else {//Đã check và ko tìm thấy thông tin sản phẩm
                toastr.warning('Không tìm thấy thông tin sản phẩm, vui lòng kiểm tra lại model và serial sản phẩm', 'Cảnh báo');
                return;
            }
        } else {//Chưa check
            toastr.warning('Không tìm thấy thông tin sản phẩm, vui lòng kiểm tra lại model và serial sản phẩm', 'Cảnh báo');
            return;
        }

        if (itemCheck.Item !== $('#ddlRProductModel').val() || itemCheck.Model !== $('select#ddlRProductModel').select2('data')[0].text) {
            toastr.warning('Đã thay đổi thông tin model sản phẩm, vui lòng kiểm tra lại bảo hành', 'Cảnh báo');
            return;
        }
        if (itemCheck.Serial !== $('#txtRProductSeriBH').val().trim()) {
            toastr.warning('Đã thay đổi thông tin serial sản phẩm, vui lòng kiểm tra lại bảo hành', 'Cảnh báo');
            return;
        }


        var $this = $(this);
        var obj = new Object();

        obj.Serial = $('input#txtRProductSeriBH').val();
        if (!obj.Serial) {
            toastr.warning('Vui lòng nhập seri bảo hành', 'Thông báo');
            return;
        }
        obj.Serial = obj.Serial.trim();
        obj.Item = $('#ddlRProductModel').val();
        obj.ItemGroup = $('#ddlRProductType').val();
        if (!obj.ItemGroup) {
            toastr.warning('Vui lòng nhập vui lòng chọn loại nghành hàng', 'Thông báo');
            return;
        }

        if (!obj.Item || obj.Item === '-Chọn-') {
            toastr.warning('Vui lòng nhập vui lòng chọn model', 'Thông báo');
            return;
        }
        obj.Model = $('select#ddlRProductModel').select2('data')[0].text;

        var checkPurchaseDate = '';
        var checkDocument = $('#chkRHasDocument').iCheck('update')[0].checked;
        if (checkDocument) {
            obj.DocDate = $('#txtRProductBuyDate').val();
            checkPurchaseDate = obj.DocDate;
        } else {
            obj.InvoiceDate = $('input#hddRPRoductExpiredDate').val();
            checkPurchaseDate = obj.InvoiceDate;
        }

        if (!obj.DocDate && !obj.InvoiceDate) {
            toastr.warning('Vui lòng kiểm tra số serial BH trước.', 'Thông báo');
            return;
        }

        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();
        $.ajax({
            type: 'POST',
            url: '/receive/CheckMPLoadEWarrantyView',
            dataType: "json",
            data: obj,
            success: function (msg) {

                msg.PurchaseDate = checkPurchaseDate;
                msg.Item = itemCheck.Item;
                msg.Serial = itemCheck.Serial;
                msg.Model = itemCheck.Model;
                msg.RequiredDoc = itemCheck.RequiredDoc;
                msg.RequestDoc = true;
                msg.INVOICEDATE = itemCheck.INVOICEDATE;

                $('#hddRPINVOICEDATE').val(msg.INVOICEDATE);

                $('#hdCheckWaranty').val(JSON.stringify(msg))

                $('#txtRPRoductExpiredDate').val(msg.ExpiredDate);

                if (msg.Found) {
                    if (msg.IsExpired) {
                        toastr.warning(msg.Message, 'Cảnh báo');
                    } else {
                        toastr.info(msg.Message, 'Thông báo');
                    }
                    $('#txtRPRoductExpiredDate').val(msg.ExpiredDate);
                } else {
                    toastr.error(msg.Message, 'Cảnh báo');
                }

                $('i.icheck-document-found-item').show().attr('data-original-title',
                    "Kiểm tra với serial: " + obj.Serial + ", Mã SP: " + obj.Item + ", Model: " + obj.Model + ", " + msg.Message +
                    ", " + (checkDocument ? ("có chứng từ - " + obj.DocDate) : (("không chứng từ - " + obj.InvoiceDate))));

            },
            complete: function (msg) {
                mylop.stop();
            }
        });
    });
}


