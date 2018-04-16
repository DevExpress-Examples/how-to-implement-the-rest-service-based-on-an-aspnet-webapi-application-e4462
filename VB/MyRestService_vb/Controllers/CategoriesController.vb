Imports System.Net
Imports System.Web.Http

Public Class CategoriesController
    Inherits ApiController
    Private _context As TestDataEntities
    Public Sub New()
        _context = New TestDataEntities()
    End Sub
    Public Function [Get]() As IEnumerable(Of Category)
        Return _context.Categories.AsEnumerable()
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
