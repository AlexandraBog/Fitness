using DAL;
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

namespace Fitness.Views
{
    /// <summary>
    /// Логика взаимодействия для SubscriptionWindow.xaml
    /// </summary>
    public partial class SubscriptionWindow : Window
    {
        public SubscriptionWindow(Subscription subscription)
        {
            InitializeComponent();
            if (subscription.ID == 0)
            {
                subscription.TimeOfActionStart = DateTime.Now;
                subscription.TimeOfActionEnd = DateTime.Now.AddYears(1);
            }
            SubscriptionGrid.DataContext = subscription;
        }
    }
}
