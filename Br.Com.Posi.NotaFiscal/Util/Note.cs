using Br.Com.Posi.NotaFiscal.Enums;
using Br.Com.Posi.NotaFiscal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Br.Com.Posi.NotaFiscal.Util
{
    public class Note
    {
        private static readonly string[] REDES = { "BOX", "CAS", "CHU", "COM", "HAB", "HIB", "RAG", "REX", "SEL" };

        public static string Connect(string server, string user, string pass)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("NET", string.Format(@"USE Z: \\{0} /USER:{1} {2}", server, user, pass));

            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            process.WaitForExit();
            if (process.StandardError.ReadToEnd().Any())
            {
                string aux = process.StandardError.ReadToEnd();
                return aux;
            }
            else
            {
                string aux = process.StandardOutput.ReadToEnd();
                return aux;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        /// <exception cref="IOException">Usuário ou senha invalido ou caminho de destino invalido</exception>
        public static string[] ListNetwork(string server)
        {
            return Directory.GetDirectories(server);
        }

        public static string[] ListStoreNumber(string server, string network)
        {
            return Directory.GetDirectories(server + @"\" + network);
        }

        public static string[] ListYearPerStore(string server, string network, string store)
        {
            return Directory.GetDirectories(server + @"\" + network + @"\" + store);
        }

        public static string[] ListMonthPerStore(string server, string network, string store, string year)
        {
            return Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year);
        }

        public static List<NoteModel> ListNotePerMonthPerStore(string server, string network, string store, string year, string month, string type, BackgroundWorker w)
        {
            List<NoteModel> models = new List<NoteModel>();
            string[] days = Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year + @"\" + month);
            foreach (string d in days)
            {
                string[] types = Directory.GetDirectories(d);
                foreach (string t in types)
                {
                    if (t.Split('\\').Last().Equals(type, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] extracts = Directory.GetDirectories(t);
                        foreach (string e in extracts)
                        {
                            if (e.Split('\\').Last().Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                                || e.Split('\\').Last().Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                                || e.Split('\\').Last().Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                                || e.Split('\\').Last().Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                            {
                                switch (e.Split('\\').Last())
                                {
                                    case "Enviadas":
                                        string[] notes = Directory.GetFiles(e);
                                        foreach (string n in notes)
                                        {
                                            if (w.CancellationPending)
                                            {
                                                return new List<NoteModel>();
                                            }
                                            NoteModel note = new NoteModel();
                                            FileInfo info = new FileInfo(n);
                                            note.DataCriado = info.CreationTime;
                                            note.Chave = info.Name.Replace("NFe", "").Replace("CFe", "");//n.Substring(n.LastIndexOf(@"\") + 1).Replace("NFe", "").Replace("CFe", "");
                                            note.extrato = Extrato.Venda;
                                            models.Add(note);
                                        }
                                        break;

                                    case "Inutilizadas":
                                        notes = Directory.GetFiles(e);
                                        foreach (string n in notes)
                                        {
                                            if (w.CancellationPending)
                                            {
                                                return new List<NoteModel>();
                                            }
                                            NoteModel note = new NoteModel();
                                            FileInfo info = new FileInfo(n);
                                            note.extrato = Extrato.Inutilizado;
                                            note.DataCriado = info.CreationTime;
                                            note.Chave = info.Name.Replace("NFe", "").Replace("CFe", "");
                                            models.Add(note);
                                        }
                                        break;

                                    case "Canceladas":
                                        notes = Directory.GetFiles(e);
                                        foreach (string n in notes)
                                        {
                                            if (w.CancellationPending)
                                            {
                                                return new List<NoteModel>();
                                            }
                                            NoteModel note = new NoteModel();
                                            FileInfo info = new FileInfo(n);
                                            note.extrato = Extrato.Cancelado;
                                            note.DataCriado = info.CreationTime;
                                            note.Chave = info.Name.Replace("NFe", "").Replace("CFe", "");
                                            models.Add(note);
                                        }
                                        break;

                                    case "Extrato":
                                        notes = Directory.GetFiles(e);
                                        DirectoryInfo dirInfo = new DirectoryInfo(e);
                                        foreach (string n in notes)
                                        {
                                            if (w.CancellationPending)
                                            {
                                                return new List<NoteModel>();
                                            }
                                            NoteModel note = new NoteModel();
                                            //FileInfo info = new FileInfo(n);
                                            note.DataCriado = dirInfo.CreationTime;//info.CreationTime;
                                            note.Chave = n.Substring(n.LastIndexOf(@"\") + 1).Replace("NFe", "").Replace("CFe", "");
                                            models.Add(note);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return models;
        }

        public static List<NoteModel> GetFirstNote(string server, string network, string store, string year, string month, string type)
        {
            List<NoteModel> models = new List<NoteModel>();

            if (month.Equals("1") || month.Equals("01"))
            {
                month = "12";
                year = (Convert.ToInt32(year) - 1).ToString();
            }
            else if (month.Length != 2)
            {
                month = "0" + (Convert.ToInt32(month) - 1).ToString();
            }

            string[] days = Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year + @"\" + month);
            string d = days.OrderBy(e => e).Last();
            string[] types = Directory.GetDirectories(d);
            foreach (string t in types)
            {
                if (t.Substring(t.LastIndexOf(@"\") + 1).Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    string[] extracts = Directory.GetDirectories(t);
                    foreach (string e in extracts)
                    {
                        if (e.Substring(e.LastIndexOf(@"\") + 1).Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] notes = Directory.GetFiles(e);
                            foreach (string n in notes)
                            {
                                NoteModel note = new NoteModel();
                                note.Chave = n.Substring(n.LastIndexOf(@"\") + 1).Replace("NFe", "").Replace("CFe", "");
                                models.Add(note);
                            }
                        }
                    }
                }
            }
            return models;
        }

        public static List<NoteModel> GetLastNote(string server, string network, string store, string year, string month, string type)
        {
            List<NoteModel> models = new List<NoteModel>();

            if (month.Equals("12"))
            {
                month = "01";
                year = (Convert.ToInt32(year) + 1).ToString();
            }
            else if (month.Length != 2)
            {
                month = "0" + (Convert.ToInt32(month) + 1).ToString();
            }

            string[] days = Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year + @"\" + month);
            string d = days.OrderBy(e => e).First();
            string[] types = Directory.GetDirectories(d);
            foreach (string t in types)
            {
                if (t.Substring(t.LastIndexOf(@"\") + 1).Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    string[] extracts = Directory.GetDirectories(t);
                    foreach (string e in extracts)
                    {
                        if (e.Substring(e.LastIndexOf(@"\") + 1).Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] notes = Directory.GetFiles(e);
                            foreach (string n in notes)
                            {
                                NoteModel note = new NoteModel();
                                note.Chave = n.Substring(n.LastIndexOf(@"\") + 1).Replace("NFe", "").Replace("CFe", "");
                                models.Add(note);
                            }
                        }
                    }
                }
            }
            return models;
        }

        public static int ListaLojaPorAnoMes(string server, int mes, int ano)
        {
            if (!Directory.Exists(server))
            {
                throw new DirectoryNotFoundException("Diretorio não existe!");
            }

            int qtsLojas = 0;

            foreach (string aux in Directory.GetDirectories(server))
            {
                if (REDES.Contains(aux.Split('\\').Last()))
                {
                    foreach (string lojas in Directory.GetDirectories(aux))
                    {
                        foreach (string anos in Directory.GetDirectories(lojas))
                        {
                            if (int.TryParse(anos.Split('\\').Last(), out int year) && year == ano)
                            {
                                foreach (string meses in Directory.GetDirectories(anos))
                                {
                                    if (int.TryParse(meses.Split('\\').Last(), out int month) && month == mes)
                                    {
                                        qtsLojas++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return qtsLojas;
        }

        public static int ListaXMLPorAnoMes(string server, int mes, int ano)
        {
            if (!Directory.Exists(server))
            {
                throw new DirectoryNotFoundException("Diretorio não existe!");
            }

            int qtsXML = 0;

            foreach (string aux in Directory.GetDirectories(server))
            {
                if (REDES.Contains(aux.Split(':').Last()))
                {
                    foreach (string lojas in Directory.GetDirectories(aux))
                    {
                        foreach (string anos in Directory.GetDirectories(lojas))
                        {
                            if (int.TryParse(anos.Split('\\').Last(), out int year) && year == ano)
                            {
                                foreach (string meses in Directory.GetDirectories(anos))
                                {
                                    if (int.TryParse(meses.Split('\\').Last(), out int month) && month == mes)
                                    {
                                        foreach (string dias in Directory.GetDirectories(meses))
                                        {
                                            foreach (string tipos in Directory.GetDirectories(dias))
                                            {
                                                foreach (string e in Directory.GetDirectories(tipos))
                                                {
                                                    if (e.Split('\\').Last().Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                                                        || e.Split('\\').Last().Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                                                        || e.Split('\\').Last().Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                                                        || e.Split('\\').Last().Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                                                    {
                                                        qtsXML += Directory.GetDirectories(e).Length;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return qtsXML;
        }

        public static IEnumerable<int> CompactarPorAnoMes(string origem, string destino, int mes, int ano)
        {
            if (!Directory.Exists(origem))
            {
                throw new DirectoryNotFoundException("Diretorio não existe!");
            }

            int count = 0;

            string rede = string.Empty;

            foreach (string aux in Directory.GetDirectories(origem))
            {
                if (REDES.Contains(aux.Split('\\').Last()))
                {
                    rede = aux;
                    foreach (string lojas in Directory.GetDirectories(aux))
                    {
                        foreach (string anos in Directory.GetDirectories(lojas))
                        {
                            if (int.TryParse(anos.Split('\\').Last(), out int year) && year == ano)
                            {
                                foreach (string meses in Directory.GetDirectories(anos))
                                {
                                    if (int.TryParse(meses.Split('\\').Last(), out int month) && month == mes)
                                    {
                                        string source = Path.Combine(origem, aux.Split('\\').Last(), lojas.Split('\\').Last(), anos.Split('\\').Last(), meses.Split('\\').Last());
                                        string destiny = Path.Combine(destino, ano.ToString().Split('\\').Last(), mes.ToString().Split('\\').Last(), rede.Split('\\').Last());
                                        if (!Directory.Exists(destiny))
                                        {
                                            Directory.CreateDirectory(destiny);
                                        }

                                        if (File.Exists(destiny + "\\" + lojas.Split('\\').Last() + ".zip"))
                                        {
                                            File.Delete(destiny + "\\" + lojas.Split('\\').Last() + ".zip");
                                        }

                                        if (!File.Exists(destiny + "\\" + lojas.Split('\\').Last() + ".zip"))
                                        {
                                            ZipFile.CreateFromDirectory(source, destiny + "\\" + lojas.Split('\\').Last() + ".zip");
                                        }

                                        yield return count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
