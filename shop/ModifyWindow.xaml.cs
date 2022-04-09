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

namespace shop
{
    /// <summary>
    /// Interaction logic for ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        Termek kivalasztott = null;

        public ModifyWindow(Termek t)
        {
            InitializeComponent();

            kivalasztott = t;

            vonalkod.Text = kivalasztott.Vonalkod;
            nev.Text = kivalasztott.Megnevezes;
            ar.Text = kivalasztott.Egysegar.ToString();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool helyesVonalkod = true;
                foreach (var item in vonalkod.Text)
                {
                    try
                    {
                        int a = int.Parse(item.ToString());
                    }
                    catch (Exception)
                    {
                        helyesVonalkod = false;
                    }
                }

                if (helyesVonalkod && vonalkod.Text.Length == 13)
                {
                    Termek modositottTermek = new Termek(vonalkod.Text, nev.Text, kivalasztott.Raktarkeszlet, double.Parse(ar.Text));

                    ((MainWindow)Application.Current.MainWindow).termeklista[((MainWindow)Application.Current.MainWindow).tablazat.SelectedIndex] = modositottTermek;
                    ((MainWindow)Application.Current.MainWindow).tablazat.Items.Refresh();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hibás vonalkód!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hibás adat!");
            }
        }
    }
}
