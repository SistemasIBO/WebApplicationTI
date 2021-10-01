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
    public class CamarasController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Camaras
        public ActionResult Index()
        {
            return View(db.Camaras.ToList());
        }

        // GET: Camaras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camaras camaras = db.Camaras.Find(id);
            if (camaras == null)
            {
                return HttpNotFound();
            }
            return View(camaras);
        }

        // GET: Camaras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Camaras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCamara,Marca,Modelo,Ubicacion,NoSerie")] Camaras camaras)
        {
            if (ModelState.IsValid)
            {
                db.Camaras.Add(camaras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(camaras);
        }

        // GET: Camaras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camaras camaras = db.Camaras.Find(id);
            if (camaras == null)
            {
                return HttpNotFound();
            }
            return View(camaras);
        }

        // POST: Camaras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCamara,Marca,Modelo,Ubicacion,NoSerie")] Camaras camaras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(camaras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(camaras);
        }

        // GET: Camaras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camaras camaras = db.Camaras.Find(id);
            if (camaras == null)
            {
                return HttpNotFound();
            }
            return View(camaras);
        }

        // POST: Camaras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Camaras camaras = db.Camaras.Find(id);
            db.Camaras.Remove(camaras);
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
