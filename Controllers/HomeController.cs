using System.Web.Mvc;
using MVCEmail.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CPP2.Models;
using CPP2.Services;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace CPP2.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            
            return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "CPP connects ESL and EFL students with English speakers around the world";

			return View();
		}

        public ActionResult Contact2()
        {
            return View();
        }

		public ActionResult Contact()
		{
			ViewBag.Message = "Contact ODU's Spring 2017 CS411W Red Team";
            var usr = User.Identity.GetUserId();
            var vm = new ViolationViewModel();
            var db = new CPPdatabaseEntities();
            var thisCppUserId = CppUserService.GetCppUserId(User.Identity.GetUserId(), db);
            var sessions =
            db.SessionLogs.Where(session => session.CallReceiverId == thisCppUserId)
            .Select(session => session.CallSenderId)
            .ToList();
            sessions.AddRange(db.SessionLogs.Where(session => session.CallSenderId == thisCppUserId).Select(session => session.CallReceiverId));

            foreach (var partnerId in sessions)
            {
                //vm.List.Add(db.CppUsers.Where(u => u.Id == partnerId).Select(u => u.AspNetUser).Single());
                vm.List.Add(db.CppUsers.Where(u => u.Id == partnerId).Select(x => new AspNetUserDTO()
                {
                    Email = x.AspNetUser.Email,
                    Id = x.AspNetUser.Id,
                    UserName = x.AspNetUser.UserName
                }).Single());
            }
            var withoutDupes = vm.List.GroupBy(user => user.Email).Select(group => group.First());
            vm.List = withoutDupes.ToList();
            ViewData["ViolationList"] = vm.List;
            //ViewData["userIsAdmin"] = db.CppUsers.Where(x => x.AspNetUserId == usr).Select(x => x.PermissionLevel).Single() == 1;
            ViewBag.ListItems = vm.List;
			ViewData["userIsAdmin"] = db.CppUsers.Single(x => x.Id == thisCppUserId).PermissionLevel == 1;
            return View();
		} 

        public ActionResult Call()
        {
            ViewBag.Message = "Make a call";

            return View();
        }

       

        public ActionResult Sent()
        {
            return View();
        }
    }
}