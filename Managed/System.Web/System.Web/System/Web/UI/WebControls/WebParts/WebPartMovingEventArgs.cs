using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoving" /> event. </summary>
	// Token: 0x020006CC RID: 1740
	public class WebPartMovingEventArgs : WebPartCancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartMovingEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control being moved.</param>
		/// <param name="zone">The target <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> to which <paramref name="webPart" /> is being moved.</param>
		/// <param name="zoneIndex">An integer that indicates the index of <paramref name="webPart" /> relative to other controls within <paramref name="zone" />.</param>
		// Token: 0x060049E4 RID: 18916 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartMovingEventArgs(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Web Parts zone to which the Web Parts control is being moved.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> to which the Web Parts control is being moved.</returns>
		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060049E6 RID: 18918 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		/// <summary>Gets or sets the index position of a Web Parts control within its zone.</summary>
		/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x000C7D24 File Offset: 0x000C5F24
		// (set) Token: 0x060049E8 RID: 18920 RVA: 0x0000B3D8 File Offset: 0x000095D8
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
