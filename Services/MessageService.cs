using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Twilio.Http;
namespace CPP2.Services
{
	public static class MessageService
	{
		public static bool SendEmailRequest(string sourceEmail, string recipientEmail, string session)
		{
			string senderName;
			int senderCppUserId;
			int recipientCppUserId;

			using (var dc = new CPPdatabaseEntities())
			{
				var senderProfile = dc.Profiles.SingleOrDefault(p => p.CppUser.AspNetUser.Email == sourceEmail);
				if (senderProfile != null) senderName = senderProfile.FirstName + " " + senderProfile.LastName;
				else senderName = sourceEmail;

				senderCppUserId =
					dc.CppUsers.Where(u => u.AspNetUser.Email == sourceEmail).Select(u => u.Id).SingleOrDefault();

			recipientCppUserId =
					dc.CppUsers.Where(u => u.AspNetUser.Email == recipientEmail).Select(u => u.Id).SingleOrDefault();
			}
			
			MailMessage callRequestEmail = new MailMessage();

			callRequestEmail.From = new MailAddress(sourceEmail);
			callRequestEmail.To.Add(recipientEmail);
			callRequestEmail.Subject = senderName + " has requested a conversation! " + DateTime.Now.ToString();
			//callRequestEmail.Body = "Click this link to join the session https://appr.tc/r/" + session +
			//						" and then click Join to to enter the call.";
			callRequestEmail.Body = "Click this link to join the session http://localhost:61441/Call/InitiateCall?session=" + session + "&sender=" + senderCppUserId + "&recipient=" + recipientCppUserId +" and then click Join to to enter the call.";
			SmtpClient client = new SmtpClient();
			client.UseDefaultCredentials = true;
			client.Host = "smtp.gmail.com";
			client.Port = 587;
			client.EnableSsl = true;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.Credentials = new NetworkCredential("cppcallrequest@gmail.com", "Redteam17");
			client.Timeout = 20000;
			try
			{
				client.Send(callRequestEmail);
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				callRequestEmail.Dispose();
			}

			return true;
		}

	

    public static bool SendEmailInvite(string sourceEmail, string targetEmail, string message)
    {
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(sourceEmail);
        msg.To.Add(targetEmail);//target email
        msg.Subject = sourceEmail + " invited you to join CPP! " + DateTime.Now.ToString();
        msg.Body = "Come Join CPP: Conversation Partner Platform!\n" + message;
        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = true;
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.EnableSsl = true;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new NetworkCredential("cppcallrequest@gmail.com", "Redteam17");
        client.Timeout = 20000;
        try
        {
            client.Send(msg);
        }
        catch (Exception exInvite)
        {
            return false;
        }
        finally
        {
            msg.Dispose();
        }
        return true;
    }

        public static bool SendViolationEmail(int violationCode, int reportedId, string sourceEmail)
        {
            var db = new CPPdatabaseEntities();
            string senderName;
            int senderCppUserId;
            int recipientCppUserId;

            var recipientEmail = "hhorn001@odu.edu";
            var SourceEmail = sourceEmail;
            using (var dc = new CPPdatabaseEntities())
            {
                var senderProfile = dc.Profiles.SingleOrDefault(p => p.CppUser.AspNetUser.Email == SourceEmail);
                if (senderProfile != null) senderName = senderProfile.FirstName + " " + senderProfile.LastName;
                else senderName = SourceEmail;

                senderCppUserId =
                    dc.CppUsers.Where(u => u.AspNetUser.Email == SourceEmail).Select(u => u.Id).SingleOrDefault();

                recipientCppUserId =
                        dc.CppUsers.Where(u => u.AspNetUser.Email == recipientEmail).Select(u => u.Id).SingleOrDefault();
            }

            MailMessage callRequestEmail = new MailMessage();
	        var msg = db.ViolationTypes.Where(x => x.Id == violationCode).Select(x => x.Description).Single();
            callRequestEmail.From = new MailAddress(SourceEmail);
            callRequestEmail.To.Add(recipientEmail);
            callRequestEmail.Subject = senderName + " has reported a violation! " + DateTime.Now.ToString();
            //callRequestEmail.Body = "Click this link to join the session https://appr.tc/r/" + session +
            //						" and then click Join to to enter the call.";
            callRequestEmail.Body = "Violation Code: " + msg + ", Reported Id: " + CppUserService.GetCppUserEmail2(reportedId,db);
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("cppcallrequest@gmail.com", "Redteam17");
            client.Timeout = 20000;
            try
            {
                client.Send(callRequestEmail);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                callRequestEmail.Dispose();
            }

            return true;
        }

    }
}