using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public BookmarkService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            if(bookmark.Category.Name != null)
            {
                Category category = _ReadLaterDataContext.Categories
                    .Where(x => x.Name == bookmark.Category.Name).FirstOrDefault();
                if(category != null)
                {
                    bookmark.CategoryId= category.ID;
                    bookmark.Category = null;
                }
            }
            else
            {
                bookmark.CategoryId = null;
                bookmark.Category = null;
            }
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            if(bookmark.Category != null)
            {
                _ReadLaterDataContext.Categories.Remove(bookmark.Category);
            }
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id)
                .Include(b => b.Category)
                .FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks(string userId)
        {
            return _ReadLaterDataContext.Bookmark.Where(x=>x.CreatedByUserId == userId).Include(b => b.Category).ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
