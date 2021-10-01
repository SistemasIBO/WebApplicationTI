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
    public class ConsumiblesController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Consumibles
        public ActionResult Index()
        {
            var consumibles = db.Consumibles.Include(c => c.Proveedor);
            return View(consumibles.ToList());
        }

        // GET: Consumibles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumibles consumibles = db.Consumibles.Find(id);
            if (consumibles == null)
            {
                return HttpNotFound();
            }
            return View(consumibles);
        }

        // GET: Consumibles/Create
        public ActionResult Create()
        {
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "RazonSocial");
            return View();
        }

        // POST: Consumibles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConsumible,IdProveedor,Producto,Descripcion,Precio,Cantidad,FechaCompra,Observaciones")] Consumibles consumibles)
        {
            if (ModelState.IsValid)
            {
                db.Consumibles.Add(consumibles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "RazonSocial", consumibles.IdProveedor);
            return View(consumibles);
        }

        // GET: Consumibles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumibles consumibles = db.Consumibles.Find(id);
            if (consumibles == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "RazonSocial", consumibles.IdProveedor);
            return View(consumibles);
        }

        // POST: Consumibles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdConsumible,IdProveedor,Producto,Descripcion,Precio,Cantidad,FechaCompra,Observaciones")] Consumibles consumibles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumibles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "RazonSocial", consumibles.IdProveedor);
            return View(consumibles);
        }

        // GET: Consumibles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumibles consumibles = db.Consumibles.Find(id);
            if (consumibles == null)
            {
                return HttpNotFound();
            }
            return View(consumibles);
        }

        // POST: Consumibles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consumibles consumibles = db.Consumibles.Find(id);
            db.Consumibles.Remove(consumibles);
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
