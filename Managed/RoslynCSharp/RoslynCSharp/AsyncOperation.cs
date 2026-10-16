using System;
using System.Threading;
using UnityEngine;

namespace RoslynCSharp
{
	// Token: 0x02000005 RID: 5
	public abstract class AsyncOperation : CustomYieldInstruction
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000257C File Offset: 0x0000077C
		public bool IsDone
		{
			get
			{
				return this.isDone;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002584 File Offset: 0x00000784
		public bool IsSuccessful
		{
			get
			{
				return this.isSuccessful;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000258C File Offset: 0x0000078C
		public override bool keepWaiting
		{
			get
			{
				if (!this.hasStarted)
				{
					ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						try
						{
							this.hasStarted = true;
							this.RunAsyncOperation();
						}
						catch (Exception ex2)
						{
							Debug.LogException(ex2);
							this.isSuccessful = false;
						}
						this.threadExit = true;
					});
				}
				if (!this.threadExit)
				{
					return true;
				}
				try
				{
					this.RunSyncFinalize();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					this.isSuccessful = false;
				}
				this.isDone = true;
				return false;
			}
		}

		// Token: 0x06000013 RID: 19
		protected abstract void RunAsyncOperation();

		// Token: 0x06000014 RID: 20 RVA: 0x000025EC File Offset: 0x000007EC
		protected virtual void RunSyncFinalize()
		{
		}

		// Token: 0x04000012 RID: 18
		private bool hasStarted;

		// Token: 0x04000013 RID: 19
		private bool threadExit;

		// Token: 0x04000014 RID: 20
		private bool isDone;

		// Token: 0x04000015 RID: 21
		protected bool isSuccessful;
	}
}
