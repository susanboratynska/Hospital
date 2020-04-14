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
    public class FaqController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Faq
        public ActionResult List(string searchkey, int pagenum = 0)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Searching for " + searchkey);

            //get a list of all faqs
            List<Faq> faqs = db.Faqs.Where(f => (searchkey != null) ? f.Question.Contains(searchkey) : true).ToList();

            //pagination algorithm provided by https://github.com/christinebittle/PetGroomingMVC
            int perpage = 5;
            int questioncount = faqs.Count();
            int maxpage = (int)Math.Ceiling((decimal)questioncount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";

            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);

                faqs = db.Faqs.Where(f => (searchkey != null) ? f.Question.Contains(searchkey) : true).OrderBy(f => f.FaqID).Skip(start).Take(perpage).ToList();
            }

            //return list
            return View(faqs);
        }
        //method for public view
        public ActionResult Public(string searchkey)
        {
            Debug.WriteLine("Searching for " + searchkey);

            //get a list of all faqs and search for searchkey
            List<Faq> faqs = db.Faqs.Where(f => (searchkey != null) ? f.Question.Contains(searchkey) : true).ToList();

            return View(faqs);
        }
        public ActionResult Show(int FaqID)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Showing details for faq: " + FaqID);

            //get faq based on id and return it
            Faq Faq = db.Faqs.FirstOrDefault(f => f.FaqID == FaqID);

            return View(Faq);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string Question, string Answer, bool Published)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Attemping to add a new faq: " + Question);

            //create a new faq
            Faq Faq = new Faq();

            //bind passed in params
            Faq.Question = Question;
            Faq.Answer = Answer;
            Faq.Published = Published;

            //add to the db and save
            db.Faqs.Add(Faq);
            db.SaveChanges();

            //redirect to list view
            return RedirectToAction("List");
        }
        public ActionResult Update(int FaqID)
        {
            //get faq based on the passed in id and return it
            Faq Faq = db.Faqs.FirstOrDefault(f => f.FaqID == FaqID);

            return View(Faq);
        }
        [HttpPost]
        public ActionResult Update(int FaqID, string Question, string Answer, bool Published)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Attemping to update faq: " + FaqID);

            //search for the specific faq info
            Faq Faq = db.Faqs.Find(FaqID);

            //bind new params
            Faq.Question = Question;
            Faq.Answer = Answer;
            Faq.Published = Published;

            //save changes
            db.SaveChanges();

            //redirect to list view
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Delete(int FaqID)
        {
            //check if the user is logged in and is an admin and direct to login if they are not an admin or logged in
            //put the rest of the code in an else statement

            Debug.WriteLine("Attemping to delete faq: " + FaqID);

            //search for specific faq based on passed in id
            Faq Faq = db.Faqs.Find(FaqID);

            //delete the faq and save changes
            db.Faqs.Remove(Faq);
            db.SaveChanges();

            //redirect to list view
            return RedirectToAction("List");
        }
    }
}