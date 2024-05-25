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
    public class PedidosController : Controller
    {
        private NeptunoEntities db = new NeptunoEntities();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.clientes).Include(p => p.compañiasdeenvios).Include(p => p.Empleados);
            return View(pedidos.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.clientes, "idCliente", "NombreCompañia");
            ViewBag.FormaEnvio = new SelectList(db.compañiasdeenvios, "idCompañiaEnvios", "nombreCompañia");
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos");
            return View();
        }

        // POST: Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPedido,IdCliente,IdEmpleado,FechaPedido,FechaEntrega,FechaEnvio,FormaEnvio,Cargo,Destinatario,DireccionDestinatario,CiudadDestinatario,RegionDestinatario,CodPostalDestinatario,PaisDestinatario")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedidos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.clientes, "idCliente", "NombreCompañia", pedidos.IdCliente);
            ViewBag.FormaEnvio = new SelectList(db.compañiasdeenvios, "idCompañiaEnvios", "nombreCompañia", pedidos.FormaEnvio);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", pedidos.IdEmpleado);
            return View(pedidos);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.clientes, "idCliente", "NombreCompañia", pedidos.IdCliente);
            ViewBag.FormaEnvio = new SelectList(db.compañiasdeenvios, "idCompañiaEnvios", "nombreCompañia", pedidos.FormaEnvio);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", pedidos.IdEmpleado);
            return View(pedidos);
        }

        // POST: Pedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPedido,IdCliente,IdEmpleado,FechaPedido,FechaEntrega,FechaEnvio,FormaEnvio,Cargo,Destinatario,DireccionDestinatario,CiudadDestinatario,RegionDestinatario,CodPostalDestinatario,PaisDestinatario")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.clientes, "idCliente", "NombreCompañia", pedidos.IdCliente);
            ViewBag.FormaEnvio = new SelectList(db.compañiasdeenvios, "idCompañiaEnvios", "nombreCompañia", pedidos.FormaEnvio);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", pedidos.IdEmpleado);
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedidos pedidos = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedidos);
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
