using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class GASRESULT
    {
        public string seed { get; set; }
        public string password { get; set; }
    }

    public class RootObject
    {
        public GASRESULT GASRESULT { get; set; }
    }
}