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
using System.Threading;
using System.Windows.Threading;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Lotto
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       

        DispatcherTimer t;

        double preis = 1.20;
        int sekunden;

        int vzahl1;
        int vzahl2;
        int vzahl3;
        int vzahl4;
        int vzahl5;
        int vzahl6;
        int anzahlversuche = 10000000;

        int[] meinezahlen;


        Random rnd = new Random();
        int[] lotto = new int[6];

        public void zahlenerstellen()
        {



            for (int i3 = 0; i3 <= anzahlversuche; i3++)
            {
                if (i3 == anzahlversuche)
                {
                    Dispatcher.Invoke(new Action(() => lbl.Content = ""));
                    Dispatcher.Invoke(new Action(() => t.Stop()));
                    MessageBox.Show("Anazhl der maximalen Versuche erreicht." + " " + anzahlversuche.ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR")));
                    Dispatcher.Invoke(new Action(() => btn_spielen.IsEnabled = true));
                    break;
                }


                for (int i = 0; i < 6; i++)
                {
                    int zufallszahl = rnd.Next(1, 47);
                    while (true)
                    {

                        if (lotto[0] == zufallszahl | lotto[1] == zufallszahl | lotto[2] == zufallszahl | lotto[3] == zufallszahl | lotto[4] == zufallszahl | lotto[5] == zufallszahl)
                        {
                            zufallszahl = rnd.Next(1, 47);
                        }
                        else
                        {
                            break;
                        }
                    }
                    lotto[i] = zufallszahl;

                }

                Array.Sort(lotto);
                Array.Sort(meinezahlen);

                if (meinezahlen.SequenceEqual(lotto) == true)
                {
                    Dispatcher.Invoke(new Action(() => lbl.Content = ""));
                    Dispatcher.Invoke(new Action(() => t.Stop()));
                    MessageBox.Show("Gewonnen!\n" + "Versuch Nr.: " + i3.ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR")) + "\n" + "Kosten: " + (i3 * preis).ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR")) + " €");
                    Dispatcher.Invoke(new Action(() => btn_spielen.IsEnabled = true));

                    break;
                }


            }

        }


        private  void Uhrzeit(object sender, EventArgs e)
        {
            sekunden++;
            lbl.Content = sekunden.ToString() + " Sekunden";
        }


        private void btn_spielen_Click(object sender, RoutedEventArgs e)
        {
         
           
                btn_spielen.IsEnabled = false;
                sekunden = 0;
                lbl.Content = "0 Sekunden";
                vzahl1 = int.Parse(zahl1.Text);
                vzahl2 = int.Parse(zahl2.Text);
                vzahl3 = int.Parse(zahl3.Text);
                vzahl4 = int.Parse(zahl4.Text);
                vzahl5 = int.Parse(zahl5.Text);
                vzahl6 = int.Parse(zahl6.Text);

                meinezahlen = new int[] { vzahl1, vzahl2, vzahl3, vzahl4, vzahl5, vzahl6 };
                Thread newThread = new Thread(zahlenerstellen);
                newThread.Start();

                t = new DispatcherTimer();
                t.Stop();
                t.Interval = TimeSpan.FromSeconds(1);
                t.Tick += Uhrzeit;
                t.Start();
            }
  

        private void zahl_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox Lottotextbox = (TextBox)sender;
            
            bool n = int.TryParse(Lottotextbox.Text, out int zahl);
            if(!n |  zahl < 1 | zahl > 46)
            {
                Lottotextbox.Text = "";
            }

            if(zahl1.Text != "" & zahl2.Text != "" & zahl3.Text != "" & zahl4.Text != "" & zahl5.Text != "" & zahl6.Text != "")
            {
                btn_spielen.IsEnabled = true;
            }
            else
            {
                btn_spielen.IsEnabled = false;
            }

         

        }
    }
}
