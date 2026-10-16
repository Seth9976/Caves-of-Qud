using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Transforms data in a Web Parts connection from a provider that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartRow" /> interface to a consumer expecting to receive data from a provider that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartParameters" /> interface.</summary>
	// Token: 0x020007B7 RID: 1975
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartParameters))]
	public sealed class RowToParametersTransformer : WebPartTransformer, IWebPartParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.RowToParametersTransformer" /> class. </summary>
		// Token: 0x06004FA5 RID: 20389 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public RowToParametersTransformer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the list of names in the consumer that will receive the values from the provider.</summary>
		/// <returns>An array of <see cref="T:System.String" /> values representing the consumer fields.</returns>
		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004FA7 RID: 20391 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string[] ConsumerFieldNames
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the list of field names from the provider.</summary>
		/// <returns>An array of <see cref="T:System.String" /> values representing the provider fields.</returns>
		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004FA9 RID: 20393 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string[] ProviderFieldNames
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		PropertyDescriptorCollection IWebPartParameters.get_Schema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides an ASP.NET control that allows the user to configure a <see cref="T:System.Web.UI.WebControls.WebParts.RowToParametersTransformer" /> transformer in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> zone.</summary>
		/// <returns>An ASP.NET control that displays a user interface (UI) that allows the user to configure a <see cref="T:System.Web.UI.WebControls.WebParts.RowToParametersTransformer" />.</returns>
		// Token: 0x06004FAB RID: 20395 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override Control CreateConfigurationControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void LoadConfigurationState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal override object SaveConfigurationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value of the data from the connection provider.</summary>
		/// <param name="callback">The delegate instance to be used when retrieving a value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> equals null.</exception>
		// Token: 0x06004FAE RID: 20398 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IWebPartParameters.GetParametersData(ParametersCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the property descriptors for the properties that the consumer receives when the <see cref="M:System.Web.UI.WebControls.WebParts.IWebPartParameters.GetParametersData(System.Web.UI.WebControls.WebParts.ParametersCallback)" /> method is called.</summary>
		/// <param name="schema">The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> object returned by <see cref="P:System.Web.UI.WebControls.WebParts.RowToParametersTransformer.System#Web#UI#WebControls#WebParts#IWebPartParameters#Schema" />.</param>
		// Token: 0x06004FAF RID: 20399 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IWebPartParameters.SetConsumerSchema(PropertyDescriptorCollection schema)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides an object for transforming the data.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data to be transformed.</returns>
		/// <param name="providerData">The provider data to be transformed.</param>
		// Token: 0x06004FB0 RID: 20400 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override object Transform(object providerData)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
