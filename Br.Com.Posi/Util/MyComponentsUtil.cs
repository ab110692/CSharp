using System.Windows;

namespace Br.Com.Posi.Util
{
    public static class MyComponentsUtil
    {
        /// <summary>
        /// Habilitar ou Desabilitar diversos components
        /// </summary>
        /// <param name="enable">True: para habilitar os components; False: para desabilitar os components</param>
        /// <param name="element">Informar quaisquer component grafico</param>
        public static void IsEnableComponents(bool enable, params UIElement[] element)
        {
            foreach (UIElement component in element)
            {
                component.IsEnabled = enable;
            }
        }
    }
}
