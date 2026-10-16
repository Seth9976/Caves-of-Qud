using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Contains the parameters defining the flush buffer characteristics.</summary>
	// Token: 0x0200073F RID: 1855
	public sealed class WebEventBufferFlushInfo
	{
		// Token: 0x06004C60 RID: 19552 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal WebEventBufferFlushInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the events collection in the current message.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebBaseEventCollection" /> events contained in the current message.</returns>
		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06004C61 RID: 19553 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebBaseEventCollection Events
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of events dropped since the last notification.</summary>
		/// <returns>The number of events dropped by the buffering mechanism since the last notification.</returns>
		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06004C62 RID: 19554 RVA: 0x000C8558 File Offset: 0x000C6758
		public int EventsDiscardedSinceLastNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of events in the buffer.</summary>
		/// <returns>The number of events in the buffer. </returns>
		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06004C63 RID: 19555 RVA: 0x000C8574 File Offset: 0x000C6774
		public int EventsInBuffer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the date and the time of the last notification.</summary>
		/// <returns>The date and the time of the last notification.</returns>
		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x000C8590 File Offset: 0x000C6790
		public DateTime LastNotificationUtc
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the message sequence in the current notification.</summary>
		/// <returns>The number indicating the message sequence order in the current notification, beginning with an index of zero.</returns>
		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06004C65 RID: 19557 RVA: 0x000C85AC File Offset: 0x000C67AC
		public int NotificationSequence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the current notification type.</summary>
		/// <returns>One of the <see cref="T:System.Web.Management.EventNotificationType" /> values.</returns>
		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06004C66 RID: 19558 RVA: 0x000C85C8 File Offset: 0x000C67C8
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
