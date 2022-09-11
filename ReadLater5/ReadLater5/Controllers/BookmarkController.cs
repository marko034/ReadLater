using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Models;
using Services;
using System.Collections.Generic;

namespace ReadLater5.Controllers
{
    [Authorize(AuthenticationSchemes = AuthSchemes.AuthScheme)]
    public class BookmarkController : Controller
    {
        IBookmarkService _bookmarkService;
        private readonly UserManager<IdentityUser> _userManager;

        public BookmarkController(IBookmarkService bookmarkService, UserManager<IdentityUser> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        // GET: BookmarkController
        public ActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            List<Bookmark> bookmarks = _bookmarkService.GetBookmarks(userId);
            return View(bookmarks);
        }

        // GET: BookmarkController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // GET: BookmarkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookmarkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                bookmark.CreatedByUserId = userId;
                _bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        // GET: BookmarkController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: BookmarkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                bookmark.CreatedByUserId = userId;
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(bookmark);
        }

        // GET: BookmarkController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: BookmarkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
