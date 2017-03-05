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
        System.Windows.Point pos1;
        System.Windows.Point pos2;

        System.Windows.Point posA;
        System.Windows.Point posB;
        System.Windows.Point posC;
        System.Windows.Point posD;


        internal List<Geometrische_Form> geo_formen = new List<Geometrische_Form>();

        internal System.Drawing.Color color;

        SolidColorBrush prev_brush;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Initialisierung
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            color = System.Drawing.Color.FromArgb(200, 80, 10);
        }

        //          METHODEN

        private void AddToImage()
        {
            //Fügt die Bitmap zur ImageBox hinzu

            using (MemoryStream memory = new MemoryStream())
            {
                bmSurface.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = memory;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                drawingSurface.Source = bi;
            }
        }

        private void AddToList(System.Windows.Point position1, System.Windows.Point position2)
        {
            if(radioButtonRectangle.IsChecked == true)
            {
                Rectangle rectangle = new Rectangle(posA, posB, posC, posD, "Rechteck");
                geo_formen.Add(rectangle);
                listBoxObjects.Items.Add(rectangle.getName());           
            }
           
        }

        private void DrawRectangle(System.Windows.Point position1, System.Windows.Point position2)
        {
            //Zeichnet das Rechteck

            // 1. Von links nach rechts hinunter zeichnen
            if(position1.X < position2.X && position1.Y  < position2.Y)
            {
                for (int x = Convert.ToInt16(position1.X); x < position2.X; x++)
                {
                    for (int y = Convert.ToInt16(position1.Y); y < position2.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, color);
                    }
                }

                //Positionen für das Objekt berechnen
                posA = new System.Windows.Point(pos1.X, pos2.Y);
                posB = pos2;
                posC = new System.Windows.Point(pos2.X, pos1.Y);
                posD = pos1;
            }

            //2. Von links nach rechts hinauf zeichnen
            if (position1.X < position2.X && position1.Y > position2.Y)
            {
                for (int x = Convert.ToInt16(position1.X); x < position2.X; x++)
                {
                    for (int y = Convert.ToInt16(position2.Y); y < position1.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, color);
                    }
                }

                //Positionen für das Objekt berechnen
                posA = pos1;
                posB = new System.Windows.Point(pos2.X, pos1.Y);
                posC = pos2;
                posD = new System.Windows.Point(pos1.X, pos2.Y);
            }

            //3. Von rechts nach links hinauf zeichnen
            if (position1.X > position2.X && position1.Y > position2.Y)
            {
                for (int x = Convert.ToInt16(position2.X); x < position1.X; x++)
                {
                    for (int y = Convert.ToInt16(position2.Y); y < position1.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, color);
                    }
                }

                //Positionen für das Objekt berechnen
                posA = new System.Windows.Point(pos2.X, pos1.Y);
                posB = pos1;
                posC = new System.Windows.Point(pos1.X, pos2.Y);
                posD = pos2;
            }

            //4. Von rechts nach links hinunter zeichnen
            if (position1.X > position2.X && position1.Y < position2.Y)
            {
                for (int x = Convert.ToInt16(position2.X); x < position1.X; x++)
                {
                    for (int y = Convert.ToInt16(position1.Y); y < position2.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, color);
                    }
                }

                //Positionen für das Objekt berechnen
                posA = pos2;
                posB = new System.Windows.Point(pos1.X, pos2.Y);
                posC = pos1;
                posD = new System.Windows.Point(pos1.X, pos2.Y);
            }

        }

        //          EVENTS

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Neues Bild

            NewImage image = new NewImage();
            image.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //Bild öffnen
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //Bild speichern

            sfd = new SaveFileDialog();
            sfd.Filter = "Windows Bitmap | *.bmp";

            if (sfd.ShowDialog() == true)
            {
                bmSurface.Save(sfd.FileName);
            }

            MessageBox.Show("Gespeichert in: " + sfd.FileName.ToString());
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //Programm beenden

            Application.Current.Shutdown();
        }

        private void buttonChangeColor_Click(object sender, RoutedEventArgs e)
        {
            // Öffnet den Color Dialog

            ColorDialog cd = new ColorDialog();
            cd.Show();


            prev_brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Convert.ToByte(color.R), Convert.ToByte(color.G), Convert.ToByte(color.B)));
            rect_prev.Fill = prev_brush;
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

            pos1 = Mouse.GetPosition(drawingSurface);
        }

        private void drawingSurface_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
            //Bool mdown wieder auf false setzen

            mdown = false;

            //endPos auf die Mausposition setzen + Rechteck zeichnen + zu Liste und Listbox hinzufügen

            pos2 = Mouse.GetPosition(drawingSurface);

            //Rechteck
            if (radioButtonRectangle.IsChecked == true)
            {
                DrawRectangle(pos1, pos2);
                AddToImage();
                AddToList(pos1, pos2);
            }

        }

        private void listBoxObjects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Eigenschaften des Objekts in InfoBox ausgeben

            if(listBoxObjects.SelectedItem != null)
            {
                InfoBox.Text = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].ausgabe();
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            // RenameWindow aufrufen

            RenameWindow renameWin = new RenameWindow();
            renameWin.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            for (int x = Convert.ToInt16(geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD().X); x < geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getWidth(); x++)
            {
                for (int y = Convert.ToInt16(geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD().Y); y < geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getHeight(); y++)
                {
                    bmSurface.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                }

            }

            AddToImage();

            geo_formen.RemoveAt(Convert.ToInt16(listBoxObjects.SelectedIndex));
            listBoxObjects.Items.RemoveAt(Convert.ToInt16(listBoxObjects.SelectedIndex));
        }
    }
}