using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Shelf.Enums;
using System;

namespace Br.Com.Posi.Shelf.Model
{
    public class AtendimentoDetalhado : IModelo
    {

        public long IDAtendimentoDetalhado { get; set; }
        
        public Atendimento Atendimento { get; set; }

        public StatusAtendimento StatusAtendimento { get; set; }

        public Funcionario Funcionario { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }

        public string Solucao { get; set; }

        public string Contato { get; set; }

        public AtendimentoDetalhado()
        {
            
        }

        ~AtendimentoDetalhado()
        {
            
        }

        public long ID
        {
            get
            {
                return IDAtendimentoDetalhado;
            }
        }
    }
}