using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that uses templates to define and format e-mails it sends for event notifications.</summary>
	// Token: 0x0200074A RID: 1866
	public sealed class TemplatedMailWebEventProvider : MailWebEventProvider
	{
		// Token: 0x06004CA6 RID: 19622 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal TemplatedMailWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the event notification object that provides the event information used by the e-mail template.</summary>
		/// <returns>The <see cref="T:System.Web.Management.MailEventNotificationInfo" /> object currently being processed.</returns>
		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static MailEventNotificationInfo CurrentNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
