using Br.Com.Posi.NoteAnalyzer.Enums;
using NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Enums;
using System;

namespace Br.Com.Posi.NoteAnalyzer.DataGrid.Model
{
    public class NoteModel
    {
        public String UF
        {
            get
            {
                if (Chave.Length > 13)
                {
                    return Chave.Substring(0, 2);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public String AA
        {
            get
            {
                if (Chave.Length > 13)
                {
                    return Chave.Substring(2, 2);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public String MM
        {
            get
            {
                if (Chave.Length > 13)
                {
                    return Chave.Substring(4, 2);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public String CNPJ
        {
            get
            {
                if (Chave.Length > 20)
                {
                    return Chave.Substring(6, 14);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public String Mod
        {
            get
            {
                if (Chave.Length > 22)
                {
                    return Chave.Substring(20, 2);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public String Serie
        {
            get
            {
                if (Chave.Length > 25)
                {
                    if ((Convert.ToInt32(Mod) == (int)Modelo.NFCE))
                    {
                        return Chave.Substring(22, 3);
                    }
                    else
                    {
                        return Chave.Substring(22, 9);
                    }
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public DateTime DataCriado { get; set; }

        public bool Inutilizado
        {
            get
            {
                if (Chave.Length >= 44)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public int NumeroNota
        {
            get
            {
                if (Chave.Length > 34)
                {
                    if (Convert.ToInt32(Mod) == (int)Modelo.NFCE)
                    {
                        return Convert.ToInt32(Chave.Substring(25, 9));
                    }
                    else
                    {
                        return Convert.ToInt32(Chave.Substring(31, 6));
                    }

                }
                else
                {
                    try
                    {
                        return Convert.ToInt32(Chave.Substring(0, 9));
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                }
            }
        }

        public String TipoEmissao
        {
            get
            {
                if (Chave.Length > 35)
                {
                    if ((Convert.ToInt32(Mod) == (int)Modelo.NFCE))
                    {
                        return Chave.Substring(34, 1);
                    }
                    else
                    {
                        return "0";
                    }

                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public String Chave { get; set; }

        public Extrato extrato { get; set; }

    }
}
