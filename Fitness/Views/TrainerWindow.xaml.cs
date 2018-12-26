using DAL;
using System.Windows;

namespace Fitness.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTrainerWindow.xaml
    /// </summary>
    public partial class TrainerWindow : Window
    {
        public TrainerWindow(Trainer newTrainer, string title, string buttonName)
        {
            InitializeComponent();

            Panel.DataContext = newTrainer;
            TitleLabel.Content = title;
            Button.Content = buttonName;
        }
    }
}
