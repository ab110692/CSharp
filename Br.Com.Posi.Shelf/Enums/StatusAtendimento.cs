using System;

namespace Br.Com.Posi.Shelf.Enums
{
    public enum StatusAtendimento
    {
        Em_Andamento = 0,
        Atribuido = 1,
        Pendente = 2,
        Aguardando_Feedback = 3,
        Reabertura_reincidencia = 4,
        Solucionado = 5,
        Nao_solucionado = 6,
        Especialistas = 7,
        Campo = 8,
        Desconhecido = 99
    }

    public static class MyStatusAtendimentoExtension
    {
        public static int GetStatus(this StatusAtendimento status)
        {
            switch (status)
            {
                case StatusAtendimento.Em_Andamento:
                    return 0;
                case StatusAtendimento.Atribuido:
                    return 1;
                case StatusAtendimento.Pendente:
                    return 2;
                case StatusAtendimento.Aguardando_Feedback:
                    return 3;
                case StatusAtendimento.Reabertura_reincidencia:
                    return 4;
                case StatusAtendimento.Solucionado:
                    return 5;
                case StatusAtendimento.Nao_solucionado:
                    return 6;
                case StatusAtendimento.Especialistas:
                    return 7;
                case StatusAtendimento.Campo:
                    return 8;
                default:
                    return 99;
            }
        }

        public static StatusAtendimento FromStatus(this StatusAtendimento s, int status)
        {
            foreach (StatusAtendimento aux in Enum.GetValues(typeof(StatusAtendimento)))
            {
                if (aux.GetStatus().Equals(status))
                {
                    return aux;
                }
            }
            return s;
        }

        public static string GetName(this StatusAtendimento statusAtendimento)
        {
            switch (statusAtendimento)
            {
                case StatusAtendimento.Em_Andamento:
                    return "Em Andamento";
                case StatusAtendimento.Atribuido:
                    return "Atribuido";
                case StatusAtendimento.Pendente:
                    return "Pendente";
                case StatusAtendimento.Aguardando_Feedback:
                    return "Aguardando Feedback";
                case StatusAtendimento.Reabertura_reincidencia:
                    return "Reabertura ou Reincidencia";
                case StatusAtendimento.Solucionado:
                    return "Solucionado";
                case StatusAtendimento.Nao_solucionado:
                    return "Não solucionado";
                case StatusAtendimento.Especialistas:
                    return "Especialistas";
                case StatusAtendimento.Campo:
                    return "Campo";
                default:
                    return "Desconhecido";
            }
        }
    }
}
