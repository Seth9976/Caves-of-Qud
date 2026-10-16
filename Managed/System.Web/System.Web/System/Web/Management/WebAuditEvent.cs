using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Serves as the base class for all ASP.NET health-monitoring audit events. </summary>
	// Token: 0x0200074D RID: 1869
	public class WebAuditEvent : WebManagementEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CAF RID: 19631 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebAuditEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuditEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004CB0 RID: 19632 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Get the information associated with the Web request.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebRequestInformation" /> that contains the information associated with the Web request.</returns>
		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06004CB1 RID: 19633 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
