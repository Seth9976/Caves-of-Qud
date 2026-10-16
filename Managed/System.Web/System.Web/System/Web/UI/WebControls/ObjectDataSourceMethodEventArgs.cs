using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserting" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updating" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleting" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002ED RID: 749
	public class ObjectDataSourceMethodEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> class using the specified input parameters collection.</summary>
		/// <param name="inputParameters">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that represent the names of the parameters of the business object method and their associated values. </param>
		// Token: 0x06001B7F RID: 7039 RVA: 0x0004373E File Offset: 0x0004193E
		public ObjectDataSourceMethodEventArgs(IOrderedDictionary inputParameters)
		{
			this._inputParameters = inputParameters;
		}

		/// <summary>Gets a collection that contains business object method parameters and their values.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent the business object method parameters and their corresponding values.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0004374D File Offset: 0x0004194D
		public IOrderedDictionary InputParameters
		{
			get
			{
				return this._inputParameters;
			}
		}

		// Token: 0x04001725 RID: 5925
		private IOrderedDictionary _inputParameters;
	}
}
