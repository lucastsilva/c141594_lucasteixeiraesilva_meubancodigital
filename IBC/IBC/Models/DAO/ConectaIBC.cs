using HtmlAgilityPack;
using IBC.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace IBC.Models
{
    public class ConectaIBC
    {
        public HttpClient clienteWeb { get; set; }
        public GASRESULT GASRESULT { get; set; }
        public Saldo saldo { get; set; }
        public ContaCliente contaCliente { get; set; }
        public Categoria categoria { get; set; }
        public DadosUsuario dadosUsuario { get; set; }
        public Extrato extrato { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public int co_cliente { get; set; }

        public ConectaIBC(string usuario, string senha)
        {
            clienteWeb = new HttpClient();
            this.usuario = usuario;
            this.senha = senha;
        }

        public void RecebeSeed()
        {
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", "")
            });

            Task<HttpResponseMessage> post = clienteWeb.PostAsync(Constantes.edAPI + "tpSeed?deviceKey=" + Constantes.deviceKey, formContent);
            post.Wait();
            Task<string> contents = post.Result.Content.ReadAsStringAsync();
            contents.Wait();
            RootObject result = new JavaScriptSerializer().Deserialize<RootObject>(contents.Result);
            GASRESULT = result.GASRESULT;
        }

        public void EnviaChave()
        {
            Task<HttpResponseMessage> get = clienteWeb.GetAsync(Constantes.edAPI + "loginTp?nocache=" + Constantes.noCache + "&context=" + Constantes.context);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();
        }

        public void EnviaUsuario()
        {
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nomeUsuario", usuario),
                new KeyValuePair<string, string>("segmento", "1"),
                new KeyValuePair<string, string>("userAgent", "Mozilla/5.0 (iPad; CPU OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1")

            });
            Task<HttpResponseMessage> post = clienteWeb.PostAsync(Constantes.edAPI + "login/authUserNameTp?nocache=" + Constantes.noCacheUser, formContent);
            post.Wait();
            Task<string> contents = post.Result.Content.ReadAsStringAsync();
            contents.Wait();

        }

        public void EnviaSenha()
        {
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("password", senha),
                new KeyValuePair<string, string>("segmento", "1"),
                new KeyValuePair<string, string>("userAgent", "Mozilla/5.0 (iPad; CPU OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1")

            });
            Task<HttpResponseMessage> post = clienteWeb.PostAsync(Constantes.edAPI + "login/authPasswordTp?nocache=" + Constantes.noCachePass, formContent);
            post.Wait();
            Task<string> contents = post.Result.Content.ReadAsStringAsync();
            contents.Wait();

        }

        public void CapturaSaldo()
        {
            Task<HttpResponseMessage> get = clienteWeb.GetAsync(Constantes.edAPI + "componentSaldo/atualizaSaldo?context=" + Constantes.context);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();
            saldo = new JavaScriptSerializer().Deserialize<Saldo>(contents.Result);
        }

        public void CapturaCliente()
        {
            Task<HttpResponseMessage> get = clienteWeb.GetAsync(Constantes.edAPI + "contas/listarTodasXs?nocache=" + Constantes.noCacheContas);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();
            contaCliente = new JavaScriptSerializer().Deserialize<ContaCliente>(contents.Result);
        }

        public void CapturaCategoria()
        {
            Task<HttpResponseMessage> get = clienteWeb.GetAsync(Constantes.edAPI + "carrossel/carregar?_=" + Constantes.noCacheCarrossel);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();
            categoria = new JavaScriptSerializer().Deserialize<Categoria>(contents.Result);


        }

        public void CapturaAlertas()
        {
            Task<HttpResponseMessage> get = clienteWeb.GetAsync(Constantes.edAPI + "ferramentas/avisos/buscarAvisos?_=" + Constantes.noCacheAlertas);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();
        }

        public Saldo CapturaSaldoCompleto(ConectaIBC ibc)
        {
            Saldo saldo = ibc.saldo;

            string tableClass = "produto";

            Task<HttpResponseMessage> get = clienteWeb.GetAsync("https://tgy64w74i567hklqjb-internetbanking.caixa.gov.br/SIIBC/extrato.processa?ajax=ajax&_=1507985758900");
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();

            HtmlDocument tableDoc = getTabela(contents, tableClass);

            foreach (HtmlNode node in tableDoc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode row in node.SelectNodes("tr"))
                {
                    string campo = row.ChildNodes[0].InnerText;
                    if (campo.Trim() != "")
                    {
                        string valor = row.ChildNodes[1].InnerText;

                        if (campo == "Saldo total")
                        {
                            saldo.saldoTotal = valor;
                            continue;
                        }
                        if (campo == "Saldo disponivel c/limite")
                        {
                            saldo.saldoDisponivelComLimite = valor;
                            continue;
                        }
                        if (campo == "Saldo bloqueado")
                        {
                            saldo.saldoBloqueado = valor;
                            continue;
                        }
                        if (campo == "Limite do Cheque Especial")
                        {
                            saldo.limite = valor;
                            continue;
                        }
                        if (campo == "Saldo")
                        {
                            saldo.saldo = valor;
                            continue;
                        }
                    }
                }
            }

            return saldo;
        }

        private HtmlDocument getTabela(Task<string> contents, string tableClass)
        {
            // From String
            var doc = new HtmlDocument();
            doc.LoadHtml(contents.Result.ToString());

            //Remover os comentários
            doc.DocumentNode.Descendants()
             .Where(n => n.NodeType == HtmlAgilityPack.HtmlNodeType.Comment)
             .ToList()
             .ForEach(n => n.Remove());

            var table = doc.DocumentNode.SelectSingleNode("//table[@class='" + tableClass + "']");

            foreach (HtmlNode node in table.SelectNodes("*"))
            {
                if (node.Name == "thead")
                {
                    node.Remove();
                }
                else
                {
                    RemoveTags(doc, node.Name);
                }
            }

            string tableWanted = "<table>" + table.InnerHtml.TrimStart().TrimEnd() + "</table>";

            var tableDoc = new HtmlDocument();
            tableDoc.LoadHtml(tableWanted);

            return tableDoc;
        }

        public static void RemoveTags(HtmlDocument html, string tagName)
        {
            var tags = html.DocumentNode.SelectNodes("//" + tagName);
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    if (!tag.HasChildNodes)
                    {
                        tag.ParentNode.RemoveChild(tag);
                        continue;
                    }

                    for (var i = tag.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        var child = tag.ChildNodes[i];
                        tag.ParentNode.InsertAfter(child, tag);
                    }
                    tag.ParentNode.RemoveChild(tag);
                }
            }
        }

        public void CapturaDadosUsuario()
        {
            Task<HttpResponseMessage> get = clienteWeb.GetAsync(Constantes.edAPI + "clientInfo/buscarDadosUsuario?" + Constantes.noCacheDadosUsuario);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();
            dadosUsuario = new JavaScriptSerializer().Deserialize<DadosUsuario>(contents.Result);
        }

        public Extrato CapturaExtrato5Dias()
        {
            string url = Constantes.edSIIBC + "extrato.processa" + Constantes.ajax + Constantes.noCacheExtrato;
            string tableClass = "movimentacao";

            Task<HttpResponseMessage> get = clienteWeb.GetAsync(url);
            get.Wait();
            Task<string> contents = get.Result.Content.ReadAsStringAsync();
            contents.Wait();

            HtmlDocument tableDoc = getTabela(contents, tableClass);

            Extrato extrato = arrangeTableExtrato(tableDoc);

            return extrato;
        }

        private Extrato arrangeTableExtrato(HtmlDocument tableDoc)
        {
            List<Movimentacao> lsMovimentacao = new List<Movimentacao>();
            Extrato extrato = new Extrato();



            HtmlDocument table = tableDoc;
            //Remover a primeira linha
            table.DocumentNode.SelectSingleNode("//tr").Remove();

            foreach (HtmlNode node in table.DocumentNode.SelectNodes("//table"))
            {
                for (int i = 0; i < node.SelectNodes("tr").Count(); i = i + 2)
                {
                    Movimentacao movimentacao = new Movimentacao();
                    movimentacao.dt_movimentacao = node.SelectNodes("tr")[i].SelectNodes("td")[0].InnerText;
                    movimentacao.nr_doc = node.SelectNodes("tr")[i].SelectNodes("td")[1].InnerText;
                    movimentacao.historico = node.SelectNodes("tr")[i].SelectNodes("td")[2].InnerText;
                    movimentacao.valor = node.SelectNodes("tr")[i].SelectNodes("td")[3].InnerText;
                    movimentacao.saldo = node.SelectNodes("tr")[i + 1].SelectNodes("td")[1].InnerText;
                    lsMovimentacao.Add(movimentacao);
                }
            }

            extrato.lsMovimentacao = lsMovimentacao;
            return extrato;
        }

        public Extrato getExtratoPorPeriodo(DateTime hdnDataInicio, DateTime hdnDataFinal, int sltOutroMes, int txtDataInicio, int txtDataFinal)
        {
            string config = "{'request':{},'session':{},'todos':{}}";
            string token = "token";
            string extratoEmArquivo = "false";
            string hdnFormatoArquivo = "";
            string rdoTipoExtrato = "O";
            string hdnMoneyExtrato = "2";
            string rdoFormatoExtrato = "";
            string siperResourceCorrente = "044600001000000021266";

            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("config", config),
                new KeyValuePair<string, string>("token", token),
                new KeyValuePair<string, string>("extratoEmArquivo", extratoEmArquivo),
                new KeyValuePair<string, string>("hdnFormatoArquivo", hdnFormatoArquivo),
                new KeyValuePair<string, string>("hdnDataInicio", hdnDataInicio.ToShortDateString()),
                new KeyValuePair<string, string>("hdnDataFinal", hdnDataFinal.ToShortDateString()),
                new KeyValuePair<string, string>("hdnMoneyExtrato", hdnMoneyExtrato),
                new KeyValuePair<string, string>("rdoTipoExtrato", rdoTipoExtrato),
                new KeyValuePair<string, string>("sltOutroMes", sltOutroMes.ToString()),
                new KeyValuePair<string, string>("txtDataInicio", txtDataInicio.ToString()),
                new KeyValuePair<string, string>("txtDataFinal", txtDataFinal.ToString()),
                new KeyValuePair<string, string>("rdoFormatoExtrato", rdoFormatoExtrato),
                new KeyValuePair<string, string>("siperResourceCorrente", siperResourceCorrente)
            });

            Task<HttpResponseMessage> post = clienteWeb.PostAsync(Constantes.edSIIBC + "data_extrato.processa" + Constantes.ajaxFalse + Constantes.noCacheDataExtrato, formContent);
            post.Wait();
            Task<string> contents = post.Result.Content.ReadAsStringAsync();
            contents.Wait();

            string tableClass = "movimentacao";

            HtmlDocument tableDoc = getTabela(contents, tableClass);

            Extrato extrato = arrangeTableExtrato(tableDoc);

            return extrato;
        }
    }
}