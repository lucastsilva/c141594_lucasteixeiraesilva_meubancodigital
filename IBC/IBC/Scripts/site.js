function marcarFavorito(element) {
    var star = $(element).children();
    var star_class = star.attr('class');
    if (star_class === "icon-star-empty3") {
        star.removeClass("icon-star-empty3")
            .addClass("icon-star-full2")
            .css({ 'color': '#e5e500' });
    }
    else {
        star.removeClass("icon-star-full2")
            .addClass("icon-star-empty3")
            .css({ 'color': 'black' });
    }

    salvarOpcoesFavoritos();
}

function salvarOpcoesFavoritos() {
    var co_cliente = $('#co_cliente').val();
    var linksFavoritos = $('.menu_favorito');

    var lsCoMenuFavorito = [];
    jQuery.each(linksFavoritos, function (i, element) {
        var star_class = $(element).children().attr('class');
        if (star_class === "icon-star-full2") {
            lsCoMenuFavorito.push($(element).attr('id'));
        }
    });

    $.ajax({
        type: "POST",
        url: "../MenuFavorito/salvarMenuFavorito",
        data: { co_cliente: co_cliente, lsCoMenuFavorito: lsCoMenuFavorito },
        success: function (msg) {
            //alert(msg);
        }
    });
}

function carregaFavoritos() {
    var arrayLinksFavoritos = $('#lsCoMenusDisponiveis').val().split(',');
    
    $("div[id^='div_menu_favorito_']").each(function (i, el) {
        $(this).hide();

    });

    jQuery.each(arrayLinksFavoritos, function (i, co_menu_favorito) {
        var linkFavorito = $('#' + co_menu_favorito);

        var star = $(linkFavorito).children();
        star.removeClass("icon-star-empty3")
            .addClass("icon-star-full2")
            .css({ 'color': '#e5e500' });

        mostrarFavorito(co_menu_favorito);
    });
}

function mostrarFavorito(co_menu_favorito) {

    $("div[id=div_menu_favorito_" + co_menu_favorito + "]").show();
}

function fecharElement(id) {
    $("div[id=div_menu_favorito_" + id + "]").hide();
}



// ========================================
//
// Content area height
//
// ========================================


// Calculate min height
function containerHeight() {
    var availableHeight = $(window).height() - $('.page-container').offset().top - $('.navbar-fixed-bottom').outerHeight();

    $('.page-container').attr('style', 'min-height:' + availableHeight + 'px');
}

// Initialize
containerHeight();

function marcarNumero(element) {
    var id = $(element).attr('id');
    if ($('#' + id).hasClass("nrAtivo")) {
        $("#" + id).removeClass("nrAtivo");
    }
    else {
        $("#" + id).addClass("nrAtivo");
    }   
}



$(document).ready(function () {
    carregaFavoritos();

    // Button with progress
    Ladda.bind('.btn-ladda-progress', {
        callback: function (instance) {
            var progress = 0;
            var interval = setInterval(function () {
                progress = Math.min(progress + Math.random() * 0.1, 1);
                instance.setProgress(progress);

                if (progress === 1) {
                    instance.stop();
                    clearInterval(interval);
                }
            }, 200);
        }
    });

    $(".applyBtn").click(function () {
        $("#extratoTable").html("<div style='text-align: center'><i class='fa fa-spinner fa-pulse fa-3x fa-fw'></i><span class='sr-only'>Carregando...</span></div>");
        $("#extrato-stats").hide();
        $.ajax({
            type: "POST",
            url: 'getExtratoPorPeriodo',
            data: {
                hdnDataInicio: "01/09/2017",
                hdnDataFinal: "30/09/2017",
                sltOutroMes: "1",
                txtDataInicio: "01",
                txtDataFinal: "30"
            },
            success: function (data) {
                $("#extrato-stats").show();
                $("#extratoTable").html(data);
            }
        });
    });

    $('input[type=radio][name=qtdNrs]').change(function () {
        var nr = $('input[name=qtdNrs]:checked').val();
        if (nr == 6) {
            $('#custoAposta').html("<p>Sua aposta custará <strong>R$ 3,50</strong></p>");
            $('#spanPontos').html("150");
            $('#spanCusto').html("R$ 3,50");
        }
        if (nr == 7) {
            $('#custoAposta').html("<p>Sua aposta custará <strong>R$ 24,50</strong></p>");
            $('#spanPontos').html("1000");
            $('#spanCusto').html("R$ 24,50");
        }
        if (nr == 8) {
            $('#custoAposta').html("<p>Sua aposta custará <strong>R$ 98,00</strong></p>");
            $('#spanPontos').html("4200");
            $('#spanCusto').html("R$ 98,00");
        }
        if (nr == 9) {
            $('#custoAposta').html("<p>Sua aposta custará <strong>R$ 294,00</strong></p>");
            $('#spanPontos').html("10000");
            $('#spanCusto').html("R$ 294,00");
        }
    });
});

