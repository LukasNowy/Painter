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
        OpenFileDialog ofd;

        internal Bitmap bmSurface;
        internal BitmapImage bi;

        bool mdown = false;
        System.Windows.Point pos1;
        System.Windows.Point pos2;
        PointF[] polyPoints = new PointF[3];

        System.Windows.Point posA;
        System.Windows.Point posB;
        System.Windows.Point posC;
        System.Windows.Point posD;

        int polygonPos = 0;
        

        internal List<Geometrische_Form> geo_formen = new List<Geometrische_Form>();

        internal System.Drawing.Color color;
        System.Drawing.Color eraserColor;

        SolidColorBrush prev_brush;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Initialisierung
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            color = System.Drawing.Color.FromArgb(200, 80, 10);
            eraserColor = System.Drawing.Color.FromArgb(255, 255, 255);
            comboBoxBrushSize.SelectedIndex = 2;
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
            // Form zur Liste und ListBox hinzufügen

            //Rechteck
            if(radioButtonRectangle.IsChecked == true)
            {
                Rectangle rectangle = new Rectangle(posA, posB, posC, posD, "Rechteck", this.color, "Rectangle");
                geo_formen.Add(rectangle);
                listBoxObjects.Items.Add(rectangle.getName());           
            }

            //Ellipse
            if (radioButtonEllipse.IsChecked == true)
            {
                Ellipse ellipse = new Ellipse(posA, posB, posC, posD, "Ellipse", this.color, "Ellipse");
                geo_formen.Add(ellipse);
                listBoxObjects.Items.Add(ellipse.getName());
            }

            //Linie
            if (radioButtonLine.IsChecked == true)
            {
                Line line = new Line(posA, posB, "Linie", this.color, "Line", Convert.ToInt16(textBoxLineSize.Text));
                geo_formen.Add(line);
                listBoxObjects.Items.Add(line.getName());
            }

            //Polygon
            if (radioButtonPolygon.IsChecked == true)
            {
                Triangle triangle = new Triangle(polyPoints[0], polyPoints[1], polyPoints[2], "Dreieck", this.color, "Triangle");
                geo_formen.Add(triangle);
                listBoxObjects.Items.Add(triangle.getName());
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

        private void DrawElipse(System.Windows.Point position1, System.Windows.Point position2)
        {

            //Zeichnet die Ellipse

            Graphics elippse = Graphics.FromImage(bmSurface);
            System.Drawing.Brush br = new System.Drawing.SolidBrush(color);

            // 1. Von links nach rechts hinunter zeichnen
            if (position1.X < position2.X && position1.Y < position2.Y)
            {
                elippse.FillEllipse(br, Convert.ToInt16(position1.X), Convert.ToInt16(position1.Y), 
                    Convert.ToInt16(position2.X) - Convert.ToInt16(position1.X), Convert.ToInt16(position2.Y) - Convert.ToInt16(position1.Y));

                //Positionen für das Objekt berechnen
                posA = new System.Windows.Point(pos1.X, pos2.Y);
                posB = pos2;
                posC = new System.Windows.Point(pos2.X, pos1.Y);
                posD = pos1;
            }

            //2. Von links nach rechts hinauf zeichnen
            if (position1.X < position2.X && position1.Y > position2.Y)
            {
                elippse.FillEllipse(br, Convert.ToInt16(position1.X), Convert.ToInt16(position2.Y),
                    Convert.ToInt16(position2.X) - Convert.ToInt16(position1.X), Convert.ToInt16(position1.Y) - Convert.ToInt16(position2.Y));

                //Positionen für das Objekt berechnen
                posA = pos1;
                posB = new System.Windows.Point(pos2.X, pos1.Y);
                posC = pos2;
                posD = new System.Windows.Point(pos1.X, pos2.Y);
            }

            //3. Von rechts nach links hinauf zeichnen
            if (position1.X > position2.X && position1.Y > position2.Y)
            {
                elippse.FillEllipse(br, Convert.ToInt16(position2.X), Convert.ToInt16(position1.Y),
                    Convert.ToInt16(position1.X) - Convert.ToInt16(position2.X), Convert.ToInt16(position2.Y) - Convert.ToInt16(position1.Y));

                //Positionen für das Objekt berechnen
                posA = new System.Windows.Point(pos2.X, pos1.Y);
                posB = pos1;
                posC = new System.Windows.Point(pos1.X, pos2.Y);
                posD = pos2;
            }

            //4. Von rechts nach links hinunter zeichnen
            if (position1.X > position2.X && position1.Y < position2.Y)
            {
                elippse.FillEllipse(br, Convert.ToInt16(position2.X), Convert.ToInt16(position1.Y),
                    Convert.ToInt16(position1.X) - Convert.ToInt16(position2.X), Convert.ToInt16(position2.Y) - Convert.ToInt16(position1.Y));

                //Positionen für das Objekt berechnen
                posA = pos2;
                posB = new System.Windows.Point(pos1.X, pos2.Y);
                posC = pos1;
                posD = new System.Windows.Point(pos1.X, pos2.Y);
            }

        }

        //Radiergummi
        private void EraseSelection(System.Windows.Point p1, System.Windows.Point p2)
        {
            // 1. Von links nach rechts hinunter zeichnen
            if (p1.X < p2.X && p1.Y < p2.Y)
            {
                for (int x = Convert.ToInt16(p1.X); x < p2.X; x++)
                {
                    for (int y = Convert.ToInt16(p1.Y); y < p2.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, eraserColor);
                    }
                }
            }

            //2. Von links nach rechts hinauf zeichnen
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                for (int x = Convert.ToInt16(p1.X); x < p2.X; x++)
                {
                    for (int y = Convert.ToInt16(p2.Y); y < p1.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, eraserColor);
                    }
                }

            }

            //3. Von rechts nach links hinauf zeichnen
            if (p1.X > p2.X && p1.Y > p2.Y)
            {
                for (int x = Convert.ToInt16(p2.X); x < p1.X; x++)
                {
                    for (int y = Convert.ToInt16(p2.Y); y < p1.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, eraserColor);
                    }
                }
            }

            //4. Von rechts nach links hinunter zeichnen
            if (p1.X > p2.X && p1.Y < p2.Y)
            {
                for (int x = Convert.ToInt16(p2.X); x < p1.X; x++)
                {
                    for (int y = Convert.ToInt16(p1.Y); y < p2.Y; y++)
                    {
                        bmSurface.SetPixel(x, y, eraserColor);
                    }
                }
            }
        }

        //Linie zeichnen
        public void DrawLine(System.Windows.Point pos1, System.Windows.Point pos2)
        {
            Graphics line= Graphics.FromImage(bmSurface);
            System.Drawing.Pen pen = new System.Drawing.Pen(color);
            pen.Width = Convert.ToInt16(textBoxLineSize.Text);

            //line.DrawLine(pen, Convert.ToSingle(pos1.X), Convert.ToSingle(pos1.Y), Convert.ToSingle(pos2.X), Convert.ToSingle(pos2.Y));

            //1. Von links nach rechts hinunter zeichnen
            if (pos1.X < pos2.X && pos1.Y < pos2.Y)
            {
                line.DrawLine(pen, Convert.ToSingle(pos1.X), Convert.ToSingle(pos1.Y), Convert.ToSingle(pos2.X), Convert.ToSingle(pos2.Y));

                //Positionen für das Objekt berechnen
                posA = pos1;
                posB = pos2;
            }

            //2. Von links nach rechts hinauf zeichnen
            if (pos1.X < pos2.X && pos1.Y > pos2.Y)
            {
                line.DrawLine(pen, Convert.ToSingle(pos1.X), Convert.ToSingle(pos1.Y), Convert.ToSingle(pos2.X), Convert.ToSingle(pos2.Y));

                //Positionen für das Objekt berechnen
                posA = pos1;
                posB = pos2;
            }

            //3. Von rechts nach links hinauf zeichnen
            if (pos1.X > pos2.X && pos1.Y > pos2.Y)
                {
                line.DrawLine(pen, Convert.ToSingle(pos1.X), Convert.ToSingle(pos1.Y), Convert.ToSingle(pos2.X), Convert.ToSingle(pos2.Y));

                //Positionen für das Objekt berechnen
                posA = pos2;
                posB = pos1;
            }

            //4. Von rechts nach links hinunter zeichnen
            if (pos1.X > pos2.X && pos1.Y < pos2.Y)
            {
                line.DrawLine(pen, Convert.ToSingle(pos1.X), Convert.ToSingle(pos1.Y), Convert.ToSingle(pos2.X), Convert.ToSingle(pos2.Y));

                //Positionen für das Objekt berechnen
                posA = pos2;
                posB = pos1;
            }

        }

        //Polygon zeichnen
        public void DrawPolygon(PointF[] points)
        {
            Graphics polygon = Graphics.FromImage(bmSurface);
            System.Drawing.Brush br = new System.Drawing.SolidBrush(color);
            polygon.FillPolygon(br, points);
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
            ofd = new OpenFileDialog();
            ofd.ShowDialog();

            bmSurface = new Bitmap(ofd.FileName);

            drawingSurface.Width = bmSurface.Width;
            drawingSurface.Height = bmSurface.Height;

            AddToImage();
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

            // Brush

            if (radioButtonBrush.IsChecked == true)
            {
                if (mdown == true)
                {
                    //Pinselgröße 1 px
                    if (comboBoxBrushSize.SelectedIndex == 0)
                    {
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y), color);
                    }

                    //Pinselgröße 3 px
                    if (comboBoxBrushSize.SelectedIndex == 1)
                    {
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) - 1, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) + 1, color);
                    }

                    //Pinselgröße 5 px
                    if (comboBoxBrushSize.SelectedIndex == 2)
                    {
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) - 1, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) + 1, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 2, Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 2, Convert.ToInt16(mousePos.Y), color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) - 2, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) + 2, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y) + 1, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y) - 1, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y) - 1, color);
                        bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y) + 1, color);
                    }

                    AddToImage();
                }
            }
            //Radiergummi

            if (radioButtonEraser.IsChecked == true)
            {

                if (mdown == true)
                {
                    {
                        //Pinselgröße 1 px
                        if (comboBoxBrushSize.SelectedIndex == 0)
                        {
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y), color);
                        }

                        //Pinselgröße 3 px
                        if (comboBoxBrushSize.SelectedIndex == 1)
                        {
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) - 1, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) + 1, eraserColor);
                        }

                        //Pinselgröße 5 px
                        if (comboBoxBrushSize.SelectedIndex == 2)
                        {
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) - 1, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) + 1, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 2, Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 2, Convert.ToInt16(mousePos.Y), eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) - 2, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X), Convert.ToInt16(mousePos.Y) + 2, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y) + 1, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) - 1, Convert.ToInt16(mousePos.Y) - 1, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y) - 1, eraserColor);
                            bmSurface.SetPixel(Convert.ToInt16(mousePos.X) + 1, Convert.ToInt16(mousePos.Y) + 1, eraserColor);
                        }
                    }

                    AddToImage();
                }

            }

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

            //endPos auf die Mausposition setzen + Formen zeichnen + zu Image und Liste und Listbox hinzufügen

            pos2 = Mouse.GetPosition(drawingSurface);

            //Rechteck
            if (radioButtonRectangle.IsChecked == true)
            {
                DrawRectangle(pos1, pos2);
                AddToImage();
                AddToList(pos1, pos2);
            }

            //Ellipse
            if(radioButtonEllipse.IsChecked == true)
            {
                DrawElipse(pos1, pos2);
                AddToImage();
                AddToList(pos1, pos2);
            }

            //Polygon
            if(radioButtonPolygon.IsChecked == true)
            {
                // Wenn Shift gedrückt ist werden dem Array polyPoints Punkte hinzugefügt
                if(Keyboard.IsKeyDown(Key.LeftShift))
                {
                    polyPoints[polygonPos] = new PointF(Convert.ToSingle(pos2.X), Convert.ToSingle(pos2.Y));
                    polygonPos++;
                }
            }

            //Linie
            if(radioButtonLine.IsChecked == true)
            {
                DrawLine(pos1, pos2);
                AddToList(pos1, pos2);
                AddToImage();
            }

            // Selection Eraser
            if(radioButtonSelectionEraser.IsChecked == true)
            {
                EraseSelection(pos1, pos2);
                AddToImage();
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
            // Einzelnes Objekt löschen
            System.Windows.Point position1 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD();
            System.Windows.Point position2 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB();

            System.Windows.Point lineposition1 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getA();
            System.Windows.Point lineposition2 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB();

            //Rechteck löschen
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Rectangle")
            {
                for (int x = Convert.ToInt16(geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD().X); x < geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB().X; x++)
                {
                    for (int y = Convert.ToInt16(geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD().Y); y < geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB().Y; y++)
                    {
                        bmSurface.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                    }

                }
            }

            //Ellipse löschen
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Ellipse")
            {
                Graphics elippse = Graphics.FromImage(bmSurface);
                System.Drawing.Brush br = System.Drawing.Brushes.White;

                

                elippse.FillEllipse(br, Convert.ToInt16(position1.X), Convert.ToInt16(position1.Y),
                Convert.ToInt16(position2.X) - Convert.ToInt16(position1.X), Convert.ToInt16(position2.Y) - Convert.ToInt16(position1.Y));
            }

            //Linie löschen
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Line")
            {
                Graphics line = Graphics.FromImage(bmSurface);
                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.White);
                pen.Width = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getLineWidth();

                line.DrawLine(pen, Convert.ToSingle(lineposition1.X), Convert.ToSingle(lineposition1.Y), Convert.ToSingle(lineposition2.X), Convert.ToSingle(lineposition2.Y));
            }

            //Polygon löschen
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Triangle")
            {
                Graphics polygon = Graphics.FromImage(bmSurface);
                System.Drawing.Brush br = System.Drawing.Brushes.White;

                PointF[] points = new PointF[3];
                points[0] = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].PgetA();
                points[1] = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].PgetB();
                points[2] = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].PgetC();

                polygon.FillPolygon(br, points);
             
            }
           


            AddToImage();

            geo_formen.RemoveAt(Convert.ToInt16(listBoxObjects.SelectedIndex));
            listBoxObjects.Items.RemoveAt(Convert.ToInt16(listBoxObjects.SelectedIndex));
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            // Alles löschen

            for (int x = 0; x < bmSurface.Width; x++)
            {
                for (int y = 0; y < bmSurface.Height; y++)
                {
                    bmSurface.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                }
                
            }

            AddToImage();

            geo_formen.Clear();
            listBoxObjects.Items.Clear();

        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            // Farbe ändern

            //Rechteck
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Rectangle")
            {
                for (int x = Convert.ToInt16(geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD().X); x < geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB().X; x++)
                {
                    for (int y = Convert.ToInt16(geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD().Y); y < geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB().Y; y++)
                    {
                        bmSurface.SetPixel(x, y, this.color);
                    }
                }
            }

            //Ellipse
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Ellipse")
            {
                Graphics elippse = Graphics.FromImage(bmSurface);
                System.Drawing.Brush br = new System.Drawing.SolidBrush(color);

                System.Windows.Point position1 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getD();
                System.Windows.Point position2 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB();

                elippse.FillEllipse(br, Convert.ToInt16(position1.X), Convert.ToInt16(position1.Y),
                Convert.ToInt16(position2.X) - Convert.ToInt16(position1.X), Convert.ToInt16(position2.Y) - Convert.ToInt16(position1.Y));
            }

            //Linie 
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Line")
            {
                System.Windows.Point lineposition1 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getA();
                System.Windows.Point lineposition2 = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getB();

                Graphics line = Graphics.FromImage(bmSurface);
                System.Drawing.Pen pen = new System.Drawing.Pen(color);
                pen.Width = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getLineWidth();

                line.DrawLine(pen, Convert.ToSingle(lineposition1.X), Convert.ToSingle(lineposition1.Y), Convert.ToSingle(lineposition2.X), Convert.ToSingle(lineposition2.Y));
            }

            //Polygon löschen
            if (geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].getType() == "Triangle")
            {
                Graphics polygon = Graphics.FromImage(bmSurface);
                System.Drawing.Brush br = new System.Drawing.SolidBrush(color);

                PointF[] points = new PointF[3];
                points[0] = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].PgetA();
                points[1] = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].PgetB();
                points[2] = geo_formen[Convert.ToInt16(listBoxObjects.SelectedIndex)].PgetC();

                polygon.FillPolygon(br, points);

            }

            AddToImage();

            geo_formen[listBoxObjects.SelectedIndex].color = this.color;

            
        }

        //Wechselt in Vollbild Ansicht
        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        // Key Down Event
        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            // Polygon zeichnen, wenn man P drückt + polyPoints auf null setzen und polygonPos auf 0 setzen
            if(e.Key == Key.P)
            {
                DrawPolygon(polyPoints);
                AddToImage();
                AddToList(pos1, pos2);
  
                //polygonPos = 0;
            }
        }


    }
}