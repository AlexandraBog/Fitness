using BLL;
using DAL;
using System.Windows;

namespace Fitness.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTrainerWindow.xaml
    /// </summary>
    public partial class TrainingWindow : Window
    {
        public TrainingWindow(Training newTraining, string title, string buttonName)
        {
            InitializeComponent();

            Panel.DataContext = newTraining;
            TitleLabel.Content = title;
            Button.Content = buttonName;

            Combobox.ItemsSource = new DBOperations().GetAllTrainers();
        }
    }
}
