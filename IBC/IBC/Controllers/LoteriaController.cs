using IBC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBC.Controllers
{
    public class LoteriaController : BaseController
    {
        public ActionResult getLoteria()
        {
            ConectaIBC ibc = Session["__IBC__"] as ConectaIBC;

            return PartialView("_Aposta", new loteriaView());
        }
    }
}