using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Media.Animation;
using HtmlAgilityPack;

namespace FIRST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Test : INotifyPropertyChanged
    {
        string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class MainWindow : Window
    {

        public static readonly DependencyProperty scale_prop = DependencyProperty.Register("Scale", typeof(double), typeof(MainWindow));
        public static readonly DependencyProperty fade_prop = DependencyProperty.Register("Fade", typeof(double), typeof(MainWindow));

        public double Scale { get { return (double)GetValue(scale_prop); } set { SetValue(scale_prop, value); } }
        public double Fade { get { return (double)GetValue(fade_prop); } set { SetValue(fade_prop, value); } }

        public MainWindow()
        {
            InitializeComponent();

            Tester.Items.Add(new Test { Name = "BEN1" });
            Tester.Items.Add(new Test { Name = "BEN2" });
            Tester.Items.Add(new Test { Name = "BEN3" });
            Tester.Items.Add(new Test { Name = "BEN4" });


            DispatcherTimer t = new DispatcherTimer() { Interval = new TimeSpan(0,0,0,2) };
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            //EventPage p = new EventPage("http://www2.usfirst.org/2011comp/events/WOR/matchresults.html");
        }

        void t_Tick(object sender, EventArgs e)
        {
            var t = FindResource("ItemAnimation");
            (t as Storyboard).Begin();
            
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            new EventLoader(2010);
        }
    }
}
