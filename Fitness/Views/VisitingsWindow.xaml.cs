using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Fitness.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitingsWindow.xaml
    /// </summary>
    public partial class VisitingsWindow : Window
    {
        public VisitingsWindow(List<Visiting> visitings)
        {
            InitializeComponent();

            VisitingListBox.ItemsSource = visitings.OrderByDescending(i => i.FinishTime);
        }
    }
}
