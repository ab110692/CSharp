using Br.Com.Posi.Connection.Model;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class Cliente : IModelo
    {
        public long IDCliente { get; set; }

        public Rede Rede { get; set; }

        public long Codigo { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string CpfCnpj { get; set; }

        public string InscricaoEstadual { get; set; }

        public string Email { get; set; }

        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Numero { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }


        public ObservableCollection<Contrato> Contratos { get; set; }

        public ObservableCollection<Telefone> Telefones { get; set; }

        public ObservableCollection<Computador> Computadores { get; set; }

        public Cliente()
        {
            this.Rede = new Rede();
            this.Contratos = new ObservableCollection<Contrato>();
            this.Contratos.CollectionChanged += Contratos_CollectionChanged;
            this.Telefones = new ObservableCollection<Telefone>();
            this.Telefones.CollectionChanged += Telefones_CollectionChanged;
            this.Computadores = new ObservableCollection<Computador>();
            this.Computadores.CollectionChanged += Computadores_CollectionChanged;
        }

        private void Telefones_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Telefones[e.NewStartingIndex].Cliente = this;
            }
        }

        private void Contratos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Contratos[e.NewStartingIndex].Cliente = this;
            }
        }

        private void Computadores_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Computadores[e.NewStartingIndex].Cliente = this;
            }
        }

        ~Cliente()
        {
            this.Rede = null;
            this.Contratos.Clear();
            this.Contratos.CollectionChanged -= Contratos_CollectionChanged;
            this.Contratos = null;
            this.Telefones.Clear();
            this.Telefones.CollectionChanged -= Telefones_CollectionChanged;
            this.Telefones = null;
            this.Computadores.Clear();
            this.Computadores.CollectionChanged -= Computadores_CollectionChanged;
            this.Computadores = null;

        }

        long IModelo.ID
        {
            get
            {
                return IDCliente;
            }
        }

    }
}
