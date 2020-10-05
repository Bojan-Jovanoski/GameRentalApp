using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRentalApp.Models
{
    public class GamesViewModel
    {
        public IEnumerable<PCGame> pcGames { get; set; }
        public IEnumerable<XBoxGame> xboxGames { get; set; }
        public IEnumerable<SwitchGame> switchGames { get; set; }
        public IEnumerable<SonyGame> sonyGames { get; set; }

    }
}