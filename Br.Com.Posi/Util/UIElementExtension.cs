using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Br.Com.Posi.Util
{
    public static class UIElementExtension
    {

        public static void IsEnableAllChildenOfUIElement(this UIElement uiElement, bool enable, params UIElement[] exception)
        {
            UIElementCollection collection = null;
            if (uiElement is Window)
            {
                collection = ((uiElement as Window).Content as Panel).Children;
            }
            else if (uiElement is Page)
            {
                collection = ((uiElement as Page).Content as Panel).Children;
            }
            else if (uiElement is Panel)
            {
                collection = (uiElement as Panel).Children;
            }
            if (collection == null)
            {
                collection = new UIElementCollection(uiElement, null) { { uiElement } };
            }
            foreach (UIElement ui in collection)
            {
                ui.IsEnabled = enable;
            }
            foreach (UIElement ui in exception)
            {
                ui.IsEnabled = !enable;
            }
        }

        public static void IsEnableAllChildenOfUIElement(this UIElement uiElement, bool enable)
        {
            IsEnableAllChildenOfUIElement(uiElement, enable, null);
        }

        public static void IsEnableUIElement(bool enable, params UIElement[] uiElements)
        {
            foreach (UIElement ui in uiElements)
            {
                ui.IsEnabled = enable;
            }
        }

        public static void ClearAllComponentWithTextOrItem(params UIElement[] uiElement)
        {
            foreach (UIElement ui in uiElement)
            {
                if (ui is TextBox)
                {
                    (ui as TextBox).Text = string.Empty;
                }
                else if (ui is ComboBox)
                {
                    (ui as ComboBox).Items.Clear();
                }
                else if (ui is DataGrid)
                {
                    (ui as DataGrid).ItemsSource = null;
                    (ui as DataGrid).Items.Clear();
                }
            }
        }

        public static void ClearAllComponentWithTextOrItem(this UIElement uiElement)
        {
            UIElementCollection collection;
            if (uiElement is Window)
            {
                collection = ((uiElement as Window).Content as Panel).Children;
            }
            else if (uiElement is Page)
            {
                collection = ((uiElement as Page).Content as Panel).Children;
            }
            else if (uiElement is Panel)
            {
                collection = (uiElement as Panel).Children;
            }
            else
            {
                collection = new UIElementCollection(uiElement, null) { { uiElement } };
            }

            foreach (UIElement ui in collection)
            {
                if (ui is TextBox)
                {
                    (ui as TextBox).Text = string.Empty;
                }
                else if (ui is ComboBox)
                {
                    (ui as ComboBox).Items.Clear();
                }
                else if (ui is DataGrid)
                {
                    (ui as DataGrid).ItemsSource = null;
                    (ui as DataGrid).Items.Clear();
                }
            }
        }
    }
}
