using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event. </summary>
	// Token: 0x020006C6 RID: 1734
	public class WebPartDisplayModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> class. </summary>
		/// <param name="oldDisplayMode">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> applied to the Web Parts control before the display mode is changed.  </param>
		// Token: 0x060049CD RID: 18893 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartDisplayModeEventArgs(WebPartDisplayMode oldDisplayMode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the former display mode for a Web Part control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> applied to a Web Parts control before the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event.</returns>
		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060049CF RID: 18895 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartDisplayMode OldDisplayMode
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
	}
}
