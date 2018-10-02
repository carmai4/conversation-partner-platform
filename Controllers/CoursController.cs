using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPP2;
using Microsoft.AspNet.Identity;

namespace CPP2.Controllers
{
    public class CoursController : Controller
    {
        private CPPdatabaseEntities mDB = new CPPdatabaseEntities();

        // GET: Cours
        public ActionResult Index()
        {
            //var courses = mDB.Courses.Include(c => c.CppUser);
            var sortedCourses = (from c in mDB.Courses orderby c.CourseNum, c.Semester ascending 
                                 select c  );

            //return View(courses.ToList());
            return View(sortedCourses.ToList());
        }

        public bool isAdmin()
        {
            String user = User.Identity.GetUserId();
            var AdminCheck = (from p in mDB.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id,
                                  p.PermissionLevel
                              }).FirstOrDefault();

            if (AdminCheck.PermissionLevel == 1)
            {
                return true;
            }
            return false;
        }

        // GET: Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = mDB.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            if(isAdmin())
            {
                return View(cours);
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "Details"));

        }

        //returns true if a course-student combination already exists
        public bool coursExists(int courseNum, int studentCppId)
        {

            var existingCours = mDB.Courses.Where(c => c.CourseNum == courseNum);

            foreach (var c1 in existingCours.ToList())
            {
                if ( c1.CppUserId == studentCppId || c1.CppUser.Id == studentCppId )
                {
                    return true;
                }
            }

            return false;
        }

        // GET: Cours/Create
        public ActionResult Create()
        {
            if(isAdmin())
            {
                ViewBag.CppUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId");

                var itemz = new List<SelectListItem>();
                foreach (var user in mDB.CppUsers)
                {
                    var namestring = "No Name; Id=" + user.Id.ToString();
                    if (user.Profiles.Count > 0 && user.Profiles.SingleOrDefault().FirstName.Length > 1 && user.Profiles.SingleOrDefault().LastName.Length > 1)
                    {
                        namestring = user.Profiles.SingleOrDefault().FirstName + " " + user.Profiles.SingleOrDefault().LastName;
                    }
                    itemz.Add(new SelectListItem { Text = namestring, Value = user.Id.ToString() });
                };

                ViewBag.CppUserList = itemz;
                
                return View();
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "Create"));
        }

        // POST: Cours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,CourseNum,Semester,CourseName,CppUserId")] Cours cours)
        {
            if(isAdmin())
            {
                if (ModelState.IsValid)
                {
                    //check if entry already exists i.e. given student already enrolled in given course number
                    if(coursExists(cours.CourseNum , cours.CppUserId))
                    {
                        Exception eAlreadyExists = new Exception();
                        return View("Error", new HandleErrorInfo(eAlreadyExists, "Cours", "Create"));
                    }

                    //assigning cours name depending on course number chosen
                    if(cours.CourseNum == 40)
                    {
                        cours.CourseName = "UG MET READING&SPEAKING";
                    }
                    else if(cours.CourseNum == 42)
                    {
                        cours.CourseName = "UNDERGRADUATE MET WRITING";
                    }
                    else if (cours.CourseNum == 50)
                    {
                        cours.CourseName = "GRADUATE BRIDGE PRONUNCIATION";
                    }
                    else if (cours.CourseNum == 52)
                    {
                        cours.CourseName = "GRADUATE MET WRITING";
                    }
                    else
                    {
                        cours.CourseName = "No Course Name Found";
                    }

                    mDB.Courses.Add(cours);
                    mDB.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CppUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId", cours.CppUserId);
                return View(cours);
                }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "Create"));
        }

        // GET: Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if(isAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cours cours = mDB.Courses.Find(id);
                if (cours == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CppUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId", cours.CppUserId);
                return View(cours);
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "Edit"));
        }

        // POST: Cours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,CourseNum,Semester,CourseName,CppUserId")] Cours cours)
        {
            if(isAdmin())
            {
                if (ModelState.IsValid)
                {
                    mDB.Entry(cours).State = EntityState.Modified;
                    mDB.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CppUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId", cours.CppUserId);
                return View(cours);
                }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "Edit"));
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if(isAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cours cours = mDB.Courses.Find(id);
                if (cours == null)
                {
                    return HttpNotFound();
                }
                return View(cours);
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "Delete"));
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(isAdmin())
            {
                Cours cours = mDB.Courses.Find(id);
                mDB.Courses.Remove(cours);
                mDB.SaveChanges();
                return RedirectToAction("Index");
            }
            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Cours", "DeleteConfirmed"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                mDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
