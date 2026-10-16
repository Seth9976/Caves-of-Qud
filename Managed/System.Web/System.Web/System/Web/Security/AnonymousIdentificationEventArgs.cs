using System;

namespace System.Web.Security
{
	/// <summary>Provides data for the AnonymousIdentification_Creating event. This class cannot be inherited.</summary>
	// Token: 0x020004B1 RID: 1201
	public sealed class AnonymousIdentificationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.AnonymousIdentificationEventArgs" /> class.</summary>
		/// <param name="context">The context for the event.</param>
		// Token: 0x06003629 RID: 13865 RVA: 0x0008BC99 File Offset: 0x00089E99
		public AnonymousIdentificationEventArgs(HttpContext context)
		{
			this.context = context;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x0008BCA8 File Offset: 0x00089EA8
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets or sets the anonymous identifier for the user.</summary>
		/// <returns>The anonymous identifier for the user.</returns>
		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x0008BCB0 File Offset: 0x00089EB0
		// (set) Token: 0x0600362C RID: 13868 RVA: 0x0008BCB8 File Offset: 0x00089EB8
		public string AnonymousID
		{
			get
			{
				return this.anonymousId;
			}
			set
			{
				this.anonymousId = value;
			}
		}

		// Token: 0x04001DA4 RID: 7588
		private HttpContext context;

		// Token: 0x04001DA5 RID: 7589
		private string anonymousId;
	}
}
