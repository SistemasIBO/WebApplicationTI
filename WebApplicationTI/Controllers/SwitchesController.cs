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
    public class SwitchesController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Switches
        public ActionResult Index()
        {
            var swi = db.Switch.Include(sw => sw.Depto);
            return View(swi.ToList());
        }

        // GET: Switches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Switch @switch = db.Switch.Find(id);
            if (@switch == null)
            {
                return HttpNotFound();
            }
            return View(@switch);
        }

        // GET: Switches/Create
        public ActionResult Create()
        {
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento");
            return View();
        }

        // POST: Switches/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSwitch,IdDepto,Marca,Modelo,NoSerie,NoPuertoEthernet,Estado,Observaciones")] Switch @switch)
        {
            if (ModelState.IsValid)
            {
                db.Switch.Add(@switch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", @switch.IdDepto);
            return View(@switch);
        }

        // GET: Switches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Switch @switch = db.Switch.Find(id);
            if (@switch == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", @switch.IdDepto);
            return View(@switch);
        }

        // POST: Switches/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSwitch,IdDepto,Marca,Modelo,NoSerie,NoPuertoEthernet,Estado,Observaciones")] Switch @switch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@switch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", @switch.IdDepto);
            return View(@switch);
        }

        // GET: Switches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Switch @switch = db.Switch.Find(id);
            if (@switch == null)
            {
                return HttpNotFound();
            }
            return View(@switch);
        }

        // POST: Switches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Switch @switch = db.Switch.Find(id);
            db.Switch.Remove(@switch);
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
