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
    public class ImpresorasController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Impresoras
        public ActionResult Index()
        {
            var impresoras = db.Impresoras.Include(i => i.Depto);
            return View(impresoras.ToList());
        }

        // GET: Impresoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impresoras impresoras = db.Impresoras.Find(id);
            if (impresoras == null)
            {
                return HttpNotFound();
            }
            return View(impresoras);
        }

        // GET: Impresoras/Create
        public ActionResult Create()
        {
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento");
            return View();
        }

        // POST: Impresoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEquipoImpresora,IdDepto,Tipo,Marcar,Modelo,Arrendamiento,PuertoEthetnet,PuertoUSB,Estado,Observaciones")] Impresoras impresoras)
        {
            if (ModelState.IsValid)
            {
                db.Impresoras.Add(impresoras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", impresoras.IdDepto);
            return View(impresoras);
        }

        // GET: Impresoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impresoras impresoras = db.Impresoras.Find(id);
            if (impresoras == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", impresoras.IdDepto);
            return View(impresoras);
        }

        // POST: Impresoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEquipoImpresora,IdDepto,Tipo,Marcar,Modelo,Arrendamiento,PuertoEthetnet,PuertoUSB,Estado,Observaciones")] Impresoras impresoras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(impresoras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", impresoras.IdDepto);
            return View(impresoras);
        }

        // GET: Impresoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impresoras impresoras = db.Impresoras.Find(id);
            if (impresoras == null)
            {
                return HttpNotFound();
            }
            return View(impresoras);
        }

        // POST: Impresoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Impresoras impresoras = db.Impresoras.Find(id);
            db.Impresoras.Remove(impresoras);
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
