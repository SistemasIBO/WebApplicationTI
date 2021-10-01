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
    public class EquiposComputoController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: EquiposComputo
        public ActionResult Index()
        {
            var equipoComputo = db.EquipoComputo.Include(e => e.Usuario);
            return View(equipoComputo.ToList());
        }

        // GET: EquiposComputo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoComputo equipoComputo = db.EquipoComputo.Find(id);
            if (equipoComputo == null)
            {
                return HttpNotFound();
            }
            return View(equipoComputo);
        }

        // GET: EquiposComputo/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: EquiposComputo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEquipoComputo,IdUsuario,TipoDeEquipo,Marca,Modelo,NoSerie,CapidadDiscoDuro,MemoriaRAM,Estado,Observaciones")] EquipoComputo equipoComputo)
        {
            if (ModelState.IsValid)
            {
                db.EquipoComputo.Add(equipoComputo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", equipoComputo.IdUsuario);
            return View(equipoComputo);
        }

        // GET: EquiposComputo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoComputo equipoComputo = db.EquipoComputo.Find(id);
            if (equipoComputo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", equipoComputo.IdUsuario);
            return View(equipoComputo);
        }

        // POST: EquiposComputo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEquipoComputo,IdUsuario,TipoDeEquipo,Marca,Modelo,NoSerie,CapidadDiscoDuro,MemoriaRAM,Estado,Observaciones")] EquipoComputo equipoComputo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipoComputo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", equipoComputo.IdUsuario);
            return View(equipoComputo);
        }

        // GET: EquiposComputo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoComputo equipoComputo = db.EquipoComputo.Find(id);
            if (equipoComputo == null)
            {
                return HttpNotFound();
            }
            return View(equipoComputo);
        }

        // POST: EquiposComputo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipoComputo equipoComputo = db.EquipoComputo.Find(id);
            db.EquipoComputo.Remove(equipoComputo);
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
