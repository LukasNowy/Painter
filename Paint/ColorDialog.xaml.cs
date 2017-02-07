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

        public ColorDialog()
        {
            InitializeComponent();
        }

        private void buttonAddColor_Click(object sender, RoutedEventArgs e)
        {
            //Farbe bestätigen
            ((MainWindow)System.Windows.Application.Current.MainWindow).color = new System.Drawing.Color();

            ((MainWindow)System.Windows.Application.Current.MainWindow).color = System.Drawing.Color.FromArgb(Convert.ToByte(textBoxRot.Text), Convert.ToByte(textBoxGruen.Text), Convert.ToByte(textBoxBlau.Text));

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
    }
}
