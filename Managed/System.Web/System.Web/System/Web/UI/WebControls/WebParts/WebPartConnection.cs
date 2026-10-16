using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides an object that enables two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls to form a connection. This class cannot be inherited.</summary>
	// Token: 0x020006B5 RID: 1717
	[ParseChildren(true, "Transformers")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public sealed class WebPartConnection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> class. </summary>
		// Token: 0x060048D4 RID: 18644 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartConnection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> object that is acting as the consumer control in a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that represents the Web Parts control acting as the consumer in a connection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The length of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartConnection.ConsumerID" /> property is zero.</exception>
		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart Consumer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the object that serves as a connection point for a control that is acting as a consumer in a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> associated with the consumer control in a connection. </returns>
		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the property value on a connection that references the ID of the object serving as the consumer connection point for that connection.</summary>
		/// <returns>A string that contains the ID for the consumer connection point.</returns>
		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060048D8 RID: 18648 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ConsumerConnectionPointID
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

		/// <summary>Gets or sets the property value on a connection that references the ID of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control acting as a consumer for that connection.</summary>
		/// <returns>A string that contains the ID of the control acting as a consumer in a connection. </returns>
		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060048DA RID: 18650 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ConsumerID
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

		/// <summary>Gets or sets the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object.</summary>
		/// <returns>A string that contains the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" />.</returns>
		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060048DC RID: 18652 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ID
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

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is currently established and able to exchange data between its provider and consumer controls.</summary>
		/// <returns>true if the connection is active; otherwise, false.</returns>
		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x060048DD RID: 18653 RVA: 0x000C7758 File Offset: 0x000C5958
		public bool IsActive
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is visible to all users or only to the current user.</summary>
		/// <returns>true if the connection is shared; otherwise, false.</returns>
		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x000C7774 File Offset: 0x000C5974
		public bool IsShared
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is declared in the markup of a Web page, or created programmatically. </summary>
		/// <returns>true if the connection is static; otherwise, false.</returns>
		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x000C7790 File Offset: 0x000C5990
		public bool IsStatic
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that acts as the provider in a Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that acts as the provider of data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The length of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartConnection.ProviderID" /> property value is zero.</exception>
		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the object that serves as a connection point for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control acting as a provider for a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> associated with the provider control in a connection.</returns>
		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the property value on a connection that references the ID of the object serving as the provider connection point for that connection.</summary>
		/// <returns>A string that contains the ID for a provider connection point object.</returns>
		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060048E3 RID: 18659 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ProviderConnectionPointID
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

		/// <summary>Gets or sets the property value on a connection that references the ID of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control acting as a provider for that connection.</summary>
		/// <returns>A string that contains the ID of the provider control.</returns>
		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060048E5 RID: 18661 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ProviderID
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

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object that is used to transform data between two otherwise incompatible connection points in a Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" />. The default value is null.</returns>
		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartTransformer Transformer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects used internally by the Web Parts control set. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformerCollection" /> containing <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</returns>
		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartTransformerCollection Transformers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
