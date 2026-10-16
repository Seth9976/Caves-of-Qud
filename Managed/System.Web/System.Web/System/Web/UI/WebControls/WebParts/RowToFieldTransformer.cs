using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Transforms data in a Web Parts connection from a provider that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartRow" /> interface to a consumer expecting data through the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartField" /> interface.</summary>
	// Token: 0x020007B5 RID: 1973
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartField))]
	public sealed class RowToFieldTransformer : WebPartTransformer, IWebPartField
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.RowToFieldTransformer" /> class. </summary>
		// Token: 0x06004F97 RID: 20375 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public RowToFieldTransformer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the value to transform.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the field to transform.</returns>
		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F99 RID: 20377 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string FieldName
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

		// Token: 0x06004F9A RID: 20378 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		PropertyDescriptor IWebPartField.get_Schema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Displays an ASP.NET control that configures a <see cref="T:System.Web.UI.WebControls.WebParts.RowToFieldTransformer" /> transformer in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> zone.</summary>
		/// <returns>An ASP.NET control that configures a <see cref="T:System.Web.UI.WebControls.WebParts.RowToFieldTransformer" />.</returns>
		// Token: 0x06004F9B RID: 20379 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override Control CreateConfigurationControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void LoadConfigurationState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal override object SaveConfigurationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value of the field that is being used by the interface as the basis of a connection between two Web Parts controls.</summary>
		/// <param name="callback">The delegate instance to be used when retrieving a value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> equals null.</exception>
		// Token: 0x06004F9E RID: 20382 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IWebPartField.GetFieldValue(FieldCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides an object for transforming the data.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data to be transformed.</returns>
		/// <param name="providerData">The provider data to be transformed.</param>
		// Token: 0x06004F9F RID: 20383 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override object Transform(object providerData)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
