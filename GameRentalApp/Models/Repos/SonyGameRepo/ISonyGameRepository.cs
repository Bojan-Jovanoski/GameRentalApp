using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRentalApp.Models.Repos.SonyGameRepo
{
    public interface ISonyGameRepository : IDisposable
    {
        IEnumerable<SonyGame> GetSonyGames();
        SonyGame GetSonyGameByID(int SonyID);
        void InsertSonyGame(SonyGame sonygame);
        void DeleteSonyGame(int SonyID);
        void UpdateSonyGame(SonyGame sonygame);
        void Save();
    }
}
