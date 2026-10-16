using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D9 RID: 2009
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncIteratorMethodBuilder
	{
		// Token: 0x060045BD RID: 17853 RVA: 0x000E51C4 File Offset: 0x000E33C4
		public static AsyncIteratorMethodBuilder Create()
		{
			return default(AsyncIteratorMethodBuilder);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x000E51DA File Offset: 0x000E33DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveNext<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x000E51E2 File Offset: 0x000E33E2
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x000E51F1 File Offset: 0x000E33F1
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x000E5200 File Offset: 0x000E3400
		public void Complete()
		{
			this._methodBuilder.SetResult();
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000E520D File Offset: 0x000E340D
		internal object ObjectIdForDebugger
		{
			get
			{
				return this._methodBuilder.ObjectIdForDebugger;
			}
		}

		// Token: 0x04002D22 RID: 11554
		private AsyncTaskMethodBuilder _methodBuilder;
	}
}
