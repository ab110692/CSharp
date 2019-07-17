
using Br.Com.Posi.NoteAnalyzer.Model;
using System.Collections.Generic;

namespace Br.Com.Posi.NoteAnalyzer.DAO
{
    public interface IClienteDAO : IDAO<Cliente>
    {
        List<Cliente> ListPerState(string UF);

        List<Cliente> ListState();
    }
}
