using Br.Com.Posi.Connection;
using Br.Com.Posi.NotaFiscal.Model;
using System.Collections.Generic;

namespace Br.Com.Posi.NotaFiscal.DAO
{
    public interface IClienteDAO : IDAO<Cliente>
    {
        List<Cliente> ListPerState(string UF);

        List<Cliente> ListState();
    }
}
