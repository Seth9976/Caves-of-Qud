using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" /> event, and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event. </summary>
	// Token: 0x02000470 RID: 1136
	public class WebPartCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) involved in the event. </param>
		// Token: 0x060033EB RID: 13291 RVA: 0x00087F4E File Offset: 0x0008614E
		public WebPartCancelEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		/// <summary>Gets the Web Parts control involved in the cancelable event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> involved in a <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" />, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event.</returns>
		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x00087F5D File Offset: 0x0008615D
		// (set) Token: 0x060033ED RID: 13293 RVA: 0x00087F65 File Offset: 0x00086165
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
			set
			{
				this._webPart = value;
			}
		}

		// Token: 0x04001CEB RID: 7403
		private WebPart _webPart;
	}
}
