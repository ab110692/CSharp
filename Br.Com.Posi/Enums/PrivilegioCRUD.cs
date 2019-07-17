using System;
using System.Collections.Generic;
using System.Linq;

namespace Br.Com.Posi.Enums
{
    /// <summary>
    /// -1 (No Read) NR
    /// 0 (Read) R
    /// 1 (Read / Delete) RD
    /// 2 (Read / Update) RU
    /// 3 (Read / Update / Delete) RUD
    /// 4 (Create / Read) CR
    /// 5 (Create / Read / Delete) CRD
    /// 6 (Create / Read / Update) CRU
    /// 7 (Create / Read / Update / Delete) Total CRUD
    /// </summary>
    public enum PrivilegioCRUD
    {
        SEM_ACESSO = -1,
        VISUALIZAR = 0,
        VISUALIZAR_DELETAR = 1,
        VISUALIZAR_ALTERAR = 2,
        VISUALIZAR_ALTERAR_DELETAR = 3,
        CRIAR_VISUALIZAR = 4,
        CRIAR_VISUALIZAR_DELETAR = 5,
        CRIAR_VISUALIZAR_ALTERAR = 6,
        CRIAR_VISUALIZAR_ALTERAR_DELETAR = 7
    }

    public static class PrivilegioCRUDExtension
    {
        public static int GetCode(this PrivilegioCRUD e)
        {
            switch (e)
            {
                default:
                case PrivilegioCRUD.SEM_ACESSO:
                    return -1;
                case PrivilegioCRUD.VISUALIZAR:
                    return 0;
                case PrivilegioCRUD.VISUALIZAR_DELETAR:
                    return 1;
                case PrivilegioCRUD.VISUALIZAR_ALTERAR:
                    return 2;
                case PrivilegioCRUD.VISUALIZAR_ALTERAR_DELETAR:
                    return 3;
                case PrivilegioCRUD.CRIAR_VISUALIZAR:
                    return 4;
                case PrivilegioCRUD.CRIAR_VISUALIZAR_DELETAR:
                    return 5;
                case PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR:
                    return 6;
                case PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR_DELETAR:
                    return 7;
            }
        }


        public static string GetName(this PrivilegioCRUD e)
        {
            switch (e)
            {
                default:
                case PrivilegioCRUD.SEM_ACESSO:
                    return "Sem Acesso";
                case PrivilegioCRUD.VISUALIZAR:
                    return "Visualizar";
                case PrivilegioCRUD.VISUALIZAR_DELETAR:
                    return "Visualizar e Deletar";
                case PrivilegioCRUD.VISUALIZAR_ALTERAR:
                    return "Visualizar e Alterar";
                case PrivilegioCRUD.VISUALIZAR_ALTERAR_DELETAR:
                    return "Visualizar, Alterar e Deletar";
                case PrivilegioCRUD.CRIAR_VISUALIZAR:
                    return "Criar e Visualizar";
                case PrivilegioCRUD.CRIAR_VISUALIZAR_DELETAR:
                    return "Criar, Visualizar e Deltar";
                case PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR:
                    return "Criar, Visualizar e Alterar";
                case PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR_DELETAR:
                    return "Acesso Total";
            }
        }



        public static PrivilegioCRUD GetFromName(this PrivilegioCRUD e, string name)
        {
            foreach (PrivilegioCRUD aux in Enum.GetValues(typeof(PrivilegioCRUD)))
            {
                if (aux.GetName().Equals(name))
                {
                    return aux;
                }
            }
            return e;
        }

        public static PrivilegioCRUD GetFromCode(this PrivilegioCRUD e, int code)
        {
            foreach (PrivilegioCRUD aux in Enum.GetValues(typeof(PrivilegioCRUD)))
            {
                if (aux.GetCode() == code)
                {
                    return aux;
                }
            }
            return e;
        }


               

        public static List<PrivilegioCRUD> GetList(this PrivilegioCRUD e)
        {
            return Enum.GetValues(typeof(PrivilegioCRUD)).OfType<PrivilegioCRUD>().ToList();
        }
    }
}
