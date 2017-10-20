/* ------------------------------------------------------------------------------
 *
 *  # Google Visualization - 3D pie
 *
 *  Google Visualization 3D pie chart demonstration
 *
 *  Version: 1.0
 *  Latest update: August 1, 2015
 *
 * ---------------------------------------------------------------------------- */


// 3D pie chart
// ------------------------------

// Initialize chart
google.load("visualization", "1", {packages:["corechart"]});
google.setOnLoadCallback(drawPie3d);


// Chart settings
function drawPie3d() {


    
    var saldo = parseFloat(document.getElementsByClassName("vrSaldoPie")[0].textContent.replace(",", ""));
    var limite = parseFloat(document.getElementsByClassName("vrSaldoPie")[1].textContent);
    var saldoBloqueado = parseFloat(document.getElementsByClassName("vrSaldoPie")[2].textContent);

    // Data
    var data = google.visualization.arrayToDataTable([
        ['Tipo', 'Valor'],
        ['Saldo', saldo],
        ['Limite do Cheque Especial', limite],
        ['Saldo bloqueado', saldoBloqueado]
    ]);
    

    // Options
    var options_pie_3d = {
        fontName: 'Roboto',
        is3D: true,
        height: 200,
        width: 300,
        chartArea: {
            left: 50,
            width: '95%',
            height: '95%'
        }
    };


    // Instantiate and draw our chart, passing in some options.
    var pie_3d = new google.visualization.PieChart($('#google-pie-3d')[0]);
    pie_3d.draw(data, options_pie_3d);
}