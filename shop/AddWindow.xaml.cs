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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
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
                    if (((MainWindow)Application.Current.MainWindow).termeklista.Where(x => x.Vonalkod == vonalkod.Text).Count() == 0)
                    {
                        Termek ujTermek = new Termek(vonalkod.Text, nev.Text, 0, double.Parse(ar.Text));

                        ((MainWindow)Application.Current.MainWindow).termeklista.Add(ujTermek);
                        ((MainWindow)Application.Current.MainWindow).tablazat.Items.Refresh();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ilyen vonalkóddal már létezik termék!");
                    }
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
