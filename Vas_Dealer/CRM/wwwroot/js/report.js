
$('#dateFrom').datetimepicker({
    format: 'd/m/Y',
    timepicker: false,
    onChangeDateTime: function (e) {
        $('#dateFrom').attr('value', moment(e).format('DD/MM/YYYY'));
    },
});
$('#dateTo').datetimepicker({
    format: 'd/m/Y',
    timepicker: false,
    onChangeDateTime: function (e) {
        $('#dateTo').attr('value', moment(e).format('DD/MM/YYYY'));
    }
});


function toHHMMSS(secs) {
    var seconds = parseInt(secs, 10)
    var hours = Math.floor(seconds / 3600);
    var minutes = Math.floor((seconds - (hours * 3600)) / 60);
    seconds = seconds - (hours * 3600) - (minutes * 60);

    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }
    var time = hours + ':' + minutes + ':' + seconds;
    return time;
}

function GenerateTable(data, type) {
    var html = "";
    var page = "";
    if (data.Results.length > 0) {
        switch (type) {
            case 'calling':
                data.Results.forEach((ele, index) => {
                    html += "<tr>";
                    html += "<td>" + (index + 1) + "</td>";
                    html += "<td>" + ele.PhoneNumber + "</td>";
                    html += "<td>" + ele.Agent + '/' + ele.Extension + "</td>";
                    html += "<td>" + ele.Direction + "</td>";
                    html += "<td>" + (ele.IsAnswer ? 'Trả lời' : 'Không trả lời') + "</td>";
                    html += "<td>" + ele.DateCallStart + "</td>";
                    html += "<td>" + ele.DateCallEnd + "</td>";
                    html += "<td>" + toHHMMSS(ele.TalkTime) + "</td>";
                    html += "</tr>";
                })
                if (data.PageCount > 1) {
                    $('#pagination').removeClass('d-none');
                    for (var i = 1; i <= data.PageCount; i++) {
                        var current = data.CurrentPage == i ? true : false;
                        page += '<li class="page-item ' + (current ? 'active' : '') + '"><a class="page-link" href="javascript:loadPage(' + i + ')">' + i + '</a></li>';
                    }
                } else {
                    page = "";
                    $('#pagination').addClass('d-none');
                }
                break;
            case 'recording':
                data.Results.forEach((ele, index) => {
                    html += "<tr>";
                    html += "<td>" + (index + 1) + "</td>";
                    html += "<td>" + ele.UniqueId + "</td>";
                    html += "<td>" + ele.Agent + '/' + ele.Extension + "</td>";
                    html += "<td>" + ele.PhoneNumber + "</td>";
                    html += "<td>" + toHHMMSS(ele.TalkTime) + "</td>";
                    html += "<td>" + ele.DateCallStart + "</td>";
                    html += "<td id='tbRecording" + (index + 1) + "'><i id='" + (index + 1) + "' uid='" + (index + 1) + "' ucallid='" + ele.UniqueId + "' class='fa fa-play play-recording'></i></td>";
                    html += "</tr>";
                })
                if (data.PageCount > 1) {
                    $('#pagination').removeClass('d-none');
                    for (var i = 1; i <= data.PageCount; i++) {
                        var current = data.CurrentPage == i ? true : false;
                        page += '<li class="page-item ' + (current ? 'active' : '') + '"><a class="page-link" href="javascript:loadPageRecording(' + i + ')">' + i + '</a></li>';
                    }
                } else {
                    page = "";
                    $('#pagination').addClass('d-none');
                }
                break;
            case 'working':
                data.Results.forEach((ele, index) => {
                    html += "<tr>";
                    html += "<td>" + ele.HourGroup + "</td>";
                    html += "<td class='text-right'>" + ele.TotalCall + "</td>";
                    html += "<td class='text-right'>" + ele.CallAnswer + "</td>";
                    html += "<td class='text-right'>" + ele.CallNoAnswer + "</td>";
                    html += "</tr>";
                })
                break;
            case 'agent_activity':
                data.Results.forEach((ele, index) => {
                    html += "<tr>";
                    html += "<td>" + (index + 1) + "</td>";
                    html += "<td>" + ele.AccountName + "</td>";
                    html += "<td>" + ele.StatusName + "</td>";
                    html += "<td>" + ele.StartTime + "</td>";
                    html += "<td>" + ele.EndTime + "</td>";
                    html += "</tr>";
                })
                if (data.PageCount > 1) {
                    $('#pagination').removeClass('d-none');
                    for (var i = 1; i <= data.PageCount; i++) {
                        var current = data.CurrentPage == i ? true : false;
                        page += '<li class="page-item ' + (current ? 'active' : '') + '"><a class="page-link" href="javascript:loadDataAgentActivity(' + i + ')">' + i + '</a></li>';
                    }
                } else {
                    page = "";
                    $('#pagination').addClass('d-none');
                }
                break;
            case 'productivity':
                data.Results.forEach((ele, index) => {
                    html += "<tr>";
                    html += "<td>" + (index + 1) + "</td>";
                    html += "<td>" + ele.DateGroup + "</td>";
                    html += "<td>" + ele.AccountName + "</td>";
                    html += "<td>" + convertDateTimeDay(ele.TotalTime) + "</td>";
                    html += "<td>" + convertDateTimeDay(ele.TotalAvailTime) + "</td>";
                    html += "<td>" + convertDateTimeDay(ele.TotalNotAvailTime) + "</td>";
                    html += "<td>" + ele.TotalCall + "</td>";
                    html += "<td>" + ele.TotalCallAns + "</td>";
                    html += "<td>" + ele.TotalCallNotAns + "</td>";
                    html += "</tr>";
                })
                break;
            default:
                break;
        }

    } else {
        html = "Không có dữ liệu";
    }

    $('#dataBody').html(html);
    $('#pagination').html(page);
}

function convertDateTimeDay(str) {
    const _str = str.split('.');
    return _str.length > 1 ? _str[0] + ' ngày ' + _str[1] : _str[0];
}

// start calling
$('#reportCalling').click(function () {
    loadPage(1);
})

function loadPage(page) {
    var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
    var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
    var phone = $('#phone').val();
    if (!dateFrom) {
        $('#dateFrom').addClass('is-invalid');
        return;
    }
    if (!dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    if (dateFrom > dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    $('#dateFrom').removeClass('is-invalid');
    $('#dateTo').removeClass('is-invalid');
    if (dateFrom == dateTo) {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 23:59:59';
    } else {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 00:00:00';
    }
    GetDataCalling(dateFrom, dateTo, phone, page);
}

function GetDataCalling(dateFrom, dateTo, phone, page) {
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();

    var dataQuery = phone ? "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&phone=" + phone + "&page=" + page : "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&page=" + page;
    $.ajax({
        type: "GET",
        url: "/api/report/calling",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            GenerateTable(msg, 'calling');
            console.log('data', msg);
        },
        error: function (msg) {
            console.log(msg);
        },
        complete: function (msg) {
        }
    });
}

// End calling

// Start Record
$('#reportRecord').click(function () {
    loadPageRecording(1);
})

function loadPageRecording(page) {
    var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
    var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
    var phone = $('#phone').val();
    var eQueue = document.getElementById("sltQueue");
    var eAgent = document.getElementById("sltAgent");
    var queue = eQueue.options[eQueue.selectedIndex].value;
    var agent = eAgent.options[eAgent.selectedIndex].value;
    if (!dateFrom) {
        $('#dateFrom').addClass('is-invalid');
        return;
    }
    if (!dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    if (dateFrom > dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    $('#dateFrom').removeClass('is-invalid');
    $('#dateTo').removeClass('is-invalid');
    if (dateFrom == dateTo) {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 23:59:59';
    } else {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 00:00:00';
    }
    GetDataRecord(dateFrom, dateTo, agent, queue, phone, page);
}

function GetDataRecord(dateFrom, dateTo, agent, queue, phone, page) {
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();

    var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&page=" + page + "&agent=" + agent + "&queue=" + queue;
    if (phone) {
        dataQuery += "&phone=" + phone;
    }
    $.ajax({
        type: "GET",
        url: "/api/report/recording",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            GenerateTable(msg, 'recording');
            console.log('data', msg);
        },
        error: function (msg) {
            console.log(msg);
        },
        complete: function (msg) {
        }
    });
}

// End Record


// Start Agent Activity
$('#reportAgentActivity').click(function () {
    loadDataAgentActivity(1)
})

function loadDataAgentActivity(page) {
    var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
    var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
    var eAgent = document.getElementById("sltAgent");
    var agent = eAgent.options[eAgent.selectedIndex].value;
    if (!dateFrom) {
        $('#dateFrom').addClass('is-invalid');
        return;
    }
    if (!dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    if (dateFrom > dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    $('#dateFrom').removeClass('is-invalid');
    $('#dateTo').removeClass('is-invalid');
    if (dateFrom == dateTo) {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 23:59:59';
    } else {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 00:00:00';
    }
    GetDataAgentActivity(dateFrom, dateTo, agent, page);
}

function GetDataAgentActivity(dateFrom, dateTo, agent, page) {
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
    var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&page=" + page + "&agent=" + agent;
    $.ajax({
        type: "GET",
        url: "/api/report/agentActivity",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            if (msg.data) {
                return;
            };
            var data = { Results: msg };
            GenerateTable(msg, 'agent_activity');
        },
        error: function (msg) {
            console.log(msg);
        },
        complete: function (msg) {
        }
    });
}

// End Agent Activity


// Start Working Time
$('#reportWorkingTime').click(function () {
    loadDataWorkingTime(1)
})

function loadDataWorkingTime(page) {
    var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
    var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
    if (!dateFrom) {
        $('#dateFrom').addClass('is-invalid');
        return;
    }
    if (!dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    if (dateFrom > dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    $('#dateFrom').removeClass('is-invalid');
    $('#dateTo').removeClass('is-invalid');
    if (dateFrom == dateTo) {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 23:59:59';
    } else {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 00:00:00';
    }
    GetDataWorkingTime(dateFrom, dateTo, page);
}

function GetDataWorkingTime(dateFrom, dateTo, page) {
    if ($.fn.dataTable.isDataTable('#tableWorking')) {
        $('#tableWorking').DataTable().destroy()
    }
    $('#dataBody').hide();
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
    var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&page=" + page;
    $.ajax({
        type: "GET",
        url: "/api/report/workingTime",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            if (msg.data) {
                return;
            }
            var data = { Results: msg }
            GenerateTable(data, 'working');
            $('#tableWorking').DataTable({
                "retrieve": true,
                "processing": true,
                "ordering": false,
                "info": false,
                "lengthChange": false,
                "searching": false,
                language: {
                    paginate: { previous: '<', next: '>' }
                }
            });
        },
        error: function (msg) {
            // $('#tableWorking').DataTable().destroy();
        },
        complete: function (msg) {
            $('#dataBody').show();
        }
    });
}

// End Working Time

// start productivity

$('#rpProductivity').click(function () {
    loadDataProductivity(1)
})


function loadDataProductivity(page) {
    var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
    var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
    var eAgent = document.getElementById("sltAgent");
    var agent = eAgent.options[eAgent.selectedIndex].value;
    var eType = document.getElementById("typeReport");
    var typeRp = eType.options[eType.selectedIndex].value;

    if (!dateFrom) {
        $('#dateFrom').addClass('is-invalid');
        return;
    }
    if (!dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    if (dateFrom > dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    $('#dateFrom').removeClass('is-invalid');
    $('#dateTo').removeClass('is-invalid');
    if (dateFrom == dateTo) {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 23:59:59';
    } else {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 00:00:00';
    }
    GetDataProductivity(dateFrom, dateTo, agent, typeRp, page);
}

function GetDataProductivity(dateFrom, dateTo, agent, type, page) {
    if ($.fn.dataTable.isDataTable('#tableProductivity')) {
        $('#tableProductivity').DataTable().destroy()
    }
    $('#dataBody').hide();
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
    var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&agent=" + agent + "&type=" + type + "&page=" + page;
    $.ajax({
        type: "GET",
        url: "/api/report/productivity",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            if (msg.data) {
                return;
            }
            var data = { Results: msg }
            GenerateTable(data, 'productivity');
            $('#tableProductivity').DataTable({
                "retrieve": true,
                "processing": true,
                "ordering": false,
                "info": false,
                "lengthChange": false,
                "searching": false,
                language: {
                    paginate: { previous: '<', next: '>' }
                }
            });
        },
        error: function (msg) {
            // $('#tableWorking').DataTable().destroy();
        },
        complete: function (msg) {
            $('#dataBody').show();
        }
    });
}
// end productivity

function loadReportDetail() {
    console.log('a', 'acscasc');
}
$('body').on('click', '.play-recording', function (e) {
    //e.preventDefault();
    //$("#spanRecording").html('');
    var $this = $(this);
    var uid = $this.attr('uid');

    //var itemDisableds = [$(this)];
    //var mylop = new myMpLoop($(this), 'Đang xử lý', $(this).html(), itemDisableds);
    //mylop.start();
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
    var dataQuery = "callid=" + $this.attr('ucallid');
    $.ajax({
        type: "GET",
        url: "/api/report/DownloadRecording",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            $(".play-recording").show();
            $(".spRecording").remove();
            $("#" + uid).hide();
            var item = msg.value;
            var html = "";
            html += '<span class="spRecording"><audio controls> <source src="' + item + '" type="audio/ mpeg"></audio></span>';
            console.log(html);
            $("#tbRecording" + uid).append(html);
            var audio = $("audio").get(0);
            audio.src = item;
            audio.load();
            audio.play();

        },
        error: function (data) {
        }
    });
});


// Start export recording
// $('body').on('click', '#exportRecord', function (e) {
$('#exportRecord').click(function () {
    var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
    var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
    var phone = $('#phone').val();
    var eQueue = document.getElementById("sltQueue");
    var eAgent = document.getElementById("sltAgent");
    var queue = eQueue.options[eQueue.selectedIndex].value;
    var agent = eAgent.options[eAgent.selectedIndex].value;
    if (!dateFrom) {
        $('#dateFrom').addClass('is-invalid');
        return;
    }
    if (!dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    if (dateFrom > dateTo) {
        $('#dateTo').addClass('is-invalid');
        return;
    }
    $('#dateFrom').removeClass('is-invalid');
    $('#dateTo').removeClass('is-invalid');
    if (dateFrom == dateTo) {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 23:59:59';
    } else {
        dateFrom = $('#dateFrom').val() + ' 00:00:00';
        dateTo = $('#dateTo').val() + ' 00:00:00';
    }
    var headers = {};
    headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
    var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&page=1&agent=" + agent + "&queue=" + queue;
    if (phone) {
        dataQuery += "&phone=" + phone;
    }
    $.ajax({
        type: "GET",
        url: "/api/report/ExportReportRecording",
        data: dataQuery,
        headers: headers,
        dataType: 'json',
        success: function (msg) {
            window.location = msg.value;
        },
        error: function (msg) {
            console.log(msg);
        },
        complete: function (msg) {
        }
    });
})



// End export recording

    $('#exportAgentActivity').on('click', function () {
        var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
        var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
        var eAgent = document.getElementById("sltAgent");
        var agent = eAgent.options[eAgent.selectedIndex].value;
        if (!dateFrom) {
            $('#dateFrom').addClass('is-invalid');
            return;
        }
        if (!dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        if (dateFrom > dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        $('#dateFrom').removeClass('is-invalid');
        $('#dateTo').removeClass('is-invalid');
        if (dateFrom == dateTo) {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 23:59:59';
        } else {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 00:00:00';
        }
        var headers = {};
        headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
        var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&agent=" + agent;
        $.ajax({
            type: "GET",
            url: "/api/report/ExportReportAgentActivity",
            data: dataQuery,
            headers: headers,
            dataType: 'json',
            success: function (msg) {
                window.location = msg.value;
            },
            error: function (msg) {
                console.log(msg);
            },
            complete: function (msg) {
            }
        });
    })

    $('body').on('click', '#exportWorkingTime', function (e) {
        var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
        var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
        if (!dateFrom) {
            $('#dateFrom').addClass('is-invalid');
            return;
        }
        if (!dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        if (dateFrom > dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        $('#dateFrom').removeClass('is-invalid');
        $('#dateTo').removeClass('is-invalid');
        if (dateFrom == dateTo) {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 23:59:59';
        } else {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 00:00:00';
        }

        var headers = {};
        headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
        var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo;
        $.ajax({
            type: "GET",
            url: "/api/report/ExportReportWorkingTime",
            data: dataQuery,
            headers: headers,
            dataType: 'json',
            success: function (msg) {
                window.location = msg.value;
            },
            error: function (msg) {
                console.log(msg);
            },
            complete: function (msg) {
            }
        });
    });


    $('body').on('click', '#exportCalling', function (e) {
        var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
        var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
        var phone = $('#phone').val();
        if (!dateFrom) {
            $('#dateFrom').addClass('is-invalid');
            return;
        }
        if (!dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        if (dateFrom > dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        $('#dateFrom').removeClass('is-invalid');
        $('#dateTo').removeClass('is-invalid');
        if (dateFrom == dateTo) {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 23:59:59';
        } else {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 00:00:00';
        }
        var headers = {};
        headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();

        var dataQuery = phone ? "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&phone=" + phone : "dateFrom=" + dateFrom + "&dateTo=" + dateTo;
        $.ajax({
            type: "GET",
            url: "/api/report/ExportReportCalling",
            data: dataQuery,
            headers: headers,
            dataType: 'json',
            success: function (msg) {
                window.location = msg.value;
            },
            error: function (msg) {
                console.log(msg);
            },
            complete: function (msg) {
            }
        });
    });


//export productivity
    $('body').on('click', '#exportProductivity', function (e) {
        var dateFrom = moment($('#dateFrom').val(), 'DD/MM/YYYY').unix();
        var dateTo = moment($('#dateTo').val(), 'DD/MM/YYYY').unix();
        var eAgent = document.getElementById("sltAgent");
        var agent = eAgent.options[eAgent.selectedIndex].value;
        var eType = document.getElementById("typeReport");
        var typeRp = eType.options[eType.selectedIndex].value;

        if (!dateFrom) {
            $('#dateFrom').addClass('is-invalid');
            return;
        }
        if (!dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        if (dateFrom > dateTo) {
            $('#dateTo').addClass('is-invalid');
            return;
        }
        $('#dateFrom').removeClass('is-invalid');
        $('#dateTo').removeClass('is-invalid');
        if (dateFrom == dateTo) {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 23:59:59';
        } else {
            dateFrom = $('#dateFrom').val() + ' 00:00:00';
            dateTo = $('#dateTo').val() + ' 00:00:00';
        }
        var headers = {};
        headers.Authorization = 'Bearer ' + $('input[id$="hidToken"]').val();
        var dataQuery = "dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&agent=" + agent + "&type=" + typeRp;
        $.ajax({
            type: "GET",
            url: "/api/report/ExportReportProductivity",
            data: dataQuery,
            headers: headers,
            dataType: 'json',
            success: function (msg) {
                window.location = msg.value;
            },
            error: function (msg) {
                console.log(msg);
            },
            complete: function (msg) {
            }
        });
    });