using Br.Com.Posi.Connection.Enums;
using Br.Com.Posi.Connection.Model;
using System;

namespace Br.Com.Posi.Connection.Util
{
    public static class FactoryConnection
    {
        public static Configuracao BuildShelf(Modo modo)
        {
            switch (modo)
            {
                case Modo.REGEDIT:
                    return MyRegister.Instance(BancoDeDados.Shelf).Readerconfiguraction();
                case Modo.XML:
                    return MyConfiguracaoXML.Instance(BancoDeDados.Shelf).Ler();
                default:
                    throw new NotImplementedException("Modo não implementado");
            }
        }
    }
}
