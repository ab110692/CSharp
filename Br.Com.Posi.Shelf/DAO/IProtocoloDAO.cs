using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Enums;
using Br.Com.Posi.Shelf.Model;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface IProtocoloDAO : IDAO<Protocolo>
    {
        Protocolo GerarProtocolo(TipoProtocolo tipo);
    }
}
