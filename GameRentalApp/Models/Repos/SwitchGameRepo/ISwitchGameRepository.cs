using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRentalApp.Models.Repos.SwitchGameRepo
{
    public interface ISwitchGameRepository : IDisposable
    {
        IEnumerable<SwitchGame> GetSwitchGames();
        SwitchGame GetSwitchGameByID(int SwitchID);
        void InsertSwitchGame(SwitchGame switchgame);
        void DeleteSwitchGame(int SwitchID);
        void UpdateSwitchGame(SwitchGame switchgame);
        void Save();
    }
}
