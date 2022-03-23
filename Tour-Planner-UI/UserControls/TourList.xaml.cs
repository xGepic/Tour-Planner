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

namespace Tour_Planner_UI.UserControls
{
    /// <summary>
    /// Interaction logic for TourList.xaml
    /// </summary>
    public partial class TourList : UserControl
    {
        public TourList()
        {
            InitializeComponent();
            ListAllTours();
        }
        private MainWindowViewModel _MainWindowViewModel = new();
        private TextBox CreateTourListItem(string name)
        {
            TextBox box = new TextBox();
            box.Text = name;
            return box;
        }

        private void ListAllTours()
        {
            foreach(Tour tour in _MainWindowViewModel._AllTours)
            {
                TextBox box = CreateTourListItem(tour.TourName);
                TourListStackPanel.Children.Add(box);
            }
            return;
        }
    }
}
