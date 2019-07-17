using Br.Com.Posi.Connection.Enums;
using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Util;
using System.IO;

namespace Br.Com.Posi.Connection.Util
{
    public class MyConfiguracaoXML
    {
        private static MyConfiguracaoXML myConfiguracao;
        private string path;
        private string fileName;

        private MyConfiguracaoXML(BancoDeDados bancoDeDados)
        {
            path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            fileName = bancoDeDados.GetName();
            this.CreatePattern();
        }

        public static MyConfiguracaoXML Instance(BancoDeDados bancoDeDados)
        {
            if (myConfiguracao == null)
            {
                myConfiguracao = new MyConfiguracaoXML(bancoDeDados);
            }
            return myConfiguracao;
        }

        private void CreatePattern()
        {
            try
            {
                this.Ler();
            }
            catch
            {
                this.Gravar(new Configuracao());
            }
        }

        public void Gravar(Configuracao configuracao)
        {
            string xml = MySerializer<Configuracao>.Serialize(configuracao);
            File.WriteAllText(Path.Combine(this.path, fileName + ".xml"), xml);
        }

        public Configuracao Ler()
        {
            string xml = File.ReadAllText(Path.Combine(this.path, fileName + ".xml"));
            return MySerializer<Configuracao>.Deserialize(xml);
        }
    }
}
