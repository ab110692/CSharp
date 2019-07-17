using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface IPerfilDAO : IDAO<Perfil>
    {
        /// <summary>
        /// Verifica se existe o perfil;
        /// </summary>
        /// <param name="perfil"></param>
        /// <returns></returns>
        Perfil VerificaPerfil(string perfil);
    }
}
