using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Represents an abstract data source that data-bound controls bind to.</summary>
	// Token: 0x0200016C RID: 364
	public interface IDataSource
	{
		/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls. </summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000F2A RID: 3882
		// (remove) Token: 0x06000F2B RID: 3883
		event EventHandler DataSourceChanged;

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.IDataSource" />.</returns>
		/// <param name="viewName">The name of the view to retrieve. </param>
		// Token: 0x06000F2C RID: 3884
		DataSourceView GetView(string viewName);

		/// <summary>Gets a collection of names representing the list of view objects associated with the <see cref="T:System.Web.UI.IDataSource" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.IDataSource" />.</returns>
		// Token: 0x06000F2D RID: 3885
		ICollection GetViewNames();
	}
}
