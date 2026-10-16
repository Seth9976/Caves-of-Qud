using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Filtering" /> event of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	// Token: 0x0200030A RID: 778
	public class SqlDataSourceFilteringEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceFilteringEventArgs" /> class by using the specified object.</summary>
		/// <param name="parameterValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
		// Token: 0x06001BC2 RID: 7106 RVA: 0x00043880 File Offset: 0x00041A80
		public SqlDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that provides access to the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</returns>
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0004388F File Offset: 0x00041A8F
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x0400175F RID: 5983
		private IOrderedDictionary _parameterValues;
	}
}
