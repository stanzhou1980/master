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
using System.Net.Mime;
using System.Net.Http.Formatting;

namespace WebAPI.Controllers
{
    public class PointsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SetPoints([FromBody]User user)
        {
            HttpResponseMessage message = null;

            try
            {
                User person = Helper.FindUserById(user.id, ref message, Request, Helper.UserList);

                if (person == null)
                    return message;

                person.points = user.points;

                return Request.CreateResponse(HttpStatusCode.OK,
                                    "{ Message: " +
                                    person.name + " updated with " +
                                    person.points.ToString() + " points! }");
            }
            catch(Exception ex)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return message;
        }
    }
}
