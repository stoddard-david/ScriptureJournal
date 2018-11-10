using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Models.ScriptureJournalContext _context;

        public IndexModel(ScriptureJournal.Models.ScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
        public string SearchString { get; set; }
        public SelectList Books { get; set; }
        public string SearchBook { get; set; }
        public string SortBy { get; set; }


    public async Task OnGetAsync(string searchString, string searchBook, string sortBy)
      {
      // Use LINQ to get list of genres.
      IQueryable<string> bookQuery = from m in _context.Scripture
                                      orderby m.Book
                                      select m.Book;

      var scriptures = from m in _context.Scripture
                    select m;

      if (!String.IsNullOrEmpty(sortBy))
      {
        if (sortBy == "Book") { 
          scriptures = scriptures.OrderBy(o => o.Book);
        } else 
        {
          scriptures = scriptures.OrderBy(o => o.AddedDate);
        }
      }

      if (!String.IsNullOrEmpty(searchString))
      {
        scriptures = scriptures.Where(s => s.Note.Contains(searchString));
      }

      if (!String.IsNullOrEmpty(searchBook))
      {
        scriptures = scriptures.Where(x => x.Book == searchBook);
      }

      Books = new SelectList(await bookQuery.Distinct().ToListAsync());
      Scripture = await scriptures.ToListAsync();
      SearchString = searchString;
      SearchBook = searchBook;
      SortBy = sortBy;
      }
    }
}
