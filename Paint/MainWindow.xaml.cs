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
using Microsoft.Win32;

namespace Paint
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Point mousePos;
        SaveFileDialog sfd;

        internal Bitmap bmSurface;
        internal BitmapImage bi;

        bool mdown = false;
        System.Windows.Point startPos;
        System.Windows.Point endPos;
        int names = 1;

        List<Geometrische_Form> geo_formen = new List<Geometrische_Form>();

        public MainWindow()
        {
            InitializeComponent();
            INIT();
        }

        private void INIT()
        {
            //Initialisierung
        }

        private void AddToImage()
        {
            //Fügt die Bitmap zur ImageBox hinzu

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Neues Image erzeugen (Fenster NewImage wird aufgerufen)

            NewImage image = new NewImage();
            image.Show();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            //Bitmap als Bild speichern

            sfd = new SaveFileDialog();
            sfd.Filter = "Windows Bitmap | *.bmp";

            if (sfd.ShowDialog() == true)
            {
                bmSurface.Save(sfd.FileName);
            }

            MessageBox.Show("Gespeichert in: " + sfd.FileName.ToString());
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            // Zeigerposition im Label ausgeben

            mousePos = Mouse.GetPosition(drawingSurface);

            textBlockPosition.Text = "Position: " + Convert.ToInt16(mousePos.X) + " ; " + Convert.ToInt16(mousePos.Y);

        }

        private void drawingSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Bool mdown auf true setzen

            mdown = true;

            //startPos auf die Mausposition setzen

            startPos = Mouse.GetPosition(drawingSurface);
        }

        private void drawingSurface_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Bool mdown wieder auf false setzen

            mdown = false;

            //endPos auf die Mausposition setzen + Rechteck zeichnen + zu Liste und Listbox hinzufügen

            endPos = Mouse.GetPosition(drawingSurface);

            if(radioButtonRectangle.IsChecked == true)
            {

                for (int x = Convert.ToInt16(startPos.X); x < endPos.X; x++)
                {
                    for (int y = Convert.ToInt16(startPos.Y); y < endPos.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 50, 200));
                    }
                }

                Rectangle rectangle = new Rectangle(Convert.ToInt16(endPos.X - startPos.X), Convert.ToInt16(endPos.Y - startPos.Y), startPos, endPos, "Rechteck");
                geo_formen.Add(rectangle);
                listBoxObjects.Items.Add(rectangle.Name + names.ToString());
                names++;

                AddToImage();
            }
        }
    }
}
