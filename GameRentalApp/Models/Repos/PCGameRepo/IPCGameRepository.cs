using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRentalApp.Models.Repos.PCGameRepo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Models.Repos
    {
        public interface IPCGamesRepository : IDisposable
        {
            IEnumerable<PCGame> GetPCGames();
            PCGame GetPCGameByID(int PCID);
            void InsertPCGame(PCGame pcgame);
            void DeletePCGame(int PCID);
            void UpdatePCGame(PCGame pcgame);
            void Save();
        }
    }

}
