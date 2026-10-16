using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides event data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartZoneBase.CreateVerbs" /> event that is used by the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartZoneBase.OnCreateVerbs(System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs)" /> method.</summary>
	// Token: 0x02000476 RID: 1142
	public class WebPartVerbsEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs" /> class.</summary>
		// Token: 0x060033F8 RID: 13304 RVA: 0x00087F85 File Offset: 0x00086185
		public WebPartVerbsEventArgs()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs" /> class using the specified Web Parts verb collection.</summary>
		/// <param name="verbs">A Web Parts verb collection.</param>
		// Token: 0x060033F9 RID: 13305 RVA: 0x00087F8E File Offset: 0x0008618E
		public WebPartVerbsEventArgs(WebPartVerbCollection verbs)
		{
			this._verbs = verbs;
		}

		/// <summary>Gets or sets the Web Parts verbs used in the event data.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" />.</returns>
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x00087F9D File Offset: 0x0008619D
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x00087FB3 File Offset: 0x000861B3
		public WebPartVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					return WebPartVerbCollection.Empty;
				}
				return this._verbs;
			}
			set
			{
				this._verbs = value;
			}
		}

		// Token: 0x04001CF5 RID: 7413
		private WebPartVerbCollection _verbs;
	}
}
