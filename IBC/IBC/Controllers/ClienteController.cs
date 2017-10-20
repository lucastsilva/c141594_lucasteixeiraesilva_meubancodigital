using IBC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBC.Controllers
{
    public class ClienteController : BaseController
    {
        private IBCModel db = new IBCModel();

        public int getCliente(string tx_login, string co_cpf)
        {
            co_cpf = LimparCPF(co_cpf);

            return db.ibctb001_cliente
                        .Where(c => c.tx_login == tx_login.Trim())
                        .Where(c => c.co_cpf == co_cpf)
                        .Select(c => c.co_cliente)
                        .FirstOrDefault();
        }

        public int addCliente(string co_agencia, string co_conta, string no_cliente, string co_cpf, string tx_login)
        {
            ibctb001_cliente cliente = new ibctb001_cliente
            {
                co_agencia = co_agencia,
                co_conta = co_conta,
                no_cliente = no_cliente,
                co_cpf = LimparCPF(co_cpf),
                tx_login = tx_login
            };


            try
            {
                db.ibctb001_cliente.Add(cliente);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

           

            return cliente.co_cliente;
        }
    }
}