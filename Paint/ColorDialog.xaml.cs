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

namespace Paint
{
    /// <summary>
    /// Interaktionslogik für ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)255, (byte)255));
        SolidColorBrush prev_brush;

        Random randomColor;

        List<ColorRessource> listCR = new List<ColorRessource>();

        public ColorDialog()
        {
            InitializeComponent();
        }

        private void buttonAddColor_Click(object sender, RoutedEventArgs e)
        {
            //Farbe bestätigen
            ((MainWindow)System.Windows.Application.Current.MainWindow).color = new System.Drawing.Color();

            ((MainWindow)System.Windows.Application.Current.MainWindow).color = System.Drawing.Color.FromArgb(Convert.ToByte(textBoxRot.Text), Convert.ToByte(textBoxGruen.Text), Convert.ToByte(textBoxBlau.Text));
            prev_brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Convert.ToByte(textBoxRot.Text), Convert.ToByte(textBoxGruen.Text), Convert.ToByte(textBoxBlau.Text)));
            ((MainWindow)System.Windows.Application.Current.MainWindow).rect_prev.Fill = prev_brush;
            this.Close();

        }

        //Farben in Textbox aktualisieren

        private void sliderRot_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxRot.Text = Convert.ToByte(sliderRot.Value).ToString();
            brush = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(textBoxRot.Text), Convert.ToByte(textBoxGruen.Text), Convert.ToByte(textBoxBlau.Text)));
            grid.Background = brush;
        }

        private void sliderGruen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxGruen.Text = Convert.ToByte(sliderGruen.Value).ToString();
            brush = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(textBoxRot.Text), Convert.ToByte(textBoxGruen.Text), Convert.ToByte(textBoxBlau.Text)));
            grid.Background = brush;
        }

        private void sliderBlau_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxBlau.Text = Convert.ToByte(sliderBlau.Value).ToString();
            brush = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(textBoxRot.Text), Convert.ToByte(textBoxGruen.Text), Convert.ToByte(textBoxBlau.Text)));
            grid.Background = brush;
        }

        private void buttonRandom_Click(object sender, RoutedEventArgs e)
        {
            // Zufällige Farbe

            randomColor = new Random();

            textBoxRot.Text = Convert.ToByte(randomColor.Next(0, 255)).ToString();        
            textBoxGruen.Text = Convert.ToByte(randomColor.Next(0, 255)).ToString();      
            textBoxBlau.Text = Convert.ToByte(randomColor.Next(0, 255)).ToString();

            sliderRot.Value = Convert.ToInt16(textBoxRot.Text);
            sliderGruen.Value = Convert.ToInt16(textBoxGruen.Text);
            sliderBlau.Value = Convert.ToInt16(textBoxBlau.Text);

            grid.Background = brush;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            //ColorRessources erstellen

            ColorRessource cr0 = new ColorRessource("Darkred", 139, 0, 0);
            ColorRessource cr1 = new ColorRessource("Firebrick", 178, 34, 34);
            ColorRessource cr2 = new ColorRessource("Indianred", 205, 92, 92);
            ColorRessource cr3 = new ColorRessource("Salmon", 250, 128, 114);
            ColorRessource cr4 = new ColorRessource("Orangered", 255, 69, 0);
            ColorRessource cr5 = new ColorRessource("Sienna", 160, 82, 45);
            ColorRessource cr6 = new ColorRessource("Chocolate", 210, 105, 30);
            ColorRessource cr7 = new ColorRessource("Goldenrod", 218, 165, 32);
            ColorRessource cr8 = new ColorRessource("Darkgoldenrod", 184, 134, 11);
            ColorRessource cr9 = new ColorRessource("Goldenrod", 218, 165, 32);
            ColorRessource cr10 = new ColorRessource("Olive", 128, 128, 0);
            ColorRessource cr11 = new ColorRessource("Yellowgreen", 154, 205, 50);
            ColorRessource cr12 = new ColorRessource("Limegreen", 50, 205, 50);
            ColorRessource cr13 = new ColorRessource("Lightseagreen", 32, 187, 170);
            ColorRessource cr14 = new ColorRessource("Darkslategrey", 47, 79, 79);
            ColorRessource cr15 = new ColorRessource("Deepskyblue", 0, 191, 255);
            ColorRessource cr16 = new ColorRessource("Midnightblue", 25, 25, 112);
            ColorRessource cr17 = new ColorRessource("Deepskyblue", 0, 191, 255);
            ColorRessource cr18 = new ColorRessource("Navy", 0, 0, 128);
            ColorRessource cr19 = new ColorRessource("Mediumblue", 0, 0, 205);
            ColorRessource cr20 = new ColorRessource("Slateblue", 106, 90, 205);
            ColorRessource cr21 = new ColorRessource("Deepskyblue", 0, 191, 255);
            ColorRessource cr22 = new ColorRessource("Indigo", 75, 0, 130);
            ColorRessource cr23 = new ColorRessource("Darkviolet", 148, 0, 211);
            ColorRessource cr24 = new ColorRessource("Magenta", 255, 0, 255);
            ColorRessource cr25 = new ColorRessource("Crimson", 220, 20, 60);
            ColorRessource cr26 = new ColorRessource("Silver", 192, 192, 192);

            //Color Ressources zur Liste hinzufügen
    
            listCR.Add(cr0);
            listCR.Add(cr2);
            listCR.Add(cr3);
            listCR.Add(cr4);
            listCR.Add(cr5);
            listCR.Add(cr6);
            listCR.Add(cr7);
            listCR.Add(cr8);
            listCR.Add(cr9);
            listCR.Add(cr10);
            listCR.Add(cr11);
            listCR.Add(cr12);
            listCR.Add(cr13);
            listCR.Add(cr14);
            listCR.Add(cr15);
            listCR.Add(cr16);
            listCR.Add(cr17);
            listCR.Add(cr18);
            listCR.Add(cr19);
            listCR.Add(cr20);
            listCR.Add(cr21);
            listCR.Add(cr22);
            listCR.Add(cr23);
            listCR.Add(cr24);
            listCR.Add(cr25);
            listCR.Add(cr26);

            //Color Ressources in ListBox ausgeben
            for(int i = 0; i < 26; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Background = new SolidColorBrush(Color.FromArgb(255, listCR[i].getR(), listCR[i].getG(), listCR[i].getB()));
                cbi.Content = listCR[i].getName();
                comboBoxColorRessources.Items.Add(cbi);
            }

        }

        private void comboBoxColorRessources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBoxRot.Text = listCR[Convert.ToInt16(comboBoxColorRessources.SelectedIndex)].getR().ToString();
            textBoxGruen.Text = listCR[Convert.ToInt16(comboBoxColorRessources.SelectedIndex)].getG().ToString();
            textBoxBlau.Text = listCR[Convert.ToInt16(comboBoxColorRessources.SelectedIndex)].getB().ToString();

            sliderRot.Value = Convert.ToInt16(textBoxRot.Text);
            sliderGruen.Value = Convert.ToInt16(textBoxGruen.Text);
            sliderBlau.Value = Convert.ToInt16(textBoxBlau.Text);

            grid.Background = brush;
        }
    }
}
