using Br.Com.Posi.Enums;
using System.Windows.Controls;

namespace Br.Com.Posi.Util
{
    public class Permissao
    {
        public static int ToValidaPrivilegioCRUD(CheckBox leitura, CheckBox criacao, CheckBox alteracao, CheckBox remocao)
        {
            int r = leitura.IsChecked == true ? (int)PrivilegioCRUD.VISUALIZAR : (int)PrivilegioCRUD.SEM_ACESSO;
            int cr = criacao.IsChecked == true ? (int)PrivilegioCRUD.CRIAR_VISUALIZAR : 0;
            int ru = alteracao.IsChecked == true ? (int)PrivilegioCRUD.VISUALIZAR_ALTERAR : 0;
            int rd = remocao.IsChecked == true ? (int)PrivilegioCRUD.VISUALIZAR_DELETAR : 0;

            return (r + cr + ru + rd);
        }

        public static bool[] FromValidaPrivilegioCRUD(PrivilegioCRUD crud)
        {
            switch (crud)
            {
                case PrivilegioCRUD.SEM_ACESSO:
                    return new bool[] { false, false, false, false };
                case PrivilegioCRUD.VISUALIZAR:
                    return new bool[] { true, false, false, false };
                case PrivilegioCRUD.VISUALIZAR_DELETAR:
                    return new bool[] { true, false, false, true };
                case PrivilegioCRUD.VISUALIZAR_ALTERAR:
                    return new bool[] { true, false, true, false };
                case PrivilegioCRUD.VISUALIZAR_ALTERAR_DELETAR:
                    return new bool[] { true, false, true, true };
                case PrivilegioCRUD.CRIAR_VISUALIZAR:
                    return new bool[] { true, true, false, false };
                case PrivilegioCRUD.CRIAR_VISUALIZAR_DELETAR:
                    return new bool[] { true, true, false, true };
                case PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR:
                    return new bool[] { true, true, true, false };
                case PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR_DELETAR:
                    return new bool[] { true, true, true, true };
                default:
                    return new bool[] { false, false, false, false };
            }
        }
    }
}
