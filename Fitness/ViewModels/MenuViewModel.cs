using Fitness.Commands;
using Fitness.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fitness.ViewModels
{
    class MenuViewModel : INotifyPropertyChanged
    {
        DelegateCommand displayClients;
        public DelegateCommand DisplayClients
        {
            get
            {
                if (displayClients == null)
                {
                    displayClients = new DelegateCommand(ShowClientsForm);
                }
                return displayClients;
            }
        }

        private void ShowClientsForm(object arg)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.DataContext = new ClientsViewModel();
            clientsWindow.Show();
        }

        DelegateCommand displayTrainings;
        public DelegateCommand DisplayTrainings
        {
            get
            {
                if (displayTrainings == null)
                {
                    displayTrainings = new DelegateCommand(ShowTrainingsForm);
                }
                return displayTrainings;
            }
        }

        private void ShowTrainingsForm(object arg)
        {
            TrainingsWindow trainingsWindow = new TrainingsWindow();
            trainingsWindow.DataContext = new TrainingsViewModel();
            trainingsWindow.Show();
        }

        DelegateCommand displayTrainers;
        public DelegateCommand DisplayTrainers
        {
            get
            {
                if (displayTrainers == null)
                {
                    displayTrainers = new DelegateCommand(ShowTrainersForm);
                }
                return displayTrainers;
            }
        }

        private void ShowTrainersForm(object arg)
        {
            TrainersWindow trainersWindow = new TrainersWindow();
            trainersWindow.DataContext = new TrainersViewModel();
            trainersWindow.Show();
        }

        DelegateCommand closeApp;
        public DelegateCommand CloseApp
        {
            get
            {
                if (closeApp == null)
                {
                    closeApp = new DelegateCommand(Close);
                }
                return closeApp;
            }
        }

        private void Close(object win)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
