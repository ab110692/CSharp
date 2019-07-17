namespace Br.Com.Posi.Util.Formatted
{
    public interface IFormatted
    {
        /// <summary>
        /// Escreve como deve ser feito a formatação da mascara.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Formatted(string text);

        /// <summary>
        /// Retorna o tamanho max do texto.
        /// </summary>
        /// <returns></returns>
        int GetLenght();

        /// <summary>
        /// Valida se a formatação é valida.
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }
}
