using System;
using System.Net.Mail;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information to the <see cref="T:System.Web.Management.TemplatedMailWebEventProvider" /> object about the current event notification.</summary>
	// Token: 0x02000743 RID: 1859
	public sealed class MailEventNotificationInfo
	{
		// Token: 0x06004C73 RID: 19571 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal MailEventNotificationInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of events for the current message.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebBaseEventCollection" /> of events for the current message.</returns>
		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebBaseEventCollection Events
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of events discarded by the buffer since the last notification.</summary>
		/// <returns>The number of events discarded by the buffer since the last notification.</returns>
		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x000C861C File Offset: 0x000C681C
		public int EventsDiscardedByBuffer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events that are discarded by the buffer because the buffer has exceeded the message limit for the current notification.</summary>
		/// <returns>The number of events discarded by the buffer because the buffer has exceeded the message limit for the current notification.</returns>
		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x000C8638 File Offset: 0x000C6838
		public int EventsDiscardedDueToMessageLimit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events remaining in the buffer after the current notification.</summary>
		/// <returns>The number of events remaining in the buffer after the current notification.</returns>
		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x000C8654 File Offset: 0x000C6854
		public int EventsInBuffer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events being processed for the current notification.</summary>
		/// <returns>The number of events being processed for the current notification.</returns>
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x000C8670 File Offset: 0x000C6870
		public int EventsInNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events remaining in the buffer after the current notification.</summary>
		/// <returns>The number of events remaining in the buffer after the current notification.</returns>
		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x000C868C File Offset: 0x000C688C
		public int EventsRemaining
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the time of the previous notification.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that specifies the time of the previous notification.</returns>
		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x000C86A8 File Offset: 0x000C68A8
		public DateTime LastNotificationUtc
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the e-mail message that will be sent as the current message.</summary>
		/// <returns>A <see cref="T:System.Web.Mail.MailMessage" /> that specifies the e-mail message to send.</returns>
		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public MailMessage Message
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the position of this message in the sequence for the current notification.</summary>
		/// <returns>The position of this message in the sequence for the current notification.</returns>
		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x000C86C4 File Offset: 0x000C68C4
		public int MessageSequence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the total number of messages in the current notification.</summary>
		/// <returns>The total number of messages in the current notification.</returns>
		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x000C86E0 File Offset: 0x000C68E0
		public int MessagesInNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the position of this notification within the total number of notifications made to date.</summary>
		/// <returns>The position of this notification within the total number of notifications made to date.</returns>
		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x000C86FC File Offset: 0x000C68FC
		public int NotificationSequence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the type of the current notification.</summary>
		/// <returns>The <see cref="T:System.Web.Management.EventNotificationType" /> for the current notification.</returns>
		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x000C8718 File Offset: 0x000C6918
		public EventNotificationType NotificationType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EventNotificationType.Regular;
			}
		}
	}
}
