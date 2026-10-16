using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Represents an event provider that routes ASP.NET tracing events to the IIS 7.0 infrastructure.</summary>
	// Token: 0x02000742 RID: 1858
	public sealed class IisTraceWebEventProvider : WebEventProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.Iis7TraceWebEventProvider" /> class.</summary>
		// Token: 0x06004C6F RID: 19567 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IisTraceWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Moves the events from the provider's buffer into the event log.</summary>
		// Token: 0x06004C70 RID: 19568 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event that was passed to the provider.</summary>
		/// <param name="eventRaised">The object to process.</param>
		// Token: 0x06004C71 RID: 19569 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks that are associated with shutting down the provider.</summary>
		// Token: 0x06004C72 RID: 19570 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
