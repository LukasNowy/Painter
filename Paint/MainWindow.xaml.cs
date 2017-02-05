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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace Paint
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap bmSurface;
        BitmapImage bi;

        public MainWindow()
        {
            InitializeComponent();
            INIT();
        }

        public void INIT()
        {
            //Initialisierung

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Bitmap erstellen

            bmSurface = new Bitmap(400, 400);


            for (int x = 0; x <= 399; x++)
            {
                for (int y = 0; y <= 399; y++)
                {
                    bmSurface.SetPixel(x, y, System.Drawing.Color.FromArgb(29, 155, 215));
                }

            }

            bmSurface.Save("test.bmp");

            using (MemoryStream memory = new MemoryStream())
            {
                bmSurface.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = memory;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                drawingSurface.Source = bi;
            }

        }

        private void drawingSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void drawingSurface_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
