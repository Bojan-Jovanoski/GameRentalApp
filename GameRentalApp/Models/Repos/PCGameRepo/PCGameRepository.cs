using GameRentalApp.Models.Repos.PCGameRepo.Models.Repos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameRentalApp.Models.Repos.PCGameRepo
{
    public class PCGameRepository : IPCGamesRepository, IDisposable
    {
        private ApplicationDbContext db;

        public PCGameRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PCGame> GetPCGames()
        {
            return db.PCGames.ToList();
        }

        public PCGame GetPCGameByID(int id)
        {
            return db.PCGames.Find(id);

        }

        public void InsertPCGame(PCGame pcgame)
        {
            db.PCGames.Add(pcgame);
        }

        public void DeletePCGame(int PCID)
        {
            PCGame pcgame = db.PCGames.Find(PCID);
            db.PCGames.Remove(pcgame);
        }

        public void UpdatePCGame(PCGame pcgame)
        {
            db.Entry(pcgame).State = EntityState.Modified;
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