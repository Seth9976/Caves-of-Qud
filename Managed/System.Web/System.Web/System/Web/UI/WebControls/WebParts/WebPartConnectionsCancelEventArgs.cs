using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> events. </summary>
	// Token: 0x020006D0 RID: 1744
	public class WebPartConnectionsCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsCancelEventArgs" /> class without a specified connection. </summary>
		/// <param name="provider">The Web Parts control providing data in the connection. </param>
		/// <param name="providerConnectionPoint">The connection point for providing data. </param>
		/// <param name="consumer">The Web Parts control consuming data in the connection. </param>
		/// <param name="consumerConnectionPoint">The connection point for consuming data. </param>
		// Token: 0x060049F8 RID: 18936 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartConnectionsCancelEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsCancelEventArgs" /> class with the specified connection. </summary>
		/// <param name="provider">The Web Parts control providing data in the connection. </param>
		/// <param name="providerConnectionPoint">The connection point for providing data. </param>
		/// <param name="consumer">The Web Parts control consuming data in the connection. </param>
		/// <param name="consumerConnectionPoint">The connection point for consuming data. </param>
		/// <param name="connection">The Web Parts connection involved in the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> event. </param>
		// Token: 0x060049F9 RID: 18937 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartConnectionsCancelEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartConnection connection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the Web Parts connection involved in the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> involved in the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> event.</returns>
		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060049FA RID: 18938 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartConnection Connection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the Web Parts control acting as the consumer in the connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> acting as the consumer in the connection.</returns>
		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart Consumer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the connection point that consumes data in the Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />.</returns>
		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x060049FC RID: 18940 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the Web Parts control acting as the provider in the connection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> acting as the provider in the connection.</returns>
		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the connection point that provides data in the Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" />.</returns>
		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
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
