namespace Controllers.Sistema
{
    using Infra.Base.Interface.Base;
    using Models.Cadastros;
    using Models.SIS;
    using Sistema;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class sis_menuController : CrudControllerBase<SIS_MENU>
    {
        protected override IOrderedQueryable<SIS_MENU> Ordenar(IQueryable<SIS_MENU> query)
        {
            return query.OrderBy(e => e.id);
        }        

    }
}
