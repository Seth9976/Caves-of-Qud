using System;
using LaundryBear.PlatformServices;
using Platform.IO;
using UnityEngine;
using XRL;

namespace Platform
{
	// Token: 0x02000330 RID: 816
	public struct DelayShutdown : IDisposable
	{
		// Token: 0x06001E78 RID: 7800 RVA: 0x00094B3C File Offset: 0x00092D3C
		public static DelayShutdown AutoScope()
		{
			DelayShutdown delayShutdown = default(DelayShutdown);
			delayShutdown.constructedProperly = true;
			delayShutdown.Begin();
			return delayShutdown;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00094B64 File Offset: 0x00092D64
		public static DelayShutdown AutoScopeForceMainThread()
		{
			DelayShutdown delayShutdown = default(DelayShutdown);
			delayShutdown.constructedProperly = true;
			delayShutdown.forceMainThread = true;
			delayShutdown.Begin();
			return delayShutdown;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00094B91 File Offset: 0x00092D91
		public void Dispose()
		{
			if (!this.WasConstructedProperly())
			{
				return;
			}
			if (this.alreadyDisposed)
			{
				return;
			}
			this.alreadyDisposed = true;
			this.End();
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00094BB2 File Offset: 0x00092DB2
		private void Begin()
		{
			if (!this.WasConstructedProperly())
			{
				return;
			}
			if (this.forceMainThread)
			{
				DelayShutdown.BeginRawForceMainThread();
				return;
			}
			DelayShutdown.BeginRaw();
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00094BD0 File Offset: 0x00092DD0
		private void End()
		{
			if (this.forceMainThread)
			{
				DelayShutdown.EndRawForceMainThread();
				return;
			}
			DelayShutdown.EndRaw();
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00094BE5 File Offset: 0x00092DE5
		public static void BeginRawForceMainThread()
		{
			The.UiContext.Send(delegate(object _)
			{
				DelayShutdown.BeginRaw();
			}, null);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00094C11 File Offset: 0x00092E11
		public static void EndRawForceMainThread()
		{
			The.UiContext.Send(delegate(object _)
			{
				DelayShutdown.EndRaw();
			}, null);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00094C40 File Offset: 0x00092E40
		public static void BeginRaw()
		{
			ICanDelayShutdown canDelayShutdown = State.GetStorage() as ICanDelayShutdown;
			if (canDelayShutdown == null)
			{
				Debug.LogError("Could not find ICanDelayShutdown service. not implemented for this platform?");
				return;
			}
			canDelayShutdown.IsUserHandlingShutdownDelay = true;
			canDelayShutdown.BeginDelayShutdown();
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00094C74 File Offset: 0x00092E74
		public static void EndRaw()
		{
			ICanDelayShutdown canDelayShutdown = State.GetStorage() as ICanDelayShutdown;
			if (canDelayShutdown == null)
			{
				Debug.LogError("Could not find ICanDelayShutdown service. not implemented for this platform?");
				return;
			}
			canDelayShutdown.EndShutdownDelay();
			canDelayShutdown.IsUserHandlingShutdownDelay = false;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00094CA7 File Offset: 0x00092EA7
		private bool WasConstructedProperly()
		{
			if (!this.constructedProperly)
			{
				Debug.LogError("DelayShutdown was not properly constructed. This struct should be constructed using AutoScope or AutoScopeForceMainThread or will not do anything.)");
			}
			return this.constructedProperly;
		}

		// Token: 0x04000FF4 RID: 4084
		private bool constructedProperly;

		// Token: 0x04000FF5 RID: 4085
		private bool alreadyDisposed;

		// Token: 0x04000FF6 RID: 4086
		private bool forceMainThread;
	}
}
