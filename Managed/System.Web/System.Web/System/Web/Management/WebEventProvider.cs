using System;
using System.Configuration.Provider;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides the base class for non buffered event providers.</summary>
	// Token: 0x0200073E RID: 1854
	public abstract class WebEventProvider : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebEventProvider" /> class.</summary>
		// Token: 0x06004C5C RID: 19548 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected WebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Moves the events from the provider's buffer into the event log. </summary>
		// Token: 0x06004C5D RID: 19549
		public abstract void Flush();

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="raisedEvent">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C5E RID: 19550
		public abstract void ProcessEvent(WebBaseEvent raisedEvent);

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C5F RID: 19551
		public abstract void Shutdown();
	}
}
