using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameRentalApp.Models;
using GameRentalApp.Models.Repos.XboxGameRepo;

namespace GameRentalApp.Controllers
{
    public class XBoxGamesController : Controller
    {
        private IXBoxGameRepository xBoxGameRepository;

        public XBoxGamesController()
        {
            this.xBoxGameRepository = new XBoxGameRepository(new ApplicationDbContext());
        }

        public XBoxGamesController(IXBoxGameRepository xboxGameRepository)
        {
            this.xBoxGameRepository = xboxGameRepository;
        }

        // GET: XBoxGames
        public ActionResult Index()
        {
            var xboxgames = xBoxGameRepository.GetXboxGames();
            return View(xboxgames);
        }

        // GET: XBoxGames/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XBoxGame xBoxGame = xBoxGameRepository.GetXboxGameByID(id);
            if (xBoxGame == null)
            {
                return HttpNotFound();
            }
            return View(xBoxGame);
        }

        // GET: XBoxGames/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: XBoxGames/Create
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "XboxID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] XBoxGame xBoxGame)
        {
            if (ModelState.IsValid)
            {
                xBoxGameRepository.InsertXboxGame(xBoxGame);
                xBoxGameRepository.Save();
                return RedirectToAction("Index");
            }

            return View(xBoxGame);
        }

        // GET: XBoxGames/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XBoxGame xBoxGame = xBoxGameRepository.GetXboxGameByID(id);
            if (xBoxGame == null)
            {
                return HttpNotFound();
            }
            return View(xBoxGame);
        }

        // POST: XBoxGames/Edit/5
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "XboxID,AvailableCopies,GameName,Genre,ImgUrl,Price,Description")] XBoxGame xBoxGame)
        {
            if (ModelState.IsValid)
            {
                xBoxGameRepository.UpdateXboxGame(xBoxGame);
                xBoxGameRepository.Save();
                return RedirectToAction("Index");
            }
            return View(xBoxGame);
        }
        
        public ActionResult Rent(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XBoxGame xBoxGame = xBoxGameRepository.GetXboxGameByID(id);
            if (xBoxGame == null)
            {
                return HttpNotFound();
            }
            return View(xBoxGame);
        }


        [HttpPost, ActionName("Rent")]
        [ValidateAntiForgeryToken]
        public ActionResult RentConfirmed(int id)
        {
            XBoxGame xBoxGame = xBoxGameRepository.GetXboxGameByID(id);
            xBoxGame.AvailableCopies--;
            xBoxGameRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: XBoxGames/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XBoxGame xBoxGame = xBoxGameRepository.GetXboxGameByID(id);
            if (xBoxGame == null)
            {
                return HttpNotFound();
            }
            return View(xBoxGame);
        }

        // POST: XBoxGames/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            xBoxGameRepository.DeleteXboxGame(id);
            xBoxGameRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                xBoxGameRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
