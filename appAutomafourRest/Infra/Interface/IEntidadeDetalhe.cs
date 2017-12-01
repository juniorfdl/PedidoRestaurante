using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Base.Interface
{
    public interface IEntidadeDetalhe
    {
        string FlagExcOrAlter { get; set; }
        // E = Excluir
        // A = Alterar
    }
}
