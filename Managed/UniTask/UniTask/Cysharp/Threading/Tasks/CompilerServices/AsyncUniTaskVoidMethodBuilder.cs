using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000126 RID: 294
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncUniTaskVoidMethodBuilder
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AsyncUniTaskVoidMethodBuilder Create()
		{
			return default(AsyncUniTaskVoidMethodBuilder);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		public UniTaskVoid Task
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(UniTaskVoid);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000FCD2 File Offset: 0x0000DED2
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetException(Exception exception)
		{
			if (this.runner != null)
			{
				this.runner.Return();
				this.runner = null;
			}
			UniTaskScheduler.PublishUnobservedTaskException(exception);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetResult()
		{
			if (this.runner != null)
			{
				this.runner.Return();
				this.runner = null;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000FD10 File Offset: 0x0000DF10
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runner == null)
			{
				AsyncUniTaskVoid<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runner);
			}
			awaiter.OnCompleted(this.runner.MoveNext);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000FD3D File Offset: 0x0000DF3D
		[DebuggerHidden]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runner == null)
			{
				AsyncUniTaskVoid<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runner);
			}
			awaiter.UnsafeOnCompleted(this.runner.MoveNext);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000FD6A File Offset: 0x0000DF6A
		[DebuggerHidden]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000FD78 File Offset: 0x0000DF78
		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		// Token: 0x04000160 RID: 352
		private IStateMachineRunner runner;
	}
}
