using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Enums;
using System.Collections.ObjectModel;

namespace Br.Com.Posi.Shelf.Model
{
    public class FuncionarioDadosPessoais : IModelo
    {

        public long IDFuncionario { get; set; }

        public string NomeCompleto { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Pis { get; set; }

        public string Email { get; set; }

        public string CEP { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Numero { get; set; }

        public string Cidade { get; set; }

        public Estado Estado { get; set; }
       
        public Perfil Perfil { get; set; }


        public ObservableCollection<Funcionario> Funcionarios { get; set; }

        public FuncionarioDadosPessoais()
        {
            Funcionarios = new ObservableCollection<Funcionario>();
            Funcionarios.CollectionChanged += Funcionario_CollectionChanged;
        }

        private void Funcionario_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    this.Funcionarios[e.NewStartingIndex].FuncionarioDadosPessoais = this;
                    break;
            }
        }

        long IModelo.ID
        {
            get
            {
                return IDFuncionario;
            }
        }

        public override string ToString()
        {

            return this.NomeCompleto;
        }
    }
}
