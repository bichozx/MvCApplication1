using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvCApplication1;

namespace MvCApplication1.Controllers
{
    public class citasController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: citas
        public ActionResult Index()
        {
            var cita = db.cita.Include(c => c.medico).Include(c => c.paciente);
            return View(cita.ToList());
        }

        // GET: citas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cita cita = db.cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: citas/Create
        public ActionResult Create()
        {
            ViewBag.id_medico = new SelectList(db.medico, "id_medico", "nom_medico");
            ViewBag.Id_paciente = new SelectList(db.paciente, "Id_paciente", "tip_doc");
            return View();
        }

        // POST: citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_cita,fecha,hora,Id_paciente,id_medico,valor,diagnostico,Nom_acompanante,activo")] cita cita)
        {
            if (ModelState.IsValid)
            {
                db.cita.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_medico = new SelectList(db.medico, "id_medico", "nom_medico", cita.id_medico);
            ViewBag.Id_paciente = new SelectList(db.paciente, "Id_paciente", "tip_doc", cita.Id_paciente);
            return View(cita);
        }

        // GET: citas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cita cita = db.cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_medico = new SelectList(db.medico, "id_medico", "nom_medico", cita.id_medico);
            ViewBag.Id_paciente = new SelectList(db.paciente, "Id_paciente", "tip_doc", cita.Id_paciente);
            return View(cita);
        }

        // POST: citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_cita,fecha,hora,Id_paciente,id_medico,valor,diagnostico,Nom_acompanante,activo")] cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_medico = new SelectList(db.medico, "id_medico", "nom_medico", cita.id_medico);
            ViewBag.Id_paciente = new SelectList(db.paciente, "Id_paciente", "tip_doc", cita.Id_paciente);
            return View(cita);
        }

        // GET: citas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cita cita = db.cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            cita cita = db.cita.Find(id);
            db.cita.Remove(cita);
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
