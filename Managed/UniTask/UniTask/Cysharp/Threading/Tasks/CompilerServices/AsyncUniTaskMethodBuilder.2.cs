using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000125 RID: 293
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncUniTaskMethodBuilder<T>
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AsyncUniTaskMethodBuilder<T> Create()
		{
			return default(AsyncUniTaskMethodBuilder<T>);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0000FBC6 File Offset: 0x0000DDC6
		public UniTask<T> Task
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
					return UniTask.FromException<T>(this.ex);
				}
				return UniTask.FromResult<T>(this.result);
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000FBFB File Offset: 0x0000DDFB
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

		// Token: 0x060006AF RID: 1711 RVA: 0x0000FC19 File Offset: 0x0000DE19
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetResult(T result)
		{
			if (this.runnerPromise == null)
			{
				this.result = result;
				return;
			}
			this.runnerPromise.SetResult(result);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000FC37 File Offset: 0x0000DE37
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine, T>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.OnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000FC64 File Offset: 0x0000DE64
		[DebuggerHidden]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine, T>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.UnsafeOnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000FC91 File Offset: 0x0000DE91
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000FC9F File Offset: 0x0000DE9F
		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		// Token: 0x0400015D RID: 349
		private IStateMachineRunnerPromise<T> runnerPromise;

		// Token: 0x0400015E RID: 350
		private Exception ex;

		// Token: 0x0400015F RID: 351
		private T result;
	}
}
