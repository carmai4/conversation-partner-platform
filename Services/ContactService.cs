using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace CPP2.Services
{
	public static class ContactService
	{
		public static List<Contact> GetContactList(int ownerId, CPPdatabaseEntities dc)
		{
			return dc.Contacts.Where(con => con.ContactListOwnerId == ownerId).ToList();
		}

	}
}