using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Shelf.Enums;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Br.Com.Posi.Shelf.Model
{
    [Serializable]
    [XmlRoot("Protocolo")]
    public class Protocolo : IModelo
    {
        [XmlElement("IDProduto")]
        public long IDProtocolo { get; set; }

        [XmlElement("NumeroProtocolo")]
        public long NumeroProtocolo { get; set; }

        [XmlElement("TipoProtocolo")]
        public TipoProtocolo TipoProtocolo { get; set; }

        public Atendimento Atendimento { get; set; }

        public Protocolo()
        {
        }
        ~Protocolo()
        {
        }

        public long ID
        {
            get
            {
                return IDProtocolo;
            }
        }

        public override string ToString()
        {
            return this.NumeroProtocolo.ToString();
        }
    }
}