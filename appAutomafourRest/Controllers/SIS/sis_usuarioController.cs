namespace Controllers.Sistema
{
    using Infra.Base;
    using Infra.Base.Interface.Base;
    using Models.Cadastros;
    using Models.SIS;
    using Newtonsoft.Json;
    using Sistema;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class sis_usuarioController : CrudControllerBase<SIS_USUARIO>
    {
        private dynamic retorno = new ExpandoObject();
        private Context dblocal = new Context();

        protected override IOrderedQueryable<SIS_USUARIO> Ordenar(IQueryable<SIS_USUARIO> query)
        {
            return query.OrderBy(e => e.id);
        }

        protected override void BeforeReturn(SIS_USUARIO item)
        {
            var empresa = db.Set<CAD_EMPRESA>();
            item.Empresas = empresa;
        }

        [Route("api/sis_usuario/localizar")]
        [HttpGet]
        public IHttpActionResult Localizar([FromUri]SIS_USUARIO usuario)
        {
            SIS_USUARIO item = this.TrazerDadosParaEdicao(db.Set<SIS_USUARIO>())
                  .FirstOrDefault(e => e.NOME == usuario.NOME & e.PWD == usuario.PWD);

            if (item == null)
            {
                return NotFound();
            }

            item.Grupos = db.Set<GRUPO>();
            item.CONFIG_RESTAURANTE = db.Set<CONFIG_RESTAURANTE>();

            foreach (GRUPO g in item.Grupos) {                
                g.Produtos = db.Set<PRODUTO>().Where(q => q.GRUPICOD == g.id);
            }
            
            return Ok(item);            
        }


        [Route("api/sis_usuario/PedidoMesa")]
        [HttpGet]
        public IHttpActionResult PedidoMesa([FromUri] Pedido mesa)
        {
            Pedido item = new Pedido();
            item.Mesa = mesa.id;
            item.id = 0;

            FuncoesBanco f = new FuncoesBanco(dblocal);

            List<dynamic> dt = f.CollectionFromSql("select first(1) PRVDICOD, VENDICOD, PRVDN2TOTITENS, CLIENTEOBS from prevenda where mesaicod = "
                  + mesa.id + " order by PRVDICOD desc ",
               new Dictionary<string, object> { }).ToList();

            foreach (dynamic d in dt)
            {
                item.id = Convert.ToInt32(d.PRVDICOD);
                item.CodUsr = Convert.ToInt32(d.VENDICOD);
                item.Total = Convert.ToDouble(d.PRVDN2TOTITENS);
                item.OBS = d.CLIENTEOBS;

                List<dynamic> lista = f.CollectionFromSql(
                    "select a.PRODICOD,a.PVITN3QTD,a.PVITN3VLRUNIT, b.proda60descr, b.grupicod "
                    + " from prevendaitem a inner join produto b on b.prodicod = a.prodicod "
                    +" where a.PRVDICOD = " + item.id,
                new Dictionary<string, object> { }).ToList();
                
                item.Produtos = lista;
            }
            
            if (item.id == 0)
            {
                item = null;
            }

            return Ok(item);
        }
                
        [Route("api/Pedido")]
        [HttpPost]
        public IHttpActionResult ConfirmarPedido([FromUri] Pedido dados)
        {            
            FuncoesBanco f = new FuncoesBanco(dblocal);
            var id = f.ExecSql("select id from SP_GRAVAR_PEDIDO_WEB("+dados.id+", "+dados.CodUsr
                + ", " + dados.Mesa + ", '" + dados.Total.ToString().Replace(",", ".") + "')");

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
                foreach (dynamic item in dados.Produtos)
                {
                    ii++;
                    PRODUTO i = JsonConvert.DeserializeObject<PRODUTO>(item);
                    f.ExecSql("select id from SP_GRAVAR_PEDIDO_ITEM_WEB(" + dados.id + ", " + dados.CodUsr
                    + ", " + dados.Mesa + ", '" + i.PRODN3VLRVENDA.ToString().Replace(",", ".")
                    + "', " + i.QTD.ToString()
                    + "," + ii
                    + ", " + i.id
                    + ")");
                }
            }

            retorno.id = dados.id;
            return CreatedAtRoute("DefaultApi", new { id = retorno.id }, retorno);
           // return Ok(retorno);
        }
               
        [Route("api/sis_usuario/Empresa")]
        [HttpGet]
        public dynamic Empresa([FromUri]SIS_USUARIO usuario)
        {                                    
            var emp = db.Set<CAD_EMPRESA>();            
            return emp;
        }
        
    }
    
    public class Pedido
    {
        public int id { get; set; }
        public int CodUsr { get; set; }
        public int Mesa { get; set; }
        public double Total { get; set; }
        public string OBS { get; set; }
        public dynamic Produtos { get; set; }
    }
}
