using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Filtering" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002EB RID: 747
	public class ObjectDataSourceFilteringEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs" /> class by using the specified object.</summary>
		/// <param name="parameterValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
		// Token: 0x06001B79 RID: 7033 RVA: 0x00043727 File Offset: 0x00041927
		public ObjectDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" />  interface that provides access to the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x00043736 File Offset: 0x00041936
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x04001724 RID: 5924
		private IOrderedDictionary _parameterValues;
	}
}
