using Br.Com.Posi.Connection.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class Setor : IModelo
    {
        public long IDSetor { get; set; }

        public string Nome { get; set; }

        public ObservableCollection<Perfil> Perfis { get; set; }

        public Setor()
        {
            Perfis = new ObservableCollection<Perfil>();
            Perfis.CollectionChanged += Perfils_CollectionChanged;
        }

        private void Perfils_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    this.Perfis[e.NewStartingIndex].Setor = this;
                    break;
            }
        }

        long IModelo.ID
        {
            get
            {
                return IDSetor;
            }
        }

        public override string ToString()
        {

            return this.Nome;
        }
    }
}
