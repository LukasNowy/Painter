using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace Paint
{
    /// <summary>
    /// Interaktionslogik für NewImage.xaml
    /// </summary>
    public partial class NewImage : Window
    {

        public NewImage()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            //Bitmap erstellen

            int width = Convert.ToInt16(textBoxWidth.Text);
            int height = Convert.ToInt16(textBoxHeight.Text);

            ((MainWindow)System.Windows.Application.Current.MainWindow).bmSurface = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).bmSurface.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                }

            }

            using (MemoryStream memory = new MemoryStream())
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).bmSurface.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                ((MainWindow)System.Windows.Application.Current.MainWindow).bi = new BitmapImage();
                ((MainWindow)System.Windows.Application.Current.MainWindow).bi.BeginInit();
                ((MainWindow)System.Windows.Application.Current.MainWindow).bi.StreamSource = memory;
                ((MainWindow)System.Windows.Application.Current.MainWindow).bi.CacheOption = BitmapCacheOption.OnLoad;
                ((MainWindow)System.Windows.Application.Current.MainWindow).bi.EndInit();

                ((MainWindow)System.Windows.Application.Current.MainWindow).drawingSurface.Width = width;
                ((MainWindow)System.Windows.Application.Current.MainWindow).drawingSurface.Height = height;
                ((MainWindow)System.Windows.Application.Current.MainWindow).drawingSurface.Source = ((MainWindow)System.Windows.Application.Current.MainWindow).bi;


            }

            this.Close();
        }
    }
}
