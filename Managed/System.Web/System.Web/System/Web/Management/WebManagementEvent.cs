using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the base class for events that carry application and process information.</summary>
	// Token: 0x020006DE RID: 1758
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class WebManagementEvent : WebBaseEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebManagementEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004A80 RID: 19072 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebManagementEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebManagementEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004A81 RID: 19073 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebManagementEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets information about the ASP.NET application-hosting process.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebProcessInformation" /> object that contains information about the process.</returns>
		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebProcessInformation ProcessInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
