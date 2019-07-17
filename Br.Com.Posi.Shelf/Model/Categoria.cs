using Br.Com.Posi.Connection.Model;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class Categoria : IModelo
    {

        public long IDCategoria { get; set; }

        public string Nome { get; set; }

        public ObservableCollection<SubCategoria> SubCategorias { get; set; }

        public long ID
        {
            get
            {
                return IDCategoria;
            }
        }

        public Categoria()
        {
            SubCategorias = new ObservableCollection<SubCategoria>();
            SubCategorias.CollectionChanged += SubCategorias_CollectionChanged;
        }

        private void SubCategorias_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                SubCategorias[e.NewStartingIndex].Categoria = this;
            }
        }

        ~Categoria()
        {
            SubCategorias.CollectionChanged -= SubCategorias_CollectionChanged;
            SubCategorias = null;
        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}