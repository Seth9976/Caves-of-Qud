using System;

namespace System.Web.Compilation
{
	/// <summary>Contains event data for the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainShutdown" /> event and the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainUnloaded" /> event. </summary>
	// Token: 0x02000639 RID: 1593
	public class BuildManagerHostUnloadEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.BuildManagerHostUnloadEventArgs" /> class. </summary>
		/// <param name="reason">The reason for the hosted application domain shutdown.</param>
		// Token: 0x06004492 RID: 17554 RVA: 0x000BA0D1 File Offset: 0x000B82D1
		public BuildManagerHostUnloadEventArgs(ApplicationShutdownReason reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets the reason the hosted application domain was shut down.</summary>
		/// <returns>One of the <see cref="T:System.Web.ApplicationShutdownReason" /> enumerated values.</returns>
		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x000BA0E0 File Offset: 0x000B82E0
		public ApplicationShutdownReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040024AF RID: 9391
		private ApplicationShutdownReason reason;
	}
}
