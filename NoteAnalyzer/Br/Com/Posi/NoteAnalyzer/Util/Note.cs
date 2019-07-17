using Br.Com.Posi.NoteAnalyzer.DataGrid.Model;
using NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Br.Com.Posi.NoteAnalyzer.Util
{
    public class Note
    {

        public static String Connect(String server, String user, String pass)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("NET", String.Format(@"USE \\{0} /USER:{1} {2}", server, user, pass));

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
                String aux = process.StandardError.ReadToEnd();

                return aux;
            }
            else
            {
                return process.StandardOutput.ReadToEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        /// <exception cref="IOException">Usuário ou senha invalido ou caminho de destino invalido</exception>
        public static String[] ListNetwork(String server)
        {
            return Directory.GetDirectories(server);
        }

        public static String[] ListStoreNumber(String server, String network)
        {
            return Directory.GetDirectories(server + @"\" + network);
        }

        public static String[] ListYearPerStore(String server, String network, String store)
        {
            return Directory.GetDirectories(server + @"\" + network + @"\" + store);
        }

        public static String[] ListMonthPerStore(String server, String network, String store, String year)
        {
            return Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year);
        }

        public static List<NoteModel> ListNotePerMonthPerStore(String server, String network, String store, String year, String month, String type, BackgroundWorker w)
        {
            List<NoteModel> models = new List<NoteModel>();
            String[] days = Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year + @"\" + month);
            foreach (String d in days)
            {
                String[] types = Directory.GetDirectories(d);
                foreach (String t in types)
                {
                    if (t.Split('\\').Last().Equals(type, StringComparison.OrdinalIgnoreCase))
                    {
                        String[] extracts = Directory.GetDirectories(t);
                        foreach (String e in extracts)
                        {
                            if (e.Split('\\').Last().Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                                || e.Split('\\').Last().Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                                || e.Split('\\').Last().Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                                || e.Split('\\').Last().Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                            {
                                switch (e.Split('\\').Last())
                                {
                                    case "Enviadas":
                                        String[] notes = Directory.GetFiles(e);
                                        foreach (String n in notes)
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
                                        foreach (String n in notes)
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
                                        foreach (String n in notes)
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
                                        foreach (String n in notes)
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

        public static List<NoteModel> GetFirstNote(String server, String network, String store, String year, String month, String type)
        {
            List<NoteModel> models = new List<NoteModel>();

            if (month.Equals("1") || month.Equals("01"))
            {
                month = "12";
                year = (Convert.ToInt32(year) - 1).ToString();
            }
            else if(month.Length != 2)
            {
                month = "0" + (Convert.ToInt32(month) - 1).ToString();
            }

            String[] days = Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year + @"\" + month);
            String d = days.OrderBy(e => e).Last();
            String[] types = Directory.GetDirectories(d);
            foreach (String t in types)
            {
                if (t.Substring(t.LastIndexOf(@"\") + 1).Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    String[] extracts = Directory.GetDirectories(t);
                    foreach (String e in extracts)
                    {
                        if (e.Substring(e.LastIndexOf(@"\") + 1).Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                        {
                            String[] notes = Directory.GetFiles(e);
                            foreach (String n in notes)
                            {
                                NoteModel note = new NoteModel();
                                //FileInfo info = new FileInfo(n);
                                //note.DataCriado = info.CreationTime;
                                note.Chave = n.Substring(n.LastIndexOf(@"\") + 1).Replace("NFe", "").Replace("CFe", "");
                                models.Add(note);
                            }
                        }
                    }
                }
            }
            return models;
        }

        public static List<NoteModel> GetLastNote(String server, String network, String store, String year, String month, String type)
        {
            List<NoteModel> models = new List<NoteModel>();

            if (month.Equals("12"))
            {
                month = "01";
                year = (Convert.ToInt32(year) + 1).ToString();
            }
            else if(month.Length != 2)
            {
                month = "0" + (Convert.ToInt32(month) + 1).ToString();
            }

            String[] days = Directory.GetDirectories(server + @"\" + network + @"\" + store + @"\" + year + @"\" + month);
            String d = days.OrderBy(e => e).First();
            String[] types = Directory.GetDirectories(d);
            foreach (String t in types)
            {
                if (t.Substring(t.LastIndexOf(@"\") + 1).Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    String[] extracts = Directory.GetDirectories(t);
                    foreach (String e in extracts)
                    {
                        if (e.Substring(e.LastIndexOf(@"\") + 1).Equals("Enviadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Inutilizadas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Canceladas", StringComparison.OrdinalIgnoreCase)
                            || e.Substring(e.LastIndexOf(@"\") + 1).Equals("Extrato", StringComparison.OrdinalIgnoreCase))
                        {
                            String[] notes = Directory.GetFiles(e);
                            foreach (String n in notes)
                            {
                                NoteModel note = new NoteModel();
                                //FileInfo info = new FileInfo(n);
                                //note.DataCriado = info.CreationTime;
                                note.Chave = n.Substring(n.LastIndexOf(@"\") + 1).Replace("NFe", "").Replace("CFe", "");
                                models.Add(note);
                            }
                        }
                    }
                }
            }
            return models;
        }
    }
}
