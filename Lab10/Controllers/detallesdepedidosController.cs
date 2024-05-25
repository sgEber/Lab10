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
    public class detallesdepedidosController : Controller
    {
        private NeptunoEntities db = new NeptunoEntities();

        // GET: detallesdepedidos
        public ActionResult Index()
        {
            var detallesdepedidos = db.detallesdepedidos.Include(d => d.Pedidos).Include(d => d.productos);
            return View(detallesdepedidos.ToList());
        }

        // GET: detallesdepedidos/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallesdepedidos detallesdepedidos = db.detallesdepedidos.Find(id);
            if (detallesdepedidos == null)
            {
                return HttpNotFound();
            }
            return View(detallesdepedidos);
        }

        // GET: detallesdepedidos/Create
        public ActionResult Create()
        {
            ViewBag.idpedido = new SelectList(db.Pedidos, "IdPedido", "IdCliente");
            ViewBag.idproducto = new SelectList(db.productos, "idproducto", "nombreProducto");
            return View();
        }

        // POST: detallesdepedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idpedido,idproducto,preciounidad,cantidad,descuento")] detallesdepedidos detallesdepedidos)
        {
            if (ModelState.IsValid)
            {
                db.detallesdepedidos.Add(detallesdepedidos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idpedido = new SelectList(db.Pedidos, "IdPedido", "IdCliente", detallesdepedidos.idpedido);
            ViewBag.idproducto = new SelectList(db.productos, "idproducto", "nombreProducto", detallesdepedidos.idproducto);
            return View(detallesdepedidos);
        }

        // GET: detallesdepedidos/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallesdepedidos detallesdepedidos = db.detallesdepedidos.Find(id);
            if (detallesdepedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idpedido = new SelectList(db.Pedidos, "IdPedido", "IdCliente", detallesdepedidos.idpedido);
            ViewBag.idproducto = new SelectList(db.productos, "idproducto", "nombreProducto", detallesdepedidos.idproducto);
            return View(detallesdepedidos);
        }

        // POST: detallesdepedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idpedido,idproducto,preciounidad,cantidad,descuento")] detallesdepedidos detallesdepedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallesdepedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idpedido = new SelectList(db.Pedidos, "IdPedido", "IdCliente", detallesdepedidos.idpedido);
            ViewBag.idproducto = new SelectList(db.productos, "idproducto", "nombreProducto", detallesdepedidos.idproducto);
            return View(detallesdepedidos);
        }

        // GET: detallesdepedidos/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallesdepedidos detallesdepedidos = db.detallesdepedidos.Find(id);
            if (detallesdepedidos == null)
            {
                return HttpNotFound();
            }
            return View(detallesdepedidos);
        }

        // POST: detallesdepedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            detallesdepedidos detallesdepedidos = db.detallesdepedidos.Find(id);
            db.detallesdepedidos.Remove(detallesdepedidos);
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
