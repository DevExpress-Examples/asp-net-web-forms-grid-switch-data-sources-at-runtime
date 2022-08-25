Imports DevExpress.Web
Imports System

Namespace Solution

    Public Partial Class [Default]
        Inherits Web.UI.Page

        Friend Enum DataSourceType
            Products
            Categories
            Shippers
        End Enum

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack Then Session.Clear()
            grid.DataBind()
        End Sub

        Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
            TryCast(sender, ASPxGridView).DataSource = GetDataSource()
        End Sub

        Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
            Session("selectedDataSource") = Integer.Parse(e.Parameters)
            grid.Columns.Clear()
            grid.AutoGenerateColumns = True
            grid.KeyFieldName = String.Empty
            grid.DataBind()
        End Sub

        Private Function GetDataSource() As Object
            Dim o As Object = Session("selectedDataSource")
            Dim dsType As DataSourceType = DataSourceType.Products
            If o IsNot Nothing Then dsType = CType(o, DataSourceType)
            Select Case dsType
                Case DataSourceType.Categories
                    Return dsCategories
                Case DataSourceType.Shippers
                    Return dsShippers
                Case Else
                    Return dsProducts
            End Select
        End Function
    End Class
End Namespace
