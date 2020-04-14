using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using HospitalProject.Data;
using HospitalProject.Models;
using Microsoft.AspNet.Identity;

namespace HospitalProject.Controllers
{
    public class HealthLibraryController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: HealthLibrary
        public ActionResult List(string searchkey, int pagenum = 0)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Searching for " + searchkey);

            //get a list of all health library posts
            List<HealthLibrary> healthLibraries = db.HealthLibraries.Where(f => (searchkey != null) ? f.Title.Contains(searchkey) : true).ToList();

            //pagination algorithm provided by 
            int perpage = 5;
            int titlecount = healthLibraries.Count();
            int maxpage = (int)Math.Ceiling((decimal)titlecount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";

            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);

                healthLibraries = db.HealthLibraries.Where(f => (searchkey != null) ? f.Title.Contains(searchkey) : true).OrderBy(f => f.HealthLibraryID).Skip(start).Take(perpage).ToList();
            }

            //return list
            return View(healthLibraries);
        }
        //method for public view
        public ActionResult Public(string searchkey)
        {
            Debug.WriteLine("Searching for " + searchkey);

            //get a list of all health library posts and search for searchkey
            List<HealthLibrary> healthLibraries = db.HealthLibraries.Where(f => (searchkey != null) ? f.Title.Contains(searchkey) : true).ToList();

            //return the list
            return View(healthLibraries);
        }
        //method for public show view
        public ActionResult PublicShow(int HealthLibraryID)
        {
            Debug.WriteLine("Showing details for post: " + HealthLibraryID);

            //get health library post based on id and return it
            HealthLibrary HealthLibrary = db.HealthLibraries.FirstOrDefault(f => f.HealthLibraryID == HealthLibraryID);

            return View(HealthLibrary);
        }

        public ActionResult Show(int HealthLibraryID)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Showing details for post: " + HealthLibraryID);

            //get health library post based on id and return it
            HealthLibrary HealthLibrary = db.HealthLibraries.FirstOrDefault(f => f.HealthLibraryID == HealthLibraryID);

            return View(HealthLibrary);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string Title, DateTime Date, string Body, bool Published)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Attemping to add a new post: " + Title);
            //create a new health library post
            HealthLibrary HealthLibrary = new HealthLibrary();

            //bind passed in params
            HealthLibrary.Title = Title;
            HealthLibrary.Date = Date;
            HealthLibrary.Body = Body;
            HealthLibrary.Published = Published;

            //add to the db and save
            db.HealthLibraries.Add(HealthLibrary);
            db.SaveChanges();

            //redirect to list view
            return RedirectToAction("List");
        }

        public ActionResult Update(int HealthLibraryID)
        {
            //get health library info based on the passed in id and return it
            HealthLibrary HealthLibrary = db.HealthLibraries.FirstOrDefault(f => f.HealthLibraryID == HealthLibraryID);

            return View(HealthLibrary);
        }
        [HttpPost]
        public ActionResult Update(int HealthLibraryID, string Title, DateTime Date, string Body, bool Published)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Attemping to update post: " + HealthLibraryID);

            //search for the specific health library post info
            HealthLibrary HealthLibrary = db.HealthLibraries.Find(HealthLibraryID);
            //bind new params
            HealthLibrary.Title = Title;
            HealthLibrary.Date = Date;
            HealthLibrary.Body = Body;
            HealthLibrary.Published = Published;

            //save changes
            db.SaveChanges();

            //redirect to list view
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int HealthLibraryID)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Attemping to delete post: " + HealthLibraryID);

            //search for specific health library post based on passed in id
            HealthLibrary HealthLibrary = db.HealthLibraries.Find(HealthLibraryID);

            //delete the post and save changes
            db.HealthLibraries.Remove(HealthLibrary);
            db.SaveChanges();

            //redirect to list view
            return RedirectToAction("List");
        }
    }
}