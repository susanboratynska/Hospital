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
            List<Faq> faqs = db.Faqs.Where(f => (searchkey != null) ? f.Question.Contains(searchkey) : true).ToList();

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

            return View(faqs);
        }
        public ActionResult Public(string searchkey)
        {
            List<Faq> faqs = db.Faqs.Where(f => (searchkey != null) ? f.Question.Contains(searchkey) : true).ToList();

            return View(faqs);
        }

        public ActionResult Show(int FaqID)
        {
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
            Faq Faq = new Faq();

            Faq.Question = Question;
            Faq.Answer = Answer;
            Faq.Published = Published;

            db.Faqs.Add(Faq);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Update(int FaqID)
        {
            Faq Faq = db.Faqs.FirstOrDefault(f => f.FaqID == FaqID);

            return View(Faq);
        }
        [HttpPost]
        public ActionResult Update(int FaqID, string Question, string Answer, bool Published)
        {
            Faq Faq = db.Faqs.Find(FaqID);
            Faq.Question = Question;
            Faq.Answer = Answer;
            Faq.Published = Published;

            db.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int FaqID)
        {
            Faq Faq = db.Faqs.Find(FaqID);

            db.Faqs.Remove(Faq);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}