using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the contract a Web Parts control implements to pass a parameter value in a Web Parts connection.</summary>
	// Token: 0x02000463 RID: 1123
	public interface IWebPartParameters
	{
		/// <summary>Gets the property descriptors for the data to be received by the consumer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060033BB RID: 13243
		PropertyDescriptorCollection Schema { get; }

		/// <summary>Gets the value of the data from the connection provider.</summary>
		/// <param name="callback">The method to call to process the data from the provider.</param>
		// Token: 0x060033BC RID: 13244
		void GetParametersData(ParametersCallback callback);

		/// <summary>Sets the property descriptors for the properties that the consumer receives when calling the <see cref="M:System.Web.UI.WebControls.WebParts.IWebPartParameters.GetParametersData(System.Web.UI.WebControls.WebParts.ParametersCallback)" /> method.</summary>
		/// <param name="schema">The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> returned by <see cref="P:System.Web.UI.WebControls.WebParts.IWebPartParameters.Schema" />.</param>
		// Token: 0x060033BD RID: 13245
		void SetConsumerSchema(PropertyDescriptorCollection schema);
	}
}
