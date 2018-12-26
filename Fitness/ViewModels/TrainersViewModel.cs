using BLL;
using DAL;
using Fitness.Commands;
using Fitness.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fitness.ViewModels
{
    class TrainersViewModel : INotifyPropertyChanged
    {
        DBOperations db;

        public TrainersViewModel()
        {
            db = new DBOperations();
            Trainers = db.GetAllTrainers();
        }

        private ObservableCollection<Trainer> trainers;
        public ObservableCollection<Trainer> Trainers
        {
            get
            {
                return trainers;
            }
            set
            {
                trainers = value;
                OnPropertyChanged();
            }
        }

        private Trainer selectedTrainer;
        public Trainer SelectedTrainer
        {
            get
            {
                return selectedTrainer;
            }
            set
            {
                selectedTrainer = value;
                OnPropertyChanged();
            }
        }

        DelegateCommand deleteTrainer;
        public DelegateCommand DeleteTrainer
        {
            get
            {
                if (deleteTrainer == null)
                {
                    deleteTrainer = new DelegateCommand(Delete);
                }
                return deleteTrainer;
            }
        }
        private void Delete(object arg)
        {
            db.DeleteTrainer(SelectedTrainer.ID);
        }

        DelegateCommand addTrainer;
        public DelegateCommand AddTrainer
        {
            get
            {
                if (addTrainer == null)
                {
                    addTrainer = new DelegateCommand(Add);
                }
                return addTrainer;
            }
        }
        private void Add(object arg)
        {
            TrainerWindow addTrainerWindow = new TrainerWindow(new Trainer(),"Добавить тренера", "Добавить");
            addTrainerWindow.DataContext = this;
            addTrainerWindow.ShowDialog();
            if (addTrainerWindow.DialogResult == true)
            {
                var newTrainer = addTrainerWindow.Panel.DataContext as Trainer;
                db.AddTrainer(newTrainer);
            }
        }

        DelegateCommand editTrainer;
        public DelegateCommand EditTrainer
        {
            get
            {
                if (editTrainer == null)
                {
                    editTrainer = new DelegateCommand(Edit);
                }
                return editTrainer;
            }
        }
        private void Edit(object arg)
        {
            TrainerWindow editTrainerWindow = new TrainerWindow(SelectedTrainer, "Изменить тренера", "Сохранить");
            editTrainerWindow.DataContext = this;
            editTrainerWindow.ShowDialog();
            if (editTrainerWindow.DialogResult == true)
            {
                var newTrainer = editTrainerWindow.Panel.DataContext as Trainer;
                db.UpdateTrainer(newTrainer);
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