using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about successful authentication events. </summary>
	// Token: 0x02000750 RID: 1872
	public class WebAuthenticationSuccessAuditEvent : WebSuccessAuditEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebSuccessAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event. </param>
		/// <param name="nameToAuthenticate">The name of the authenticated user. </param>
		// Token: 0x06004CB8 RID: 19640 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebAuthenticationSuccessAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, string nameToAuthenticate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebAuthenticationSuccessAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
		/// <param name="nameToAuthenticate">The name of the authenticated user.</param>
		// Token: 0x06004CB9 RID: 19641 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebAuthenticationSuccessAuditEvent(string message, object eventSource, int eventCode, string nameToAuthenticate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the authenticated user.</summary>
		/// <returns>The name of the authenticated user.</returns>
		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string NameToAuthenticate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
