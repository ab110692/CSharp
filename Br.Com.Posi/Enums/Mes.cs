using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.Enums
{
    public enum Mes
    {
        Janeiro = 0,
        Fevereiro = 1,
        Marco = 2,
        Abril = 3,
        Maio = 4,
        Junho = 5,
        Julho = 6,
        Agosto = 7,
        Setembro = 8,
        Outubro = 9,
        Novembro = 10,
        Dezembro = 11
    }

    public static class MesExtension
    {
        public static int GetCode(this Mes mes)
        {
            switch (mes)
            {
                default:
                case Mes.Janeiro:
                    return 0;
                case Mes.Fevereiro:
                    return 1;
                case Mes.Marco:
                    return 2;
                case Mes.Abril:
                    return 3;
                case Mes.Maio:
                    return 4;
                case Mes.Junho:
                    return 5;
                case Mes.Julho:
                    return 6;
                case Mes.Agosto:
                    return 7;
                case Mes.Setembro:
                    return 8;
                case Mes.Outubro:
                    return 9;
                case Mes.Novembro:
                    return 10;
                case Mes.Dezembro:
                    return 11;
            }
        }

        public static int GetCodeForMonth(this Mes mes)
        {
            return GetCode(mes) + 1;
        }

        public static Mes FromCode(this Mes mes, int code)
        {
            foreach (Mes aux in Enum.GetValues(typeof(Mes)))
            {
                if (aux.GetCode() == code)
                {
                    return aux;
                }
            }
            return Mes.Janeiro;
        }

        public static Mes FromCodeForMonth(this Mes mes, int code)
        {
            return mes.FromCode(code - 1);
        }
    }
}
