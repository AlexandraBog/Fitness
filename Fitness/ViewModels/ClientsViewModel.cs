using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL;
using DAL;
using Fitness.Commands;
using Fitness.Views;

namespace Fitness.ViewModels
{
    class ClientsViewModel : INotifyPropertyChanged
    {
        DBOperations db;

        public ClientsViewModel()
        {
            db = new DBOperations();
            Clients = db.GetAllClients();
        }

        private ObservableCollection<Client> clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                OnPropertyChanged();
            }
        }

        private Client selectedClient;
        public Client SelectedClient
        {
            get
            {
                return selectedClient;
            }
            set
            {
                selectedClient = value;
            }
        }

        DelegateCommand editClient;
        public DelegateCommand EditClient
        {
            get
            {
                if (editClient == null)
                {
                    editClient = new DelegateCommand(EditClientMethod);
                }
                return editClient;
            }
        }
        private void EditClientMethod(object arg)
        {
            AddNewClientWindow newClientWindow = new AddNewClientWindow();
            newClientWindow.DataContext = new NewClientViewModel(SelectedClient);
            newClientWindow.ShowDialog();
            if (newClientWindow.DialogResult == true)
            {
                var client = newClientWindow.newClientGrid.DataContext as Client;
                db.UpdateClient(client);
            }
        }

        DelegateCommand deleteClient;
        public DelegateCommand DeleteClient
        {
            get
            {
                if (deleteClient == null)
                {
                    deleteClient = new DelegateCommand(DeleteClientMethod);
                }
                return deleteClient;
            }
        }
        private void DeleteClientMethod(object arg)
        {
            db.DeleteClient(SelectedClient.ID);
        }

        DelegateCommand displaySubscription;
        public DelegateCommand DisplaySubscription
        {
            get
            {
                if (displaySubscription == null)
                {
                    displaySubscription = new DelegateCommand(ShowSubscription);
                }
                return displaySubscription;
            }
        }
        private void ShowSubscription(object arg)
        {
            Client client = arg as Client;
            if (client.Subscription != null)
            {
                var subscription = $"Дата начала действия: {client.Subscription.TimeOfActionStart.ToShortDateString()} \nДата окончания действия: {client.Subscription.TimeOfActionEnd.ToShortDateString()} \nЦена: {client.Subscription.Price} руб.";
                MessageBox.Show(subscription, "Абонемент");
            }
            else
            {
                MessageBox.Show("Абонемент не назначен", "Абонемент");
            }

        }

        DelegateCommand displayVisitings;
        public DelegateCommand DisplayVisitings
        {
            get
            {
                if (displayVisitings == null)
                {
                    displayVisitings = new DelegateCommand(ShowVisitingsWindow);
                }
                return displayVisitings;
            }
        }
        private void ShowVisitingsWindow(object arg)
        {
            Client client = arg as Client;
            Views.VisitingsWindow visitingsWindow = new Views.VisitingsWindow(client.Visitings.ToList());
            visitingsWindow.ShowDialog();
        }

        DelegateCommand addNewVisiting;
        public DelegateCommand AddNewVisiting
        {
            get
            {
                if (addNewVisiting == null)
                {
                    addNewVisiting = new DelegateCommand(AddVisiting);
                }
                return addNewVisiting;
            }
        }
        private void AddVisiting(object arg)
        {
            var currentDate = DateTime.Now;

            if (SelectedClient.Visitings.Any(i => i.StartTime.Date == currentDate.Date) || currentDate.Hour > 22)
            {
                MessageBox.Show("На сегодня посещения больше не возможны!", "Error");
                return;
            }

            if (currentDate > SelectedClient.Subscription.TimeOfActionEnd)
            {
                MessageBox.Show("Абонемент истёк, занятия более не доступны", "Error");
                SelectedClient.Subscription = null;
                SelectedClient.SubscriptionID = 0;
                db.UpdateClient(SelectedClient);
                return;
            }

            Visiting newVisiting = new Visiting()
            {
                StartTime = currentDate,
                FinishTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 0, 0),
                Client = SelectedClient,
                ClientID = SelectedClient.ID,
                Price = 0,
            };

            SelectedClient.Visitings.Add(newVisiting);

            db.UpdateClient(SelectedClient);
        }

        DelegateCommand addNewClient;
        public DelegateCommand AddNewClient
        {
            get
            {
                if (addNewClient == null)
                {
                    addNewClient = new DelegateCommand(AddClient);
                }
                return addNewClient;
            }
        }
        private void AddClient(object arg)
        {
            AddNewClientWindow newClientWindow = new AddNewClientWindow();
            newClientWindow.DataContext = new NewClientViewModel(new Client());
            newClientWindow.ShowDialog();
            if (newClientWindow.DialogResult == true)
            {
                var client = newClientWindow.newClientGrid.DataContext as Client;
                db.AddClient(client);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
