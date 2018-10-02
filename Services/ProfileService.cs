using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPP2.Services
{
	public static class ProfileService
	{
		public static Profile GetProfile(int cppUserId)
		{
			Profile profile;
			using (var dc = new CPPdatabaseEntities())
			{
				profile = dc.Profiles.Single(prof => prof.CppUserId == cppUserId);
			}

			return profile;
		}
	}
}