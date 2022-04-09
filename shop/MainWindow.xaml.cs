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
using System.IO;
using Microsoft.Win32;

namespace shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Termek> termeklista = new List<Termek>();
        public List<vasaroltTermek> vasaroltTermekek = new List<vasaroltTermek>();
        public List<bevetelezettTermek> bevetelezettTermekek = new List<bevetelezettTermek>();
        public double osszeg = 0;

        public MainWindow()
        {
            InitializeComponent();

            tablazat.ItemsSource = termeklista;
            vasaroltTermekTablazat.ItemsSource = vasaroltTermekek;
            bevetelezettTermekTablazat.ItemsSource = bevetelezettTermekek;
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            if (tablazat.Items.Count == 0)
            {
                OpenFileDialog fajl = new OpenFileDialog();
                fajl.ShowDialog();

                try
                {
                    foreach (var item in File.ReadAllLines(fajl.FileName).Skip(1))
                    {
                        termeklista.Add(new Termek(item));
                    }

                    tablazat.Items.Refresh();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                MessageBox.Show("Az importálás már megtörtént!");
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fajl = new SaveFileDialog();
            fajl.ShowDialog();

            try
            {
                StreamWriter sw = new StreamWriter(fajl.FileName);

                sw.WriteLine("Vonalkód; Megnevezés; Raktárkészlet; Egységár");

                foreach (var item in termeklista)
                {
                    sw.WriteLine($"{item.Vonalkod};{item.Megnevezes};{item.Raktarkeszlet};{item.Egysegar.ToString().Replace(",", ".")}");
                }

                sw.Close();
            }
            catch (Exception)
            {

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow a = new AddWindow();
            a.ShowDialog();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (tablazat.SelectedIndex != -1)
            {
                ModifyWindow m = new ModifyWindow(termeklista[tablazat.SelectedIndex]);
                m.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nincs kiválasztott elem!");
            }
        }

        private void Hozzaadas_Click(object sender, RoutedEventArgs e)
        {
            if (termeklista.Select(x => x.Vonalkod).Contains(eVonalkod.Text))
            {
                Termek t = termeklista.Where(x => x.Vonalkod == eVonalkod.Text).First();

                try
                {
                    if (t.Raktarkeszlet >= int.Parse(eMennyiseg.Text))
                    {
                        vasaroltTermek vt = new vasaroltTermek(t, int.Parse(eMennyiseg.Text));
                        var a = vasaroltTermekek.Where(x => x.Vonalkod == vt.Vonalkod);

                        if (a.Count() > 0)
                        {
                            var b = a.First();

                            b.Mennyiseg += int.Parse(eMennyiseg.Text);
                            b.Ar = b.Mennyiseg * (termeklista.Where(x => x.Vonalkod == b.Vonalkod).First().Egysegar);
                        }
                        else
                        {
                            vasaroltTermekek.Add(vt);
                        }

                        vasaroltTermekTablazat.Items.Refresh();

                        osszeg = vasaroltTermekek.Sum(x => x.Ar);
                        vegosszeg.Text = "" + osszeg;

                        eVonalkod.Text = "";
                        eMennyiseg.Text = "1";
                    }
                    else
                    {
                        MessageBox.Show("Jelenleg nincs elég ebből a termékből raktáron");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nem érvényes mennyiség!");
                }
                
            }
            else
            {
                MessageBox.Show("Hibás vonalkód!");
            }
        }

        private void eVonalkod_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (termeklista.Select(x => x.Vonalkod).Contains(eVonalkod.Text))
            {
                Termek t = termeklista.Where(x => x.Vonalkod == eVonalkod.Text).First();

                eNev.Text = t.Megnevezes;
                eAr.Text = "" + t.Egysegar;
            }
            else
            {
                eNev.Text = "";
                eAr.Text = "";
            }
        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            if (vasaroltTermekTablazat.SelectedIndex != -1)
            {
                vasaroltTermekek.RemoveAt(vasaroltTermekTablazat.SelectedIndex);
                vasaroltTermekTablazat.Items.Refresh();

                osszeg = vasaroltTermekek.Sum(x => x.Ar);
                vegosszeg.Text = "" + osszeg;
            }
            else
            {
                MessageBox.Show("Nincs kijelölt termék!");
            }
        }

        private void UjVevo_Click(object sender, RoutedEventArgs e)
        {
            vasaroltTermekek.Clear();
            vasaroltTermekTablazat.Items.Refresh();

            osszeg = 0;
            vegosszeg.Text = "" + osszeg;
        }

        private void Fizetes_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in vasaroltTermekek)
            {
                termeklista.Where(x => x.Vonalkod == item.Vonalkod).First().Raktarkeszlet -= item.Mennyiseg;
            }

            tablazat.Items.Refresh();

            vasaroltTermekek.Clear();
            vasaroltTermekTablazat.Items.Refresh();

            osszeg = 0;
            vegosszeg.Text = "" + osszeg;
        }

        private void BVonalkod_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (termeklista.Select(x => x.Vonalkod).Contains(bVonalkod.Text))
            {
                Termek t = termeklista.Where(x => x.Vonalkod == bVonalkod.Text).First();

                bNev.Text = t.Megnevezes;
                bAr.Text = "" + t.Egysegar;
            }
            else
            {
                bNev.Text = "";
                bAr.Text = "";
            }
        }

        private void BHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            if (termeklista.Select(x => x.Vonalkod).Contains(bVonalkod.Text))
            {
                Termek t = termeklista.Where(x => x.Vonalkod == bVonalkod.Text).First();

                try
                {
                    bevetelezettTermek bt = new bevetelezettTermek(t, int.Parse(bMennyiseg.Text));
                    var a = bevetelezettTermekek.Where(x => x.Vonalkod == bt.Vonalkod);

                    if (a.Count() > 0)
                    {
                        var b = a.First();

                        b.Mennyiseg += int.Parse(bMennyiseg.Text);
                    }
                    else
                    {
                        bevetelezettTermekek.Add(bt);
                    }

                    bevetelezettTermekTablazat.Items.Refresh();

                    bVonalkod.Text = "";
                    bMennyiseg.Text = "1";
                }
                catch (Exception)
                {
                    MessageBox.Show("Nem érvényes mennyiség!");
                }
                
            }
            else
            {
                MessageBox.Show("Hibás vonalkód!");
            }
        }

        private void UjBevetelezes_Click(object sender, RoutedEventArgs e)
        {
            bevetelezettTermekek.Clear();
            bevetelezettTermekTablazat.Items.Refresh();
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in bevetelezettTermekek)
            {
                termeklista.Where(x => x.Vonalkod == item.Vonalkod).First().Raktarkeszlet += item.Mennyiseg;
            }

            tablazat.Items.Refresh();

            bevetelezettTermekek.Clear();
            bevetelezettTermekTablazat.Items.Refresh();
        }

        private void Btorles_Click(object sender, RoutedEventArgs e)
        {
            if (bevetelezettTermekTablazat.SelectedIndex != -1)
            {
                bevetelezettTermekek.RemoveAt(bevetelezettTermekTablazat.SelectedIndex);
                bevetelezettTermekTablazat.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Nincs kijelölt termék!");
            }
        }

        private void Bmodositas_Click(object sender, RoutedEventArgs e)
        {
            if (bevetelezettTermekTablazat.SelectedIndex != -1)
            {

            }
            else
            {
                MessageBox.Show("Nincs kiválasztott elem!");
            }
        }
    }
}
