using System;

namespace System.Web.SessionState
{
	/// <summary>Represents session-state data for a session store.</summary>
	// Token: 0x020004A1 RID: 1185
	public class SessionStateStoreData
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.SessionState.SessionStateStoreData" /> class.</summary>
		/// <param name="sessionItems">The session variables and values for the current session.</param>
		/// <param name="staticObjects">The <see cref="T:System.Web.HttpStaticObjectsCollection" /> for the current session.</param>
		/// <param name="timeout">The <see cref="P:System.Web.SessionState.SessionStateStoreData.Timeout" /> for the current session.</param>
		// Token: 0x060035BE RID: 13758 RVA: 0x0008BA22 File Offset: 0x00089C22
		public SessionStateStoreData(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
		{
			this.sessionItems = sessionItems;
			this.staticObjects = staticObjects;
			this.timeout = timeout;
		}

		/// <summary>The session variables and values for the current session.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.ISessionStateItemCollection" /> object that contains variables and values for the current session.</returns>
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x0008BA3F File Offset: 0x00089C3F
		public virtual ISessionStateItemCollection Items
		{
			get
			{
				return this.sessionItems;
			}
		}

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x0008BA47 File Offset: 0x00089C47
		public virtual HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this.staticObjects;
			}
		}

		/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period in minutes.</returns>
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x0008BA4F File Offset: 0x00089C4F
		// (set) Token: 0x060035C2 RID: 13762 RVA: 0x0008BA57 File Offset: 0x00089C57
		public virtual int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x04001D8B RID: 7563
		private ISessionStateItemCollection sessionItems;

		// Token: 0x04001D8C RID: 7564
		private HttpStaticObjectsCollection staticObjects;

		// Token: 0x04001D8D RID: 7565
		private int timeout;
	}
}
