using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPP2;

namespace CPP2.Controllers
{
    public class SessionLogsController : Controller
    {
        private CPPdatabaseEntities db = new CPPdatabaseEntities();

        // GET: SessionLogs
        public ActionResult Index()
        {
            var sessionLogs = db.SessionLogs.Include(s => s.CppUser).Include(s => s.CppUser1);
            return View(sessionLogs.ToList());
        }

        // GET: SessionLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionLog sessionLog = db.SessionLogs.Find(id);
            if (sessionLog == null)
            {
                return HttpNotFound();
            }
            return View(sessionLog);
        }

        // GET: SessionLogs/Create
        public ActionResult Create()
        {
            ViewBag.CallSenderId = new SelectList(db.CppUsers, "Id", "AspNetUserId");
            ViewBag.CallReceiverId = new SelectList(db.CppUsers, "Id", "AspNetUserId");
            return View();
        }

        // POST: SessionLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CallSenderId,SessionBegin,SessionEnd,CallReceiverId,SessionString")] SessionLog sessionLog)
        {
            if (ModelState.IsValid)
            {
                db.SessionLogs.Add(sessionLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CallSenderId = new SelectList(db.CppUsers, "Id", "AspNetUserId", sessionLog.CallSenderId);
            ViewBag.CallReceiverId = new SelectList(db.CppUsers, "Id", "AspNetUserId", sessionLog.CallReceiverId);
            return View(sessionLog);
        }

        // GET: SessionLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionLog sessionLog = db.SessionLogs.Find(id);
            if (sessionLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CallSenderId = new SelectList(db.CppUsers, "Id", "AspNetUserId", sessionLog.CallSenderId);
            ViewBag.CallReceiverId = new SelectList(db.CppUsers, "Id", "AspNetUserId", sessionLog.CallReceiverId);
            return View(sessionLog);
        }

        // POST: SessionLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CallSenderId,SessionBegin,SessionEnd,CallReceiverId,SessionString")] SessionLog sessionLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CallSenderId = new SelectList(db.CppUsers, "Id", "AspNetUserId", sessionLog.CallSenderId);
            ViewBag.CallReceiverId = new SelectList(db.CppUsers, "Id", "AspNetUserId", sessionLog.CallReceiverId);
            return View(sessionLog);
        }

        // GET: SessionLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionLog sessionLog = db.SessionLogs.Find(id);
            if (sessionLog == null)
            {
                return HttpNotFound();
            }
            return View(sessionLog);
        }

        // POST: SessionLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionLog sessionLog = db.SessionLogs.Find(id);
            db.SessionLogs.Remove(sessionLog);
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
