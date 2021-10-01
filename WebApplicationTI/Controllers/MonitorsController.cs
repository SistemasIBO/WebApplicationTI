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
    public class MonitorsController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Monitors
        public ActionResult Index()
        {
            var monitor = db.Monitor.Include(m => m.EquipoComputo);
            return View(monitor.ToList());
        }

        // GET: Monitors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monitor monitor = db.Monitor.Find(id);
            if (monitor == null)
            {
                return HttpNotFound();
            }
            return View(monitor);
        }

        // GET: Monitors/Create
        public ActionResult Create()
        {
            ViewBag.IdEquipoComputo = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Monitors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMonitor,IdEquipoComputo,Marca,Modelo,NoSerie,Pulgada,NoPuertoVGA,NoPuertoHDMI,Estado,Observaciones")] Monitor monitor)
        {
            if (ModelState.IsValid)
            {
                var idE = (from userlist in db.EquipoComputo
                           where userlist.IdUsuario == monitor.IdEquipoComputo
                           select new
                           {
                               userlist.IdEquipoComputo
                           });

                monitor.IdEquipoComputo = idE.FirstOrDefault().IdEquipoComputo;
                db.Monitor.Add(monitor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEquipoComputo = new SelectList(db.Usuario, "IdUsuario", "Nombre");//, monitor.IdUsuario);
            return View(monitor);
        }

        // GET: Monitors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monitor monitor = db.Monitor.Find(id);
            if (monitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEquipoComputo = new SelectList(db.Usuario, "IdUsuario", "Nombre");//, monitor.IdUsuario);
            return View(monitor);
        }

        // POST: Monitors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMonitor,IdEquipoComputo,Marca,Modelo,NoSerie,Pulgada,NoPuertoVGA,NoPuertoHDMI,Estado,Observaciones")] Monitor monitor)
        {
            if (ModelState.IsValid)
            {
                var idE = (from userlist in db.EquipoComputo
                           where userlist.IdUsuario == monitor.IdEquipoComputo
                           select new
                           {
                               userlist.IdEquipoComputo
                           });

                monitor.IdEquipoComputo = idE.FirstOrDefault().IdEquipoComputo;
                db.Entry(monitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEquipoComputo = new SelectList(db.Usuario, "IdUsuario", "Nombre");//, monitor.IdUsuario);
            return View(monitor);
        }

        // GET: Monitors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monitor monitor = db.Monitor.Find(id);
            if (monitor == null)
            {
                return HttpNotFound();
            }
            return View(monitor);
        }

        // POST: Monitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Monitor monitor = db.Monitor.Find(id);
            db.Monitor.Remove(monitor);
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
