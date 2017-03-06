using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using AttributeRouting;
using AttributeRouting.Web.Constraints;
using AttributeRouting.Web.Logging;
using AttributeRouting.Web.Mvc;
using System.Web.Routing;

namespace WebAPI.Models
{
    public class Helper
    {
        public static List<User> UserList = new List<User>()
        {
                new User { id = 1, name = "john", points = 100 },
                new User { id = 2, name = "marry", points = 200 },
                new User { id = 3, name = "stan", points = 200 },
        };

        //======================================
        public Helper()
        { }

        //===============================================================
        public static User FindUserById(int id, 
            ref HttpResponseMessage message,
            HttpRequestMessage request,
            List<User> userList)
        {
            var person = (from u in userList where u.id == id select u).FirstOrDefault();
            if (person == null)
            {
                message = request.CreateErrorResponse(HttpStatusCode.NotFound, "User id not found!");
            }
            else
            {
                message = request.CreateResponse(HttpStatusCode.OK, person);
            }

            return person;
        }
    }
}