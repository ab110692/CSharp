using Br.Com.Posi.Connection.Model;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class SubCategoria : IModelo
    {

        public long IDSubCategoria { get; set; }

        public string Nome { get; set; }

        public Categoria Categoria { get; set; }

        public ObservableCollection<Item> Items { get; set; }

        public long ID
        {
            get
            {
                return IDSubCategoria;
            }
        }

        public SubCategoria()
        {
            Items = new ObservableCollection<Item>();
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Items[e.NewStartingIndex].SubCategoria = this;
            }
        }

        ~SubCategoria()
        {
        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}