using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200005B RID: 91
	public static class AsyncInstantiateOperationExtensions
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00009EDD File Offset: 0x000080DD
		public static UniTask<global::UnityEngine.Object[]> WithCancellation<T>(this AsyncInstantiateOperation asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009EE9 File Offset: 0x000080E9
		public static UniTask<global::UnityEngine.Object[]> WithCancellation<T>(this AsyncInstantiateOperation asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009EF8 File Offset: 0x000080F8
		public static UniTask<global::UnityEngine.Object[]> ToUniTask(this AsyncInstantiateOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<AsyncInstantiateOperation>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<global::UnityEngine.Object[]>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<global::UnityEngine.Object[]>(asyncOperation.Result);
			}
			short num;
			return new UniTask<global::UnityEngine.Object[]>(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009F47 File Offset: 0x00008147
		public static UniTask<T[]> WithCancellation<T>(this AsyncInstantiateOperation<T> asyncOperation, CancellationToken cancellationToken) where T : global::UnityEngine.Object
		{
			return asyncOperation.ToUniTask<T>(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009F53 File Offset: 0x00008153
		public static UniTask<T[]> WithCancellation<T>(this AsyncInstantiateOperation<T> asyncOperation, CancellationToken cancellationToken, bool cancelImmediately) where T : global::UnityEngine.Object
		{
			return asyncOperation.ToUniTask<T>(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009F60 File Offset: 0x00008160
		public static UniTask<T[]> ToUniTask<T>(this AsyncInstantiateOperation<T> asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false) where T : global::UnityEngine.Object
		{
			Error.ThrowArgumentNullException<AsyncInstantiateOperation<T>>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<T[]>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<T[]>(asyncOperation.Result);
			}
			short num;
			return new UniTask<T[]>(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x020001FF RID: 511
		private sealed class AsyncInstantiateOperationConfiguredSource : IUniTaskSource<global::UnityEngine.Object[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<global::UnityEngine.Object[]>, IPlayerLoopItem, ITaskPoolNode<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource>
		{
			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00029566 File Offset: 0x00027766
			public ref AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x0002956E File Offset: 0x0002776E
			static AsyncInstantiateOperationConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource), () => AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.pool.Size);
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x0002958F File Offset: 0x0002778F
			private AsyncInstantiateOperationConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x000295AC File Offset: 0x000277AC
			public static IUniTaskSource<global::UnityEngine.Object[]> Create(AsyncInstantiateOperation asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<global::UnityEngine.Object[]>.CreateFromCanceled(cancellationToken, out token);
				}
				AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource asyncInstantiateOperationConfiguredSource;
				if (!AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.pool.TryPop(out asyncInstantiateOperationConfiguredSource))
				{
					asyncInstantiateOperationConfiguredSource = new AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource();
				}
				asyncInstantiateOperationConfiguredSource.asyncOperation = asyncOperation;
				asyncInstantiateOperationConfiguredSource.progress = progress;
				asyncInstantiateOperationConfiguredSource.cancellationToken = cancellationToken;
				asyncInstantiateOperationConfiguredSource.cancelImmediately = cancelImmediately;
				asyncInstantiateOperationConfiguredSource.completed = false;
				asyncOperation.completed += asyncInstantiateOperationConfiguredSource.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					asyncInstantiateOperationConfiguredSource.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource asyncInstantiateOperationConfiguredSource2 = (AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource)state;
						asyncInstantiateOperationConfiguredSource2.core.TrySetCanceled(asyncInstantiateOperationConfiguredSource2.cancellationToken);
					}, asyncInstantiateOperationConfiguredSource);
				}
				PlayerLoopHelper.AddAction(timing, asyncInstantiateOperationConfiguredSource);
				token = asyncInstantiateOperationConfiguredSource.core.Version;
				return asyncInstantiateOperationConfiguredSource;
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x00029660 File Offset: 0x00027860
			public global::UnityEngine.Object[] GetResult(short token)
			{
				global::UnityEngine.Object[] result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
				return result;
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x000296AC File Offset: 0x000278AC
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x000296B6 File Offset: 0x000278B6
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x000296C4 File Offset: 0x000278C4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x000296D1 File Offset: 0x000278D1
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x000296E4 File Offset: 0x000278E4
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.Result);
					return false;
				}
				return true;
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x0002976C File Offset: 0x0002796C
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x000297CC File Offset: 0x000279CC
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.Result);
			}

			// Token: 0x040004E2 RID: 1250
			private static TaskPool<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource> pool;

			// Token: 0x040004E3 RID: 1251
			private AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource nextNode;

			// Token: 0x040004E4 RID: 1252
			private AsyncInstantiateOperation asyncOperation;

			// Token: 0x040004E5 RID: 1253
			private IProgress<float> progress;

			// Token: 0x040004E6 RID: 1254
			private CancellationToken cancellationToken;

			// Token: 0x040004E7 RID: 1255
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040004E8 RID: 1256
			private bool cancelImmediately;

			// Token: 0x040004E9 RID: 1257
			private bool completed;

			// Token: 0x040004EA RID: 1258
			private UniTaskCompletionSourceCore<global::UnityEngine.Object[]> core;

			// Token: 0x040004EB RID: 1259
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000200 RID: 512
		private sealed class AsyncInstantiateOperationConfiguredSource<T> : IUniTaskSource<T[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T[]>, IPlayerLoopItem, ITaskPoolNode<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>> where T : global::UnityEngine.Object
		{
			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00029820 File Offset: 0x00027A20
			public ref AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x00029828 File Offset: 0x00027A28
			static AsyncInstantiateOperationConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>), () => AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.pool.Size);
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x00029849 File Offset: 0x00027A49
			private AsyncInstantiateOperationConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x00029864 File Offset: 0x00027A64
			public static IUniTaskSource<T[]> Create(AsyncInstantiateOperation<T> asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T[]>.CreateFromCanceled(cancellationToken, out token);
				}
				AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> asyncInstantiateOperationConfiguredSource;
				if (!AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.pool.TryPop(out asyncInstantiateOperationConfiguredSource))
				{
					asyncInstantiateOperationConfiguredSource = new AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>();
				}
				asyncInstantiateOperationConfiguredSource.asyncOperation = asyncOperation;
				asyncInstantiateOperationConfiguredSource.progress = progress;
				asyncInstantiateOperationConfiguredSource.cancellationToken = cancellationToken;
				asyncInstantiateOperationConfiguredSource.cancelImmediately = cancelImmediately;
				asyncInstantiateOperationConfiguredSource.completed = false;
				asyncOperation.completed += asyncInstantiateOperationConfiguredSource.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					asyncInstantiateOperationConfiguredSource.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> asyncInstantiateOperationConfiguredSource2 = (AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>)state;
						asyncInstantiateOperationConfiguredSource2.core.TrySetCanceled(asyncInstantiateOperationConfiguredSource2.cancellationToken);
					}, asyncInstantiateOperationConfiguredSource);
				}
				PlayerLoopHelper.AddAction(timing, asyncInstantiateOperationConfiguredSource);
				token = asyncInstantiateOperationConfiguredSource.core.Version;
				return asyncInstantiateOperationConfiguredSource;
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x00029918 File Offset: 0x00027B18
			public T[] GetResult(short token)
			{
				T[] result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
				return result;
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x00029964 File Offset: 0x00027B64
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x0002996E File Offset: 0x00027B6E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x0002997C File Offset: 0x00027B7C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x00029989 File Offset: 0x00027B89
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0002999C File Offset: 0x00027B9C
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.Result);
					return false;
				}
				return true;
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x00029A24 File Offset: 0x00027C24
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.pool.TryPush(this);
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x00029A84 File Offset: 0x00027C84
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.Result);
			}

			// Token: 0x040004EC RID: 1260
			private static TaskPool<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>> pool;

			// Token: 0x040004ED RID: 1261
			private AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> nextNode;

			// Token: 0x040004EE RID: 1262
			private AsyncInstantiateOperation<T> asyncOperation;

			// Token: 0x040004EF RID: 1263
			private IProgress<float> progress;

			// Token: 0x040004F0 RID: 1264
			private CancellationToken cancellationToken;

			// Token: 0x040004F1 RID: 1265
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040004F2 RID: 1266
			private bool cancelImmediately;

			// Token: 0x040004F3 RID: 1267
			private bool completed;

			// Token: 0x040004F4 RID: 1268
			private UniTaskCompletionSourceCore<T[]> core;

			// Token: 0x040004F5 RID: 1269
			private Action<AsyncOperation> continuationAction;
		}
	}
}
