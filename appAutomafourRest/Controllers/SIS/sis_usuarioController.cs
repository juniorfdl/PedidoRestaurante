namespace Controllers.Sistema
{
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


        [Route("api/sis_usuario/ConfirmarPedido")]
        [HttpGet]
        public IHttpActionResult ConfirmarPedido([FromUri] Pedido dados)
        {
            foreach (dynamic item in dados.Produtos)
            {
                PRODUTO i = JsonConvert.DeserializeObject<PRODUTO>(item);
            }                        

            retorno = dados;
            return Ok(dados);
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
        public dynamic Produtos { get; set; }
    }
}
