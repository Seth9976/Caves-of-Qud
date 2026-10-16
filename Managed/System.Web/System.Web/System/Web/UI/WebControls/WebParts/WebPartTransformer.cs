using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides basic implementation for transformer classes to convert data between two incompatible connection points.</summary>
	// Token: 0x020006B6 RID: 1718
	public abstract class WebPartTransformer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class. </summary>
		// Token: 0x060048E8 RID: 18664 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected WebPartTransformer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Displays an ASP.NET control that configures a transformer in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> zone.</summary>
		/// <returns>An ASP.NET control that configures a transformer.</returns>
		// Token: 0x060048E9 RID: 18665 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual Control CreateConfigurationControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Loads the configuration state saved with the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartTransformer.SaveConfigurationState" /> method.</summary>
		/// <param name="savedState">An object containing configuration state saved by using <see cref="M:System.Web.UI.WebControls.WebParts.WebPartTransformer.SaveConfigurationState" />.</param>
		// Token: 0x060048EA RID: 18666 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void LoadConfigurationState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the configuration state set by the user in the ASP.NET configuration control. </summary>
		/// <returns>An object representing the configuration state.</returns>
		// Token: 0x060048EB RID: 18667 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal virtual object SaveConfigurationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When implemented, provides an object for transforming the data.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data to be transformed.</returns>
		/// <param name="providerData">The provider data to be transformed.</param>
		// Token: 0x060048EC RID: 18668
		public abstract object Transform(object providerData);
	}
}
