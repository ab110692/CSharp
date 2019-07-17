using Br.Com.Posi.MyUI.Enums;
using System;

namespace Br.Com.Posi.Util.Formatted
{
    public static class FactoryFormatted
    {
        public static FormattedImpl InitFormatted(TextBoxMasked mask)
        {
            switch (mask)
            {
                case TextBoxMasked.CEP:
                    return new FormattedCEPImpl();
                case TextBoxMasked.CPF:
                    return new FormattedCPFImpl();
                case TextBoxMasked.EMAIL:
                    return new FormattedEmailImpl();
                case TextBoxMasked.NUMERO:
                    return new FormattedNumeroImpl();
                case TextBoxMasked.PIS:
                    return new FormattedPISImpl();
                case TextBoxMasked.TELEFONE:
                    return new FormattedTelefoneImpl();
                case TextBoxMasked.TEXTO:
                    return new FormattedTextoImpl();
                default:
                    throw new NotImplementedException("Mascara inválida!");
            }
        }
    }
}
