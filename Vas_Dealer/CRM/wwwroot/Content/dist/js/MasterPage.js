var itemConfirmExit;
function setUpAjax(token) {
    jQuery.ajaxSetup({
        //headers: headers,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('AuthorizeLocation', "CLI");
            xhr.setRequestHeader('Authorization', 'Bearer ' + token);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
            if (jqXHR.status === 404) {
                alert("Element not found.");
            } else if (jqXHR.status === 200) {//Trường hợp đã đăng xuất ở tab này mà gọi api ở tab khác
                console.log(jqXHR);
                if (jqXHR.responseText.indexOf('DOCTYPE html') > 1) {
                    if (!itemConfirmExit) itemConfirmExit = $.confirm({
                        type: 'red',
                        title: '<i class="fa fa-warning fa-lg text-red"> Thông báo',
                        content: 'Phiên đăng nhập kết thúc!',
                        buttons: {
                            somethingElse: {
                                text: 'Đăng nhập lại',
                                btnClass: 'btn-blue',
                                keys: ['enter', 'shift'],
                                action: function () {
                                    location.href = '/account/logout?ReturnUrl=' + window.location.pathname;
                                }
                            }
                        }
                    });
                }
            } else if (jqXHR.status === 401) {
                if (!itemConfirmExit) itemConfirmExit = $.confirm({
                    type: 'red',
                    title: '<i class="fa fa-warning fa-lg text-red"> Thông báo',
                    content: 'Phiên đăng nhập kết thúc!',
                    buttons: {
                        somethingElse: {
                            text: 'Đăng nhập lại',
                            btnClass: 'btn-blue',
                            keys: ['enter', 'shift'],
                            action: function () {
                                debugger;
                                location.href = '/account/logout?ReturnUrl=' + window.location.pathname;
                            }
                        }
                    }
                });
            } else if (jqXHR.status === 403) {
                location.href = '/err/forbidden/?ReturnUrl=' + window.location.pathname;
            } else if (jqXHR.status === 400) {
                console.log(jqXHR);
                var mess = jqXHR.responseText;
                toastr.warning(mess, "Thông báo");
            } else if (jqXHR.status === 500) {
                console.log(jqXHR);
                var mess = jqXHR.responseText === 'timeout' ? 'Thời gian tìm kiếm quá dài vui lòng nhập thời gian ngắn hơn.' : jqXHR.responseText;
                toastr.error(mess, "Cảnh báo");
            }
        }
    });
}

function formatState(state) {
    if (!state.id) { return state.text; }
    var $state = $(
        '<span><i class="fa fa-caret-right" /> ' + state.text + '</span>'
    );
    return $state;
}

function formatState_Style1(state) {
    if (!state.id) { return state.text; }
    var $state = $(
        '<span><i class="fa fa-black-tie" /> ' + state.text + '</span>'
    );
    return $state;
}

$(function () {
    setUpAjax($('#hidToken').val());

    $('[data-toggle="tooltip"]').tooltip();
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });
    //Red color scheme for iCheck
    $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
        checkboxClass: 'icheckbox_minimal-red',
        radioClass: 'iradio_minimal-red'
    });
    //Flat red color scheme for iCheck
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

    $(".select2").select2({
        templateResult: formatState,
        placeholder: '-Chọn-',
        language: {
            noResults: function (params) {
                return "Không có kết quả!";
            }
        }
    });

    $(".select2-style1").select2({
        templateResult: formatState_Style1,
        language: {
            noResults: function (params) {
                return "Không có kết quả!";
            }
        }
    });

    $.datepicker.setDefaults($.datepicker.regional['vi']);

    $('.input-datepicker-19302018').datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '1930:2018',
        dateFormat: 'dd/mm/yy',
        lang: 'vn'
    });
    $('.input-datepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '1900:2050',
        dateFormat: 'dd/mm/yy',
        lang: 'vn'
    });
    $('.input-datetimepicker').datetimepicker({
        format: 'd/m/Y H:i:s'
    });
    $('.input-datetimepicker-ddMMyyhhmm').datetimepicker({
        format: 'd/m/Y H:i'
    });
    $('.input-datetimepicker-ddMMyy').datetimepicker({
        format: 'd/m/Y',
        pickTime: false
    });
    $('.input-timepicker').datetimepicker({
        datepicker: false,
        format: 'H:i'
    });

    $(".input-monthyearpicker").datepicker({
        dateFormat: 'MM, yy',
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,

        onClose: function (dateText, inst) {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).val($.datepicker.formatDate('MM, yy', new Date(year, month, 1)));
        }
    });

    $(".input-monthyearpicker").focus(function () {
        $(".ui-datepicker-calendar").hide();
        $("#ui-datepicker-div").position({
            my: "center top",
            at: "center bottom",
            of: $(this)
        });
    });



    //Flat red color scheme for iCheck
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
});


function cleanObject(obj) {
    for (var propName in obj) {
        if (obj[propName] === null || obj[propName] === undefined) {
            obj[propName] = '';
        }
    }
    return obj;
}

// sự kiện logout
function logOutMaster() {
    $.ajax({
        type: 'POST',
        url: '/api/Account/Logout'
    }).done(function (data) {
        localStorage.removeItem(tokenKey);
        location.href = '/';
        //$.ajax({
        //    type: 'GET',
        //    url: 'http://192.168.50.110:8080/mpcc-portal/logout/',
        //    success: function (msg) {
        //        alert("Đăng xuất MPCC thành công");
        //    }, error: function (msg) {
        //        alert("Đăng xuất MPCC thất bại");
        //    }
        //}).done(function () {
        //    localStorage.removeItem(tokenKey);
        //    location.href = '/';
        //})
    });
}


function removeVietnameseTones(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
    str = str.replace(/ + /g, " ");
    str = str.trim();
    // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
    return str;
}