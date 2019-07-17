using System.IO;

namespace Br.Com.Posi.Util
{
    public class FileManager
    {

        private static readonly FileManager _instance;

        static FileManager()
        {
            _instance = new FileManager();
        }

        public static FileManager GetInstance()
        {
            return _instance;
        }

        public void CopiarArquivo(string origem, string destino, bool overWrite = false)
        {
            if (!File.Exists(origem))
            {
                throw new FileNotFoundException("Arquivo origem inexistente");
            }
            if (!Directory.Exists(destino))
            {
                throw new DirectoryNotFoundException("Pasta destino inexistente");
            }

            File.Copy(origem, destino, overWrite);
        }

        public void CopiarPasta(string origem, string destino, bool overWrite = false)
        {
            if (!Directory.Exists(origem))
            {
                throw new DirectoryNotFoundException("Diretorio origem inexistente");
            }
            if (!Directory.Exists(destino))
            {
                Directory.CreateDirectory(destino);
            }

            foreach (string files in Directory.GetFiles(origem))
            {
                this.CopiarArquivo(origem, destino, overWrite);
            }

            foreach (string dir in Directory.GetDirectories(origem))
            {
                this.CopiarPasta(origem, destino, overWrite);
            }
        }

    }
}
