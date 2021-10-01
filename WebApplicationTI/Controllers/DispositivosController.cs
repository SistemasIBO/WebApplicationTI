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
    public class DispositivosController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Dispositivos
        public ActionResult Index()
        {
            return View(db.Dispositivos.ToList());
        }

        // GET: Dispositivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispositivos dispositivos = db.Dispositivos.Find(id);
            if (dispositivos == null)
            {
                return HttpNotFound();
            }
            return View(dispositivos);
        }

        // GET: Dispositivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dispositivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDispositivo,Producto,Descripcion,Marca,Modelo,NoSerie,Estado,Observaciones")] Dispositivos dispositivos)
        {
            if (ModelState.IsValid)
            {
                db.Dispositivos.Add(dispositivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dispositivos);
        }

        // GET: Dispositivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispositivos dispositivos = db.Dispositivos.Find(id);
            if (dispositivos == null)
            {
                return HttpNotFound();
            }
            return View(dispositivos);
        }

        // POST: Dispositivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDispositivo,Producto,Descripcion,Marca,Modelo,NoSerie,Estado,Observaciones")] Dispositivos dispositivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dispositivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dispositivos);
        }

        // GET: Dispositivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispositivos dispositivos = db.Dispositivos.Find(id);
            if (dispositivos == null)
            {
                return HttpNotFound();
            }
            return View(dispositivos);
        }

        // POST: Dispositivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dispositivos dispositivos = db.Dispositivos.Find(id);
            db.Dispositivos.Remove(dispositivos);
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
