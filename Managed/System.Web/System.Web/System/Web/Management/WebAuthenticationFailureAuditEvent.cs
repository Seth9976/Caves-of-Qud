using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about ASP.NET authentication failures. </summary>
	// Token: 0x0200074E RID: 1870
	public class WebAuthenticationFailureAuditEvent : WebFailureAuditEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuthenticationFailureAuditEvent" /> class with the specified event parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		/// <param name="nameToAuthenticate">The name of the user to authenticate. </param>
		// Token: 0x06004CB2 RID: 19634 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebAuthenticationFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, string nameToAuthenticate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuthenticationFailureAuditEvent" /> class with the specified event parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value associated with the event.</param>
		/// <param name="nameToAuthenticate">The name of the user to authenticate.</param>
		// Token: 0x06004CB3 RID: 19635 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebAuthenticationFailureAuditEvent(string message, object eventSource, int eventCode, string nameToAuthenticate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the user to authenticate.</summary>
		/// <returns>The name of the user to authenticate.</returns>
		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
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
