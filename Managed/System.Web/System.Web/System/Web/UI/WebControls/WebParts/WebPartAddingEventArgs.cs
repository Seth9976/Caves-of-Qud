using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdding" /> event. </summary>
	// Token: 0x020006CA RID: 1738
	public class WebPartAddingEventArgs : WebPartCancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAddingEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) to be added to a Web page or opened on a page.</param>
		/// <param name="zone">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" />  that <paramref name="webPart" /> is being added to.</param>
		/// <param name="zoneIndex">An integer that represents the ordinal position that <paramref name="webPart" /> occupies in <paramref name="zone" />, relative to other controls in <paramref name="zone" />.</param>
		// Token: 0x060049DB RID: 18907 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartAddingEventArgs(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Web Parts zone that the Web Parts control is being added to.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that the Web Parts control is being added to.</returns>
		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060049DD RID: 18909 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartZoneBase Zone
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

		/// <summary>Gets or sets the index position of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control within its zone.</summary>
		/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x000C7D08 File Offset: 0x000C5F08
		// (set) Token: 0x060049DF RID: 18911 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public int ZoneIndex
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
