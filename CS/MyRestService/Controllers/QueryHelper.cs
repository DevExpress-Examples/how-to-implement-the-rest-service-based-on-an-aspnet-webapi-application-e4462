using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic;

namespace MyRestService.Controllers {
    public static class QueryHelper {

        public static IEnumerable<Category> PageByOptions(this IEnumerable<Category> query, Dictionary<string, object> options) {
            if (options.ContainsKey("skip")) {
                var skip = Convert.ToInt32(options["skip"]);
                var take = Convert.ToInt32(options["take"]);
                query = query
                    .Skip(skip)
                    .Take(take);
            }
            return query;
        }

        public static IEnumerable<Category> SortByOptions(this IEnumerable<Category> query, Dictionary<string, object> options) {
            if (options.ContainsKey("sortOptions") && options["sortOptions"] != null) {
                var sortOptions = JObject.Parse(JArray.FromObject(options["sortOptions"])[0].ToString());
                var columnName = (string)sortOptions.SelectToken("selector");
                var descending = (bool)sortOptions.SelectToken("desc");

                if (descending)
                    columnName += " DESC";
                query = query.OrderBy(columnName);
            }
            return query;
        }


        public static IEnumerable<Category> FilterByOptions(this IEnumerable<Category> query, Dictionary<string, object> options) {
            if (options.ContainsKey("filterOptions") && options["filterOptions"] != null) {
                var filterTree = JArray.FromObject(options["filterOptions"]);
                return ReadExpression(query, filterTree);
            }
            return query;
        }

        public static IEnumerable<Category> ReadExpression(IEnumerable<Category> source, JArray array) {
            if (array[0].Type == JTokenType.String)
                return FilterQuery(source,
                    array[0].ToString(),
                    array[1].ToString(),
                    array[2].ToString());
            else {
                for (int i = 0; i < array.Count; i++) {
                    if (array[i].ToString().Equals("and"))
                        continue;
                    source = ReadExpression(source, (JArray)array[i]);
                }
                return source;
            }
        }

        public static IEnumerable<Category> FilterQuery(IEnumerable<Category> source, string ColumnName, string Clause, string Value) {
            switch (Clause) {
                case "=":
                    Value = System.Text.RegularExpressions.Regex.IsMatch(Value, @"^\d+$") ? Value : String.Format("\"{0}\"", Value);
                    source = source.Where(String.Format("{0} == {1}", ColumnName, Value));
                    break;
                case "contains":
                    source = source.Where(ColumnName + ".Contains(@0)", Value);
                    break;
                case "<>":
                    source = source.Where(string.Format("!{0}.StartsWith(\"{1}\")", ColumnName, Value));
                    break;
                default:
                    break;
            }
            return source;
        }
    }
}