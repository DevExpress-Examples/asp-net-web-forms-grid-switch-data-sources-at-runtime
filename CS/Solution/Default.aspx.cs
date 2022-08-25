using DevExpress.Utils;
using DevExpress.Web;
using System;
using System.Linq;

namespace Solution {
    public partial class Default : System.Web.UI.Page {
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
}