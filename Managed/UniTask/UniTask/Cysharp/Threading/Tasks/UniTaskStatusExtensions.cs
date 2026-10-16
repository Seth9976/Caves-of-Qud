using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000024 RID: 36
	public static class UniTaskStatusExtensions
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002D57 File Offset: 0x00000F57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCompleted(this UniTaskStatus status)
		{
			return status > UniTaskStatus.Pending;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002D5D File Offset: 0x00000F5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCompletedSuccessfully(this UniTaskStatus status)
		{
			return status == UniTaskStatus.Succeeded;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002D63 File Offset: 0x00000F63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCanceled(this UniTaskStatus status)
		{
			return status == UniTaskStatus.Canceled;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002D69 File Offset: 0x00000F69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFaulted(this UniTaskStatus status)
		{
			return status == UniTaskStatus.Faulted;
		}
	}
}
