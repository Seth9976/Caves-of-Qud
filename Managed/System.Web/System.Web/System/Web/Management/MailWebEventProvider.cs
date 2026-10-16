using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides the base functionality for creating event providers that send e-mail.</summary>
	// Token: 0x02000744 RID: 1860
	public abstract class MailWebEventProvider : BufferedWebEventProvider
	{
		// Token: 0x06004C80 RID: 19584 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal MailWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C81 RID: 19585 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		/// <param name="flushInfo">The <see cref="T:System.Web.Management.WebEventBufferFlushInfo" /> that contains the count of events waiting to send their notification.</param>
		// Token: 0x06004C82 RID: 19586 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C83 RID: 19587 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
