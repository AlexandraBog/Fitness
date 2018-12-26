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
    class TrainingsViewModel : INotifyPropertyChanged
    {
        DBOperations db;

        public TrainingsViewModel()
        {
            db = new DBOperations();
            Trainings = db.GetAllTrainings();
            Trainers = db.GetAllTrainers();
        }

        private ObservableCollection<Training> trainings;
        public ObservableCollection<Training> Trainings
        {
            get
            {
                return trainings;
            }
            set
            {
                trainings = value;
                OnPropertyChanged();
            }
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

        private Training selectedTraining;
        public Training SelectedTraining
        {
            get
            {
                return selectedTraining;
            }
            set
            {
                selectedTraining = value;
                OnPropertyChanged();
            }
        }

        DelegateCommand deleteTraining;
        public DelegateCommand DeleteTraining
        {
            get
            {
                if (deleteTraining == null)
                {
                    deleteTraining = new DelegateCommand(Delete);
                }
                return deleteTraining;
            }
        }
        private void Delete(object arg)
        {
            db.DeleteTraining(SelectedTraining.ID);
        }

        DelegateCommand addTraining;
        public DelegateCommand AddTraining
        {
            get
            {
                if (addTraining == null)
                {
                    addTraining = new DelegateCommand(Add);
                }
                return addTraining;
            }
        }
        private void Add(object arg)
        {
            Training newTraining = new Training();
            newTraining.StartDate = DateTime.Now;
            newTraining.EndDate = DateTime.Now.AddHours(1);
            TrainingWindow addTrainingWindow = new TrainingWindow(newTraining,"Добавить занятие", "Добавить");
            addTrainingWindow.DataContext = new TrainingsViewModel();
            addTrainingWindow.ShowDialog();
            if (addTrainingWindow.DialogResult == true)
            {
                var tr = addTrainingWindow.Panel.DataContext as Training;
                db.AddTraining(tr);
            }
        }

        DelegateCommand editTraining;
        public DelegateCommand EditTraining
        {
            get
            {
                if (editTraining == null)
                {
                    editTraining = new DelegateCommand(Edit);
                }
                return editTraining;
            }
        }
        private void Edit(object arg)
        {
            TrainingWindow editTrainingWindow = new TrainingWindow(SelectedTraining, "Изменить тренировку", "Сохранить");
            editTrainingWindow.DataContext = this;
            editTrainingWindow.ShowDialog();
            if (editTrainingWindow.DialogResult == true)
            {
                var newTraining = editTrainingWindow.Panel.DataContext as Training;
                db.UpdateTraining(newTraining);
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