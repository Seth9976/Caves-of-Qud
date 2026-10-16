using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000047 RID: 71
	[global::System.Runtime.CompilerServices.AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder<>))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct UniTask<T>
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006943 File Offset: 0x00004B43
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask(T result)
		{
			this.source = null;
			this.token = 0;
			this.result = result;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000695A File Offset: 0x00004B5A
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask(IUniTaskSource<T> source, short token)
		{
			this.source = source;
			this.token = token;
			this.result = default(T);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006976 File Offset: 0x00004B76
		public UniTaskStatus Status
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.source != null)
				{
					return this.source.GetStatus(this.token);
				}
				return UniTaskStatus.Succeeded;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006993 File Offset: 0x00004B93
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask<T>.Awaiter GetAwaiter()
		{
			return new UniTask<T>.Awaiter(in this);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000699B File Offset: 0x00004B9B
		public UniTask<T> Preserve()
		{
			if (this.source == null)
			{
				return this;
			}
			return new UniTask<T>(new UniTask<T>.MemoizeSource(this.source), this.token);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000069C4 File Offset: 0x00004BC4
		public UniTask AsUniTask()
		{
			if (this.source == null)
			{
				return UniTask.CompletedTask;
			}
			if (this.source.GetStatus(this.token).IsCompletedSuccessfully())
			{
				this.source.GetResult(this.token);
				return UniTask.CompletedTask;
			}
			return new UniTask(this.source, this.token);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006A20 File Offset: 0x00004C20
		public static implicit operator UniTask(UniTask<T> self)
		{
			return self.AsUniTask();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006A29 File Offset: 0x00004C29
		public static implicit operator ValueTask<T>(in UniTask<T> self)
		{
			if (self.source == null)
			{
				return new ValueTask<T>(self.result);
			}
			return new ValueTask<T>(self.source, self.token);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006A50 File Offset: 0x00004C50
		[return: TupleElementNames(new string[] { "IsCanceled", "Result" })]
		public UniTask<ValueTuple<bool, T>> SuppressCancellationThrow()
		{
			if (this.source == null)
			{
				return new UniTask<ValueTuple<bool, T>>(new ValueTuple<bool, T>(false, this.result));
			}
			return new UniTask<ValueTuple<bool, T>>(new UniTask<T>.IsCanceledSource(this.source), this.token);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006A84 File Offset: 0x00004C84
		public override string ToString()
		{
			if (this.source != null)
			{
				return "(" + this.source.UnsafeGetStatus().ToString() + ")";
			}
			T t = this.result;
			if (t == null)
			{
				return null;
			}
			return t.ToString();
		}

		// Token: 0x0400009B RID: 155
		private readonly IUniTaskSource<T> source;

		// Token: 0x0400009C RID: 156
		private readonly T result;

		// Token: 0x0400009D RID: 157
		private readonly short token;

		// Token: 0x020001C4 RID: 452
		private sealed class IsCanceledSource : IUniTaskSource<ValueTuple<bool, T>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<bool, T>>
		{
			// Token: 0x06000A7F RID: 2687 RVA: 0x00025053 File Offset: 0x00023253
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public IsCanceledSource(IUniTaskSource<T> source)
			{
				this.source = source;
			}

			// Token: 0x06000A80 RID: 2688 RVA: 0x00025064 File Offset: 0x00023264
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ValueTuple<bool, T> GetResult(short token)
			{
				if (this.source.GetStatus(token) == UniTaskStatus.Canceled)
				{
					return new ValueTuple<bool, T>(true, default(T));
				}
				T result = this.source.GetResult(token);
				return new ValueTuple<bool, T>(false, result);
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x000250A4 File Offset: 0x000232A4
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000A82 RID: 2690 RVA: 0x000250AE File Offset: 0x000232AE
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public UniTaskStatus GetStatus(short token)
			{
				return this.source.GetStatus(token);
			}

			// Token: 0x06000A83 RID: 2691 RVA: 0x000250BC File Offset: 0x000232BC
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x06000A84 RID: 2692 RVA: 0x000250C9 File Offset: 0x000232C9
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x040003AD RID: 941
			private readonly IUniTaskSource<T> source;
		}

		// Token: 0x020001C5 RID: 453
		private sealed class MemoizeSource : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x06000A85 RID: 2693 RVA: 0x000250D9 File Offset: 0x000232D9
			public MemoizeSource(IUniTaskSource<T> source)
			{
				this.source = source;
			}

			// Token: 0x06000A86 RID: 2694 RVA: 0x000250E8 File Offset: 0x000232E8
			public T GetResult(short token)
			{
				if (this.source == null)
				{
					if (this.exception != null)
					{
						this.exception.Throw();
					}
					return this.result;
				}
				T t;
				try
				{
					this.result = this.source.GetResult(token);
					this.status = UniTaskStatus.Succeeded;
					t = this.result;
				}
				catch (Exception ex)
				{
					this.exception = ExceptionDispatchInfo.Capture(ex);
					if (ex is OperationCanceledException)
					{
						this.status = UniTaskStatus.Canceled;
					}
					else
					{
						this.status = UniTaskStatus.Faulted;
					}
					throw;
				}
				finally
				{
					this.source = null;
				}
				return t;
			}

			// Token: 0x06000A87 RID: 2695 RVA: 0x00025188 File Offset: 0x00023388
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000A88 RID: 2696 RVA: 0x00025192 File Offset: 0x00023392
			public UniTaskStatus GetStatus(short token)
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.GetStatus(token);
			}

			// Token: 0x06000A89 RID: 2697 RVA: 0x000251AF File Offset: 0x000233AF
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				if (this.source == null)
				{
					continuation(state);
					return;
				}
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000A8A RID: 2698 RVA: 0x000251CF File Offset: 0x000233CF
			public UniTaskStatus UnsafeGetStatus()
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x040003AE RID: 942
			private IUniTaskSource<T> source;

			// Token: 0x040003AF RID: 943
			private T result;

			// Token: 0x040003B0 RID: 944
			private ExceptionDispatchInfo exception;

			// Token: 0x040003B1 RID: 945
			private UniTaskStatus status;
		}

		// Token: 0x020001C6 RID: 454
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A8B RID: 2699 RVA: 0x000251EB File Offset: 0x000233EB
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Awaiter(in UniTask<T> task)
			{
				this.task = task;
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000251F9 File Offset: 0x000233F9
			public bool IsCompleted
			{
				[DebuggerHidden]
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.task.Status.IsCompleted();
				}
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x0002520C File Offset: 0x0002340C
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public T GetResult()
			{
				IUniTaskSource<T> source = this.task.source;
				if (source == null)
				{
					return this.task.result;
				}
				return source.GetResult(this.task.token);
			}

			// Token: 0x06000A8E RID: 2702 RVA: 0x00025248 File Offset: 0x00023448
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				IUniTaskSource<T> source = this.task.source;
				if (source == null)
				{
					continuation();
					return;
				}
				source.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x00025284 File Offset: 0x00023484
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void UnsafeOnCompleted(Action continuation)
			{
				IUniTaskSource<T> source = this.task.source;
				if (source == null)
				{
					continuation();
					return;
				}
				source.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x000252C0 File Offset: 0x000234C0
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SourceOnCompleted(Action<object> continuation, object state)
			{
				IUniTaskSource<T> source = this.task.source;
				if (source == null)
				{
					continuation(state);
					return;
				}
				source.OnCompleted(continuation, state, this.task.token);
			}

			// Token: 0x040003B2 RID: 946
			private readonly UniTask<T> task;
		}
	}
}
