using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the reason a thread is waiting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000286 RID: 646
	public enum ThreadWaitReason
	{
		/// <summary>The thread is waiting for event pair high.</summary>
		// Token: 0x04000B9B RID: 2971
		EventPairHigh = 7,
		/// <summary>The thread is waiting for event pair low.</summary>
		// Token: 0x04000B9C RID: 2972
		EventPairLow,
		/// <summary>Thread execution is delayed.</summary>
		// Token: 0x04000B9D RID: 2973
		ExecutionDelay = 4,
		/// <summary>The thread is waiting for the scheduler.</summary>
		// Token: 0x04000B9E RID: 2974
		Executive = 0,
		/// <summary>The thread is waiting for a free virtual memory page.</summary>
		// Token: 0x04000B9F RID: 2975
		FreePage,
		/// <summary>The thread is waiting for a local procedure call to arrive.</summary>
		// Token: 0x04000BA0 RID: 2976
		LpcReceive = 9,
		/// <summary>The thread is waiting for reply to a local procedure call to arrive.</summary>
		// Token: 0x04000BA1 RID: 2977
		LpcReply,
		/// <summary>The thread is waiting for a virtual memory page to arrive in memory.</summary>
		// Token: 0x04000BA2 RID: 2978
		PageIn = 2,
		/// <summary>The thread is waiting for a virtual memory page to be written to disk.</summary>
		// Token: 0x04000BA3 RID: 2979
		PageOut = 12,
		/// <summary>Thread execution is suspended.</summary>
		// Token: 0x04000BA4 RID: 2980
		Suspended = 5,
		/// <summary>The thread is waiting for system allocation.</summary>
		// Token: 0x04000BA5 RID: 2981
		SystemAllocation = 3,
		/// <summary>The thread is waiting for an unknown reason.</summary>
		// Token: 0x04000BA6 RID: 2982
		Unknown = 13,
		/// <summary>The thread is waiting for a user request.</summary>
		// Token: 0x04000BA7 RID: 2983
		UserRequest = 6,
		/// <summary>The thread is waiting for the system to allocate virtual memory.</summary>
		// Token: 0x04000BA8 RID: 2984
		VirtualMemory = 11
	}
}
