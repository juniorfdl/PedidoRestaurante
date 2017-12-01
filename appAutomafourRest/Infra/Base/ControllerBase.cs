namespace Infra.Base.Interface.Base
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    public abstract class ControllerBase : ApiController
    {
        public Context db = new Context();

        protected void AtualizarRelacionamento<TRaiz, TEntidade>(TRaiz item, ICollection<TEntidade> lista, Func<TEntidade, int> fkListaParaItem)
            where TRaiz : IEntidadeBase
            where TEntidade : class, IEntidadeBase
        {
            List<TEntidade> paraRemocao = null;            

            if (lista != null)
            {
                foreach (var other in lista)//.Where(e => e.id == 0 || ((IEntidadeDetalhe)e).FlagExcOrAlter != null))
                {
                    if (other.id == 0)
                    {
                        other.id = item.id;
                        db.Entry(other).State = EntityState.Added;
                    }
                    else
                    if (other is IEntidadeDetalhe && ((IEntidadeDetalhe)other).FlagExcOrAlter == "E")
                    {
                        if (paraRemocao == null)
                            paraRemocao = new List<TEntidade>();
                        paraRemocao.Add(other);
                    }
                    else //if (other is IEntidadeDetalhe && ((IEntidadeDetalhe)other).FlagExcOrAlter == "A")
                    {
                        db.Entry(other).State = EntityState.Modified;
                    }
                }
                if (paraRemocao != null)
                {
                    foreach (var it in paraRemocao)
                    {
                        db.Entry(it).State = it.id == 0 ? EntityState.Detached : EntityState.Deleted;
                        lista.Remove(it);
                    }
                }
            }
        }

        protected T MarcarGrafoComoAdded<T>(T raiz) where T : class
        {
            db.Entry(raiz).State = EntityState.Added;
            return raiz;
        }

        protected T MarcarEntidadeComoModified<T>(T entidade) where T : class
        {
            db.Entry(entidade).State = EntityState.Modified;
            return entidade;
        }
    }
}
