using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface IContratoDAO: IDAO<Contrato>
    {
        List<Contrato> List(Cliente cliente);
    }
}
