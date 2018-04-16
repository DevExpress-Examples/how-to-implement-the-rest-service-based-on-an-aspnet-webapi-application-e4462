Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Http
Imports System.Web.Mvc
Imports System.Web.Routing

Namespace MyRestService
    Public Class MvcApplication
        Inherits System.Web.HttpApplication

        Private Shared Sub EnableCrossDomain()
            Dim origin As String = HttpContext.Current.Request.Headers("Origin")
            If String.IsNullOrEmpty(origin) Then
                Return
            End If
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin)
            Dim method As String = HttpContext.Current.Request.Headers("Access-Control-Request-Method")
            If Not String.IsNullOrEmpty(method) Then
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", method)
            End If
            Dim headers As String = HttpContext.Current.Request.Headers("Access-Control-Request-Headers")
            If Not String.IsNullOrEmpty(headers) Then
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", headers)
            End If
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true")
            If HttpContext.Current.Request.HttpMethod = "OPTIONS" Then
                HttpContext.Current.Response.StatusCode = 204
                HttpContext.Current.Response.End()
            End If
        End Sub
        Protected Sub Application_Start()
            AreaRegistration.RegisterAllAreas()

            WebApiConfig.Register(GlobalConfiguration.Configuration)
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        End Sub
        Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            EnableCrossDomain()
        End Sub
    End Class
End Namespace