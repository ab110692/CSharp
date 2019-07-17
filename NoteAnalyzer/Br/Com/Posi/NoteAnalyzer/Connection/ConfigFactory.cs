using Br.Com.Posi.NoteAnalyzer.Connection;
using NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Connection
{
    public static class ConfigFactory
    {
        public static IConfig GetConfig(DataBase dataBase)
        {
            switch (dataBase)
            {
                case DataBase.Shelf:
                    return new Shelf();
                case DataBase.Note:
                    return new Note();
                default:
                    throw new Exception("Database não informado");
            }
        }
    }
}
