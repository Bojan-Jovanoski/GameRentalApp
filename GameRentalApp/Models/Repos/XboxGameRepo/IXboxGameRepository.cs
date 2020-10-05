using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRentalApp.Models.Repos.XboxGameRepo
{
    public interface IXBoxGameRepository : IDisposable
    {
        IEnumerable<XBoxGame> GetXboxGames();
        XBoxGame GetXboxGameByID(int XboxID);
        void InsertXboxGame(XBoxGame xboxgame);
        void DeleteXboxGame(int XboxID);
        void UpdateXboxGame(XBoxGame xboxgame);
        void Save();
    }
}
