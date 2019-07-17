using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Enums;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class Perfil : IModelo
    {
        public long IDPerfil { get; set; }

        public string Nome { get; set; }

        public Setor Setor { get; set; }

        public PrivilegioCRUD Atendimento { get; set; }

        public PrivilegioCRUD Funcionario { get; set; }

        public PrivilegioCRUD Manutencao { get; set; }

        public PrivilegioCRUD Cliente { get; set; }

        public string AtendimentoTexto { get; set; }

        public string FuncionarioTexto { get; set; }

        public string ManutencaoTexto { get; set; }

        public string ClienteTexto { get; set; }

        public ObservableCollection<FuncionarioDadosPessoais> FuncionariosDadosPessoais { get; set; }

        public Perfil()
        {
            FuncionariosDadosPessoais = new ObservableCollection<Model.FuncionarioDadosPessoais>();
            FuncionariosDadosPessoais.CollectionChanged += Funcionarios_CollectionChanged;
        }

        private void Funcionarios_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    this.FuncionariosDadosPessoais[e.NewStartingIndex].Perfil = this;
                    break;
            }
        }

        long IModelo.ID
        {
            get
            {
                return IDPerfil;
            }
        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
