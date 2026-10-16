using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectDisposing" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002E8 RID: 744
	public class ObjectDataSourceDisposingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs" /> class using the specified object.</summary>
		/// <param name="objectInstance">The business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> interacts to perform data operations.</param>
		// Token: 0x06001B70 RID: 7024 RVA: 0x000436F0 File Offset: 0x000418F0
		public ObjectDataSourceDisposingEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		/// <summary>Gets an object that represents the business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs data operations.</summary>
		/// <returns>The business object the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to data operations; otherwise, null, if null is passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" />.</returns>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000436FF File Offset: 0x000418FF
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
		}

		// Token: 0x04001722 RID: 5922
		private object _objectInstance;
	}
}
