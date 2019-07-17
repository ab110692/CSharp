using NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Connection;
using System;

namespace Br.Com.Posi.NoteAnalyzer.Connection
{
    [Serializable]
    public class Shelf : IConfig
    {
        public Shelf()
        {
            DataBaseName = "iNET";
            ServerName = "192.193.10.254";
            UserName = "sa";
            Password = "Dko5cv4k";
        }
    }
}
