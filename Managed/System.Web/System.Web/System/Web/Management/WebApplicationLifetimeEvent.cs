using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Represents a significant event in the lifetime of an application.</summary>
	// Token: 0x0200074C RID: 1868
	public class WebApplicationLifetimeEvent : WebManagementEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebApplicationLifetimeEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The message associated with the event. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CAC RID: 19628 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebApplicationLifetimeEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebApplicationLifetimeEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The message associated with the event.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event. </param>
		// Token: 0x06004CAD RID: 19629 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebApplicationLifetimeEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally to increment performance counters.</summary>
		// Token: 0x06004CAE RID: 19630 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
