using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about systemic errors.</summary>
	// Token: 0x02000752 RID: 1874
	public class WebErrorEvent : WebBaseErrorEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebErrorEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004CBE RID: 19646 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebErrorEvent(string message, object eventSource, int eventCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebErrorEvent" /> class using the supplied parameters. </summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004CBF RID: 19647 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the application request information.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebRequestInformation" /> object that contains information about the current request.</returns>
		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06004CC0 RID: 19648 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the application-thread information.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebThreadInformation" /> object that contains information about the current thread.</returns>
		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebThreadInformation ThreadInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally to increment performance counters.</summary>
		// Token: 0x06004CC2 RID: 19650 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
