using System;
using System.Xml.Serialization;

namespace Br.Com.Posi.Connection.Model
{
    [Serializable]
    [XmlRoot("Configuracao")]
    public class Configuracao
    {
        /// <summary>
        /// Informar o nome do banco de dados
        /// </summary>
        [XmlElement("BancoDeDados")]
        public String BancoDeDados { get; set; }

        /// <summary>
        /// Informar o endereco de IP ou DNS
        /// </summary>
        [XmlElement("Servidor")]
        public String Servidor { get; set; }

        /// <summary>
        /// Informar a Porta de Conexao
        /// </summary>
        [XmlElement("Porta")]
        public String Porta { get; set; }

        /// <summary>
        /// Informar o Login
        /// </summary>
        [XmlElement("Usuario")]
        public String Usuario { get; set; }

        /// <summary>
        /// Informar Senha
        /// </summary>
        [XmlElement("Senha")]
        public String Senha { get; set; }
    }
}
