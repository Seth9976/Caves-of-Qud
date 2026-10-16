using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Exposes the properties that are used to display a single item in a data-bound control.</summary>
	// Token: 0x020002CE RID: 718
	public interface IDataBoundItemControl : IDataBoundControl
	{
		/// <summary>Gets the object that represents the data-key value of the row in a data-bound control.</summary>
		/// <returns>The object that represents the data-key value of the row in the data-bound control.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001B2C RID: 6956
		DataKey DataKey { get; }

		/// <summary>Gets the current mode of a data-bound control.</summary>
		/// <returns>The current mode of the data-bound control.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001B2D RID: 6957
		DataBoundControlMode Mode { get; }
	}
}
