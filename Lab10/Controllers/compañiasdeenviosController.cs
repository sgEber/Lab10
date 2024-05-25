using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab10.Models;

namespace Lab10.Controllers
{
    public class compañiasdeenviosController : Controller
    {
        private NeptunoEntities db = new NeptunoEntities();

        // GET: compañiasdeenvios
        public ActionResult Index()
        {
            return View(db.compañiasdeenvios.ToList());
        }

        // GET: compañiasdeenvios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compañiasdeenvios compañiasdeenvios = db.compañiasdeenvios.Find(id);
            if (compañiasdeenvios == null)
            {
                return HttpNotFound();
            }
            return View(compañiasdeenvios);
        }

        // GET: compañiasdeenvios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: compañiasdeenvios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompañiaEnvios,nombreCompañia,telefono")] compañiasdeenvios compañiasdeenvios)
        {
            if (ModelState.IsValid)
            {
                db.compañiasdeenvios.Add(compañiasdeenvios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compañiasdeenvios);
        }

        // GET: compañiasdeenvios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compañiasdeenvios compañiasdeenvios = db.compañiasdeenvios.Find(id);
            if (compañiasdeenvios == null)
            {
                return HttpNotFound();
            }
            return View(compañiasdeenvios);
        }

        // POST: compañiasdeenvios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompañiaEnvios,nombreCompañia,telefono")] compañiasdeenvios compañiasdeenvios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compañiasdeenvios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compañiasdeenvios);
        }

        // GET: compañiasdeenvios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compañiasdeenvios compañiasdeenvios = db.compañiasdeenvios.Find(id);
            if (compañiasdeenvios == null)
            {
                return HttpNotFound();
            }
            return View(compañiasdeenvios);
        }

        // POST: compañiasdeenvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            compañiasdeenvios compañiasdeenvios = db.compañiasdeenvios.Find(id);
            db.compañiasdeenvios.Remove(compañiasdeenvios);
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
