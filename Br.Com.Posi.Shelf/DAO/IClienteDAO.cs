using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface IClienteDAO : IDAO<Cliente>
    {
        List<Cliente> GetListInner();

        List<Cliente> GetClienteByRede(Rede rede);

    }
}
