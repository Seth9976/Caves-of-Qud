using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200012C RID: 300
	internal sealed class AsyncUniTask<TStateMachine> : IStateMachineRunnerPromise, IUniTaskSource, IValueTaskSource, ITaskPoolNode<AsyncUniTask<TStateMachine>> where TStateMachine : IAsyncStateMachine
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public Action MoveNext { get; }

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000FE74 File Offset: 0x0000E074
		private AsyncUniTask()
		{
			this.MoveNext = new Action(this.Run);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000FE90 File Offset: 0x0000E090
		public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunnerPromise runnerPromiseFieldRef)
		{
			AsyncUniTask<TStateMachine> asyncUniTask;
			if (!AsyncUniTask<TStateMachine>.pool.TryPop(out asyncUniTask))
			{
				asyncUniTask = new AsyncUniTask<TStateMachine>();
			}
			runnerPromiseFieldRef = asyncUniTask;
			asyncUniTask.stateMachine = stateMachine;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		public ref AsyncUniTask<TStateMachine> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		static AsyncUniTask()
		{
			TaskPool.RegisterSizeGetter(typeof(AsyncUniTask<TStateMachine>), () => AsyncUniTask<TStateMachine>.pool.Size);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
		private void Return()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			AsyncUniTask<TStateMachine>.pool.TryPush(this);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000FF0E File Offset: 0x0000E10E
		private bool TryReturn()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			return AsyncUniTask<TStateMachine>.pool.TryPush(this);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000FF32 File Offset: 0x0000E132
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Run()
		{
			this.stateMachine.MoveNext();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000FF45 File Offset: 0x0000E145
		public UniTask Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask(this, this.core.Version);
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000FF58 File Offset: 0x0000E158
		[DebuggerHidden]
		public void SetResult()
		{
			this.core.TrySetResult(AsyncUnit.Default);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000FF6B File Offset: 0x0000E16B
		[DebuggerHidden]
		public void SetException(Exception exception)
		{
			this.core.TrySetException(exception);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000FF7C File Offset: 0x0000E17C
		[DebuggerHidden]
		public void GetResult(short token)
		{
			try
			{
				this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000FFBE File Offset: 0x0000E1BE
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000FFCB File Offset: 0x0000E1CB
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x04000165 RID: 357
		private static TaskPool<AsyncUniTask<TStateMachine>> pool;

		// Token: 0x04000167 RID: 359
		private TStateMachine stateMachine;

		// Token: 0x04000168 RID: 360
		private UniTaskCompletionSourceCore<AsyncUnit> core;

		// Token: 0x04000169 RID: 361
		private AsyncUniTask<TStateMachine> nextNode;
	}
}
