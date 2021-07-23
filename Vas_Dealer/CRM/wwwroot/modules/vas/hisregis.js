
$(function () {
    BindDataToGrid(); 
});

 


/**
 * Lấy danh sách lưới lịch sử
 * */
var tableData;
function BindDataToGrid() { 
    $.ajax({
        type: "POST",
        url: "/VAS/GetListLotto",
        dataType: "json",
        success: function (msg) {
            debugger;
            var html = "";
            var item = msg.value;
            var count = 1;
            for (var i = 0; i < item.length; i++) {
                if (count <= 33) {
                    html += "<tr> <td value='" + count + "'>" + count + "</td>";
                    html += "<td value='" + item[i].AccountUserName + "'>" + item[i].AccountUserName + "</td>";
                    html += "<td value='" + item[i].AccountFullName + "'>" + item[i].AccountFullName + "</td>";
                    html += "<td value='" + item[i].Score + "'>" + item[i].Score + "</td> </tr>";
                    count++;
                }
                else {
                    html += "<tr style = 'background-color: red;'> <td value='" + count + "'>" + count + "</td>";
                    html += "<td value='" + item[i].AccountUserName + "'>" + item[i].AccountUserName + "</td>";
                    html += "<td value='" + item[i].AccountFullName + "'>" + item[i].AccountFullName + "</td>";
                    html += "<td value='" + item[i].Score + "'>" + item[i].Score + "</td> </tr>";
                    count++;
                }
            }
            $("#bdList").html(html);
        }
    });
}

 