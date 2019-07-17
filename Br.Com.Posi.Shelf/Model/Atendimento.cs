using Br.Com.Posi.Connection.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class Atendimento : IModelo
    {

        public long IDAtendimento { get; set; }

        public string Problema { get; set; }

        public Protocolo Protocolo { get; set; }

        public Cliente Cliente { get; set; }

        public ObservableCollection<Problema> Problemas { get; set; }

        public ObservableCollection<AtendimentoDetalhado> AtendimentoDetalhado {get;set;}

    public long ID
    {
        get
        {
            return IDAtendimento;
        }
    }

    public Atendimento()
    {
            Problemas = new ObservableCollection<Model.Problema>();
            Problemas.CollectionChanged += Problemas_CollectionChanged;

            AtendimentoDetalhado = new ObservableCollection<Model.AtendimentoDetalhado>();
            AtendimentoDetalhado.CollectionChanged += AtendimentoDetalhado_CollectionChanged;
    }

        private void AtendimentoDetalhado_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                AtendimentoDetalhado[e.NewStartingIndex].Atendimento = this;
            }
        }

        private void Problemas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Problemas[e.NewStartingIndex].Atendimento = this;
            }
        }

        ~Atendimento()
    {

    }
}
}