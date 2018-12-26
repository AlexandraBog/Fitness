using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DBOperations
    {
        FitnessDB db = new FitnessDB();

        public ObservableCollection<Client> GetAllClients()
        {
            db.Clients.Load();
            return db.Clients.Local;
        }

        #region Клиенты

        public bool AddClient(Client client)
        {
            db.Clients.Add(client);
            return Save();
        }

        public bool UpdateClient(Client client)
        {
            var cl = db.Clients.Find(client.ID);

            if (cl != null)
            {
                cl = client;
            }
            return Save();
        }

        public bool DeleteClient(int id)
        {
            var client = db.Clients.Find(id);
            if (client != null)
            {
                db.Clients.Remove(client);
            }
            return Save();
        }

        #endregion

        #region Тренеры

        public ObservableCollection<Trainer> GetAllTrainers()
        {
            db.Trainers.Load();
            return db.Trainers.Local;
        }

        public bool AddTrainer(Trainer trainer)
        {
            db.Trainers.Add(trainer);
            return Save();
        }

        public bool UpdateTrainer(Trainer trainer)
        {
            var tr = db.Trainers.Find(trainer.ID);

            if(tr != null)
            {
                tr = trainer;
            }
            return Save();
        }

        public bool DeleteTrainer(int id)
        {
            var trainer = db.Trainers.Find(id);
            if (trainer != null)
            {
                db.Trainers.Remove(trainer);
            }
            return Save();
        }

        #endregion

        #region ЗАнятия

        public ObservableCollection<Training> GetAllTrainings()
        {
            db.Trainings.Load();
            return db.Trainings.Local;
        }

        public bool AddTraining(Training training)
        {
            db.Trainings.Add(training);
            return Save();
        }

        public bool UpdateTraining(Training training)
        {
            var tr = db.Trainings.Find(training.ID);

            if (tr != null)
            {
                tr = training;
            }
            return Save();
        }

        public bool DeleteTraining(int id)
        {
            var training = db.Trainings.Find(id);
            if (training != null)
            {
                db.Trainings.Remove(training);
            }
            return Save();
        }

        #endregion

        public bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            return false;
        }


    }
}
