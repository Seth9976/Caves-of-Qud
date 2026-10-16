using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000124 RID: 292
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncUniTaskMethodBuilder
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0000FACC File Offset: 0x0000DCCC
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AsyncUniTaskMethodBuilder Create()
		{
			return default(AsyncUniTaskMethodBuilder);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000FAE2 File Offset: 0x0000DCE2
		public UniTask Task
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.runnerPromise != null)
				{
					return this.runnerPromise.Task;
				}
				if (this.ex != null)
				{
					return UniTask.FromException(this.ex);
				}
				return UniTask.CompletedTask;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000FB11 File Offset: 0x0000DD11
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetException(Exception exception)
		{
			if (this.runnerPromise == null)
			{
				this.ex = exception;
				return;
			}
			this.runnerPromise.SetException(exception);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000FB2F File Offset: 0x0000DD2F
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetResult()
		{
			if (this.runnerPromise != null)
			{
				this.runnerPromise.SetResult();
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000FB44 File Offset: 0x0000DD44
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.OnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000FB71 File Offset: 0x0000DD71
		[DebuggerHidden]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.UnsafeOnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000FB9E File Offset: 0x0000DD9E
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		// Token: 0x0400015B RID: 347
		private IStateMachineRunnerPromise runnerPromise;

		// Token: 0x0400015C RID: 348
		private Exception ex;
	}
}
