/* ------------------------------------------------------------------------------
 *
 *  # Google Visualization - area
 *
 *  Google Visualization area chart demonstration
 *
 *  Version: 1.0
 *  Latest update: August 1, 2015
 *
 * ---------------------------------------------------------------------------- */


// Area chart
// ------------------------------

// Initialize chart
google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawAreaChart);


var myTableArray = [];
myTableArray.push(['Data', 'Saldo']);
$(".table_extrato tr").each(function () {
    var arrayOfThisRow = [];

    var tableData = $(this).find('td');
    if (tableData.length > 0) {
        tableData.each(function (i) {
            //var saldo = parseFloat(document.getElementsByClassName("vrSaldoPie")[0].textContent.replace(",", ""));
            if (i === 0 || i === 4) {
                arrayOfThisRow.push($(this).text());
            }
        });
        myTableArray.push(arrayOfThisRow);
    }
});

var newTableArray = [];
jQuery.each(myTableArray, function (index, item) {
    if (index > 0) {
        newTableArray.push([$(item)[0] === "" ? "Saldo_Anterior" : $(item)[0], parseInt($(item)[1].replace(/\./g, "").slice(0, -5))]);
    }
    else {
        newTableArray.push(item);
    }
});

// Chart settings
function drawAreaChart() {

    // Data
    var data = google.visualization.arrayToDataTable(newTableArray);

    // Options
    var options = {
        fontName: 'Roboto',
        height: 200,
        curveType: 'function',
        fontSize: 12,
        areaOpacity: 0.4,
        chartArea: {
            left: '10%',
            width: '85%',
            height: 150
        },
        pointSize: 4,
        tooltip: {
            textStyle: {
                fontName: 'Roboto',
                fontSize: 13
            }
        },
        vAxis: {
            title: 'Saldo',
            titleTextStyle: {
                fontSize: 13,
                italic: false
            },
            gridarea: {
                color: '#e5e5e5',
                count: 10
            },
            minValue: 0
        },
        legend: {
            position: 'top',
            alignment: 'end',
            textStyle: {
                fontSize: 12
            }
        }
    };

    // Draw chart
    var area_chart = new google.visualization.AreaChart($('#google-area')[0]);
    area_chart.draw(data, options);
}


// Resize chart
// ------------------------------

$(function () {

    // Resize chart on sidebar width change and window resize
    $(window).on('resize', resize);
    $(".sidebar-control").on('click', resize);

    // Resize function
    function resize() {
        drawAreaChart();
    }
});
