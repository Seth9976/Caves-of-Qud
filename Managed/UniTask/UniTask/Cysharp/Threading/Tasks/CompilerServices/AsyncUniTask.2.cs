using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200012D RID: 301
	internal sealed class AsyncUniTask<TStateMachine, T> : IStateMachineRunnerPromise<T>, IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITaskPoolNode<AsyncUniTask<TStateMachine, T>> where TStateMachine : IAsyncStateMachine
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		public Action MoveNext { get; }

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000FFE3 File Offset: 0x0000E1E3
		private AsyncUniTask()
		{
			this.MoveNext = new Action(this.Run);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00010000 File Offset: 0x0000E200
		public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunnerPromise<T> runnerPromiseFieldRef)
		{
			AsyncUniTask<TStateMachine, T> asyncUniTask;
			if (!AsyncUniTask<TStateMachine, T>.pool.TryPop(out asyncUniTask))
			{
				asyncUniTask = new AsyncUniTask<TStateMachine, T>();
			}
			runnerPromiseFieldRef = asyncUniTask;
			asyncUniTask.stateMachine = stateMachine;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00010030 File Offset: 0x0000E230
		public ref AsyncUniTask<TStateMachine, T> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00010038 File Offset: 0x0000E238
		static AsyncUniTask()
		{
			TaskPool.RegisterSizeGetter(typeof(AsyncUniTask<TStateMachine, T>), () => AsyncUniTask<TStateMachine, T>.pool.Size);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00010059 File Offset: 0x0000E259
		private void Return()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			AsyncUniTask<TStateMachine, T>.pool.TryPush(this);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001007E File Offset: 0x0000E27E
		private bool TryReturn()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			return AsyncUniTask<TStateMachine, T>.pool.TryPush(this);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000100A2 File Offset: 0x0000E2A2
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Run()
		{
			this.stateMachine.MoveNext();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000100B5 File Offset: 0x0000E2B5
		public UniTask<T> Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask<T>(this, this.core.Version);
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000100C8 File Offset: 0x0000E2C8
		[DebuggerHidden]
		public void SetResult(T result)
		{
			this.core.TrySetResult(result);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000100D7 File Offset: 0x0000E2D7
		[DebuggerHidden]
		public void SetException(Exception exception)
		{
			this.core.TrySetException(exception);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000100E8 File Offset: 0x0000E2E8
		[DebuggerHidden]
		public T GetResult(short token)
		{
			T result;
			try
			{
				result = this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
			return result;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00010120 File Offset: 0x0000E320
		[DebuggerHidden]
		void IUniTaskSource.GetResult(short token)
		{
			this.GetResult(token);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001012A File Offset: 0x0000E32A
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00010138 File Offset: 0x0000E338
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00010145 File Offset: 0x0000E345
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x0400016A RID: 362
		private static TaskPool<AsyncUniTask<TStateMachine, T>> pool;

		// Token: 0x0400016C RID: 364
		private TStateMachine stateMachine;

		// Token: 0x0400016D RID: 365
		private UniTaskCompletionSourceCore<T> core;

		// Token: 0x0400016E RID: 366
		private AsyncUniTask<TStateMachine, T> nextNode;
	}
}
