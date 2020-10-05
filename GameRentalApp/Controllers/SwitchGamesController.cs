using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameRentalApp.Models;
using GameRentalApp.Models.Repos.SwitchGameRepo;

namespace GameRentalApp.Controllers
{
    public class SwitchGamesController : Controller
    {
        private ISwitchGameRepository switchGameRepository;

        public SwitchGamesController()
        {
            this.switchGameRepository = new SwitchGameRepository(new ApplicationDbContext());
        }
        public SwitchGamesController(ISwitchGameRepository switchGameRepository)
        {
            this.switchGameRepository = switchGameRepository;
        }

        // GET: SwitchGames
        public ActionResult Index()
        {
            var games = switchGameRepository.GetSwitchGames();
            return View(games);
        }

        // GET: SwitchGames/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwitchGame switchGame = switchGameRepository.GetSwitchGameByID(id);
            if (switchGame == null)
            {
                return HttpNotFound();
            }
            return View(switchGame);
        }

        // GET: SwitchGames/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SwitchGames/Create
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SwitchID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] SwitchGame switchGame)
        {
            if (ModelState.IsValid)
            {
                switchGameRepository.InsertSwitchGame(switchGame);
                switchGameRepository.Save();
                return RedirectToAction("Index");
            }

            return View(switchGame);
        }

        // GET: SwitchGames/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwitchGame switchGame = switchGameRepository.GetSwitchGameByID(id);
            if (switchGame == null)
            {
                return HttpNotFound();
            }
            return View(switchGame);
        }

        // POST: SwitchGames/Edit/5
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SwitchID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] SwitchGame switchGame)
        {
            if (ModelState.IsValid)
            {
                switchGameRepository.UpdateSwitchGame(switchGame);
                switchGameRepository.Save();
                return RedirectToAction("Index");
            }
            return View(switchGame);
        }

        // GET: SwitchGames/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwitchGame switchGame = switchGameRepository.GetSwitchGameByID(id);
            if (switchGame == null)
            {
                return HttpNotFound();
            }
            return View(switchGame);
        }

        // POST: SwitchGames/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            switchGameRepository.DeleteSwitchGame(id);
            switchGameRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Rent(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwitchGame switchGame = switchGameRepository.GetSwitchGameByID(id);
            if (switchGame == null)
            {
                return HttpNotFound();
            }
            return View(switchGame);
        }


        [HttpPost, ActionName("Rent")]
        [ValidateAntiForgeryToken]
        public ActionResult RentConfirmed(int id)
        {
            SwitchGame switchGame = switchGameRepository.GetSwitchGameByID(id);
            switchGame.AvailableCopies--;
            switchGameRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                switchGameRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
