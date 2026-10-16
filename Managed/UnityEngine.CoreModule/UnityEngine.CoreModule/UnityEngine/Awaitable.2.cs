using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Internal;
using UnityEngine.Pool;

namespace UnityEngine
{
	// Token: 0x02000282 RID: 642
	[AsyncMethodBuilder(typeof(Awaitable.AwaitableAsyncMethodBuilder<>))]
	public class Awaitable<T>
	{
		// Token: 0x0600201F RID: 8223 RVA: 0x0003DEE6 File Offset: 0x0003C0E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ContinueWith(Action continuation)
		{
			this._awaitable.SetContinuation(continuation);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0003DEF8 File Offset: 0x0003C0F8
		private T GetResult()
		{
			T result;
			try
			{
				this._awaitable.PropagateExceptionAndRelease();
				result = this._result;
			}
			finally
			{
				this._awaitable = null;
				this._result = default(T);
				Awaitable<T>._pool.Value.Release(this);
			}
			return result;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0003DF54 File Offset: 0x0003C154
		internal void SetResultAndRaiseContinuation(T result)
		{
			this._result = result;
			this._awaitable.RaiseManagedCompletion();
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0003DF6A File Offset: 0x0003C16A
		internal void SetExceptionAndRaiseContinuation(Exception exception)
		{
			this._awaitable.RaiseManagedCompletion(exception);
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0003DF7A File Offset: 0x0003C17A
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x0003DF87 File Offset: 0x0003C187
		internal Awaitable.AwaiterCompletionThreadAffinity CompletionThreadAffinity
		{
			get
			{
				return this._awaitable.CompletionThreadAffinity;
			}
			set
			{
				this._awaitable.CompletionThreadAffinity = value;
			}
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0003DF96 File Offset: 0x0003C196
		public void Cancel()
		{
			this._awaitable.Cancel();
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00030B93 File Offset: 0x0002ED93
		private Awaitable()
		{
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0003DFA8 File Offset: 0x0003C1A8
		internal static Awaitable<T> GetManaged()
		{
			Awaitable awaitable = Awaitable.NewManagedAwaitable();
			Awaitable<T> awaitable2 = Awaitable<T>._pool.Value.Get();
			awaitable2._awaitable = awaitable;
			return awaitable2;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0003DFD8 File Offset: 0x0003C1D8
		[ExcludeFromDocs]
		public Awaitable<T>.Awaiter GetAwaiter()
		{
			return new Awaitable<T>.Awaiter(this);
		}

		// Token: 0x0400098C RID: 2444
		private static readonly ThreadLocal<ObjectPool<Awaitable<T>>> _pool = new ThreadLocal<ObjectPool<Awaitable<T>>>(() => new ObjectPool<Awaitable<T>>(() => new Awaitable<T>(), null, null, null, false, 10, 10000));

		// Token: 0x0400098D RID: 2445
		private Awaitable _awaitable;

		// Token: 0x0400098E RID: 2446
		private T _result;

		// Token: 0x02000283 RID: 643
		[ExcludeFromDocs]
		public struct Awaiter : INotifyCompletion
		{
			// Token: 0x0600202A RID: 8234 RVA: 0x0003E00C File Offset: 0x0003C20C
			public Awaiter(Awaitable<T> coroutine)
			{
				this._coroutine = coroutine;
			}

			// Token: 0x0600202B RID: 8235 RVA: 0x0003E016 File Offset: 0x0003C216
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				this._coroutine.ContinueWith(continuation);
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x0600202C RID: 8236 RVA: 0x0003E026 File Offset: 0x0003C226
			public bool IsCompleted
			{
				get
				{
					return this._coroutine._awaitable.IsCompleted;
				}
			}

			// Token: 0x0600202D RID: 8237 RVA: 0x0003E038 File Offset: 0x0003C238
			public T GetResult()
			{
				return this._coroutine.GetResult();
			}

			// Token: 0x0400098F RID: 2447
			private readonly Awaitable<T> _coroutine;
		}
	}
}
