using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002C RID: 44
	internal abstract class AsyncOperationBase : CustomYieldInstruction, IAsyncOperation, IEnumerator, INotifyCompletion
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000026D3 File Offset: 0x000008D3
		public override bool keepWaiting
		{
			get
			{
				return !this.IsCompleted;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009A RID: 154
		public abstract bool IsCompleted { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000026DE File Offset: 0x000008DE
		public bool IsDone
		{
			get
			{
				return this.IsCompleted;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009C RID: 156
		public abstract AsyncOperationStatus Status { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009D RID: 157
		public abstract Exception Exception { get; }

		// Token: 0x0600009E RID: 158
		public abstract void GetResult();

		// Token: 0x0600009F RID: 159
		public abstract AsyncOperationBase GetAwaiter();

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000A0 RID: 160 RVA: 0x000026E6 File Offset: 0x000008E6
		// (remove) Token: 0x060000A1 RID: 161 RVA: 0x0000270F File Offset: 0x0000090F
		public event Action<IAsyncOperation> Completed
		{
			add
			{
				if (this.IsDone)
				{
					value(this);
					return;
				}
				this.m_CompletedCallback = (Action<IAsyncOperation>)Delegate.Combine(this.m_CompletedCallback, value);
			}
			remove
			{
				this.m_CompletedCallback = (Action<IAsyncOperation>)Delegate.Remove(this.m_CompletedCallback, value);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002728 File Offset: 0x00000928
		protected void DidComplete()
		{
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback == null)
			{
				return;
			}
			completedCallback(this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000273C File Offset: 0x0000093C
		public virtual void OnCompleted(Action continuation)
		{
			this.Completed += delegate(IAsyncOperation op)
			{
				Action continuation2 = continuation;
				if (continuation2 == null)
				{
					return;
				}
				continuation2();
			};
		}

		// Token: 0x04000027 RID: 39
		private Action<IAsyncOperation> m_CompletedCallback;
	}
}
