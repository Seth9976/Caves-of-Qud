using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that logs ASP.NET health-monitoring events into the Windows Application Event Log. </summary>
	// Token: 0x02000741 RID: 1857
	public sealed class EventLogWebEventProvider : WebEventProvider
	{
		// Token: 0x06004C6B RID: 19563 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal EventLogWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Moves events from the provider's buffer into the event log.</summary>
		// Token: 0x06004C6C RID: 19564 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C6D RID: 19565 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C6E RID: 19566 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
