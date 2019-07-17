using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Connection
{
    public class Note : IConfig
    {
        public Note()
        {
            DataBaseName = "Note";
            ServerName = "192.193.10.254";
            UserName = "sa";
            Password = "Dko5cv4k";
        }
    }
}
