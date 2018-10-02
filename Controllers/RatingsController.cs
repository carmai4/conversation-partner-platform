using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using CPP2;
using CPP2.Services;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace CPP2.Controllers
{
    public class RatingsController : Controller
    {
        private CPPdatabaseEntities mDB = new CPPdatabaseEntities();
        SqlConnection mSqlConn = new SqlConnection();
        DataSet mDS = new DataSet();
        SqlDataAdapter mSqlDA = new SqlDataAdapter();
        SqlCommandBuilder mSqlCB = new SqlCommandBuilder();


        // GET: Ratings
        public ActionResult Index()
        {
            String user = User.Identity.GetUserId();
            var latest = (from p in mDB.CppUsers
                          where
                          p.AspNetUserId == user
                          select new
                          {
                              p.Id
                          }).First();

            //Current User's ID
            var id1 = latest.Id;

            //Find ratings given by current user
            var latest1 = mDB.Ratings.Where(b => b.RaterId == id1);

            return View(latest1.ToList());
        }


        /*
         * returns true if user is admin level
         * */
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

            if(AdminCheck.PermissionLevel == 1)
            {
                return true;
            }
            return false;

        }
       

        /*
         * Admin may view and delete all existing ratings
         */
        public ActionResult AdminRating()
        {
            if(isAdmin())
            {
                var rList = mDB.Ratings;
                return View(rList.ToList());
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Ratings", "AdminRating"));
        }

        /*
         * 
         */
        public bool openSqlConnection()
        {
            mSqlConn.ConnectionString = "Server=conversationpartnersplatform.database.windows.net; Database=CPPdatabase;UID=redteam411;PWD=Redteam17";

            try
            {
                mSqlConn.Open();
                return true;
            }
            catch(Exception eOpenSqlConnection)
            {
                throw new Exception("throw eOpenSqlConnection", eOpenSqlConnection);
            }
        }

        public bool closeSqlConnection()
        {
            try
            {
                mSqlConn.Close();
                return true;
            }
            catch(Exception eCloseSqlConnection)
            {
                throw new Exception("throw eCloseSqlConnection", eCloseSqlConnection);
            }
        }


        public ActionResult AdminDetails(int? id)
        {
            if(isAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rating rating = mDB.Ratings.Find(id);
                if (rating == null)
                {
                    return HttpNotFound();
                }
                return View(rating);
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Ratings", "AdminDetails"));

        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = mDB.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }



        //TODO: find default definition of Create() for rating from a previous commit
        //copy it here 
        //must be able to select raterId and ratedId
        public ActionResult AdminCreateRating()
        {
            if(isAdmin())
            {
                ViewBag.RatedUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId");
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
            return View("Error", new HandleErrorInfo(eNotAdmin, "Ratings", "AdminCreateRating"));
        }

        /*
         * Admin may create a rating
         * */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreateRating([Bind(Include = "Id,RaterId,RatedUserId,Comment,InterestingScore,ComprehensibleScore,PolitenessScore")] Rating rating)
        {
            if (isAdmin())
            {
                if (ModelState.IsValid)
                {
                    //store raterId
                    //var raterIdToStore = CppUserService.GetCppUserId(User.Identity.GetUserId(), mDB);
                    var ratedIdToStore = rating.RatedUserId;

                    //raterId must not equal ratedUserId
                    if (rating.RatedUserId.Equals(rating.RaterId))
                    {
                        Exception eCreate = new Exception("RatedUserId == RaterId");
                        return View("Error", new HandleErrorInfo(eCreate, "Ratings", "AdminCreateRating"));
                    }

                    //WRITE A NEW RATINGEXISTS() with two parameters
                    //both raterid and ratedid


                    //if this particular rater-ratee combination exists
                    //then remove that rating first to replace it with a new rating
                    if (ratingExists(rating.RaterId, rating.RatedUserId))
                    {
                        var toRemove = (from r in mDB.Ratings
                                        where
                                        r.RaterId == rating.RaterId && r.RatedUserId == ratedIdToStore
                                        select new
                                        {
                                            r.Id
                                        }).FirstOrDefault();

                        AdminDeleteConfirmed(toRemove.Id);
                    }

                    //compute average rating from user input
                    decimal average;
                    average = (rating.InterestingScore + rating.ComprehensibleScore + rating.PolitenessScore) / (decimal)3;
                    rating.AverageScore = average;

                    //comment shall indicate this is created by admin
                    rating.Comment += " (created by admin)";

                    mDB.Ratings.Add(rating);
                    mDB.SaveChanges();

                    return RedirectToAction("AdminRating");
                }

                ViewBag.RatedUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId", rating.RatedUserId);

                return View(rating);
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Ratings", "AdminCreateRating"));
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.RatedUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId");
            var itemz = new List<SelectListItem>();
            foreach( var user in mDB.CppUsers)
            {
                var namestring = "No Name; Id=" + user.Id.ToString();
                if(user.Profiles.Count > 0 && user.Profiles.SingleOrDefault().FirstName.Length > 1 && user.Profiles.SingleOrDefault().LastName.Length > 1)
                {
                    namestring = user.Profiles.SingleOrDefault().FirstName + " " + user.Profiles.SingleOrDefault().LastName;
                }
                itemz.Add(new SelectListItem { Text = namestring, Value = user.Id.ToString() });
            };

            ViewBag.CppUserList = itemz;
            return View();
        }

        /*
         * returns true if a rating already exists between two users
         * users passed as Id parameters
         */
        public bool ratingExists(int ratedId)
        {
            int raterId = CppUserService.GetCppUserId(User.Identity.GetUserId(), mDB);

            var pastRatings = mDB.Ratings.Where(b => b.RaterId == raterId);

            foreach (var r1 in pastRatings.ToList())
            {
                if (r1.RatedUserId == ratedId)
                {
                    return true;
                }
            }

            return false;
        }

        //to be used by Admin functions
        public bool ratingExists(int raterId, int ratedId)
        {
            
            var pastRatings = mDB.Ratings.Where(b => b.RaterId == raterId);

            foreach (var r1 in pastRatings.ToList())
            {
                if (r1.RatedUserId == ratedId)
                {
                    return true;
                }
            }

            return false;
        }
        /*
         * returns true if successfully updated given user's average rating 
         * to be shown in their profile
         * */
        public bool updateUserAverage(int userId)
        {
            return false;
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RaterId,RatedUserId,Comment,InterestingScore,ComprehensibleScore,PolitenessScore")] Rating rating)
        {
            //check in session log table to see if two users ever had a session 
                //if not, rating cannot be created
    
            if (ModelState.IsValid) 
            {  
                //store raterId
                var raterIdToStore = CppUserService.GetCppUserId(User.Identity.GetUserId() , mDB);
                var ratedIdToStore = rating.RatedUserId;

                //raterId must not equal ratedUserId
                if (raterIdToStore.Equals(ratedIdToStore))
                {
                    Exception eCreate = new Exception("RatedUserId == RaterId");
                    return View("Error", new HandleErrorInfo(eCreate, "Ratings", "Create"));
                }

                //if this particular rater-ratee combination exists
                //then remove that rating first to replace it with a new rating
                if (ratingExists(ratedIdToStore))
                {
                    var toRemove = (from r in mDB.Ratings
                                    where
                                    r.RaterId == raterIdToStore && r.RatedUserId == ratedIdToStore
                                    select new
                                    {
                                        r.Id
                                    }).FirstOrDefault();

                    DeleteConfirmed(toRemove.Id);
                }

                rating.RaterId = raterIdToStore;
                rating.RaterId = raterIdToStore;

                //compute average rating from user input
                decimal average;
                average = (rating.InterestingScore + rating.ComprehensibleScore + rating.PolitenessScore) / (decimal)3;
                rating.AverageScore = average;

                mDB.Ratings.Add(rating);
                mDB.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.RatedUserId = new SelectList(mDB.CppUsers, "Id", "AspNetUserId", rating.RatedUserId);
            return View(rating);
        }

        public ActionResult AdminDeleteRating(int? id)
        {
            if(isAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rating rating = mDB.Ratings.Find(id);
                if (rating == null)
                {
                    return HttpNotFound();
                }
                return View(rating);
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Ratings", "AdminDeleteRating"));
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = mDB.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        [HttpPost, ActionName("AdminDeleteRating")]
        [ValidateAntiForgeryToken]
        public ActionResult AdminDeleteConfirmed(int id)
        {
            if(isAdmin())
            {
                Rating rating = mDB.Ratings.Find(id);
                mDB.Ratings.Remove(rating);
                mDB.SaveChanges();
                return RedirectToAction("AdminRating");
            }

            Exception eNotAdmin = new Exception();
            return View("Error", new HandleErrorInfo(eNotAdmin, "Ratings", "AdminDeleteConfirmed"));
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = mDB.Ratings.Find(id);
            mDB.Ratings.Remove(rating);
            mDB.SaveChanges();
            return RedirectToAction("Index");
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
