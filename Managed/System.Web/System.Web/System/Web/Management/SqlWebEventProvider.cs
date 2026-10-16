using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that saves event notifications to an SQL database.</summary>
	// Token: 0x02000749 RID: 1865
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SqlWebEventProvider : BufferedWebEventProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlWebEventProvider" /> class.</summary>
		// Token: 0x06004CA1 RID: 19617 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal SqlWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when event processing is complete.</summary>
		/// <param name="raisedEvents">A <see cref="T:System.Web.Management.WebBaseEventCollection" /> object of events raised.</param>
		// Token: 0x06004CA2 RID: 19618 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void EventProcessingComplete(WebBaseEventCollection raisedEvents)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004CA3 RID: 19619 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		/// <param name="flushInfo">The <see cref="T:System.Web.Management.WebEventBufferFlushInfo" /> object that contains the buffer information to be flushed.</param>
		// Token: 0x06004CA4 RID: 19620 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004CA5 RID: 19621 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
