// Write your Javascript code.
$('table tr').each(function (index) {
    var lastCol = $(this).children("td").eq(2).text();
    if (lastCol.indexOf("Waiting on Business") >= 0) {
        $(this).css("background-color", "orange");
    }
    if (lastCol.indexOf("Closed") >= 0) {
        $(this).css("background-color", "#90EE90");
    }
    if (lastCol.indexOf("Work In Progress") >= 0) {
        $(this).css("background-color", "yellow");
    }
    if (lastCol.indexOf("On Hold") >= 0) {
        $(this).css("background-color", "Red");
    }
    if (lastCol.indexOf("Waiting on the Vendor") >= 0) {
        $(this).css("background-color", "#FFA07A");
    }
    if (lastCol.indexOf("Waiting on WT Tech Solutions") >= 0) {
        $(this).css("background-color", "#E9967A");
    }
    if (lastCol.indexOf("Open") >= 0) {
        $(this).css("background-color", "#FFFFE0");
    }
    if (lastCol.indexOf("Canceled") >= 0) {
        $(this).css("background-color", "#FFF8DC");
    }
    if (lastCol.indexOf("Delayed") >= 0) {
        $(this).css("background-color", "#E6E6FA");
    }
    if (lastCol.indexOf("New") >= 0) {
        $(this).css("background-color", "#DCDCDC");
    }
});
