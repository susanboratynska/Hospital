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


namespace HospitalProject.Controllers
{
    public class InvoiceController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Invoice> invoices= db.Invoices.SqlQuery("Select * from Invoices").ToList();
            return View(invoices);
        }
        public ActionResult Add()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AddInvoice viewmodel = new AddInvoice();
                viewmodel.Patients = db.Patients.ToList();
                viewmodel.Services = db.Services.ToList();
                return View(viewmodel);
            }
        }

        [HttpPost]
        public ActionResult Add(int PatientID, int ServiceID, int Amount, string DateOfService, bool Status)
        {
            Invoice newinvoice = new Invoice();
            newinvoice.PatientID = PatientID;
            newinvoice.ServiceID = ServiceID;
            newinvoice.Amount= Amount;
            newinvoice.DateOfService= Convert.ToDateTime(DateOfService);
            newinvoice.Status= Status;

            //first add invoice 
            db.Invoices.Add(newinvoice);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Show(string id)
        {
            //find data about individual invoice
            string main_query = "Select * from Invoices where InvoiceID=@id";
            var pk_parameter = new SqlParameter("@id", id);
            Invoice invoice = db.Invoices.SqlQuery(main_query, pk_parameter).FirstOrDefault();
            return View(invoice);
        }

        public ActionResult Update(int id)
        {
            UpdateInvoice viewmodel = new UpdateInvoice();

            //get the invoice
            //Include(invoice>invoice.services) => join on the invoice,invoicesxservices, services bridging table
            viewmodel.Invoice =
                db.Invoices
                .Include(invoice => invoice.Service)
                .FirstOrDefault(invoice => invoice.InvoiceID == id);

            //get all the patients
            viewmodel.Patients= db.Patients.ToList();

            //get all the services
            viewmodel.Services= db.Services.ToList();

            return View(viewmodel);
        }


        [HttpPost]
        public ActionResult Update(int id,int PatientID, int ServiceID, int Amount, string DateOfService, bool Status)
        {
            string query = "update Invoices set PatientID = @PatientID, ServiceID= @ServiceID, Amount =@Amount, DateOfService = @DateOfService, Status = @Status where InvoiceID= @id";

            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@PatientID", PatientID);
            sqlparams[2] = new SqlParameter("@ServiceID", ServiceID);
            sqlparams[3] = new SqlParameter("@Amount", Amount);
            sqlparams[4] = new SqlParameter("@DateOfService", DateOfService);
            sqlparams[5] = new SqlParameter("@Status", Status);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(string id)
        {
            string query = "select * from Invoices where InvoiceID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Invoice invoice = db.Invoices.SqlQuery(query, param).FirstOrDefault();
            return View(invoice);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string query = "delete from Invoices where InvoiceID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}