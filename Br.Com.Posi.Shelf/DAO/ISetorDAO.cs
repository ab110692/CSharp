using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;

namespace Br.Com.Posi.Shelf.DAO
{
    public interface ISetorDAO : IDAO<Setor>
    {
        /// <summary>
        /// Verifica se existe o setor.
        /// </summary>
        /// <param name="setor"></param>
        /// <returns></returns>
        Setor VerificaSetor(string setor);
    }
}
