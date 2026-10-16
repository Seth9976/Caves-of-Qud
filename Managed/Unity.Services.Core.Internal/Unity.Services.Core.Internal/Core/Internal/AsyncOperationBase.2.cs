using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002D RID: 45
	internal abstract class AsyncOperationBase<T> : CustomYieldInstruction, IAsyncOperation<T>, IEnumerator, INotifyCompletion
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002770 File Offset: 0x00000970
		public override bool keepWaiting
		{
			get
			{
				return !this.IsCompleted;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A6 RID: 166
		public abstract bool IsCompleted { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000277B File Offset: 0x0000097B
		public bool IsDone
		{
			get
			{
				return this.IsCompleted;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A8 RID: 168
		public abstract AsyncOperationStatus Status { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A9 RID: 169
		public abstract Exception Exception { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AA RID: 170
		public abstract T Result { get; }

		// Token: 0x060000AB RID: 171
		public abstract T GetResult();

		// Token: 0x060000AC RID: 172
		public abstract AsyncOperationBase<T> GetAwaiter();

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000AD RID: 173 RVA: 0x00002783 File Offset: 0x00000983
		// (remove) Token: 0x060000AE RID: 174 RVA: 0x000027AC File Offset: 0x000009AC
		public event Action<IAsyncOperation<T>> Completed
		{
			add
			{
				if (this.IsDone)
				{
					value(this);
					return;
				}
				this.m_CompletedCallback = (Action<IAsyncOperation<T>>)Delegate.Combine(this.m_CompletedCallback, value);
			}
			remove
			{
				this.m_CompletedCallback = (Action<IAsyncOperation<T>>)Delegate.Remove(this.m_CompletedCallback, value);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000027C5 File Offset: 0x000009C5
		protected void DidComplete()
		{
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback == null)
			{
				return;
			}
			completedCallback(this);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000027D8 File Offset: 0x000009D8
		public virtual void OnCompleted(Action continuation)
		{
			this.Completed += delegate(IAsyncOperation<T> op)
			{
				Action continuation2 = continuation;
				if (continuation2 == null)
				{
					return;
				}
				continuation2();
			};
		}

		// Token: 0x04000028 RID: 40
		private Action<IAsyncOperation<T>> m_CompletedCallback;
	}
}
