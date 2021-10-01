using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationTI.Models;

namespace WebApplicationTI.Controllers
{
    public class RoutersController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Routers
        public ActionResult Index()
        {
            return View(db.Router.ToList());
        }

        // GET: Routers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Router router = db.Router.Find(id);
            if (router == null)
            {
                return HttpNotFound();
            }
            return View(router);
        }

        // GET: Routers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Routers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRouter,Marca,Modelo,Serie,Estado,Observaciones")] Router router)
        {
            if (ModelState.IsValid)
            {
                db.Router.Add(router);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(router);
        }

        // GET: Routers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Router router = db.Router.Find(id);
            if (router == null)
            {
                return HttpNotFound();
            }
            return View(router);
        }

        // POST: Routers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRouter,Marca,Modelo,Serie,Estado,Observaciones")] Router router)
        {
            if (ModelState.IsValid)
            {
                db.Entry(router).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(router);
        }

        // GET: Routers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Router router = db.Router.Find(id);
            if (router == null)
            {
                return HttpNotFound();
            }
            return View(router);
        }

        // POST: Routers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Router router = db.Router.Find(id);
            db.Router.Remove(router);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
