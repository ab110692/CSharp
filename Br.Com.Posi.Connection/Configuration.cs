using System;

namespace Br.Com.Posi.Connection
{
    [Serializable]
    public class Configuration
    {
        /// <summary>
        /// Informar o nome do banco de dados
        /// </summary>
        public String DataBaseName { get; set; }

        /// <summary>
        /// Informar o endereco de IP ou DNS
        /// </summary>
        public String ServerName { get; set; }

        /// <summary>
        /// Informar a Porta de Conexao
        /// </summary>
        public String ServerPort { get; set; }

        /// <summary>
        /// Informar o Login
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// Informar Senha
        /// </summary>
        public String Password { get; set; }
    }
}
