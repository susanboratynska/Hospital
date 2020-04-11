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
    public class JobController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Job
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JobsListing()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "Select * from Jobs";
                List<Job> jobs = db.Jobs.SqlQuery(query).ToList();
                return View(jobs);
            }
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
                string query = "Select * from JobTypes";
                List<JobType> JobTypes = db.JobTypes.SqlQuery(query).ToList();
                return View(JobTypes);
            }
        }

        public ActionResult AddJob(string Position, int jobTypeID, string Qualification, int Experience)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Job job = new Job();
                job.JobTypeID = jobTypeID;
                job.Position = Position;
                job.Qualification = Qualification;
                job.Experience = Experience;

                // Equivalent to SQL insert statement:
                db.Jobs.Add(job);

                db.SaveChanges();
                return RedirectToAction("JobsListing");
            }
        }

        public ActionResult Update(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "Select * from JobTypes";
                List<JobType> jobTypes = db.JobTypes.SqlQuery(query).ToList();
                query = "select * from Jobs where JobID = @job_id";
                Job job = db.Jobs.SqlQuery(query, new SqlParameter("@job_id", id)).FirstOrDefault();
                UpdateJob updateJob = new UpdateJob();
                updateJob.Job = job;
                updateJob.JobTypes = jobTypes;
                return View(updateJob);
            }
        }

        public ActionResult UpdateJob(int jobID, string Position, int jobTypeID, string Qualification, int Experience)
        {
            Job job = db.Jobs.Find(jobID);
            job.JobTypeID = jobTypeID;
            job.Position = Position;
            job.Qualification = Qualification;
            job.Experience = Experience;
            db.SaveChanges();
            return RedirectToAction("JobsListing");
        }

        public ActionResult DeleteConfirm(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "DELETE from Jobs where JobID = @job_id";
                db.Database.ExecuteSqlCommand(query, new SqlParameter("@job_id", id));
                return RedirectToAction("JobsListing");
            }
        }

        public ActionResult Delete(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "select * from Jobs where JobID = @job_id";
                Job job = db.Jobs.SqlQuery(query, new SqlParameter("@job_id", id)).FirstOrDefault();
                return View(job);
            }
        }

        public ActionResult JobPosts()
        {
            string query = "Select * from Jobs";
            List<Job> jobs = db.Jobs.SqlQuery(query).ToList();
            return View(jobs);
        }
    }
}