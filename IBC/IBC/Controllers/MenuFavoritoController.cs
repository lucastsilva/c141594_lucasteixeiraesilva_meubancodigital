using IBC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;

namespace IBC.Controllers
{
    public class MenuFavoritoController : BaseController
    {
        private IBCModel db = new IBCModel();

        public List<int> getLsCoMenuFavoritos(int co_cliente)
        {
            return db.ibctb001_cliente
                .Include(c => c.ibctb003_menu_favorito)
                .Where(c => c.co_cliente == co_cliente)
                .SelectMany(m => m.ibctb003_menu_favorito)
                .Select(m => m.co_menu_favorito)
                .ToList();
        }

        public void salvarMenuFavorito(int co_cliente, List<int> lsCoMenuFavorito)
        {
            List<ibctb003_menu_favorito> lsMenuFavorito = new List<ibctb003_menu_favorito>();
            ibctb001_cliente cliente = db.ibctb001_cliente
                    .Include(c => c.ibctb003_menu_favorito)
                    .Where(c => c.co_cliente == co_cliente)
                    .FirstOrDefault();

            if (lsCoMenuFavorito != null)
            {
                foreach (int co_menu_favorito in lsCoMenuFavorito)
                {
                    ibctb003_menu_favorito menu_favorito = db.ibctb003_menu_favorito.Find(co_menu_favorito);
                    lsMenuFavorito.Add(menu_favorito);
                }
                
                cliente.ibctb003_menu_favorito = lsMenuFavorito;
            }
            else
            {
                cliente.ibctb003_menu_favorito = lsMenuFavorito;
            }

            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}