using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.AuthorizeWebPart" /> event. </summary>
	// Token: 0x02000487 RID: 1159
	public class WebPartAuthorizationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAuthorizationEventArgs" /> class. </summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the control being checked for authorization. </param>
		/// <param name="path">The relative application path to the source file for the control being authorized, if the control is a user control. </param>
		/// <param name="authorizationFilter">An arbitrary string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used for authorizing whether a control can be added to a page. </param>
		/// <param name="isShared">Indicates whether the control being checked for authorization is a shared control, meaning that it is visible to many or all users of the application, and its <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsShared" /> property value is set to true. </param>
		// Token: 0x0600349C RID: 13468 RVA: 0x000888AC File Offset: 0x00086AAC
		public WebPartAuthorizationEventArgs(Type type, string path, string authorizationFilter, bool isShared)
		{
			this.type = type;
			this.path = path;
			this.authorizationFilter = authorizationFilter;
			this.isShared = isShared;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the Web Parts control being checked for authorization.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the control being checked for authorization.</returns>
		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000888D1 File Offset: 0x00086AD1
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the relative application path to the source file for the control being authorized, if the control is a user control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the relative application path.</returns>
		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000888D9 File Offset: 0x00086AD9
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets the string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used for authorizing whether a control can be added to a page.</summary>
		/// <returns>A <see cref="T:System.String" /> used in determining whether a control is authorized to be added to a page.</returns>
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000888E1 File Offset: 0x00086AE1
		public string AuthorizationFilter
		{
			get
			{
				return this.authorizationFilter;
			}
		}

		/// <summary>Gets a value that indicates whether a Web Parts control is visible to all users of a Web Parts page.</summary>
		/// <returns>true if the Web Parts control is visible to all users of the page; otherwise, false.</returns>
		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000888E9 File Offset: 0x00086AE9
		public bool IsShared
		{
			get
			{
				return this.isShared;
			}
		}

		/// <summary>Gets or sets the value indicating whether a Web Parts control can be added to a page.</summary>
		/// <returns>true if the Web Parts control can be added to the page; otherwise, false.</returns>
		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000888F1 File Offset: 0x00086AF1
		// (set) Token: 0x060034A2 RID: 13474 RVA: 0x000888F9 File Offset: 0x00086AF9
		public bool IsAuthorized
		{
			get
			{
				return this.authorized;
			}
			set
			{
				this.authorized = value;
			}
		}

		// Token: 0x04001D2A RID: 7466
		private bool authorized;

		// Token: 0x04001D2B RID: 7467
		private Type type;

		// Token: 0x04001D2C RID: 7468
		private string path;

		// Token: 0x04001D2D RID: 7469
		private string authorizationFilter;

		// Token: 0x04001D2E RID: 7470
		private bool isShared;
	}
}
