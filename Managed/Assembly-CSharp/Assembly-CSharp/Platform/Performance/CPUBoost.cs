using System;
using System.Threading;
using LaundryBear;
using LaundryBear.PlatformServices;
using UnityEngine;

namespace Platform.Performance
{
	// Token: 0x02000332 RID: 818
	public struct CPUBoost : IDisposable
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x00094CCA File Offset: 0x00092ECA
		public static void InitOnMainThread()
		{
			CPUBoost.mainManagedThreadId = Thread.CurrentThread.ManagedThreadId;
			CPUBoost.mainSyncContext = SynchronizationContext.Current;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00094CE8 File Offset: 0x00092EE8
		public static CPUBoost AutoScopeForceMainThread()
		{
			CPUBoost cpuboost = default(CPUBoost);
			cpuboost.forceMainThread = true;
			cpuboost.Begin();
			return cpuboost;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00094D10 File Offset: 0x00092F10
		public static CPUBoost AutoScope()
		{
			CPUBoost cpuboost = default(CPUBoost);
			cpuboost.Begin();
			return cpuboost;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00094D2D File Offset: 0x00092F2D
		public void Dispose()
		{
			this.End();
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00094D38 File Offset: 0x00092F38
		public void Begin()
		{
			if (!this.forceMainThread && CPUBoost.mainManagedThreadId != Thread.CurrentThread.ManagedThreadId)
			{
				Debug.LogError("CPUBoost was called off the main UI thread which is invalid usage.");
				return;
			}
			if (this.beginWasCalled)
			{
				return;
			}
			Debug.Log("CPUBoost:: FastLoad");
			Interlocked.Increment(ref CPUBoost.requestCount);
			this.beginWasCalled = true;
			if (CPUBoost.requestCount > 0)
			{
				if (this.forceMainThread)
				{
					CPUBoost.RawBeginForceMainThread();
					return;
				}
				CPUBoost.RawBegin();
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00094DAC File Offset: 0x00092FAC
		public void End()
		{
			if (!this.forceMainThread && CPUBoost.mainManagedThreadId != Thread.CurrentThread.ManagedThreadId)
			{
				Debug.LogError("CPUBoost:: Dispose was called off the main UI thread which is invalid usage. Will continue anyways...");
				return;
			}
			if (!this.beginWasCalled)
			{
				Debug.LogWarning("CPUBoost:: non-symetric Begin-End calls. Prefer using AutoScope over manualy begin() end() calls");
				return;
			}
			if (this.endWasCalled)
			{
				return;
			}
			Debug.Log("CPUBoost:: Normal");
			this.endWasCalled = true;
			Interlocked.Decrement(ref CPUBoost.requestCount);
			if (CPUBoost.requestCount == 0)
			{
				if (this.forceMainThread)
				{
					CPUBoost.RawEndForceMainThread();
					return;
				}
				CPUBoost.RawEnd();
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00094E2F File Offset: 0x0009302F
		public static void RawBeginForceMainThread()
		{
			CPUBoost.mainSyncContext.Send(delegate(object _)
			{
				CPUBoost.RawBegin();
			}, null);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00094E5B File Offset: 0x0009305B
		public static void RawEndForceMainThread()
		{
			CPUBoost.mainSyncContext.Send(delegate(object _)
			{
				CPUBoost.RawEnd();
			}, null);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00094E88 File Offset: 0x00093088
		private static void RawBegin()
		{
			IPerformanceService performanceService;
			if (ServiceLocator.TryGetService<IPerformanceService>(out performanceService))
			{
				performanceService.BeginCpuCritical();
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00094EA4 File Offset: 0x000930A4
		private static void RawEnd()
		{
			IPerformanceService performanceService;
			if (ServiceLocator.TryGetService<IPerformanceService>(out performanceService))
			{
				performanceService.EndCpuCritical();
			}
		}

		// Token: 0x04000FF7 RID: 4087
		private static int requestCount;

		// Token: 0x04000FF8 RID: 4088
		private bool endWasCalled;

		// Token: 0x04000FF9 RID: 4089
		private bool beginWasCalled;

		// Token: 0x04000FFA RID: 4090
		private bool forceMainThread;

		// Token: 0x04000FFB RID: 4091
		private static int mainManagedThreadId;

		// Token: 0x04000FFC RID: 4092
		private static SynchronizationContext mainSyncContext;
	}
}
