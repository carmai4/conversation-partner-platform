using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using CPP2;

namespace CPP2.Controllers
{
    public class ScheduleController : Controller
    {
        private CPPdatabaseEntities db = new CPPdatabaseEntities();

        // GET: Schedules
        public ActionResult Index()
        {

            String user = User.Identity.GetUserId();
            var latest = (from p in db.CppUsers
                          where
                          p.AspNetUserId == user
                          select new
                          {
                              p.Id
                          }).FirstOrDefault();

            //Current User's ID
            var id = latest.Id;
            var availabilitySchedules = db.AvailabilitySchedules.Where(a => a.CppUserId == id);
            return View(availabilitySchedules.ToList());
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CppUserId,Sunday0,Sunday1,Sunday2,Sunday3,Sunday4,Sunday5,Sunday6,Sunday7,Sunday8,Sunday9,Sunday10,Sunday11,Sunday12,Sunday13,Sunday14,Sunday15,Sunday16,Sunday17,Sunday18,Sunday19,Sunday20,Sunday21,Sunday22,Sunday23,Mon0,Mon1,Mon2,Mon3,Mon4,Mon5,Mon6,Mon7,Mon8,Mon9,Mon10,Mon11,Mon12,Mon13,Mon14,Mon15,Mon16,Mon17,Mon18,Mon19,Mon20,Mon21,Mon22,Mon23,Tue0,Tue1,Tue2,Tue3,Tue4,Tue5,Tue6,Tue7,Tue8,Tue9,Tue10,Tue11,Tue12,Tue13,Tue14,Tue15,Tue16,Tue17,Tue18,Tue19,Tue20,Tue21,Tue22,Tue23,Wed0,Wed1,Wed2,Wed3,Wed4,Wed5,Wed6,Wed7,Wed8,Wed9,Wed10,Wed11,Wed12,Wed13,Wed14,Wed15,Wed16,Wed17,Wed18,Wed19,Wed20,Wed21,Wed22,Wed23,Thu0,Thu1,Thu2,Thu3,Thu4,Thu5,Thu6,Thu7,Thu8,Thu9,Thu10,Thu11,Thu12,Thu13,Thu14,Thu15,Thu16,Thu17,Thu18,Thu19,Thu20,Thu21,Thu22,Thu23,Fri0,Fri1,Fri2,Fri3,Fri4,Fri5,Fri6,Fri7,Fri8,Fri9,Fri10,Fri11,Fri12,Fri13,Fri14,Fri15,Fri16,Fri17,Fri18,Fri19,Fri20,Fri21,Fri22,Fri23,Sat0,Sat1,Sat2,Sat3,Sat4,Sat5,Sat6,Sat7,Sat8,Sat9,Sat10,Sat11,Sat12,Sat13,Sat14,Sat15,Sat16,Sat17,Sat18,Sat19,Sat20,Sat21,Sat22,Sat23")] AvailabilitySchedule availabilitySchedule)
        {
            if (ModelState.IsValid)
            {
                String user = User.Identity.GetUserId();
                var latest = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id
                              }).FirstOrDefault();

                //var result = db.AvailabilitySchedules.OrderByDescending(x => x.Id).First();


                //Current User's ID
                var id = latest.Id;
                // int max2 = Int32.Parse(result.Id.ToString());

                availabilitySchedule.CppUserId = id;
                //availabilitySchedule.Id = max2 + 1;
                db.AvailabilitySchedules.Add(availabilitySchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId", availabilitySchedule.CppUserId);
            return View(availabilitySchedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit()
        {

            String user = User.Identity.GetUserId();
            var latest = (from p in db.CppUsers
                          where
                          p.AspNetUserId == user
                          select new
                          {
                              p.Id
                          }).First();

            //Current User's ID
            var id = latest.Id;

            var latest1 = db.AvailabilitySchedules.Where(b => b.CppUserId == id).ToList().FirstOrDefault();

            if (latest1 == null)
            {
                return Redirect("Index");
            }
            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId", latest1.CppUserId);
            return View(latest1);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CppUserId,Sunday0,Sunday1,Sunday2,Sunday3,Sunday4,Sunday5,Sunday6,Sunday7,Sunday8,Sunday9,Sunday10,Sunday11,Sunday12,Sunday13,Sunday14,Sunday15,Sunday16,Sunday17,Sunday18,Sunday19,Sunday20,Sunday21,Sunday22,Sunday23,Mon0,Mon1,Mon2,Mon3,Mon4,Mon5,Mon6,Mon7,Mon8,Mon9,Mon10,Mon11,Mon12,Mon13,Mon14,Mon15,Mon16,Mon17,Mon18,Mon19,Mon20,Mon21,Mon22,Mon23,Tue0,Tue1,Tue2,Tue3,Tue4,Tue5,Tue6,Tue7,Tue8,Tue9,Tue10,Tue11,Tue12,Tue13,Tue14,Tue15,Tue16,Tue17,Tue18,Tue19,Tue20,Tue21,Tue22,Tue23,Wed0,Wed1,Wed2,Wed3,Wed4,Wed5,Wed6,Wed7,Wed8,Wed9,Wed10,Wed11,Wed12,Wed13,Wed14,Wed15,Wed16,Wed17,Wed18,Wed19,Wed20,Wed21,Wed22,Wed23,Thu0,Thu1,Thu2,Thu3,Thu4,Thu5,Thu6,Thu7,Thu8,Thu9,Thu10,Thu11,Thu12,Thu13,Thu14,Thu15,Thu16,Thu17,Thu18,Thu19,Thu20,Thu21,Thu22,Thu23,Fri0,Fri1,Fri2,Fri3,Fri4,Fri5,Fri6,Fri7,Fri8,Fri9,Fri10,Fri11,Fri12,Fri13,Fri14,Fri15,Fri16,Fri17,Fri18,Fri19,Fri20,Fri21,Fri22,Fri23,Sat0,Sat1,Sat2,Sat3,Sat4,Sat5,Sat6,Sat7,Sat8,Sat9,Sat10,Sat11,Sat12,Sat13,Sat14,Sat15,Sat16,Sat17,Sat18,Sat19,Sat20,Sat21,Sat22,Sat23")] AvailabilitySchedule availabilitySchedule)
        {
            if (ModelState.IsValid)
            {
                String user = User.Identity.GetUserId();
                var latest = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id
                              }).First();

                //Current User's ID
                var id = latest.Id;

                availabilitySchedule.CppUserId = id;


                //var result = db.AvailabilitySchedules.Where(a => a.CppUserId == id).First();
                //int theid = Int32.Parse(result.Id.ToString());
                //availabilitySchedule.Id = theid;
                db.Entry(availabilitySchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId", availabilitySchedule.CppUserId);
            return View(availabilitySchedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailabilitySchedule availabilitySchedule = db.AvailabilitySchedules.Find(id);
            if (availabilitySchedule == null)
            {
                return HttpNotFound();
            }
            return View(availabilitySchedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AvailabilitySchedule availabilitySchedule = db.AvailabilitySchedules.Find(id);
            db.AvailabilitySchedules.Remove(availabilitySchedule);
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