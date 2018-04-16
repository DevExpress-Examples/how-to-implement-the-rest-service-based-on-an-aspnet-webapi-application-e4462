Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports System.Web.Http
Imports Newtonsoft.Json

Namespace MyRestService.Controllers
    Public Class CategoriesController
        Inherits ApiController

        Private _context As TestDataEntities
        Public Sub New()
            _context = New TestDataEntities()
        End Sub
        Public Function [Get]() As IEnumerable(Of Category)
            Dim options = Request.GetQueryNameValuePairs().ToDictionary(Function(x) x.Key, Function(x) JsonConvert.DeserializeObject(x.Value)) 'parsed options

            'see the QueryHelper class for the implementation
            Dim query = _context.Categories.AsEnumerable().FilterByOptions(options).SortByOptions(options).PageByOptions(options) 'paging - sorting - filtering
            Return query
        End Function
        Public Function [Get](ByVal id As Integer) As Category
            Return _context.Categories.Find(id)
        End Function
        Public Function Post(ByVal cat As Category) As Integer
            _context.Categories.Add(cat)
            Return _context.SaveChanges()
        End Function
        Public Function Put(ByVal cat As Category) As Integer
            Dim categ As Category = _context.Categories.Find(cat.CategoryID)
            categ.CategoryName = cat.CategoryName
            Return _context.SaveChanges()
        End Function
        Public Function Delete(ByVal id As Integer) As Integer
            Dim cat As Category = _context.Categories.Find(id)
            _context.Categories.Remove(cat)
            Return _context.SaveChanges()
        End Function
    End Class
End Namespace
