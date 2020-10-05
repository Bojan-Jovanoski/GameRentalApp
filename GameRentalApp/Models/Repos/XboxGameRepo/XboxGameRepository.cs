using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameRentalApp.Models.Repos.XboxGameRepo
{
    public class XBoxGameRepository : IXBoxGameRepository, IDisposable
    {
        private ApplicationDbContext db;

        public XBoxGameRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<XBoxGame> GetXboxGames()
        {
            return db.XBoxGames.ToList();
        }

        public XBoxGame GetXboxGameByID(int id)
        {
            return db.XBoxGames.Find(id);

        }

        public void InsertXboxGame(XBoxGame xboxgame)
        {
            db.XBoxGames.Add(xboxgame);
        }

        public void DeleteXboxGame(int XboxID)
        {
            XBoxGame xboxgame = db.XBoxGames.Find(XboxID);
            db.XBoxGames.Remove(xboxgame);
        }

        public void UpdateXboxGame(XBoxGame xboxgame)
        {
            db.Entry(xboxgame).State = EntityState.Modified;
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