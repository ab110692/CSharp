namespace Br.Com.Posi.Connection.Enums
{
    public enum BancoDeDados
    {
        Shelf = 0
    }

    public static class BancoDeDadosExtension
    {
        public static string GetName(this BancoDeDados dataBase)
        {
            switch (dataBase)
            {
                case BancoDeDados.Shelf:
                    return "Shelf";
            }
            return string.Empty;
        }
    }
}
