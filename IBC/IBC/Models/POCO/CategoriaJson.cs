using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class Item
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public int idServico { get; set; }
        public int posicao { get; set; }
        public string textoToolTipString { get; set; }
        public string listaDescritiva { get; set; }
        public string listaDescritivaTxt { get; set; }
        public string siperCode { get; set; }
        public string urlAplicacaoString { get; set; }
        public bool urlExterna { get; set; }
        public bool habilitado { get; set; }
        public bool linkAtivo { get; set; }
        public string tiposContas { get; set; }
        public string tiposAssinaturas { get; set; }
    }

    public class Submenu
    {
        public int id { get; set; }
        public int idCategoria { get; set; }
        public string titulo { get; set; }
        public IList<Item> itens { get; set; }
    }

    public class CategoriasCarrossel
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public bool personalizavel { get; set; }
        public string imagemDesktop { get; set; }
        public string imagemSmartphone { get; set; }
        public int posicao { get; set; }
        public IList<Submenu> submenus { get; set; }
    }

    public class Categoria
    {
        public IList<CategoriasCarrossel> categoriasCarrossel { get; set; }
        public string dispositivo { get; set; }
        public string segmento { get; set; }
    }
}