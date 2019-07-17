using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface IRedeDAO : IDAO<Rede>
    {
        Rede GetRede(Cliente cliente);
    }
}
