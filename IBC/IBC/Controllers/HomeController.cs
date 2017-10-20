using IBC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IBC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nomeUsuario, string password)
        {
            ConectaIBC ibc = new ConectaIBC(nomeUsuario, password);
            ibc.RecebeSeed();
            ibc.EnviaChave();
            ibc.EnviaUsuario();
            ibc.EnviaSenha();
            ibc.CapturaSaldo();
            ibc.CapturaCliente();
            ibc.CapturaCategoria();
            ibc.CapturaAlertas();
            ibc.CapturaDadosUsuario();

            Conta conta = ibc.contaCliente.contas.FirstOrDefault();

            ibc.co_cliente = new ClienteController().getCliente(nomeUsuario, conta.cpf);
            if (ibc.co_cliente == 0)
            {
                ibc.co_cliente = new ClienteController().addCliente(conta.agencia, conta.conta, ibc.dadosUsuario.nome, conta.cpf, nomeUsuario);
            }

            Session["__IBC__"] = ibc;

            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            ViewBag.saldoConta = ibc.saldo.saldo;

            return View(new paginaInicialView(ibc));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SideBar()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_SideBar", new sideBarView(ibc));
        }

        public ActionResult NavBar()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_Navbar", new navBarView(ibc));
        }

        public ActionResult Saldo()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_SaldoBox", new CapturaSaldo(ibc));
        }

        public ActionResult PontosCAIXA()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_PontosCAIXABox", new buscaPontosCAIXA(ibc.co_cliente));
        }

        public ActionResult Extrato()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_Extrato", new CapturaExtrato(ibc));
        }

        public ActionResult getExtratoPorPeriodo(DateTime hdnDataInicio, DateTime hdnDataFinal, string rdoTipoExtrato, int sltOutroMes, int txtDataInicio, int txtDataFinal)
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_ExtratoTable", new CapturaExtrato(ibc, hdnDataInicio, hdnDataFinal, rdoTipoExtrato, sltOutroMes, txtDataInicio, txtDataFinal));
        }
    }
}