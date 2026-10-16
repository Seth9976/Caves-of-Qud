using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a way for developers to declare static connections in a content page when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control has been declared in the content page's associated master page.</summary>
	// Token: 0x020007B4 RID: 1972
	[ParseChildren(true)]
	[Bindable(false)]
	[NonVisualControl]
	[PersistChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.ProxyWebPartManagerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProxyWebPartManager : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPartManager" /> class. </summary>
		// Token: 0x06004F95 RID: 20373 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ProxyWebPartManager()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of static connections declared within the &lt;asp:proxywebpartmanager&gt; element on a content page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection" /> that contains all static <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects declared within an &lt;asp:proxywebpartmanager&gt; element. </returns>
		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06004F96 RID: 20374 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ProxyWebPartConnectionCollection StaticConnections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
