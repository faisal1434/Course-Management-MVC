using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseManagement_MVC.Models;
using CourseManagement_MVC.ViewModels;
using PagedList;
using PagedList.Mvc;
namespace CourseManagement_MVC.Controllers
{
    [Authorize]
  
    public class CoursesController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

       
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Summary(int page = 1, string sort = "", string search = "")
        {
            ViewBag.NameSort = sort == "name" ? "name_desc" : "name";
            ViewBag.FeeSort = sort == "fee" ? "fee_desc" : "fee";
            ViewBag.DurationSort = sort == "duration" ? "duration_desc" : "duration";
            ViewBag.StartSort = sort == "start" ? "start_desc" : "start";
            ViewBag.CurrentSort = sort;
            var data = db.Courses.AsEnumerable().Select(x => new CourseViewModel
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                CourseFee = x.CourseFee,
                Duration = x.Duration,
                StartDate = x.StartDate,
                StudentCount = x.Students.Count
            });
            switch (sort)
            {
                case "fee":
                    data = data.OrderBy(x => x.CourseFee);
                    break;
                case "fee_desc":
                    data = data.OrderByDescending(x => x.CourseFee);
                    break;
                case "name":
                    data = data.OrderBy(x => x.CourseName);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(x => x.CourseName);
                    break;
                case "duraion":
                    data = data.OrderBy(x => x.Duration);
                    break;
                case "duration_desc":
                    data = data.OrderByDescending(x => x.Duration);
                    break;
                case "start":
                    data = data.OrderBy(x => x.StartDate);
                    break;
                case "start_desc":
                    data = data.OrderByDescending(x => x.StartDate);
                    break;
                default:
                    data = data.OrderBy(x => x.CourseId);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.CourseName.ToLower().StartsWith(search.ToLower()));
            }
            return PartialView("_courseSummary",data.ToPagedList(page, 5));
        }
        public ActionResult CrudUI()
        {
            return View(db.Courses.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,CourseFee,Duration,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,CourseFee,Duration,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
