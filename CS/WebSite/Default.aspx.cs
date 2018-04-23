using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;

public partial class _Default : System.Web.UI.Page {
	enum DataSourceType {
		Products,
		Categories,
		Shippers
	}

	protected void Page_Init(object sender, EventArgs e) {
		if (!this.IsPostBack)
			Session.Clear();

		grid.DataBind();
	}

	protected void grid_DataBinding(object sender, EventArgs e) {
		(sender as ASPxGridView).DataSource = GetDataSource();
	}
	protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
		Session["selectedDataSource"] = Int32.Parse(e.Parameters);

		grid.Columns.Clear();
		grid.AutoGenerateColumns = true;
		grid.KeyFieldName = String.Empty;
		grid.DataBind();		
	}

	
	private object GetDataSource() {
		object o = Session["selectedDataSource"];
		DataSourceType dsType = DataSourceType.Products;
		if (o != null)
			dsType = (DataSourceType)o;

		switch (dsType) {
			case DataSourceType.Categories:
				return dsCategories;
			case DataSourceType.Shippers:
				return dsShippers;
			default:
				return dsProducts;
		}
	}

}
