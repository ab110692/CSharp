using Br.Com.Posi.Connection.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
#pragma warning disable CS0659 // O tipo substitui Object. Equals (objeto o), mas não substitui o Object.GetHashCode()
    public class Rede : IModelo
#pragma warning restore CS0659 // O tipo substitui Object. Equals (objeto o), mas não substitui o Object.GetHashCode()
    {
        public long IDRede { get; set; }

        public long Codigo { get; set; }

        public string Nome { get; set; }

        public ObservableCollection<Cliente> Clientes { get; set; }

        long IModelo.ID
        {
            get
            {
                return IDRede;
            }
        }

        public Rede()
        {
            Clientes = new ObservableCollection<Cliente>();
            Clientes.CollectionChanged += Clientes_CollectionChanged;
        }

        private void Clientes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                this.Clientes[e.NewStartingIndex].Rede = this;
            }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public override bool Equals(object obj)
        {
            Rede rede = obj as Rede;
            return rede.IDRede == IDRede && rede.Codigo == Codigo && rede.Nome == Nome;
        }
    }
}
