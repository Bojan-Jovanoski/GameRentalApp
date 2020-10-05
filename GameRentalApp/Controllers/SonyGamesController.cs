using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using GameRentalApp.Models;
using GameRentalApp.Models.Repos.SonyGameRepo;

namespace GameRentalApp.Controllers
{
    public class SonyGamesController : Controller
    {
        private ISonyGameRepository sonyGameRepository;

        public SonyGamesController()
        {
            this.sonyGameRepository = new SonyGameRepository(new ApplicationDbContext());
        }

        public SonyGamesController(ISonyGameRepository sonyGamesRepository)
        {
            this.sonyGameRepository = sonyGameRepository;
        }
        // GET: SonyGames
        public ActionResult Index()
        {
            var sonygames = sonyGameRepository.GetSonyGames();
            return View(sonygames);
        }

        // GET: SonyGames/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SonyGame sonyGame = sonyGameRepository.GetSonyGameByID(id);
            if (sonyGame == null)
            {
                return HttpNotFound();
            }
            return View(sonyGame);
        }

        // GET: SonyGames/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SonyGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "SonyID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] SonyGame sonyGame)
        {
            if (ModelState.IsValid)
            {
                sonyGameRepository.InsertSonyGame(sonyGame);
                sonyGameRepository.Save();
                return RedirectToAction("Index");
            }

            return View(sonyGame);
        }

        // GET: SonyGames/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SonyGame sonyGame = sonyGameRepository.GetSonyGameByID(id);
            if (sonyGame == null)
            {
                return HttpNotFound();
            }
            return View(sonyGame);
        }

        // POST: SonyGames/Edit/5
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SonyID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] SonyGame sonyGame)
        {
            if (ModelState.IsValid)
            {
                sonyGameRepository.UpdateSonyGame(sonyGame);
                sonyGameRepository.Save();
                return RedirectToAction("Index");
            }
            return View(sonyGame);
        }

        // GET: SonyGames/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SonyGame sonyGame = sonyGameRepository.GetSonyGameByID(id);
            if (sonyGame == null)
            {
                return HttpNotFound();
            }
            return View(sonyGame);
        }

        // POST: SonyGames/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sonyGameRepository.DeleteSonyGame(id);
            sonyGameRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Rent(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SonyGame sonyGame = sonyGameRepository.GetSonyGameByID(id);
            if (sonyGame == null)
            {
                return HttpNotFound();
            }
            return View(sonyGame);
        }


        [HttpPost, ActionName("Rent")]
        [ValidateAntiForgeryToken]
        public ActionResult RentConfirmed(int id)
        {
            SonyGame sonyGame = sonyGameRepository.GetSonyGameByID(id);
            sonyGame.AvailableCopies--;
            sonyGameRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sonyGameRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
