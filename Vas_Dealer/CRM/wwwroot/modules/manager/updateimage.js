$(function () {
    BindEventReport1();
});

/**
 * Sự kiện tìm kiếm
 * */
function BindEventReport1() {
    $('button.btn-search').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin tìm kiếm
         * */
        var item = GetSearchParams1();
        /**
         * Lấy kết quả tìm kiếm
         * @param {any} control
         * @param {any} item
         */
        GetSearchResult1($(this), item);
    });
}

/**
 * Lấy thông tin tìm kiếm
 * */
function GetSearchParams1() {
    var obj = new Object();
    if ($("#txtTicketId").val() == null) {
        obj.TickitId = "";
    } else { obj.TickitId = $("#txtTicketId").val(); };
    return obj;
}
/**
 * Lấy kết quả tìm kiếm
 * @param {any} control
 * @param {any} item
 */
var table1;
function GetSearchResult1(control, item) {
    var itemDisableds = [control];
    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: '/manager/GetRepairInsuranceResponse',
        dataType: "json",
        data: JSON.stringify(item),
        success: function (msg) {
            if (table1) table1.destroy();
            table1 = $('table#tblList').DataTable({
                "processing": true,
                "searching": false,
                "data": msg,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle text-center'
                    },
                    {
                        "data": "FileName",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle phone'
                    },
                    {
                        "data": "FileContentType",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle text-center'
                    },
                    {
                        "data": "UploadType",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle text-center'
                    },

                ],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 10
            });
            if (msg != null && msg.length > 0) {
                $("#uploadfile").show();
                if (location.hash) {
                    $('.nav-tabs a[href="' + location.hash + '"]').tab('show');
                }
                pond1.files = [];
                pond2.files = [];
                pond3.files = [];
                for (var i = 0; i < msg.length; i++) {
                    $("#hddTicketTemplate").val(msg[0].TempFolder);
                    if (msg[i].UploadType == '1' ) {
                        pond1.files = [
                            {
                                source: msg[i].TempFolder + "|1",
                                options: {
                                    type: 'local'
                                }
                            }
                        ]
                    }
                    else if (msg[i].UploadType == '2') {
                        pond2.files = [
                            {
                                source: msg[i].TempFolder + "|2",
                                options: {
                                    type: 'local'
                                }
                            }
                        ]
                    }
                    else if (msg[i].UploadType == '3') {
                        pond3.files = [
                            {
                                source: msg[i].TempFolder + "|3",
                                options: {
                                    type: 'local'
                                }
                            }
                        ]
                    }
                }
            }
            else {
                pond1.files = [];
                pond2.files = [];
                pond3.files = [];
            }
            
        },
        complete: function (msg) {
            mylop.stop();
        }
    });

    
}
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
            request.open('POST', '/manager/UploadFile');
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
        load: '/manager/LoadUploadFile/',
        remove: (source, load, error) => {
            const request = new XMLHttpRequest();
            request.open('GET', '/manager/RemoveUploadFile/' + source);
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
            request.open('POST', '/manager/UploadFile');
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
        load: '/manager/LoadUploadFile/',
        remove: (source, load, error) => {
            const request = new XMLHttpRequest();
            request.open('GET', '/manager/RemoveUploadFile/' + source);
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
            request.open('POST', '/manager/UploadFile');
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
        load: '/manager/LoadUploadFile/',
        remove: (source, load, error) => {
            const request = new XMLHttpRequest();
            request.open('GET', '/manager/RemoveUploadFile/' + source);
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

