using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.Shelf.Enums
{
    public enum TipoProtocolo
    {
        Atendimento = 0
    }

    public static class MyTipoProtocoloExtesion
    {
        public static int GetTipo(this TipoProtocolo tipo)
        {
            switch (tipo)
            {
                case TipoProtocolo.Atendimento:
                    return 0;
                default:
                    return -1;
            }
        }

        public static TipoProtocolo FromStatus(this TipoProtocolo t, int code)
        {
            foreach (TipoProtocolo aux in System.Enum.GetValues(typeof(TipoProtocolo)))
            {
                if (aux.GetTipo().Equals(code))
                {
                    return aux;
                }
            }
            return t;
        }

        public static string GetName(this TipoProtocolo tipo)
        {
            switch (tipo)
            {
                case TipoProtocolo.Atendimento:
                    return "Atendimento";
                default:
                    return "Desconhecido";
            }
        }
    }
}
