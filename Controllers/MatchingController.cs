using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CPP2;

namespace CPP2.Controllers
{
    public class MatchingController : Controller
    {
        private CPPdatabaseEntities db = new CPPdatabaseEntities();

        // GET: Matching
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

            var me = from a in db.CppUsers
                     where a.Id == id
                     select a;

            var others = from a in db.CppUsers
                         where a.Id != id
                         select a;

            var others2 = from c in others
                          where c.AvailabilitySchedules.FirstOrDefault().CppUserId == c.Id
                          select c;

            if (me.FirstOrDefault().Profiles.FirstOrDefault().PreferredGender.ToString().Equals("None"))
            {
                others2 = from d in others
                          where d.AvailabilitySchedules.FirstOrDefault().CppUserId == d.Id
                          select d;

                others2 = from d in others2
                          where d.Profiles.FirstOrDefault().PreferredGender.ToString().Equals("None") ||
                          d.Profiles.FirstOrDefault().PreferredGender.ToString().Equals(me.FirstOrDefault().Profiles.FirstOrDefault().Gender.ToString())
                          select d;

            }

            if (me.FirstOrDefault().Profiles.FirstOrDefault().PreferredGender.ToString().Equals("Male"))
            {
                others2 = from d in others
                          where d.AvailabilitySchedules.FirstOrDefault().CppUserId == d.Id &&
                          d.Profiles.FirstOrDefault().Gender.ToString().Equals("Male")
                          select d;
            }

            else if (me.FirstOrDefault().Profiles.FirstOrDefault().PreferredGender.ToString().Equals("Female"))
            {
                others2 = from e in others
                          where e.AvailabilitySchedules.FirstOrDefault().CppUserId == e.Id &&
                          e.Profiles.FirstOrDefault().Gender.ToString().Equals("Female")
                          select e;
            }



            foreach (var s in others2)
            {
                int sum = 0;
                if (s.AvailabilitySchedules.FirstOrDefault().Sunday0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday0 == true)
                {
                    sum++;
                }


                if (s.AvailabilitySchedules.FirstOrDefault().Sunday1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday1 == true)
                {
                    sum++;
                }


                if (s.AvailabilitySchedules.FirstOrDefault().Sunday2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday2 == true)
                {
                    sum++;
                }


                if (s.AvailabilitySchedules.FirstOrDefault().Sunday3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday3 == true)
                {
                    sum++;
                }


                if (s.AvailabilitySchedules.FirstOrDefault().Sunday4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday4 == true)
                {
                    sum++;
                }


                if (s.AvailabilitySchedules.FirstOrDefault().Sunday5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday9 == true)
                {
                    sum++;
                }
                if (s.AvailabilitySchedules.FirstOrDefault().Sunday10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sunday23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sunday23 == true)
                {
                    sum++;
                }



                if (s.AvailabilitySchedules.FirstOrDefault().Mon0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon0 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon1 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon2 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon3 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon4 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon9 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Mon23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Mon23 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue0 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue1 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue2 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue3 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue4 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue9 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Tue23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Tue23 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed0 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed1 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed2 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed3 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed4 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed9 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Wed23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Wed23 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu0 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu1 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu2 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu3 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu4 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu9 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Thu23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Thu23 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri0 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri1 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri2 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri3 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri4 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri9 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Fri23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Fri23 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat0 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat0 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat1 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat1 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat2 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat2 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat3 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat3 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat4 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat4 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat5 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat5 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat6 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat6 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat7 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat7 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat8 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat8 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat9 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat9 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat10 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat10 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat11 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat11 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat12 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat12 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat13 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat13 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat14 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat14 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat15 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat15 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat16 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat16 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat17 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat17 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat18 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat18 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat19 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat19 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat20 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat20 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat21 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat21 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat22 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat22 == true)
                {
                    sum++;
                }

                if (s.AvailabilitySchedules.FirstOrDefault().Sat23 == true && me.FirstOrDefault().AvailabilitySchedules.FirstOrDefault().Sat23 == true)
                {
                    sum++;
                }



                s.matchsum = sum;
                s.matchsum = s.matchsum / me.FirstOrDefault().Profiles.FirstOrDefault().Desired_Hours;
                s.matchtotal = sum;
                s.myhours = me.FirstOrDefault().Profiles.FirstOrDefault().Desired_Hours;
                s.matchsum = s.matchsum * 100;
                s.matchsum = Math.Round(s.matchsum, 0);
            }



            //Interest match

            foreach (var s in others2)
            {
                double interestMatch = 0;

                if (me.FirstOrDefault().Profiles.FirstOrDefault().Interest1.Equals(s.Profiles.FirstOrDefault().Interest1) ||
                    me.FirstOrDefault().Profiles.FirstOrDefault().Interest1.Equals(s.Profiles.FirstOrDefault().Interest2) ||
                    me.FirstOrDefault().Profiles.FirstOrDefault().Interest1.Equals(s.Profiles.FirstOrDefault().Interest3) ||
                    me.FirstOrDefault().Profiles.FirstOrDefault().Interest1.Equals(s.Profiles.FirstOrDefault().Interest4) ||
                    me.FirstOrDefault().Profiles.FirstOrDefault().Interest1.Equals(s.Profiles.FirstOrDefault().Interest5))
                {
                    interestMatch++;
                }

                if (me.FirstOrDefault().Profiles.FirstOrDefault().Interest2.Equals(s.Profiles.FirstOrDefault().Interest1) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest2.Equals(s.Profiles.FirstOrDefault().Interest2) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest2.Equals(s.Profiles.FirstOrDefault().Interest3) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest2.Equals(s.Profiles.FirstOrDefault().Interest4) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest2.Equals(s.Profiles.FirstOrDefault().Interest5))
                {
                    interestMatch++;
                }


                if (me.FirstOrDefault().Profiles.FirstOrDefault().Interest3.Equals(s.Profiles.FirstOrDefault().Interest1) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest3.Equals(s.Profiles.FirstOrDefault().Interest2) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest3.Equals(s.Profiles.FirstOrDefault().Interest3) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest3.Equals(s.Profiles.FirstOrDefault().Interest4) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest3.Equals(s.Profiles.FirstOrDefault().Interest5))
                {
                    interestMatch++;
                }


                if (me.FirstOrDefault().Profiles.FirstOrDefault().Interest4.Equals(s.Profiles.FirstOrDefault().Interest1) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest4.Equals(s.Profiles.FirstOrDefault().Interest2) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest4.Equals(s.Profiles.FirstOrDefault().Interest3) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest4.Equals(s.Profiles.FirstOrDefault().Interest4) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest4.Equals(s.Profiles.FirstOrDefault().Interest5))
                {
                    interestMatch++;
                }


                if (me.FirstOrDefault().Profiles.FirstOrDefault().Interest5.Equals(s.Profiles.FirstOrDefault().Interest1) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest5.Equals(s.Profiles.FirstOrDefault().Interest2) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest5.Equals(s.Profiles.FirstOrDefault().Interest3) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest5.Equals(s.Profiles.FirstOrDefault().Interest4) ||
    me.FirstOrDefault().Profiles.FirstOrDefault().Interest5.Equals(s.Profiles.FirstOrDefault().Interest5))
                {
                    interestMatch++;
                }

                s.interestmatchsum = interestMatch;
                s.interestmatchsum = 100 * s.interestmatchsum / 5;
            }

            others2.OrderByDescending(c => c.matchsum);

            return View(others2.ToList());
        }

    }
}