using System;

namespace System.Web.UI
{
	/// <summary>Stores ASP.NET page view state on the Web server.</summary>
	// Token: 0x0200021E RID: 542
	public class SessionPageStatePersister : PageStatePersister
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SessionPageStatePersister" /> class.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.SessionState.HttpSessionState" /> is null (Nothing in Visual Basic)</exception>
		// Token: 0x06001624 RID: 5668 RVA: 0x00039605 File Offset: 0x00037805
		public SessionPageStatePersister(Page page)
			: base(page)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes and loads persisted state from the server-side session object when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
		/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.SessionPageStatePersister.Load" /> method could not successfully deserialize the state contained in the request to the Web server.</exception>
		// Token: 0x06001625 RID: 5669 RVA: 0x00003A2B File Offset: 0x00001C2B
		public override void Load()
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes any object state contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> or the <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> property and writes the state to the session object.</summary>
		// Token: 0x06001626 RID: 5670 RVA: 0x00003A2B File Offset: 0x00001C2B
		public override void Save()
		{
			throw new NotImplementedException();
		}
	}
}
