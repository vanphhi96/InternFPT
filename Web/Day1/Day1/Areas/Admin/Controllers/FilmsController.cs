﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using PagedList;
using Service;
using Service.Interface;

namespace Day1.Areas.Admin.Controllers
{
    public class FilmsController : Controller
    {
        private IFilmService filmService = new FilmService();
        private ILanguageService languageService = new LanguageService();
        private const int PAGE_SIZE = 8;
        private ICommentService commentService = new CommentService();
        private film filmDetails = new film();
        public ActionResult Index(int ?page, string name)
        {
           if( Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }

            if (string.IsNullOrEmpty(name))
            {

                return View(filmService.GetAll().OrderBy(q => q.film_id).ToPagedList(page ?? 1, 12));
            }
            else
            {
                return View(filmService.FindFilm(name).OrderBy(q => q.film_id).ToPagedList(page ?? 1, 12));
            }

        }

        // GET: Admin/Films/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = filmService.GetByID(id.Value);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Admin/Films/Create
        public ActionResult Create()
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            ViewBag.language_id = new SelectList(languageService.GetAll(), "language_id", "name");
            ViewBag.original_language_id = new SelectList(languageService.GetAll(), "language_id", "name");
            return View();
        }

        // POST: Admin/Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "film_id,title,description,release_year,language_id,original_language_id,rental_duration,rental_rate,length,replacement_cost,rating,special_features,last_update,image,url")] film film)
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Image/"), filename);
                        film.image = filename;
                        film.last_update = DateTime.Now;
                        file.SaveAs(path);
                        filmService.Create(film);
                    }
                    else
                    {
                        film.last_update = DateTime.Now;
                        filmService.Create(film);
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.language_id = new SelectList(languageService.GetAll(), "language_id", "name", film.language_id);
            ViewBag.original_language_id = new SelectList(languageService.GetAll(), "language_id", "name", film.original_language_id);
            return View(film);
        }

        // GET: Admin/Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = filmService.GetByID(id.Value);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.language_id = new SelectList(languageService.GetAll(), "language_id", "name", film.language_id);
            ViewBag.original_language_id = new SelectList(languageService.GetAll(), "language_id", "name", film.original_language_id);
            return View(film);
        }

        // POST: Admin/Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "film_id,title,description,release_year,language_id,original_language_id,rental_duration,rental_rate,length,replacement_cost,rating,special_features,last_update,image,url")] film film)
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Image/"), filename);
                        film.image = filename;
                        film.last_update = DateTime.Now;
                        file.SaveAs(path);
                        filmService.Update(film);
                    }
                    else
                    {
                        film.last_update = DateTime.Now;
                        filmService.Update(film);
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.language_id = new SelectList(languageService.GetAll(), "language_id", "name", film.language_id);
            ViewBag.original_language_id = new SelectList(languageService.GetAll(), "language_id", "name", film.original_language_id);
            return View(film);
        }

        // GET: Admin/Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = filmService.GetByID(id.Value);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Admin/Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["loggedadmin"] == null)
            {
                return Redirect("/Account/Login");
            }
            filmService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               
            }
            base.Dispose(disposing);
        }
    }
}