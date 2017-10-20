using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IBC.Controllers;

namespace IBC.Models
{
    public class paginaInicialView
    {
        
        public string saldo { get; set; }
        public string lsCoMenusDisponiveis { get; set; }
        public int co_cliente { get; set; }

        public paginaInicialView(ConectaIBC ibc)
        {            
            this.saldo = ibc.saldo.saldo;
            this.lsCoMenusDisponiveis = string.Join(",", new MenuFavoritoController().getLsCoMenuFavoritos(ibc.co_cliente).ToArray());
            this.co_cliente = ibc.co_cliente;
        }
    }

    public class sideBarView
    {
        public string nomeCliente { get; set; }
        public string agencia { get; set; }
        public string contaCorrente { get; set; }
        public bool ic_trocarConta { get; set; }
        public Categoria categoria { get; set; }

        public sideBarView(ConectaIBC ibc)
        {
            //Verifica se o cliente possui mais de uma conta, acionando o botão de Trocar Conta
            this.ic_trocarConta = ibc.contaCliente.contas.Count() > 1 ? true : false;

            Conta conta = ibc.contaCliente.contas.FirstOrDefault();

            this.nomeCliente = ibc.dadosUsuario.nome.Trim();
            this.agencia = conta.agencia;
            this.contaCorrente = conta.conta.TrimStart(new Char[] { '0' });
            this.categoria = ibc.categoria;
        }
    }

    public class navBarView
    {
        public string nomeCliente { get; set; }
        public string dtAcesso { get; set; }

        public navBarView(ConectaIBC ibc)
        {
            this.nomeCliente = ibc.dadosUsuario.nome.Split(new Char[] { ' ' }).First().Trim();
            this.dtAcesso = "Último acesso em " + ibc.dadosUsuario.dataUltimoAcesso + " às " + ibc.dadosUsuario.horaUltimoAcesso;
        }
    }

    public class CapturaSaldo
    {
        public Saldo saldo { get; set; }

        public CapturaSaldo(ConectaIBC ibc)
        {
            this.saldo = ibc.CapturaSaldoCompleto(ibc);
        }
    }

    public class CapturaExtrato
    {
        public Extrato extrato { get; set; }

        public CapturaExtrato(ConectaIBC ibc)
        {
            this.extrato = ibc.CapturaExtrato5Dias();
        }

        public CapturaExtrato(ConectaIBC ibc, DateTime hdnDataInicio, DateTime hdnDataFinal, string rdoTipoExtrato, int sltOutroMes, int txtDataInicio, int txtDataFinal)
        {
            this.extrato = ibc.getExtratoPorPeriodo(hdnDataInicio, hdnDataFinal, sltOutroMes, txtDataInicio, txtDataFinal);
        }
    }

    public class buscaPontosCAIXA
    {
        public int saldo { get; set; }
        public int expirar { get; set; }

        public buscaPontosCAIXA(int co_cliente)
        {
            this.saldo = new PontosCaixaController().getPontosCAIXA(co_cliente);
            this.expirar = new PontosCaixaController().getPontosCAIXAaExpirar(co_cliente);
        }
    }

    public class loteriaView
    {
        public List<int> lsQtdNrs { get; set; }

        public loteriaView()
        {
            this.lsQtdNrs = new List<int> { 6,7,8,9 };
        }
    }
}