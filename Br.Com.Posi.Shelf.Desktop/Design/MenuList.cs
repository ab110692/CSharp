using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Br.Com.Posi.Shelf.Desktop.Design
{
    public class MenuList
    {
        public int Position { get; set; }

        public String Title { get; set; }

        public BitmapImage ImageData { get; set; }

        public string ImagePath { get; set; }

        public Geometry Geometry { get; set; }

        public bool IsTimer { get; set; }

        public string Tempo
        {
            get
            {
                return IsTimer ? DateTime.Now.TimeOfDay.Subtract(Timer).ToString(@"hh\:mm\:ss") : string.Empty;
            }
        }

        private TimeSpan Timer { get; set; }

        public MenuList()
        {
            Timer = DateTime.Now.TimeOfDay;
        }

    }

}
