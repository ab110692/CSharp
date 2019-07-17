using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Connection
{
    public abstract class IConfig
    {
        public String DataBaseName { get; set; }

        public String ServerName { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }
    }
}
