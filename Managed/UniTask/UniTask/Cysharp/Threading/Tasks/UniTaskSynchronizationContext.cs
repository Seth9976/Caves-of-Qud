using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000058 RID: 88
	public class UniTaskSynchronizationContext : SynchronizationContext
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000948B File Offset: 0x0000768B
		public override void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009494 File Offset: 0x00007694
		public override void Post(SendOrPostCallback d, object state)
		{
			bool flag = false;
			try
			{
				UniTaskSynchronizationContext.gate.Enter(ref flag);
				if (UniTaskSynchronizationContext.dequing)
				{
					if (UniTaskSynchronizationContext.waitingList.Length == UniTaskSynchronizationContext.waitingListCount)
					{
						int num = UniTaskSynchronizationContext.waitingListCount * 2;
						if (num > 2146435071)
						{
							num = 2146435071;
						}
						UniTaskSynchronizationContext.Callback[] array = new UniTaskSynchronizationContext.Callback[num];
						Array.Copy(UniTaskSynchronizationContext.waitingList, array, UniTaskSynchronizationContext.waitingListCount);
						UniTaskSynchronizationContext.waitingList = array;
					}
					UniTaskSynchronizationContext.waitingList[UniTaskSynchronizationContext.waitingListCount] = new UniTaskSynchronizationContext.Callback(d, state);
					UniTaskSynchronizationContext.waitingListCount++;
				}
				else
				{
					if (UniTaskSynchronizationContext.actionList.Length == UniTaskSynchronizationContext.actionListCount)
					{
						int num2 = UniTaskSynchronizationContext.actionListCount * 2;
						if (num2 > 2146435071)
						{
							num2 = 2146435071;
						}
						UniTaskSynchronizationContext.Callback[] array2 = new UniTaskSynchronizationContext.Callback[num2];
						Array.Copy(UniTaskSynchronizationContext.actionList, array2, UniTaskSynchronizationContext.actionListCount);
						UniTaskSynchronizationContext.actionList = array2;
					}
					UniTaskSynchronizationContext.actionList[UniTaskSynchronizationContext.actionListCount] = new UniTaskSynchronizationContext.Callback(d, state);
					UniTaskSynchronizationContext.actionListCount++;
				}
			}
			finally
			{
				if (flag)
				{
					UniTaskSynchronizationContext.gate.Exit(false);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000095A4 File Offset: 0x000077A4
		public override void OperationStarted()
		{
			Interlocked.Increment(ref UniTaskSynchronizationContext.opCount);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000095B1 File Offset: 0x000077B1
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref UniTaskSynchronizationContext.opCount);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000095BE File Offset: 0x000077BE
		public override SynchronizationContext CreateCopy()
		{
			return this;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000095C4 File Offset: 0x000077C4
		internal static void Run()
		{
			bool flag = false;
			try
			{
				UniTaskSynchronizationContext.gate.Enter(ref flag);
				if (UniTaskSynchronizationContext.actionListCount == 0)
				{
					return;
				}
				UniTaskSynchronizationContext.dequing = true;
			}
			finally
			{
				if (flag)
				{
					UniTaskSynchronizationContext.gate.Exit(false);
				}
			}
			for (int i = 0; i < UniTaskSynchronizationContext.actionListCount; i++)
			{
				UniTaskSynchronizationContext.Callback callback = UniTaskSynchronizationContext.actionList[i];
				UniTaskSynchronizationContext.actionList[i] = default(UniTaskSynchronizationContext.Callback);
				callback.Invoke();
			}
			bool flag2 = false;
			try
			{
				UniTaskSynchronizationContext.gate.Enter(ref flag2);
				UniTaskSynchronizationContext.dequing = false;
				UniTaskSynchronizationContext.Callback[] array = UniTaskSynchronizationContext.actionList;
				UniTaskSynchronizationContext.actionListCount = UniTaskSynchronizationContext.waitingListCount;
				UniTaskSynchronizationContext.actionList = UniTaskSynchronizationContext.waitingList;
				UniTaskSynchronizationContext.waitingListCount = 0;
				UniTaskSynchronizationContext.waitingList = array;
			}
			finally
			{
				if (flag2)
				{
					UniTaskSynchronizationContext.gate.Exit(false);
				}
			}
		}

		// Token: 0x040000C6 RID: 198
		private const int MaxArrayLength = 2146435071;

		// Token: 0x040000C7 RID: 199
		private const int InitialSize = 16;

		// Token: 0x040000C8 RID: 200
		private static SpinLock gate = new SpinLock(false);

		// Token: 0x040000C9 RID: 201
		private static bool dequing = false;

		// Token: 0x040000CA RID: 202
		private static int actionListCount = 0;

		// Token: 0x040000CB RID: 203
		private static UniTaskSynchronizationContext.Callback[] actionList = new UniTaskSynchronizationContext.Callback[16];

		// Token: 0x040000CC RID: 204
		private static int waitingListCount = 0;

		// Token: 0x040000CD RID: 205
		private static UniTaskSynchronizationContext.Callback[] waitingList = new UniTaskSynchronizationContext.Callback[16];

		// Token: 0x040000CE RID: 206
		private static int opCount;

		// Token: 0x020001EF RID: 495
		[StructLayout(LayoutKind.Auto)]
		private readonly struct Callback
		{
			// Token: 0x06000B04 RID: 2820 RVA: 0x00027D16 File Offset: 0x00025F16
			public Callback(SendOrPostCallback callback, object state)
			{
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x06000B05 RID: 2821 RVA: 0x00027D28 File Offset: 0x00025F28
			public void Invoke()
			{
				try
				{
					this.callback(this.state);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}

			// Token: 0x04000489 RID: 1161
			private readonly SendOrPostCallback callback;

			// Token: 0x0400048A RID: 1162
			private readonly object state;
		}
	}
}
