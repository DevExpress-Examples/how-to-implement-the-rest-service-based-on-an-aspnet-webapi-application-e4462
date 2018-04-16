using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace MyRestService.Controllers {
    public class CategoriesController : ApiController {
        private TestDataEntities _context;
        public CategoriesController() {
            _context = new TestDataEntities();
        }
        public IEnumerable<Category> Get() {
            var options = Request.GetQueryNameValuePairs()
                .ToDictionary(x => x.Key, x => JsonConvert.DeserializeObject(x.Value)); //parsed options

            //see the QueryHelper class for the implementation
            var query = _context.Categories.AsEnumerable()
                .FilterByOptions(options)   //filtering
                .SortByOptions(options)     //sorting
                .PageByOptions(options);    //paging
            return query;
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
