using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPP2.Services
{
	public static class CppUserService
	{
		public static int GetCppUserId(string aspNetUserId, CPPdatabaseEntities dc)
		{
			return dc.CppUsers.Where(c => c.AspNetUserId == aspNetUserId).Select(c => c.Id).SingleOrDefault();
		}

        public static string GetCppUserEmail(string aspNetUserId, CPPdatabaseEntities dc)
        {
            return dc.CppUsers.Where(c => c.AspNetUserId==aspNetUserId).Select(c => c.AspNetUser.Email).ToString();
        }

        public static string GetCppUserEmail2(int cppUserId, CPPdatabaseEntities dc)
        {
            return dc.AspNetUsers.Where(u => u.CppUsers.FirstOrDefault().Id == cppUserId).Select(u => u.Email).SingleOrDefault();
        }
    }
}