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
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Web.UI.WebControls;

namespace CPP2.Controllers
{
    public class ProfilesController : Controller
    {
        private CPPdatabaseEntities db = new CPPdatabaseEntities();

        // GET: Profiles
        public ActionResult Index()
        {
            //query for user ID
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
            // load profile
            var current = db.Profiles.Where(b => b.CppUserId == id);
            IEnumerable<CppUser> list1 = from b in current
                                         join d in db.CppUsers on b.CppUserId equals d.Id
                                         select d;
            return View(list1.ToList());
        }
        [HttpGet]
        public ActionResult AddImage()
        {

            Profile p1 = new CPP2.Profile();
            return View(p1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(HttpPostedFileBase image1)
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
            // load profile
            Profile p1 = db.Profiles.Where(b => b.CppUserId == id).ToList().FirstOrDefault();

            // if not image exist load image into profile
            if (image1 != null)
            {
                p1.Photo = new byte[image1.ContentLength];
                image1.InputStream.Read(p1.Photo, 0, image1.ContentLength);
            }
            db.Entry(p1).State = EntityState.Modified;
            db.SaveChanges();
            return View(p1);
        }
        // GET: Profiles/Create
        public ActionResult Create()
        {
            String user = User.Identity.GetUserId();
            var latest = (from p in db.CppUsers
                          where
                          p.AspNetUserId == user
                          select new
                          {
                              p.Id
                          }).First();
            // current user Id
            var id1 = latest.Id;
            // check if Profile Id exist
            var profileId = (from p1 in db.Profiles
                             where
                             p1.CppUserId == id1
                             select new
                             {
                                 p1.Id
                             }).Count();
            //If profile Id exist navagate to Index page
            if (profileId != 0)
            {
                return RedirectToAction("Index");
            }
            // otherwise contiue to Create page
            var interests = db.UserInterests.Select(x =>
                                new
                                {
                                    Value = x.Interest.ToString(),
                                    Text = x.Interest.ToString()
                                });
            ViewBag.interestList = interests;
            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId");
            return View();
        }
        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Gender,Age,PrimaryLanguge,LanguageLearning,AverageRating,SpeakingAbility,ComprehensionAbility,City,State,Country,TimeZoneCode,Desired_Hours,Interest1,Interest2,Interest3,Interest4,Interest5,PreferredGender")] Profile profile)
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
                //Current User's ID
                var id1 = latest.Id;

                profile.CppUserId = id1;

                db.Profiles.Add(profile);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId", profile.CppUserId);
            return View(profile);
        }
        // GET: Profiles/Edit/5
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
            // load profile
            var latest1 = db.Profiles.Where(b => b.CppUserId == id).ToList().FirstOrDefault();

            var interests = db.UserInterests.Select(x =>
                    new
                    {
                        Value = x.Interest.ToString(),
                        Text = x.Interest.ToString()
                    });

            ViewBag.interestList = interests;

            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId", latest1.CppUserId);
            return View(latest1);
        }
        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age,Gender,PrimaryLanguge,LanguageLearning,SpeakingAbility,ComprehensionAbility,City,State,Country,TimeZoneCode,Desired_Hours,Interest1,Interest2,Interest3,Interest4,Interest5,PreferredGender")] Profile profile)
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
                //Current User's ID
                var id1 = latest.Id;
                // loads previoius saved photo
                byte[] photo;
                var photoArray = (from p1 in db.Profiles
                                  where
                                  p1.CppUserId == id1
                                  select new
                                  {
                                      p1.Photo
                                  });
                // Current Profiles photo
                photo = photoArray.First().Photo;

                // load current photo into Profile
                profile.CppUserId = id1;
                profile.Photo = photo;
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CppUserId = new SelectList(db.CppUsers, "Id", "AspNetUserId", profile.CppUserId);
            return View(profile);
        }
        public ActionResult Admin()
        {

            String user = User.Identity.GetUserId();
            var AdminCheck = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id,
                                  p.PermissionLevel
                              }).FirstOrDefault();

            if (AdminCheck.PermissionLevel == 1)
            {
                return View();
            }
            return HttpNotFound();
        }
        public ActionResult AdminInterests()
        {

            String user = User.Identity.GetUserId();
            var AdminCheck = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id,
                                  p.PermissionLevel
                              }).FirstOrDefault();

            if (AdminCheck.PermissionLevel == 1)
            {
                return View(db.UserInterests.ToList());
            }
            return HttpNotFound();
        }


        // GET: Admin/AddInterest
        public ActionResult AddInterest()
        {
            String user = User.Identity.GetUserId();
            var AdminCheck = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id,
                                  p.PermissionLevel
                              }).FirstOrDefault();

            if (AdminCheck.PermissionLevel == 1)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Admin/AddInterest
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInterest([Bind(Include = "Interest")] UserInterest userInterest)
        {
            if (ModelState.IsValid)
            {
                db.UserInterests.Add(userInterest);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(userInterest);
        }

        // GET: Admin/Edit/5
        public ActionResult EditInterest(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            String user = User.Identity.GetUserId();
            var AdminCheck = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id,
                                  p.PermissionLevel
                              }).FirstOrDefault();

            if (AdminCheck.PermissionLevel == 1)
            {
                UserInterest userInterest = db.UserInterests.Find(id);
                if (userInterest == null)
                {
                    return HttpNotFound();
                }

                return View(userInterest);
            }
            else
            {
                return HttpNotFound();
            }


        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInterest([Bind(Include = "Id,Interest")] UserInterest userInterest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInterest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(userInterest);
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteInterest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            String user = User.Identity.GetUserId();
            var AdminCheck = (from p in db.CppUsers
                              where
                              p.AspNetUserId == user
                              select new
                              {
                                  p.Id,
                                  p.PermissionLevel
                              }).FirstOrDefault();

            if (AdminCheck.PermissionLevel == 1)
            {
                UserInterest userInterest = db.UserInterests.Find(id);
                if (userInterest == null)
                {
                    return HttpNotFound();
                }
                return View(userInterest);
            }
            else
            {
                return HttpNotFound();
            }

        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("DeleteInterest")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInterestConfirmed(int id)
        {
            UserInterest userInterest = db.UserInterests.Find(id);
            db.UserInterests.Remove(userInterest);
            db.SaveChanges();
            return RedirectToAction("Admin");
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