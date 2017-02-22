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
    /// Interaktionslogik für RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        public RenameWindow()
        {
            InitializeComponent();
        }

        private void buttonRename_Click(object sender, RoutedEventArgs e)
        {
            // Objekt in der Liste umbenennen

            ((MainWindow)System.Windows.Application.Current.MainWindow).geo_formen[Convert.ToInt16(
                ((MainWindow)System.Windows.Application.Current.MainWindow).listBoxObjects.SelectedIndex)].changeName(textBoxNewName.Text);

            // ListBox aktualisieren

            ((MainWindow)System.Windows.Application.Current.MainWindow).listBoxObjects.Items[((MainWindow)System.Windows.Application.Current.MainWindow).listBoxObjects.SelectedIndex] =
                ((MainWindow)System.Windows.Application.Current.MainWindow).geo_formen[Convert.ToInt16(
                    ((MainWindow)System.Windows.Application.Current.MainWindow).listBoxObjects.SelectedIndex)].getName();

            this.Close();
        }
    }
}
