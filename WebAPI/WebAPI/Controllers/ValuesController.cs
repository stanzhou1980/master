using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using AttributeRouting;
using AttributeRouting.Web.Constraints;
using AttributeRouting.Web.Logging;
using AttributeRouting.Web.Mvc;
using System.Web.Routing;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private const string SEPARATOR = "/";

        //=============================================================
        public HttpResponseMessage Get()
        {
            return Helper.UserList.Any()
                    ? Request.CreateResponse(HttpStatusCode.OK, Helper.UserList)
                    : Request.CreateErrorResponse(HttpStatusCode.NoContent, "No users!");
        }

        
        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage msg = null;

            try
            {
                Helper.FindUserById(id, ref msg, Request, Helper.UserList);
            }
            catch(Exception ex)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return msg;
        }

        //================================================================
        public HttpResponseMessage Post([FromBody]User user)
        { 
            if (null == user)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User object is invalid!");
            }

            if (String.IsNullOrEmpty(user.name) || String.IsNullOrEmpty(user.name.Trim()))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You must provide a user name!");
            }

            HttpResponseMessage msg = null;

            try
            {
                var person = (from u in Helper.UserList where u.name.ToLower() == user.name.ToLower() select u).FirstOrDefault();                

                if (person == null)
                {
                    int maxId = Helper.UserList.Any() ? Helper.UserList.Max(u => u.id) : 0;
                    user.id = maxId + 1;

                    Helper.UserList.Add(user);

                    string requestUri = Request.RequestUri.ToString();
                    if (!requestUri.EndsWith(SEPARATOR))
                    {
                        requestUri += SEPARATOR;
                    }
                    msg = Request.CreateResponse(HttpStatusCode.Created,
                                        new Uri(requestUri + user.id.ToString()));
                }
                else
                {
                    msg = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User already exists!");
                }
            }
            catch(Exception ex)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return msg;
        }
    }
}
