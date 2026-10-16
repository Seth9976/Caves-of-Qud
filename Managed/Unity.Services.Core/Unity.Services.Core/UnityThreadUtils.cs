using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core
{
	// Token: 0x02000011 RID: 17
	internal static class UnityThreadUtils
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000264F File Offset: 0x0000084F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002656 File Offset: 0x00000856
		internal static TaskScheduler UnityThreadScheduler { get; private set; }

		// Token: 0x0600004C RID: 76 RVA: 0x0000265E File Offset: 0x0000085E
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void CaptureUnityThreadInfo()
		{
			UnityThreadUtils.s_UnityThreadId = Thread.CurrentThread.ManagedThreadId;
			UnityThreadUtils.UnityThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002679 File Offset: 0x00000879
		public static bool IsRunningOnUnityThread
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId == UnityThreadUtils.s_UnityThreadId;
			}
		}

		// Token: 0x04000021 RID: 33
		private static int s_UnityThreadId;
	}
}
