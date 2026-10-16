using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines a provider interface for connecting two server controls using a single field of data.</summary>
	// Token: 0x02000464 RID: 1124
	public interface IWebPartRow
	{
		/// <summary>Gets the schema information for a data row that is used to share data between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060033BE RID: 13246
		PropertyDescriptorCollection Schema { get; }

		/// <summary>Returns the data for the row that is being used by the interface as the basis of a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="callback">A <see cref="T:System.Web.UI.WebControls.WebParts.RowCallback" /> delegate that contains the address of a method that receives the data.</param>
		// Token: 0x060033BF RID: 13247
		void GetRowData(RowCallback callback);
	}
}
