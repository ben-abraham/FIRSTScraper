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

        public Event SelectedEvent;

        private Task eventLoader;
      
        public EventSelector()
        {
            InitializeComponent();
            
            selector = new EventLoader();
            EventYear.Text = ""+DateTime.Now.Year;
            IsVisibleChanged += (sender, ev) =>
            {
                if (Visibility == Visibility.Visible)
                    BeginRequestEvents(DateTime.Now.Year);
            };
        }

        private void BeginRequestEvents(int year)
        {
            if (eventLoader == null || eventLoader.Status != TaskStatus.Running)
            {
                eventLoader = new Task(() => selector.GetEvents(year));
                eventLoader.ContinueWith(UpdateEventBox, TaskScheduler.FromCurrentSynchronizationContext());
                eventLoader.Start();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs ev)
        {
            int year;
            if (int.TryParse(EventYear.Text, out year))
            {
                Mouse.SetCursor(Cursors.ArrowCD);
        
            }
        }

        private void UpdateEventBox(Task page)
        {
            EventsBox.Items.Clear();
            foreach (Event e in selector.events)
            {
                EventsBox.Items.Add(e);
            }
            EventsBox.SelectedIndex = 0;
            Mouse.SetCursor(Cursors.ArrowCD);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (EventsBox.SelectedItem != null)
            {
                SelectedEvent = EventsBox.SelectedItem as Event;
                
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please Select an Event");
            }
        }
    }
}

