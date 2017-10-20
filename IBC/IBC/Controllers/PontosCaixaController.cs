using IBC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBC.Controllers
{
    public class PontosCaixaController : BaseController
    {
        private IBCModel db = new IBCModel();

        public int getPontosCAIXA(int co_cliente)
        {
            IQueryable<ibctb002_pontos_caixa> iqPontosCaixa = db.ibctb002_pontos_caixa.Where(p => p.co_cliente == co_cliente);
            int qtdPontosEntrada = iqPontosCaixa
                 .Where(p => p.ic_movimentacao)
                 .Select(p => p.nu_pontos_caixa)
                 .DefaultIfEmpty(0)
                 .Sum();
            return qtdPontosEntrada - getPontosResgatados(co_cliente);
        }

        public int getPontosCAIXAaExpirar(int co_cliente)
        {
            IQueryable<ibctb002_pontos_caixa> iqPontosCaixa = db.ibctb002_pontos_caixa.Where(p => p.co_cliente == co_cliente);
            int qtdPontosAExpirar = iqPontosCaixa
                 .Where(p => p.dt_expiracao.Value.Month <= DateTime.Now.Month && p.dt_expiracao.Value.Year <= DateTime.Now.Year)
                 .Select(p => p.nu_pontos_caixa)
                 .DefaultIfEmpty(0)
                 .Sum();

            int pontosTotais = getPontosCAIXA(co_cliente);

            if (qtdPontosAExpirar > pontosTotais)
            {
                qtdPontosAExpirar = pontosTotais;
            }

            return qtdPontosAExpirar;
        }

        public int getPontosResgatados(int co_cliente)
        {
            IQueryable<ibctb002_pontos_caixa> iqPontosCaixa = db.ibctb002_pontos_caixa.Where(p => p.co_cliente == co_cliente);
            return iqPontosCaixa
                 .Where(p => !p.ic_movimentacao)
                 .Select(p => p.nu_pontos_caixa)
                 .DefaultIfEmpty(0)
                 .Sum();
        }
    }
}