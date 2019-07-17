using System;
using System.Xml.Serialization;

namespace Br.Com.Posi.SoftwareCamila.Desktop.model
{
    [Serializable]
    [XmlRoot("Conta")]
    public class Conta
    {
        public string ServidorSMTP { get; set; }
        public int PortaSMTP { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string DataBase { get; set; }
    }
}
