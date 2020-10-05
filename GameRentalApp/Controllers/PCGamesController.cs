using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameRentalApp.Models;
using GameRentalApp.Models.Repos.PCGameRepo;
using GameRentalApp.Models.Repos.PCGameRepo.Models.Repos;

namespace GameRentalApp.Controllers
{
    public class PCGamesController : Controller
    {
        private IPCGamesRepository pcGamesRepository;

        public PCGamesController()
        {
            this.pcGamesRepository = new PCGameRepository(new ApplicationDbContext());
        }
        public PCGamesController(IPCGamesRepository pCGamesRepository)
        {
            this.pcGamesRepository = pCGamesRepository;
        }
        // GET: PCGames
        public ActionResult Index()
        {
            var pcgames = pcGamesRepository.GetPCGames();
            return View(pcgames);
        }

        // GET: PCGames/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCGame pCGame = pcGamesRepository.GetPCGameByID(id);
            if (pCGame == null)
            {
                return HttpNotFound();
            }
            return View(pCGame);
        }

        // GET: PCGames/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PCGames/Create
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PCID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] PCGame pCGame)
        {
            if (ModelState.IsValid)
            {
                pcGamesRepository.InsertPCGame(pCGame);
                pcGamesRepository.Save();
                return RedirectToAction("Index");
            }

            return View(pCGame);
        }

        // GET: PCGames/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCGame pCGame = pcGamesRepository.GetPCGameByID(id);
            if (pCGame == null)
            {
                return HttpNotFound();
            }
            return View(pCGame);
        }

        // POST: PCGames/Edit/5
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PCID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] PCGame pCGame)
        {
            if (ModelState.IsValid)
            {
                pcGamesRepository.UpdatePCGame(pCGame);
                pcGamesRepository.Save();
                return RedirectToAction("Index");
            }
            return View(pCGame);
        }

        // GET: PCGames/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCGame pCGame = pcGamesRepository.GetPCGameByID(id);
            if (pCGame == null)
            {
                return HttpNotFound();
            }
            return View(pCGame);
        }

        // POST: PCGames/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pcGamesRepository.DeletePCGame(id);
            pcGamesRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Rent(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCGame pCGame = pcGamesRepository.GetPCGameByID(id);
            if (pCGame == null)
            {
                return HttpNotFound();
            }
            return View(pCGame);
        }


        [HttpPost, ActionName("Rent")]
        [ValidateAntiForgeryToken]
        public ActionResult RentConfirmed(int id)
        {
            PCGame pCGame = pcGamesRepository.GetPCGameByID(id);
            pCGame.AvailableCopies--;
            pcGamesRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pcGamesRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
