using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about security failures. </summary>
	// Token: 0x0200074F RID: 1871
	public class WebFailureAuditEvent : WebAuditEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebFailureAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CB5 RID: 19637 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebFailureAuditEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebFailureAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004CB6 RID: 19638 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the Audit Failure Events Raised performance counter.</summary>
		// Token: 0x06004CB7 RID: 19639 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
