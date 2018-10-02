using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPP2;
using CPP2.Services;
using Microsoft.AspNet.Identity;

namespace CPP2.Controllers
{
    public class ContactsController : Controller
    {
        private CPPdatabaseEntities db = new CPPdatabaseEntities();

        // GET: Contacts
        public ActionResult Index() { 
        
            String user = User.Identity.GetUserId();
            var latest = (from p in db.CppUsers
                          where
                          p.AspNetUserId == user
                          select new
                          {
                              p.Id
                          }).First();

            //Current User's ID
            var id1 = latest.Id;

            //Find Current User's contacts
            var latest1 = db.Contacts.Where(b => b.ContactListOwnerId == id1);
            IEnumerable<CppUser> list1 = from b in latest1
                                         join d in db.CppUsers on b.ContactListMemberId equals d.Id
                                         select d;
            return View(list1.ToList());

        }

        public ActionResult ViewProfile(int? id)
        {   
            // select contact's profile
            var current = db.Profiles.Where(b => b.CppUserId == id);
            return View(current.ToList());
        }
        
        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.ContactListOwnerId = new SelectList(db.CppUsers, "Id");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContactListMemberId")] Contact contact)
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
                var id1 = latest.Id;
                contact.ContactListOwnerId = id1;

                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactListOwnerId = new SelectList(db.CppUsers, "Id", "AspNetUserId", contact.ContactListOwnerId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactListOwnerId = new SelectList(db.CppUsers, "Id", "AspNetUserId", contact.ContactListOwnerId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactListOwnerId,ContactListMemberId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactListOwnerId = new SelectList(db.CppUsers, "Id", "AspNetUserId", contact.ContactListOwnerId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
            var id1 = latest.Id;
            Contact contact = new Contact();
            contact.ContactListMemberId = id;
            contact.ContactListOwnerId = id1;

            //Find primary key
            var keyid = (from p in db.Contacts
                         where
                         p.ContactListOwnerId == id1
                         select new
                         {
                             p.Id,
                             p.ContactListMemberId
                         });

            var keyid2 = (from h in keyid
                          where h.ContactListMemberId == id
                          select new
                          {
                              h.Id
                          }).First();

            var id3 = keyid2.Id;
            contact.Id = id3;

            if (ModelState.IsValid)
            {
                db.Contacts.Attach(contact);
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Contacts/Add/5
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddConfirmed(int id)
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
            var id1 = latest.Id;
            Contact contact = new Contact();
            contact.ContactListMemberId = id;
            contact.ContactListOwnerId = id1;


            if (ModelState.IsValid)
            {
                //db.Contacts.Attach(contact);
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        // GET: Contacts
        public ActionResult Test()
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
            var id1 = latest.Id;

            //Find Current User's contacts
            var latest1 = db.CppUsers;

            IEnumerable<CppUser> list1 = from b in latest1
                                         select b;
            return View(list1.ToList());
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
