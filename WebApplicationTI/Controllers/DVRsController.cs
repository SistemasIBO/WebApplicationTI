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
    public class DVRsController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: DVRs
        public ActionResult Index()
        {
            return View(db.DVR.ToList());
        }

        // GET: DVRs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVR dVR = db.DVR.Find(id);
            if (dVR == null)
            {
                return HttpNotFound();
            }
            return View(dVR);
        }

        // GET: DVRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DVRs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDVR,Marca,Modelo,NoSerie,PuertoUSB,NoCamaras,PuertoVGA,Estado,Observaciones")] DVR dVR)
        {
            if (ModelState.IsValid)
            {
                db.DVR.Add(dVR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dVR);
        }

        // GET: DVRs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVR dVR = db.DVR.Find(id);
            if (dVR == null)
            {
                return HttpNotFound();
            }
            return View(dVR);
        }

        // POST: DVRs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDVR,Marca,Modelo,NoSerie,PuertoUSB,NoCamaras,PuertoVGA,Estado,Observaciones")] DVR dVR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dVR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dVR);
        }

        // GET: DVRs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVR dVR = db.DVR.Find(id);
            if (dVR == null)
            {
                return HttpNotFound();
            }
            return View(dVR);
        }

        // POST: DVRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DVR dVR = db.DVR.Find(id);
            db.DVR.Remove(dVR);
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
