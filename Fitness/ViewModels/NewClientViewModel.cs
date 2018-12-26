using Fitness.Commands;
using Fitness.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using DAL;
using System.Windows;

namespace Fitness.ViewModels
{
    class NewClientViewModel : INotifyPropertyChanged
    {

        private Client newClient;
        public Client NewClient
        {
            get
            {
                return newClient;
            }
            set
            {
                newClient = value;
                OnPropertyChanged();
            }
        }

        public string Title { get; set; }

        public NewClientViewModel(Client client)
        {
            NewClient = client;
            if (NewClient.ID == 0)
            {
                Title = "Оформление нового клиента";
            }
            else
            {
                Title = "Изменение информации о клиенте";
            }
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

        DelegateCommand addSubscription;
        public DelegateCommand AddSubscription
        {
            get
            {
                if (addSubscription == null)
                {
                    addSubscription = new DelegateCommand(AddSubscriptionMethod);
                }
                return addSubscription;
            }
        }
        private void AddSubscriptionMethod(object arg)
        {
            Subscription subscription;
            if (NewClient.Subscription == null)
            {
                subscription = new Subscription();
            }
            else
            {
                subscription = NewClient.Subscription;
            }
            SubscriptionWindow newSubscription = new SubscriptionWindow(subscription);
            newSubscription.DataContext = this;
            newSubscription.ShowDialog();
            if (newSubscription.DialogResult == true)
            {
                NewClient.Subscription = newSubscription.SubscriptionGrid.DataContext as Subscription;
            }
        }

        private DelegateCommand closeWindow;
        public DelegateCommand CloseWindow
        {
            get
            {
                if (closeWindow == null)
                {
                    closeWindow = new DelegateCommand(CloseWindowMethod);
                }
                return closeWindow;
            }
        }
        private void CloseWindowMethod(object arg)
        {
            var window = arg as Window;
            window.DialogResult = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
