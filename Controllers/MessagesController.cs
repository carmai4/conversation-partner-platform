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
using System.Text;

namespace CPP2.Controllers
{
    public class MessagesController : Controller
    {
        private CPPdatabaseEntities db = new CPPdatabaseEntities();

        // GET: Messages
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
            var id1 = latest.Id;

            //Get all messages for current user
            var myMessages = db.Messages.Where(b => b.ReceiverId == id1 || b.SenderId == id1);

            //Get most recent sent and received
            var latestSent = from c in myMessages
                             group c by c.SenderId into grp
                             select grp.OrderByDescending(c => c.Id).FirstOrDefault();

            //Get most recent received
            var latestReceived = from c in myMessages
                                 group c by c.ReceiverId into grp
                                 select grp.OrderByDescending(c => c.Id).FirstOrDefault();

            //Concat send and received
            var AllLatest = latestReceived.Concat(latestSent);

            //Get count in AllLatest          
            int count = AllLatest.ToList().Count();

            //ToList
            var AllLatestList = AllLatest.ToList();

            //Empty list
            var ListA = AllLatestList.ToList();
            ListA.Clear();

            //List A: User has received but not sent messages to partner
            //List B: Latest between message users who both have sent messages to each other
            //List C: Partner has received but not sent messages to user


            //
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    if (AllLatestList.ElementAt(j).SenderId.Equals(AllLatestList.ElementAt(i).ReceiverId))
                    {
                        if (AllLatestList.ElementAt(j).Id > AllLatestList.ElementAt(i).Id)
                        {
                            ListA.Add(AllLatestList.ElementAt(j));

                            if (AllLatestList.ElementAt(j).ReceiverId == id1)
                            {
                                ListA.Last().partner = AllLatestList.ElementAt(j).SenderId;
                            }
                            else if (AllLatestList.ElementAt(j).SenderId == id1)
                            {
                                ListA.Last().partner = AllLatestList.ElementAt(j).ReceiverId;
                            }

                        }
                    }
                }
            }

            //Remove duplicates
            var DistinctA = ListA.Distinct().ToList();
            List<int> wob = new List<int>();

            //New List of All latest messages
            var ListB = AllLatestList.ToList();
            var ListC = AllLatestList.ToList();

            //Find messages where only one user has messaged the other
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {

                    if (AllLatestList.ElementAt(j).SenderId.Equals(AllLatestList.ElementAt(i).ReceiverId))
                    {
                        ListB.Remove(AllLatestList.ElementAt(j));
                    }

                }
            }

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    if (AllLatestList.ElementAt(j).ReceiverId.Equals(AllLatestList.ElementAt(i).SenderId))
                    {
                        ListC.Remove(AllLatestList.ElementAt(j));
                    }

                }
            }


            //Remove Duplicates
            var DistinctB = ListB.Distinct().ToList();
            var DistinctC = ListC.Distinct().ToList();

            //Add partner and add item to B list
            foreach (var item in DistinctB)
            {
                if (item.ReceiverId == id1)
                {
                    item.partner = item.SenderId;
                }

                else if (item.SenderId == id1)
                {
                    item.partner = item.ReceiverId;
                }

                DistinctA.Add(item);
            }

            foreach (var item in DistinctC)
            {
                if (item.ReceiverId == id1)
                {
                    item.partner = item.SenderId;
                }

                else if (item.SenderId == id1)
                {
                    item.partner = item.ReceiverId;
                }

                DistinctA.Add(item);
            }


            //Order List
            var final = DistinctA.OrderByDescending(b => b.Id).Distinct().ToList();

            //final contains latest message between users
            return View(final);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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

            //Get all messages
            var results = from row in db.Messages
                          where row.SenderId == id1 || row.ReceiverId == id1
                          select row;

            var results1 = from row in results
                           where row.SenderId == id || row.ReceiverId == id
                           select row;

            //Order by Id
            var ordered = from row in results1
                          orderby row.Id ascending
                          select row;

            //Add partner's id to message.partner

            foreach (var item in ordered)
            {
                item.partner = id.GetValueOrDefault();
            }

            return View(ordered);
        }

        // GET: Messages/Send
        public ActionResult MyContacts()
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
            var latest1 = db.Contacts.Where(b => b.ContactListOwnerId == id1);
            IEnumerable<CppUser> list1 = from b in latest1
                                         join d in db.CppUsers on b.ContactListMemberId equals d.Id
                                         select d;
            return View(list1.ToList());
        }

        // GET: Messages/Send
        public ActionResult Send(int? ReceiverId)
        {
            if (ReceiverId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Messages/Send
        [HttpPost, ActionName("Send")]
        [ValidateAntiForgeryToken]
        public ActionResult SendConfirmed(int ReceiverId, [Bind(Include = "Message1")] Message message)
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
            message.SenderId = id1;
            message.ReceiverId = ReceiverId;

            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
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
