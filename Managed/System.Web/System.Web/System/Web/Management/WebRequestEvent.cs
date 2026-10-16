using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the base class for events providing Web-request information.</summary>
	// Token: 0x02000756 RID: 1878
	public class WebRequestEvent : WebManagementEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The message associated with the event.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The <see cref="T:System.Web.Management.WebEventCodes" /> code associated with the event. It must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CD2 RID: 19666 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebRequestEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The message associated with the event.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The <see cref="T:System.Web.Management.WebEventCodes" /> code associated with the event. It must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> detail code associated with the event.</param>
		// Token: 0x06004CD3 RID: 19667 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebRequestEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the information associated with the Web-application request.</summary>
		/// <returns>The information associated with the Web-application request.</returns>
		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally to increment the performance counters.</summary>
		// Token: 0x06004CD5 RID: 19669 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
