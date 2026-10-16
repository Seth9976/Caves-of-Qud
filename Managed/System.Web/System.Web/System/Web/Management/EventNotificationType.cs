using System;

namespace System.Web.Management
{
	/// <summary>Specifies the type of event notification.</summary>
	// Token: 0x02000524 RID: 1316
	public enum EventNotificationType
	{
		/// <summary>The notification of an event is triggered on a regularly scheduled interval.</summary>
		// Token: 0x04001F59 RID: 8025
		Regular,
		/// <summary>Notification triggered by exceeding the urgent event threshold.</summary>
		// Token: 0x04001F5A RID: 8026
		Urgent,
		/// <summary>The notification of an event is triggered by a requested flush.</summary>
		// Token: 0x04001F5B RID: 8027
		Flush,
		/// <summary>Every event is treated as if a flush has occurred.</summary>
		// Token: 0x04001F5C RID: 8028
		Unbuffered
	}
}
