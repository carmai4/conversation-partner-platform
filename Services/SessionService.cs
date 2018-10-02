using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPP2.Services
{
	public static class SessionService
	{
		public static void BeginSessionLog(int senderId, int receiverId, string sessionString)
		{
			var sessionLog = new SessionLog()
			{
				CallSenderId = senderId,
				CallReceiverId = receiverId,
				SessionBegin = DateTime.Now.TimeOfDay,
				SessionEnd = null,
				SessionString = sessionString,
				Date = DateTime.Today
			};

			using (var dc = new CPPdatabaseEntities())
			{
				dc.SessionLogs.Add(sessionLog);
				dc.SaveChanges();
			}
		}

		public static void EndSessionLog(int sessionId)
		{
			using (var dc = new CPPdatabaseEntities())
			{
				var unfinishedSession = dc.SessionLogs.Single(s => s.Id == sessionId);
				unfinishedSession.SessionEnd = DateTime.Now.TimeOfDay;
				dc.SaveChanges();
			}
		}

	}

}