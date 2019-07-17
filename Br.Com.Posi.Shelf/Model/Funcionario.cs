using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class Funcionario : IModelo
    {
        public long IDFuncionarioLogin { get; set; }
        /// <summary>
        /// Tabela Acima
        /// </summary>
        public string Nome { get; set; }
        public string Senha { get; set; }
        /// <summary>
        /// Tabela Abaixo
        /// </summary>
        public FuncionarioDadosPessoais FuncionarioDadosPessoais { get; set; }
        long IModelo.ID
        {
            get
            {
                return IDFuncionarioLogin;
            }
        }
        public override string ToString()
        {

            return this.Nome;
        }
    }
}
