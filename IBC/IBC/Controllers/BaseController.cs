using IBC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBC.Controllers
{
    public class BaseController : Controller
    {
        public string LimparCPF(string value)
        {
            return value.Replace(".", "").Replace("-", "").Substring(value.Length - 11); ;
        }
    }
}