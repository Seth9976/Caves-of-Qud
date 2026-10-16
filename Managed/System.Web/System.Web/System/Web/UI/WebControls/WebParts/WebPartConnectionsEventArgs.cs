using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides the event data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnected" /> and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnected" /> events of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
	// Token: 0x020006CE RID: 1742
	public class WebPartConnectionsEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsEventArgs" /> class without requiring a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object.</summary>
		/// <param name="provider">The control acting as the provider.</param>
		/// <param name="providerConnectionPoint">The provider connection point.</param>
		/// <param name="consumer">The control acting as the consumer.</param>
		/// <param name="consumerConnectionPoint">The consumer connection point.</param>
		// Token: 0x060049ED RID: 18925 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartConnectionsEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the class using the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object.</summary>
		/// <param name="provider">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control acting as the provider.</param>
		/// <param name="providerConnectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> for the connection.</param>
		/// <param name="consumer">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control acting as the consumer.</param>
		/// <param name="consumerConnectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />  for the connection.</param>
		/// <param name="connection">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object.</param>
		// Token: 0x060049EE RID: 18926 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartConnectionsEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartConnection connection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object of the current connection.</summary>
		/// <returns>The current <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" />.</returns>
		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartConnection Connection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is acting as the consumer in the connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> acting as the consumer.</returns>
		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart Consumer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> object of the current connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> of the current connection.</returns>
		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is acting as the provider in the connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> acting as the provider.</returns>
		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> object of the current connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> of the current connection.</returns>
		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
