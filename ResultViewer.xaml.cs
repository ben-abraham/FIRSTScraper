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

namespace FIRST
{
    /// <summary>
    /// Interaction logic for ResultViewer.xaml
    /// </summary>
    public partial class ResultViewer : Window
    {
        public ResultViewer(EventResults s)
        {
        
            InitializeComponent();
            grid.ItemsSource = s.QualificationMatches;
            eliminationGrid.ItemsSource = s.EliminationMatches;
        }
    }
}
