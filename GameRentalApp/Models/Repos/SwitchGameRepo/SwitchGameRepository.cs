using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameRentalApp.Models.Repos.SwitchGameRepo
{
    public class SwitchGameRepository : ISwitchGameRepository, IDisposable
    {
        private ApplicationDbContext db;

        public SwitchGameRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SwitchGame> GetSwitchGames()
        {
            return db.SwitchGames.ToList();
        }

        public SwitchGame GetSwitchGameByID(int id)
        {
            return db.SwitchGames.Find(id);

        }

        public void InsertSwitchGame(SwitchGame switchgame)
        {
            db.SwitchGames.Add(switchgame);
        }

        public void DeleteSwitchGame(int SwitchID)
        {
            SwitchGame switchgame = db.SwitchGames.Find(SwitchID);
            db.SwitchGames.Remove(switchgame);
        }

        public void UpdateSwitchGame(SwitchGame switchgame)
        {
            db.Entry(switchgame).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}