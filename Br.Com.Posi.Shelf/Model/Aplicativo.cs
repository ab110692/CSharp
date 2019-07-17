using Br.Com.Posi.Connection.Model;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class Aplicativo : IModelo
    {

        public long IDAplicativo { get; set; }

        public string Descricao { get; set; }

        public ObservableCollection<Versao> Versoes { get; set; }

        public long ID
        {
            get
            {
                return IDAplicativo;
            }
        }

        public Aplicativo()
        {
            Versoes = new ObservableCollection<Versao>();
            Versoes.CollectionChanged += Versoes_CollectionChanged;
        }

        ~Aplicativo()
        {
            Versoes.Clear();
            Versoes.CollectionChanged -= Versoes_CollectionChanged;
        }

        private void Versoes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Versoes[e.NewStartingIndex].Aplicativo = this;
            }
        }

        public override string ToString()
        {
            return this.Descricao;
        }
    }
}