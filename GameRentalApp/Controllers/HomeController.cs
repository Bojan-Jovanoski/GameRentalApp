using GameRentalApp.Models;
using GameRentalApp.Models.Repos.PCGameRepo;
using GameRentalApp.Models.Repos.PCGameRepo.Models.Repos;
using GameRentalApp.Models.Repos.SonyGameRepo;
using GameRentalApp.Models.Repos.SwitchGameRepo;
using GameRentalApp.Models.Repos.XboxGameRepo;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace GameRentalApp.Controllers
{
    public class HomeController : Controller
    {
        private IPCGamesRepository pCGamesRepository;
        private ISonyGameRepository sonyGameRepository;
        private IXBoxGameRepository xBoxGameRepository;
        private ISwitchGameRepository switchGameRepository;

        public HomeController()
        {
            this.pCGamesRepository = new PCGameRepository(new Models.ApplicationDbContext());
            this.sonyGameRepository = new SonyGameRepository(new Models.ApplicationDbContext());
            this.xBoxGameRepository = new XBoxGameRepository(new Models.ApplicationDbContext());
            this.switchGameRepository = new SwitchGameRepository(new Models.ApplicationDbContext());

        }
        public HomeController(IPCGamesRepository pCGamesRepository, ISonyGameRepository sonyGameRepository,
            IXBoxGameRepository xBoxGameRepository, ISwitchGameRepository switchGameRepository)
        {
            this.pCGamesRepository = pCGamesRepository;
            this.sonyGameRepository = sonyGameRepository;
            this.xBoxGameRepository = xBoxGameRepository;
            this.switchGameRepository = switchGameRepository;
        }
        public ActionResult Index()
        {
            GamesViewModel games = new GamesViewModel();
            games.xboxGames = xBoxGameRepository.GetXboxGames();
            games.sonyGames = sonyGameRepository.GetSonyGames();
            games.pcGames = pCGamesRepository.GetPCGames();
            games.switchGames = switchGameRepository.GetSwitchGames();
            return View(games);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}