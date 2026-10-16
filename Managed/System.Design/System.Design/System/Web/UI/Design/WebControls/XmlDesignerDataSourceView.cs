using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a design-time view of data for the <see cref="T:System.Web.UI.Design.WebControls.XmlDataSourceDesigner" /> class.</summary>
	// Token: 0x020001BC RID: 444
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesignerDataSourceView : DesignerDataSourceView
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.XmlDesignerDataSourceView" /> class.</summary>
		/// <param name="owner">The parent <see cref="T:System.Web.UI.Design.WebControls.XmlDataSourceDesigner" />.</param>
		/// <param name="viewName">The name of the view in the data source.</param>
		// Token: 0x06000BC9 RID: 3017 RVA: 0x00009525 File Offset: 0x00007725
		public XmlDesignerDataSourceView(XmlDataSourceDesigner owner, string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
