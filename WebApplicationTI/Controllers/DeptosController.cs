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
    public class DeptosController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Deptos
        public ActionResult Index()
        {
            return View(db.Depto.ToList());
        }

        // GET: Deptos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depto depto = db.Depto.Find(id);
            if (depto == null)
            {
                return HttpNotFound();
            }
            return View(depto);
        }

        // GET: Deptos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deptos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDepto,Departamento,JefeDelDpto,Observaciones")] Depto depto)
        {
            if (db.Depto.Any(d => d.Departamento == depto.Departamento))
            {
                ModelState.AddModelError("DeptoNameCreate", "El nombre del departamento ya existe");
            }

            if (ModelState.IsValid)
            {
                db.Depto.Add(depto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(depto);
        }

        // GET: Deptos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depto depto = db.Depto.Find(id);
            if (depto == null)
            {
                return HttpNotFound();
            }
            return View(depto);
        }

        // POST: Deptos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDepto,Departamento,JefeDelDpto,Observaciones")] Depto depto)
        {
            if (db.Depto.Any(d => d.Departamento == depto.Departamento))
            {
                ModelState.AddModelError("DeptoNameEdit", "El nombre del departamento ya existe");
            }

            if (ModelState.IsValid)
            {
                db.Entry(depto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(depto);
        }

        // GET: Deptos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depto depto = db.Depto.Find(id);
            if (depto == null)
            {
                return HttpNotFound();
            }
            return View(depto);
        }

        // POST: Deptos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Depto depto = db.Depto.Find(id);
            db.Depto.Remove(depto);
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
