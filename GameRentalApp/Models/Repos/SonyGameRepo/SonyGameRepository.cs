using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameRentalApp.Models.Repos.SonyGameRepo
{
    public class SonyGameRepository : ISonyGameRepository, IDisposable
    {
        private ApplicationDbContext db;

        public SonyGameRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SonyGame> GetSonyGames()
        {
            return db.SonyGames.ToList();
        }

        public SonyGame GetSonyGameByID(int id)
        {
            return db.SonyGames.Find(id);

        }

        public void InsertSonyGame(SonyGame sonygame)
        {
            db.SonyGames.Add(sonygame);
        }

        public void DeleteSonyGame(int SonyID)
        {
            SonyGame sonygame = db.SonyGames.Find(SonyID);
            db.SonyGames.Remove(sonygame);
        }

        public void UpdateSonyGame(SonyGame sonygame)
        {
            db.Entry(sonygame).State = EntityState.Modified;
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