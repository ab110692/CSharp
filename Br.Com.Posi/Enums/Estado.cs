using System;
using System.Collections.Generic;


namespace Br.Com.Posi.Enums
{
    public enum Estado
    {
        ACRE,
        ALAGOAS,
        AMAPA,
        AMAZONAS,
        BAHIA,
        CEARA,
        DISTRITO_FEDERAL,
        ESPIRITO_SANTO,
        GOIAS,
        MARANHAO,
        MATO_GROSSO,
        MATO_GROSSO_DO_SUL,
        MINAS_GERAIS,
        PARA,
        PARAIBA,
        PARANA,
        PERNAMBUCO,
        PIAUI,
        RIO_DE_JANEIRO,
        RIO_GRANDE_DO_NORTE,
        RIO_GRANDE_DO_SUL,
        RONDONIA,
        RORAIMA,
        SANTA_CATARINA,
        SAO_PAULO,
        SERGIPE,
        TOCANTINS
    }

    public static class EstadoExtension
    {
        public static string[] GetAllInitials(this Estado estado)
        {
            List<string> list = new List<string>();
            foreach (Estado e in Enum.GetValues(typeof(Estado)))
            {
                list.Add(e.GetInitials());
            }
            return list.ToArray();
        }

        public static string GetInitials(this Estado estado)
        {
            switch (estado)
            {
                case Estado.ACRE:
                    return "AC";
                case Estado.ALAGOAS:
                    return "AL";
                case Estado.AMAPA:
                    return "AP";
                case Estado.AMAZONAS:
                    return "AM";
                case Estado.BAHIA:
                    return "BA";
                case Estado.CEARA:
                    return "CE";
                case Estado.DISTRITO_FEDERAL:
                    return "DF";
                case Estado.ESPIRITO_SANTO:
                    return "ES";
                case Estado.GOIAS:
                    return "GO";
                case Estado.MARANHAO:
                    return "MA";
                case Estado.MATO_GROSSO:
                    return "MT";
                case Estado.MATO_GROSSO_DO_SUL:
                    return "MS";
                case Estado.MINAS_GERAIS:
                    return "MG";
                case Estado.PARA:
                    return "PA";
                case Estado.PARAIBA:
                    return "PB";
                case Estado.PARANA:
                    return "PR";
                case Estado.PERNAMBUCO:
                    return "PE";
                case Estado.PIAUI:
                    return "PI";
                case Estado.RIO_DE_JANEIRO:
                    return "RJ";
                case Estado.RIO_GRANDE_DO_NORTE:
                    return "RN";
                case Estado.RIO_GRANDE_DO_SUL:
                    return "RS";
                case Estado.RONDONIA:
                    return "RO";
                case Estado.RORAIMA:
                    return "RR";
                case Estado.SANTA_CATARINA:
                    return "SC";
                case Estado.SAO_PAULO:
                    return "SP";
                case Estado.SERGIPE:
                    return "SE";
                case Estado.TOCANTINS:
                    return "TO";
                default:
                    return "SP";
            }
        }

        public static Estado FromInitials(this Estado estado, string initials)
        {
            foreach (Estado e in Enum.GetValues(typeof(Estado)))
            {
                if (e.GetInitials().Equals(initials))
                {
                    return e;
                }
            }
            return Estado.SAO_PAULO;
        }

        public static string GetNomeEstado(this Estado estado)
        {
            switch (estado)
            {
                case Estado.ACRE:
                    return "Acre";
                case Estado.ALAGOAS:
                    return "Alagoas";
                case Estado.AMAPA:
                    return "Amapa";
                case Estado.AMAZONAS:
                    return "Amazonas";
                case Estado.BAHIA:
                    return "Bahia";
                case Estado.CEARA:
                    return "Ceara";
                case Estado.DISTRITO_FEDERAL:
                    return "Distrito Federal";
                case Estado.ESPIRITO_SANTO:
                    return "Espirito Santo";
                case Estado.GOIAS:
                    return "Goias";
                case Estado.MARANHAO:
                    return "Maranha";
                case Estado.MATO_GROSSO:
                    return "Mato Grosso";
                case Estado.MATO_GROSSO_DO_SUL:
                    return "Mato Grosso do Sul";
                case Estado.MINAS_GERAIS:
                    return "Minas Gerais";
                case Estado.PARA:
                    return "Para";
                case Estado.PARAIBA:
                    return "Paraiba";
                case Estado.PARANA:
                    return "Parana";
                case Estado.PERNAMBUCO:
                    return "Pernambuco";
                case Estado.PIAUI:
                    return "Piaui";
                case Estado.RIO_DE_JANEIRO:
                    return "Rio de Janeiro";
                case Estado.RIO_GRANDE_DO_NORTE:
                    return "Rio Grande do Norte";
                case Estado.RIO_GRANDE_DO_SUL:
                    return "Rio Grande do Sul";
                case Estado.RONDONIA:
                    return "Rondonia";
                case Estado.RORAIMA:
                    return "Roraima";
                case Estado.SANTA_CATARINA:
                    return "Santa Catarina";
                case Estado.SAO_PAULO:
                    return "Sao Paulo";
                case Estado.SERGIPE:
                    return "Sergipe";
                case Estado.TOCANTINS:
                    return "Tocantins";
                default:
                    return "Inválido";
            }
        }

        public static Estado FromNomeEstado(this Estado es, string estado)
        {
            foreach (Estado e in Enum.GetValues(typeof(Estado)))
            {
                if (e.GetNomeEstado().Equals(estado))
                {
                    return e;
                }
            }
            return Estado.SAO_PAULO;
        }
    }
}
