using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyRestService.Controllers {
    public class CategoriesController : ApiController {
        private TestDataEntities _context;
        public CategoriesController() {
            _context = new TestDataEntities();
        }
        public IEnumerable<Category> Get() {
            return _context.Categories.AsEnumerable();
        }
        public Category Get(int id) {
            return _context.Categories.Find(id);
        }
        public int Post(Category cat) {
            _context.Categories.Add(cat);
            return _context.SaveChanges();
        }
        public int Put(Category cat) {
            Category categ = _context.Categories.Find(cat.CategoryID);
            categ.CategoryName = cat.CategoryName;
            return _context.SaveChanges();
        }
        public int Delete(int id) {
            Category cat = _context.Categories.Find(id);
            _context.Categories.Remove(cat);
            return _context.SaveChanges();
        }
    }
}
