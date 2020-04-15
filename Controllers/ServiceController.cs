using System;
using System.Collections.Generic;
using System.Data;
// Required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;

namespace HospitalProject.Controllers
{
    public class ServiceController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        public ServiceController() { }

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Service> services = db.Services.SqlQuery("Select * from Services").ToList();
            return View(services);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string ServiceName, string ServiceDetails)
        {
            Service newservice = new Service();
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            newservice.ServiceName= ServiceName;
            newservice.ServiceDetails = ServiceDetails;
            
            
            db.Services.Add(newservice);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Show(string id)
        {
            //find data about the individual service
            string main_query = "Select * from Services where ServiceID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            Service service= db.Services.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            return View(service);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from Services where ServiceID= @id";
            var parameter = new SqlParameter("@id", id);
            Service service = db.Services.SqlQuery(query, parameter).FirstOrDefault();

            return View(service);
        }
        [HttpPost]
        public ActionResult Update(int id, string ServiceName, string ServiceDetails)
        {
            string query = "update Services set ServiceName= @ServiceName, ServiceDetails= @ServiceDetails where ServiceID= @id";

            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@ServiceName",ServiceName);
            sqlparams[2] = new SqlParameter("@ServiceDetails", ServiceDetails);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(string id)
        {
            string query = "select * from Services where ServiceID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Service service = db.Services.SqlQuery(query, param).FirstOrDefault();
            return View(service);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string query = "delete from Services where ServiceID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

    }
}