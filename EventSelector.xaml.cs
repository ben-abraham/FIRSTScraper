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
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace FIRST
{
    /// <summary>
    /// Interaction logic for EventSelector.xaml
    /// </summary>
    public partial class EventSelector : Window
    {
        EventLoader selector;

        public Event selectedEvent;

        public EventSelector()
        {
            InitializeComponent();
            selector = new EventLoader();
            EventYear.Text = ""+DateTime.Now.Year;
        }

        private void Button_Click(object sender, RoutedEventArgs ev)
        {
            int year;
            if (int.TryParse(EventYear.Text, out year))
            {
                Task t = new Task(() => 
                {
                    selector.GetEvents(year);

                    EventsBox.Dispatcher.Invoke((Action)(() =>
                        {
                            EventsBox.Items.Clear();
                            foreach (Event e in selector.events)
                            {
                                EventsBox.Items.Add(e);
                            }
                            EventsBox.SelectedIndex = 0;
                        }));
                    Mouse.SetCursor(Cursors.ArrowCD);
                });
                t.Start();
                Mouse.SetCursor(Cursors.AppStarting);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (EventsBox.SelectedItem != null)
            {
                selectedEvent = EventsBox.SelectedItem as Event;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please Select an Event");
            }
        }
    }
}

