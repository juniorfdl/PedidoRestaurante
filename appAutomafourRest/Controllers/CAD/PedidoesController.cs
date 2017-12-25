using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Controllers.Sistema;
using Infra.Base;
using System.Dynamic;
using Models.Cadastros;
using Newtonsoft.Json;

namespace appAutomafourRest.Controllers.CAD
{
    public class PedidoesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Pedidoes
        public IQueryable<Pedido> GetPedidoes()
        {
            return db.Pedidoes;
        }

        private dynamic retorno = new ExpandoObject();
        private Context dblocal = new Context();

        public dynamic ConfirmarPedido(Pedido dados)
        {
            FuncoesBanco f = new FuncoesBanco(dblocal);

            if (dados.id < 0)
            {
                dados.id = 0;
            }
            
            var id = f.ExecSql("select id from SP_GRAVAR_PEDIDO_WEB(" + dados.id + ", " + dados.CodUsr
                + ", " + dados.Mesa + ", '" + dados.Total.ToString().Replace(",", ".")                
                + "' , '" + dados.OBS.Trim()    
                + "')");

            if (id != null && id.Count > 0)
            {
                dados.id = Convert.ToInt32(id[0]);
            }

            int ii = 0;
            if (dados.Produtos is string)
            {
                PRODUTO i = JsonConvert.DeserializeObject<PRODUTO>(dados.Produtos);
                f.ExecSql("select id from SP_GRAVAR_PEDIDO_ITEM_WEB(" + dados.id + ", " + dados.CodUsr
                + ", " + dados.Mesa + ", '" + i.PRODN3VLRVENDA.ToString().Replace(",", ".")
                + "', " + i.QTD.ToString()
                + "," + ii
                + ", " + i.id
                + ")");
            }
            else
            {
                PRODUTO i = new PRODUTO();
                foreach (dynamic item in dados.Produtos)
                {
                    
                    foreach (dynamic filho in item)
                    {
                        foreach (dynamic campo in filho)
                        {
                            if (filho.Name == "id")
                            {
                                i.id = (int)campo.Value;
                            }
                            else
                            if (filho.Name == "PRODN3VLRVENDA")
                            {
                                i.PRODN3VLRVENDA = campo.Value;
                            }else
                            if (filho.Name == "QTD")
                            {
                                i.QTD = (int)campo.Value;
                            }
                        }                            
                    }

                    ii++;
                    //item;//JsonConvert.DeserializeObject<PRODUTO>(item);
                    f.ExecSql("select id from SP_GRAVAR_PEDIDO_ITEM_WEB(" + dados.id + ", " + dados.CodUsr
                    + ", " + dados.Mesa + ", '" + i.PRODN3VLRVENDA.ToString().Replace(",", ".")
                    + "', " + i.QTD.ToString()
                    + "," + ii
                    + ", " + i.id
                    + ")");
                }
            }

            //retorno.id = dados.id;
            return dados;
        }

        //GET: api/Pedidoes/5

        [ResponseType(typeof(Pedido))]
        public IHttpActionResult GetPedido(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // PUT: api/Pedidoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedido(int id, Pedido pedido)
        {
            var retorno = ConfirmarPedido(pedido);
            return CreatedAtRoute("DefaultApi", new { id = retorno.id }, retorno);
        }

        // POST: api/Pedidoes
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult PostPedido(Pedido pedido)
        {
            var retorno = ConfirmarPedido(pedido);
            return CreatedAtRoute("DefaultApi", new { id = retorno.id }, retorno);
        }

        // DELETE: api/Pedidoes/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult DeletePedido(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            db.Pedidoes.Remove(pedido);
            db.SaveChanges();

            return Ok(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(int id)
        {
            return db.Pedidoes.Count(e => e.id == id) > 0;
        }
    }
}