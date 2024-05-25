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
    public class productosController : Controller
    {
        private NeptunoEntities db = new NeptunoEntities();

        // GET: productos
        public ActionResult Index()
        {
            var productos = db.productos.Include(p => p.categorias).Include(p => p.proveedores);
            return View(productos.ToList());
        }

        // GET: productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: productos/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria");
            ViewBag.idProveedor = new SelectList(db.proveedores, "idProveedor", "nombreCompañia");
            return View();
        }

        // POST: productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idproducto,nombreProducto,idProveedor,idCategoria,cantidadPorUnidad,precioUnidad,unidadesEnExistencia,unidadesEnPedido,nivelNuevoPedido,suspendido,categoriaProducto")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria", productos.idCategoria);
            ViewBag.idProveedor = new SelectList(db.proveedores, "idProveedor", "nombreCompañia", productos.idProveedor);
            return View(productos);
        }

        // GET: productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria", productos.idCategoria);
            ViewBag.idProveedor = new SelectList(db.proveedores, "idProveedor", "nombreCompañia", productos.idProveedor);
            return View(productos);
        }

        // POST: productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idproducto,nombreProducto,idProveedor,idCategoria,cantidadPorUnidad,precioUnidad,unidadesEnExistencia,unidadesEnPedido,nivelNuevoPedido,suspendido,categoriaProducto")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria", productos.idCategoria);
            ViewBag.idProveedor = new SelectList(db.proveedores, "idProveedor", "nombreCompañia", productos.idProveedor);
            return View(productos);
        }

        // GET: productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
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
