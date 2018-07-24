using IPTSE_portal.BLL;
using IPTSE_portal.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;

namespace IPTSE_portal.Controllers.Api
{
    public class SchoolRegistrationAPIController : ApiController
    {
        // GET: api/Exporter
        public HttpResponseMessage Get(string searchText)
        {
            try
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found!");
            }
            catch (SecurityException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.InnerException);
            }
            catch (Exception ex)
            {
                HttpRequestMessage Request = new HttpRequestMessage();
                //new EventLogManager().LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }

        }

        // GET: api/Exporter/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found!");
            }
            catch (SecurityException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.InnerException);
            }
            catch (Exception ex)
            {
                HttpRequestMessage Request = new HttpRequestMessage();
                //new EventLogManager().LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST: api/Exporter
        public HttpResponseMessage Post(SchoolRegistrationModel schoolModel)
        {
            try
            {
                SchoolRegistrationBLL _objSchoolRegistration = new SchoolRegistrationBLL();
                _objSchoolRegistration.InsertSchool(schoolModel);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found!");
            }
            catch (SecurityException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.InnerException);
            }
            catch (Exception ex)
            {
                HttpRequestMessage Request = new HttpRequestMessage();
                //new EventLogManager().LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT: api/Exporter/5
        public HttpResponseMessage Put(SchoolRegistrationModel schoolModel)
        {
            try
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found!");
            }
            catch (SecurityException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.InnerException);
            }
            catch (Exception ex)
            {
                HttpRequestMessage Request = new HttpRequestMessage();
                //new EventLogManager().LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Exporter/5
        public void Delete(int id)
        {
        }
    }
}
