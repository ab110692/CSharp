using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface IComputadorDAO : IDAO<Computador>
    {
        List<Computador> List(Cliente cliente);
    }
}
