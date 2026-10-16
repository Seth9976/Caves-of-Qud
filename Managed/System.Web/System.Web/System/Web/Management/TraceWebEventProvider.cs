using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that sends ASP.NET health-monitoring events as trace messages.</summary>
	// Token: 0x0200074B RID: 1867
	public sealed class TraceWebEventProvider : WebEventProvider
	{
		// Token: 0x06004CA8 RID: 19624 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal TraceWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		// Token: 0x06004CA9 RID: 19625 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004CAA RID: 19626 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004CAB RID: 19627 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
