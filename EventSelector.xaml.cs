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
            EventYear.Text = "" + DateTime.Now.Year;

            selector.DownloadProgressChanged += (sender, e) =>
            {
                DownloadProgress.Dispatcher.Invoke(new Action(() =>
                {
                    DownloadProgress.Value = (e.ProgressPercentage - 50) * 2;
                }));
            };

            IsVisibleChanged += (sender, ev) =>
            {
                if (Visibility == Visibility.Visible)
                    BeginRequestEvents(DateTime.Now.Year);
            };

            selector.EventsUpdated += (sender, e) =>
            {
                if (Visibility == Visibility.Visible)
                {
            
                    SetProgressVisibility(System.Windows.Visibility.Hidden);
                    EventsBox.Dispatcher.Invoke(new Action(() =>
                    {
                        UpdateEventBox();
                    }));
                }
            };
        }

        private void BeginRequestEvents(int year)
        {
            SetProgressVisibility(System.Windows.Visibility.Visible);
            selector.GetEvents(year);
        }

        private void Button_Click(object sender, RoutedEventArgs ev)
        {
            int year;
            if (int.TryParse(EventYear.Text, out year))
            {
                Mouse.SetCursor(Cursors.AppStarting);
                BeginRequestEvents(year);
            }
        }

        private void UpdateEventBox()
        {
            EventsBox.Items.Clear();
            foreach (Event e in selector.events)
            {
                EventsBox.Items.Add(e);
            }
            EventsBox.SelectedIndex = 0;
            Mouse.SetCursor(Cursors.AppStarting);
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

        private void SetProgressVisibility(Visibility visible)
        {
            DownloadProgress.Dispatcher.Invoke(new Action(() => DownloadProgress.Visibility = visible));
        }
    }
}

