using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" />, and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> events. </summary>
	// Token: 0x02000472 RID: 1138
	public class WebPartEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) involved in the event. </param>
		// Token: 0x060033F2 RID: 13298 RVA: 0x00087F6E File Offset: 0x0008616E
		public WebPartEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		/// <summary>Gets the Web Parts control involved in the event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> involved in a <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" />, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event.</returns>
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x00087F7D File Offset: 0x0008617D
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
		}

		// Token: 0x04001CEC RID: 7404
		private WebPart _webPart;
	}
}
