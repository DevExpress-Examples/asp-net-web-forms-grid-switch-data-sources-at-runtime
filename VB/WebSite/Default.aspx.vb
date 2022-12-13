Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private Enum DataSourceType
		Products
		Categories
		Shippers
	End Enum

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		If (Not Me.IsPostBack) Then
			Session.Clear()
		End If

		grid.DataBind()
	End Sub

	Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
		TryCast(sender, ASPxGridView).DataSource = GetDataSource()
	End Sub
	Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Session("selectedDataSource") = Int32.Parse(e.Parameters)

		grid.Columns.Clear()
		grid.AutoGenerateColumns = True
		grid.KeyFieldName = String.Empty
		grid.DataBind()
	End Sub


	Private Function GetDataSource() As Object
		Dim o As Object = Session("selectedDataSource")
		Dim dsType As DataSourceType = DataSourceType.Products
		If o IsNot Nothing Then
			dsType = CType(o, DataSourceType)
		End If

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
