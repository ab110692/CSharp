using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Br.Com.Posi.NoteAnalyzer.Connection
{
    class ConnectionArguments
    {
        private static ConnectionArguments _connection;
        String path;
        protected ConnectionArguments()
        {
            path = Path.GetTempPath() + "\\.POSI\\SHELF\\";
            CreatePattern(path);
        }
        public static ConnectionArguments Instance()
        {
            if (_connection == null)
            {
                _connection = new ConnectionArguments();
            }
            return _connection;
        }

        private void CreatePattern(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void WriterConfig(Shelf config)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Shelf));
            using (StreamWriter writer = new StreamWriter(path + "\\Config.xml"))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    xml.Serialize(xmlWriter, config);
                }
            }
        }
        public Shelf ReaderConfig()
        {
            Shelf config = null;
            if (File.Exists(path + "\\Config.xml"))
            {
                
                XmlSerializer xml = new XmlSerializer(typeof(Shelf));
                using (FileStream reader = new FileStream(path + "\\Config.xml", FileMode.Open))
                {
                    using (XmlReader xmlReader = XmlReader.Create(reader))
                    {
                        return (Shelf)xml.Deserialize(xmlReader);
                    }
                }
            }
            return config;
        }

    }
}
