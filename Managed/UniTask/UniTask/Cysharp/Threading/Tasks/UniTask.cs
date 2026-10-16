using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.CompilerServices;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000039 RID: 57
	[global::System.Runtime.CompilerServices.AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct UniTask
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000455F File Offset: 0x0000275F
		public static IEnumerator ToCoroutine(Func<UniTask> taskFactory)
		{
			return taskFactory().ToCoroutine(null);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000456D File Offset: 0x0000276D
		public static YieldAwaitable Yield()
		{
			return new YieldAwaitable(PlayerLoopTiming.Update);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004575 File Offset: 0x00002775
		public static YieldAwaitable Yield(PlayerLoopTiming timing)
		{
			return new YieldAwaitable(timing);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004580 File Offset: 0x00002780
		public static UniTask Yield(CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.YieldPromise.Create(PlayerLoopTiming.Update, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000045A0 File Offset: 0x000027A0
		public static UniTask Yield(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.YieldPromise.Create(timing, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000045C0 File Offset: 0x000027C0
		public static UniTask NextFrame()
		{
			short num;
			return new UniTask(UniTask.NextFramePromise.Create(PlayerLoopTiming.Update, CancellationToken.None, false, out num), num);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000045E4 File Offset: 0x000027E4
		public static UniTask NextFrame(PlayerLoopTiming timing)
		{
			short num;
			return new UniTask(UniTask.NextFramePromise.Create(timing, CancellationToken.None, false, out num), num);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004608 File Offset: 0x00002808
		public static UniTask NextFrame(CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.NextFramePromise.Create(PlayerLoopTiming.Update, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004628 File Offset: 0x00002828
		public static UniTask NextFrame(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.NextFramePromise.Create(timing, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004648 File Offset: 0x00002848
		public static async UniTask WaitForEndOfFrame(CancellationToken cancellationToken = default(CancellationToken))
		{
			await Awaitable.EndOfFrameAsync(cancellationToken);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000468C File Offset: 0x0000288C
		public static UniTask WaitForEndOfFrame(MonoBehaviour coroutineRunner)
		{
			short num;
			return new UniTask(UniTask.WaitForEndOfFramePromise.Create(coroutineRunner, CancellationToken.None, false, out num), num);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000046B0 File Offset: 0x000028B0
		public static UniTask WaitForEndOfFrame(MonoBehaviour coroutineRunner, CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.WaitForEndOfFramePromise.Create(coroutineRunner, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000046CD File Offset: 0x000028CD
		public static YieldAwaitable WaitForFixedUpdate()
		{
			return UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000046D5 File Offset: 0x000028D5
		public static UniTask WaitForFixedUpdate(CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			return UniTask.Yield(PlayerLoopTiming.LastFixedUpdate, cancellationToken, cancelImmediately);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000046DF File Offset: 0x000028DF
		public static UniTask WaitForSeconds(float duration, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(Mathf.RoundToInt(1000f * duration), ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000046F7 File Offset: 0x000028F7
		public static UniTask WaitForSeconds(int duration, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(1000 * duration, ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000470C File Offset: 0x0000290C
		public static UniTask DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			if (delayFrameCount < 0)
			{
				throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount.ToString());
			}
			short num;
			return new UniTask(UniTask.DelayFramePromise.Create(delayFrameCount, delayTiming, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004745 File Offset: 0x00002945
		public static UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(TimeSpan.FromMilliseconds((double)millisecondsDelay), ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004758 File Offset: 0x00002958
		public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			DelayType delayType = (ignoreTimeScale ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime);
			return UniTask.Delay(delayTimeSpan, delayType, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004778 File Offset: 0x00002978
		public static UniTask Delay(int millisecondsDelay, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(TimeSpan.FromMilliseconds((double)millisecondsDelay), delayType, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000478C File Offset: 0x0000298C
		public static UniTask Delay(TimeSpan delayTimeSpan, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			if (delayTimeSpan < TimeSpan.Zero)
			{
				string text = "Delay does not allow minus delayTimeSpan. delayTimeSpan:";
				TimeSpan timeSpan = delayTimeSpan;
				throw new ArgumentOutOfRangeException(text + timeSpan.ToString());
			}
			switch (delayType)
			{
			case DelayType.UnscaledDeltaTime:
			{
				short num;
				return new UniTask(UniTask.DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out num), num);
			}
			case DelayType.Realtime:
			{
				short num2;
				return new UniTask(UniTask.DelayRealtimePromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out num2), num2);
			}
			}
			short num3;
			return new UniTask(UniTask.DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out num3), num3);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004814 File Offset: 0x00002A14
		public static UniTask FromException(Exception ex)
		{
			OperationCanceledException ex2 = ex as OperationCanceledException;
			if (ex2 != null)
			{
				return UniTask.FromCanceled(ex2.CancellationToken);
			}
			return new UniTask(new UniTask.ExceptionResultSource(ex), 0);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004844 File Offset: 0x00002A44
		public static UniTask<T> FromException<T>(Exception ex)
		{
			OperationCanceledException ex2 = ex as OperationCanceledException;
			if (ex2 != null)
			{
				return UniTask.FromCanceled<T>(ex2.CancellationToken);
			}
			return new UniTask<T>(new UniTask.ExceptionResultSource<T>(ex), 0);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004873 File Offset: 0x00002A73
		public static UniTask<T> FromResult<T>(T value)
		{
			return new UniTask<T>(value);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000487B File Offset: 0x00002A7B
		public static UniTask FromCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken == CancellationToken.None)
			{
				return UniTask.CanceledUniTask;
			}
			return new UniTask(new UniTask.CanceledResultSource(cancellationToken), 0);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000489C File Offset: 0x00002A9C
		public static UniTask<T> FromCanceled<T>(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken == CancellationToken.None)
			{
				return UniTask.CanceledUniTaskCache<T>.Task;
			}
			return new UniTask<T>(new UniTask.CanceledResultSource<T>(cancellationToken), 0);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000048BD File Offset: 0x00002ABD
		public static UniTask Create(Func<UniTask> factory)
		{
			return factory();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000048C5 File Offset: 0x00002AC5
		public static UniTask Create(Func<CancellationToken, UniTask> factory, CancellationToken cancellationToken)
		{
			return factory(cancellationToken);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000048CE File Offset: 0x00002ACE
		public static UniTask Create<T>(T state, Func<T, UniTask> factory)
		{
			return factory(state);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000048D7 File Offset: 0x00002AD7
		public static UniTask<T> Create<T>(Func<UniTask<T>> factory)
		{
			return factory();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000048DF File Offset: 0x00002ADF
		public static AsyncLazy Lazy(Func<UniTask> factory)
		{
			return new AsyncLazy(factory);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000048E7 File Offset: 0x00002AE7
		public static AsyncLazy<T> Lazy<T>(Func<UniTask<T>> factory)
		{
			return new AsyncLazy<T>(factory);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000048F0 File Offset: 0x00002AF0
		public static void Void(Func<UniTaskVoid> asyncAction)
		{
			asyncAction().Forget();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000490C File Offset: 0x00002B0C
		public static void Void(Func<CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			asyncAction(cancellationToken).Forget();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004928 File Offset: 0x00002B28
		public static void Void<T>(Func<T, UniTaskVoid> asyncAction, T state)
		{
			asyncAction(state).Forget();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004944 File Offset: 0x00002B44
		public static Action Action(Func<UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction().Forget();
			};
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000495D File Offset: 0x00002B5D
		public static Action Action(Func<CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate
			{
				asyncAction(cancellationToken).Forget();
			};
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000497D File Offset: 0x00002B7D
		public static Action Action<T>(T state, Func<T, UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction(state).Forget();
			};
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000499D File Offset: 0x00002B9D
		public static UnityAction UnityAction(Func<UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction().Forget();
			};
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000049B6 File Offset: 0x00002BB6
		public static UnityAction UnityAction(Func<CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate
			{
				asyncAction(cancellationToken).Forget();
			};
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000049D6 File Offset: 0x00002BD6
		public static UnityAction UnityAction<T>(T state, Func<T, UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction(state).Forget();
			};
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000049F6 File Offset: 0x00002BF6
		public static UnityAction<T> UnityAction<T>(Func<T, UniTaskVoid> asyncAction)
		{
			return delegate(T arg)
			{
				asyncAction(arg).Forget();
			};
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004A0F File Offset: 0x00002C0F
		public static UnityAction<T0, T1> UnityAction<T0, T1>(Func<T0, T1, UniTaskVoid> asyncAction)
		{
			return delegate(T0 arg0, T1 arg1)
			{
				asyncAction(arg0, arg1).Forget();
			};
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004A28 File Offset: 0x00002C28
		public static UnityAction<T0, T1, T2> UnityAction<T0, T1, T2>(Func<T0, T1, T2, UniTaskVoid> asyncAction)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2)
			{
				asyncAction(arg0, arg1, arg2).Forget();
			};
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004A41 File Offset: 0x00002C41
		public static UnityAction<T0, T1, T2, T3> UnityAction<T0, T1, T2, T3>(Func<T0, T1, T2, T3, UniTaskVoid> asyncAction)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
			{
				asyncAction(arg0, arg1, arg2, arg3).Forget();
			};
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004A5A File Offset: 0x00002C5A
		public static UnityAction<T> UnityAction<T>(Func<T, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T arg)
			{
				asyncAction(arg, cancellationToken).Forget();
			};
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004A7A File Offset: 0x00002C7A
		public static UnityAction<T0, T1> UnityAction<T0, T1>(Func<T0, T1, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T0 arg0, T1 arg1)
			{
				asyncAction(arg0, arg1, cancellationToken).Forget();
			};
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004A9A File Offset: 0x00002C9A
		public static UnityAction<T0, T1, T2> UnityAction<T0, T1, T2>(Func<T0, T1, T2, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2)
			{
				asyncAction(arg0, arg1, arg2, cancellationToken).Forget();
			};
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004ABA File Offset: 0x00002CBA
		public static UnityAction<T0, T1, T2, T3> UnityAction<T0, T1, T2, T3>(Func<T0, T1, T2, T3, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
			{
				asyncAction(arg0, arg1, arg2, arg3, cancellationToken).Forget();
			};
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004ADA File Offset: 0x00002CDA
		public static UniTask Defer(Func<UniTask> factory)
		{
			return new UniTask(new UniTask.DeferPromise(factory), 0);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public static UniTask<T> Defer<T>(Func<UniTask<T>> factory)
		{
			return new UniTask<T>(new UniTask.DeferPromise<T>(factory), 0);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004AF6 File Offset: 0x00002CF6
		public static UniTask Defer<TState>(TState state, Func<TState, UniTask> factory)
		{
			return new UniTask(new UniTask.DeferPromiseWithState<TState>(state, factory), 0);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B05 File Offset: 0x00002D05
		public static UniTask<TResult> Defer<TState, TResult>(TState state, Func<TState, UniTask<TResult>> factory)
		{
			return new UniTask<TResult>(new UniTask.DeferPromiseWithState<TState, TResult>(state, factory), 0);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B14 File Offset: 0x00002D14
		public static UniTask Never(CancellationToken cancellationToken)
		{
			return new UniTask<AsyncUnit>(new UniTask.NeverPromise<AsyncUnit>(cancellationToken), 0);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004B27 File Offset: 0x00002D27
		public static UniTask<T> Never<T>(CancellationToken cancellationToken)
		{
			return new UniTask<T>(new UniTask.NeverPromise<T>(cancellationToken), 0);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004B35 File Offset: 0x00002D35
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Action action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004B3F File Offset: 0x00002D3F
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004B4A File Offset: 0x00002D4A
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Func<UniTask> action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004B54 File Offset: 0x00002D54
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Func<object, UniTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004B5F File Offset: 0x00002D5F
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, configureAwait, cancellationToken);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004B69 File Offset: 0x00002D69
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<UniTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, configureAwait, cancellationToken);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004B73 File Offset: 0x00002D73
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, state, configureAwait, cancellationToken);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004B7E File Offset: 0x00002D7E
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, state, configureAwait, cancellationToken);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static async UniTask RunOnThreadPool(Action action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					action();
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				action();
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public static async UniTask RunOnThreadPool(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					action(state);
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				action(state);
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004C3C File Offset: 0x00002E3C
		public static async UniTask RunOnThreadPool(Func<UniTask> action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					await action();
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				await action();
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004C90 File Offset: 0x00002E90
		public static async UniTask RunOnThreadPool(Func<object, UniTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					await action(state);
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				await action(state);
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004CEC File Offset: 0x00002EEC
		public static async UniTask<T> RunOnThreadPool<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = func();
					num = 1;
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				t2 = func();
			}
			return t2;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004D40 File Offset: 0x00002F40
		public static async UniTask<T> RunOnThreadPool<T>(Func<UniTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = await func();
					num = 1;
				}
				catch (object obj)
				{
				}
				cancellationToken.ThrowIfCancellationRequested();
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				T t3 = await func();
				cancellationToken.ThrowIfCancellationRequested();
				t2 = t3;
			}
			return t2;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004D94 File Offset: 0x00002F94
		public static async UniTask<T> RunOnThreadPool<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = func(state);
					num = 1;
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				t2 = func(state);
			}
			return t2;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public static async UniTask<T> RunOnThreadPool<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = await func(state);
					num = 1;
				}
				catch (object obj)
				{
				}
				cancellationToken.ThrowIfCancellationRequested();
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				T t3 = await func(state);
				cancellationToken.ThrowIfCancellationRequested();
				t2 = t3;
			}
			return t2;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004E4B File Offset: 0x0000304B
		public static SwitchToMainThreadAwaitable SwitchToMainThread(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new SwitchToMainThreadAwaitable(PlayerLoopTiming.Update, cancellationToken);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004E54 File Offset: 0x00003054
		public static SwitchToMainThreadAwaitable SwitchToMainThread(PlayerLoopTiming timing, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new SwitchToMainThreadAwaitable(timing, cancellationToken);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004E5D File Offset: 0x0000305D
		public static ReturnToMainThread ReturnToMainThread(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToMainThread(PlayerLoopTiming.Update, cancellationToken);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004E66 File Offset: 0x00003066
		public static ReturnToMainThread ReturnToMainThread(PlayerLoopTiming timing, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToMainThread(timing, cancellationToken);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004E6F File Offset: 0x0000306F
		public static void Post(Action action, PlayerLoopTiming timing = PlayerLoopTiming.Update)
		{
			PlayerLoopHelper.AddContinuation(timing, action);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004E78 File Offset: 0x00003078
		public static SwitchToThreadPoolAwaitable SwitchToThreadPool()
		{
			return default(SwitchToThreadPoolAwaitable);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004E90 File Offset: 0x00003090
		public static SwitchToTaskPoolAwaitable SwitchToTaskPool()
		{
			return default(SwitchToTaskPoolAwaitable);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004EA6 File Offset: 0x000030A6
		public static SwitchToSynchronizationContextAwaitable SwitchToSynchronizationContext(SynchronizationContext synchronizationContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			Error.ThrowArgumentNullException<SynchronizationContext>(synchronizationContext, "synchronizationContext");
			return new SwitchToSynchronizationContextAwaitable(synchronizationContext, cancellationToken);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004EBA File Offset: 0x000030BA
		public static ReturnToSynchronizationContext ReturnToSynchronizationContext(SynchronizationContext synchronizationContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToSynchronizationContext(synchronizationContext, false, cancellationToken);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004EC4 File Offset: 0x000030C4
		public static ReturnToSynchronizationContext ReturnToCurrentSynchronizationContext(bool dontPostWhenSameContext = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToSynchronizationContext(SynchronizationContext.Current, dontPostWhenSameContext, cancellationToken);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004ED4 File Offset: 0x000030D4
		public static UniTask WaitUntil(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.WaitUntilPromise.Create(predicate, timing, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004EF4 File Offset: 0x000030F4
		public static UniTask WaitUntil<T>(T state, Func<T, bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.WaitUntilPromise<T>.Create(state, predicate, timing, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004F14 File Offset: 0x00003114
		public static UniTask WaitWhile(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.WaitWhilePromise.Create(predicate, timing, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004F34 File Offset: 0x00003134
		public static UniTask WaitWhile<T>(T state, Func<T, bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short num;
			return new UniTask(UniTask.WaitWhilePromise<T>.Create(state, predicate, timing, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004F54 File Offset: 0x00003154
		public static UniTask WaitUntilCanceled(CancellationToken cancellationToken, PlayerLoopTiming timing = PlayerLoopTiming.Update, bool completeImmediately = false)
		{
			short num;
			return new UniTask(UniTask.WaitUntilCanceledPromise.Create(cancellationToken, timing, completeImmediately, out num), num);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004F74 File Offset: 0x00003174
		public static UniTask<U> WaitUntilValueChanged<T, U>(T target, Func<T, U> monitorFunction, PlayerLoopTiming monitorTiming = PlayerLoopTiming.Update, IEqualityComparer<U> equalityComparer = null, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false) where T : class
		{
			short num;
			return new UniTask<U>((target is global::UnityEngine.Object) ? UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.Create(target, monitorFunction, equalityComparer, monitorTiming, cancellationToken, cancelImmediately, out num) : UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.Create(target, monitorFunction, equalityComparer, monitorTiming, cancellationToken, cancelImmediately, out num), num);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004FB8 File Offset: 0x000031B8
		public static UniTask<ValueTuple<T1, T2>> WhenAll<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2>>(new ValueTuple<T1, T2>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2>>(new UniTask.WhenAllPromise<T1, T2>(task1, task2), 0);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005018 File Offset: 0x00003218
		public static UniTask<ValueTuple<T1, T2, T3>> WhenAll<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3>>(new ValueTuple<T1, T2, T3>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3>>(new UniTask.WhenAllPromise<T1, T2, T3>(task1, task2, task3), 0);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005098 File Offset: 0x00003298
		public static UniTask<ValueTuple<T1, T2, T3, T4>> WhenAll<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4>>(new ValueTuple<T1, T2, T3, T4>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4>>(new UniTask.WhenAllPromise<T1, T2, T3, T4>(task1, task2, task3, task4), 0);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005134 File Offset: 0x00003334
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5>> WhenAll<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5>>(new ValueTuple<T1, T2, T3, T4, T5>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5), 0);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000051F8 File Offset: 0x000033F8
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>> WhenAll<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>>(new ValueTuple<T1, T2, T3, T4, T5, T6>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6), 0);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000052E0 File Offset: 0x000034E0
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>> WhenAll<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7), 0);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000053F0 File Offset: 0x000035F0
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8>(task8.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>(task1, task2, task3, task4, task5, task6, task7, task8), 0);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005528 File Offset: 0x00003728
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>(task1, task2, task3, task4, task5, task6, task7, task8, task9), 0);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005684 File Offset: 0x00003884
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10), 0);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005804 File Offset: 0x00003A04
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11), 0);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000059A8 File Offset: 0x00003BA8
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12), 0);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005B6C File Offset: 0x00003D6C
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully() && task13.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12, T13>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult(), task13.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13), 0);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D54 File Offset: 0x00003F54
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully() && task13.Status.IsCompletedSuccessfully() && task14.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult(), task13.GetAwaiter().GetResult(), task14.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14), 0);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005F60 File Offset: 0x00004160
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully() && task13.Status.IsCompletedSuccessfully() && task14.Status.IsCompletedSuccessfully() && task15.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult(), task13.GetAwaiter().GetResult(), task14.GetAwaiter().GetResult(), new ValueTuple<T15>(task15.GetAwaiter().GetResult()))));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15), 0);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006192 File Offset: 0x00004392
		public static UniTask<T[]> WhenAll<T>(params UniTask<T>[] tasks)
		{
			if (tasks.Length == 0)
			{
				return UniTask.FromResult<T[]>(Array.Empty<T>());
			}
			return new UniTask<T[]>(new UniTask.WhenAllPromise<T>(tasks, tasks.Length), 0);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000061B4 File Offset: 0x000043B4
		public static UniTask<T[]> WhenAll<T>(IEnumerable<UniTask<T>> tasks)
		{
			UniTask<T[]> uniTask;
			using (ArrayPoolUtil.RentArray<UniTask<T>> rentArray = ArrayPoolUtil.Materialize<UniTask<T>>(tasks))
			{
				uniTask = new UniTask<T[]>(new UniTask.WhenAllPromise<T>(rentArray.Array, rentArray.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006204 File Offset: 0x00004404
		public static UniTask WhenAll(params UniTask[] tasks)
		{
			if (tasks.Length == 0)
			{
				return UniTask.CompletedTask;
			}
			return new UniTask(new UniTask.WhenAllPromise(tasks, tasks.Length), 0);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006220 File Offset: 0x00004420
		public static UniTask WhenAll(IEnumerable<UniTask> tasks)
		{
			UniTask uniTask;
			using (ArrayPoolUtil.RentArray<UniTask> rentArray = ArrayPoolUtil.Materialize<UniTask>(tasks))
			{
				uniTask = new UniTask(new UniTask.WhenAllPromise(rentArray.Array, rentArray.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006270 File Offset: 0x00004470
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2" })]
		public static UniTask<ValueTuple<int, T1, T2>> WhenAny<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
		{
			return new UniTask<ValueTuple<int, T1, T2>>(new UniTask.WhenAnyPromise<T1, T2>(task1, task2), 0);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000627F File Offset: 0x0000447F
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3" })]
		public static UniTask<ValueTuple<int, T1, T2, T3>> WhenAny<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3>>(new UniTask.WhenAnyPromise<T1, T2, T3>(task1, task2, task3), 0);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000628F File Offset: 0x0000448F
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4" })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4>> WhenAny<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4>(task1, task2, task3, task4), 0);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000062A0 File Offset: 0x000044A0
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4", "result5" })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5>> WhenAny<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5), 0);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000062B3 File Offset: 0x000044B3
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6" })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6>> WhenAny<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6), 0);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000062C8 File Offset: 0x000044C8
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", null })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>> WhenAny<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7), 0);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000062DF File Offset: 0x000044DF
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", null,
			null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>(task1, task2, task3, task4, task5, task6, task7, task8), 0);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000062F8 File Offset: 0x000044F8
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>(task1, task2, task3, task4, task5, task6, task7, task8, task9), 0);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006320 File Offset: 0x00004520
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10), 0);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006348 File Offset: 0x00004548
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", null, null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11), 0);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006374 File Offset: 0x00004574
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", null, null, null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12), 0);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000063A0 File Offset: 0x000045A0
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", "result13", null, null, null, null, null, null,
			null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13), 0);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000063D0 File Offset: 0x000045D0
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", "result13", "result14", null, null, null, null, null,
			null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14), 0);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006400 File Offset: 0x00004600
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", "result13", "result14", "result15", null, null, null, null,
			null, null, null, null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15), 0);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006432 File Offset: 0x00004632
		[return: TupleElementNames(new string[] { "hasResultLeft", "result" })]
		public static UniTask<ValueTuple<bool, T>> WhenAny<T>(UniTask<T> leftTask, UniTask rightTask)
		{
			return new UniTask<ValueTuple<bool, T>>(new UniTask.WhenAnyLRPromise<T>(leftTask, rightTask), 0);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006441 File Offset: 0x00004641
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result" })]
		public static UniTask<ValueTuple<int, T>> WhenAny<T>(params UniTask<T>[] tasks)
		{
			return new UniTask<ValueTuple<int, T>>(new UniTask.WhenAnyPromise<T>(tasks, tasks.Length), 0);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006454 File Offset: 0x00004654
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result" })]
		public static UniTask<ValueTuple<int, T>> WhenAny<T>(IEnumerable<UniTask<T>> tasks)
		{
			UniTask<ValueTuple<int, T>> uniTask;
			using (ArrayPoolUtil.RentArray<UniTask<T>> rentArray = ArrayPoolUtil.Materialize<UniTask<T>>(tasks))
			{
				uniTask = new UniTask<ValueTuple<int, T>>(new UniTask.WhenAnyPromise<T>(rentArray.Array, rentArray.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000064A4 File Offset: 0x000046A4
		public static UniTask<int> WhenAny(params UniTask[] tasks)
		{
			return new UniTask<int>(new UniTask.WhenAnyPromise(tasks, tasks.Length), 0);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000064B8 File Offset: 0x000046B8
		public static UniTask<int> WhenAny(IEnumerable<UniTask> tasks)
		{
			UniTask<int> uniTask;
			using (ArrayPoolUtil.RentArray<UniTask> rentArray = ArrayPoolUtil.Materialize<UniTask>(tasks))
			{
				uniTask = new UniTask<int>(new UniTask.WhenAnyPromise(rentArray.Array, rentArray.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006508 File Offset: 0x00004708
		public static IUniTaskAsyncEnumerable<WhenEachResult<T>> WhenEach<T>(IEnumerable<UniTask<T>> tasks)
		{
			return new WhenEachEnumerable<T>(tasks);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006510 File Offset: 0x00004710
		public static IUniTaskAsyncEnumerable<WhenEachResult<T>> WhenEach<T>(params UniTask<T>[] tasks)
		{
			return new WhenEachEnumerable<T>(tasks);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006518 File Offset: 0x00004718
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask(IUniTaskSource source, short token)
		{
			this.source = source;
			this.token = token;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006528 File Offset: 0x00004728
		public UniTaskStatus Status
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.source == null)
				{
					return UniTaskStatus.Succeeded;
				}
				return this.source.GetStatus(this.token);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006545 File Offset: 0x00004745
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask.Awaiter GetAwaiter()
		{
			return new UniTask.Awaiter(in this);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006550 File Offset: 0x00004750
		public UniTask<bool> SuppressCancellationThrow()
		{
			UniTaskStatus status = this.Status;
			if (status == UniTaskStatus.Succeeded)
			{
				return CompletedTasks.False;
			}
			if (status == UniTaskStatus.Canceled)
			{
				return CompletedTasks.True;
			}
			return new UniTask<bool>(new UniTask.IsCanceledSource(this.source), this.token);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006590 File Offset: 0x00004790
		public static implicit operator ValueTask(in UniTask self)
		{
			if (self.source == null)
			{
				return default(ValueTask);
			}
			return new ValueTask(self.source, self.token);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000065C0 File Offset: 0x000047C0
		public override string ToString()
		{
			if (this.source == null)
			{
				return "()";
			}
			return "(" + this.source.UnsafeGetStatus().ToString() + ")";
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006603 File Offset: 0x00004803
		public UniTask Preserve()
		{
			if (this.source == null)
			{
				return this;
			}
			return new UniTask(new UniTask.MemoizeSource(this.source), this.token);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000662C File Offset: 0x0000482C
		public UniTask<AsyncUnit> AsAsyncUnitUniTask()
		{
			if (this.source == null)
			{
				return CompletedTasks.AsyncUnit;
			}
			if (this.source.GetStatus(this.token).IsCompletedSuccessfully())
			{
				this.source.GetResult(this.token);
				return CompletedTasks.AsyncUnit;
			}
			IUniTaskSource<AsyncUnit> uniTaskSource = this.source as IUniTaskSource<AsyncUnit>;
			if (uniTaskSource != null)
			{
				return new UniTask<AsyncUnit>(uniTaskSource, this.token);
			}
			return new UniTask<AsyncUnit>(new UniTask.AsyncUnitSource(this.source), this.token);
		}

		// Token: 0x0400007B RID: 123
		private static readonly UniTask CanceledUniTask = (() => new UniTask(new UniTask.CanceledResultSource(CancellationToken.None), 0))();

		// Token: 0x0400007C RID: 124
		public static readonly UniTask CompletedTask = default(UniTask);

		// Token: 0x0400007D RID: 125
		private readonly IUniTaskSource source;

		// Token: 0x0400007E RID: 126
		private readonly short token;

		// Token: 0x02000167 RID: 359
		private sealed class YieldPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.YieldPromise>
		{
			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001192E File Offset: 0x0000FB2E
			public ref UniTask.YieldPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x00011936 File Offset: 0x0000FB36
			static YieldPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.YieldPromise), () => UniTask.YieldPromise.pool.Size);
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x00011957 File Offset: 0x0000FB57
			private YieldPromise()
			{
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x00011960 File Offset: 0x0000FB60
			public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.YieldPromise yieldPromise;
				if (!UniTask.YieldPromise.pool.TryPop(out yieldPromise))
				{
					yieldPromise = new UniTask.YieldPromise();
				}
				yieldPromise.cancellationToken = cancellationToken;
				yieldPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					yieldPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.YieldPromise yieldPromise2 = (UniTask.YieldPromise)state;
						yieldPromise2.core.TrySetCanceled(yieldPromise2.cancellationToken);
					}, yieldPromise);
				}
				PlayerLoopHelper.AddAction(timing, yieldPromise);
				token = yieldPromise.core.Version;
				return yieldPromise;
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x000119F0 File Offset: 0x0000FBF0
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x00011A3C File Offset: 0x0000FC3C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x00011A4A File Offset: 0x0000FC4A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x00011A57 File Offset: 0x0000FC57
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x00011A67 File Offset: 0x0000FC67
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x00011A98 File Offset: 0x0000FC98
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.YieldPromise.pool.TryPush(this);
			}

			// Token: 0x040001E0 RID: 480
			private static TaskPool<UniTask.YieldPromise> pool;

			// Token: 0x040001E1 RID: 481
			private UniTask.YieldPromise nextNode;

			// Token: 0x040001E2 RID: 482
			private CancellationToken cancellationToken;

			// Token: 0x040001E3 RID: 483
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040001E4 RID: 484
			private bool cancelImmediately;

			// Token: 0x040001E5 RID: 485
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000168 RID: 360
		private sealed class NextFramePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.NextFramePromise>
		{
			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x00011ACE File Offset: 0x0000FCCE
			public ref UniTask.NextFramePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000791 RID: 1937 RVA: 0x00011AD6 File Offset: 0x0000FCD6
			static NextFramePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.NextFramePromise), () => UniTask.NextFramePromise.pool.Size);
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x00011AF7 File Offset: 0x0000FCF7
			private NextFramePromise()
			{
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x00011B00 File Offset: 0x0000FD00
			public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.NextFramePromise nextFramePromise;
				if (!UniTask.NextFramePromise.pool.TryPop(out nextFramePromise))
				{
					nextFramePromise = new UniTask.NextFramePromise();
				}
				nextFramePromise.frameCount = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				nextFramePromise.cancellationToken = cancellationToken;
				nextFramePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					nextFramePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.NextFramePromise nextFramePromise2 = (UniTask.NextFramePromise)state;
						nextFramePromise2.core.TrySetCanceled(nextFramePromise2.cancellationToken);
					}, nextFramePromise);
				}
				PlayerLoopHelper.AddAction(timing, nextFramePromise);
				token = nextFramePromise.core.Version;
				return nextFramePromise;
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x00011BA4 File Offset: 0x0000FDA4
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x00011BF0 File Offset: 0x0000FDF0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x00011BFE File Offset: 0x0000FDFE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000797 RID: 1943 RVA: 0x00011C0B File Offset: 0x0000FE0B
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x00011C1C File Offset: 0x0000FE1C
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.frameCount == Time.frameCount)
				{
					return true;
				}
				this.core.TrySetResult(AsyncUnit.Default);
				return false;
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x00011C6B File Offset: 0x0000FE6B
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				return UniTask.NextFramePromise.pool.TryPush(this);
			}

			// Token: 0x040001E6 RID: 486
			private static TaskPool<UniTask.NextFramePromise> pool;

			// Token: 0x040001E7 RID: 487
			private UniTask.NextFramePromise nextNode;

			// Token: 0x040001E8 RID: 488
			private int frameCount;

			// Token: 0x040001E9 RID: 489
			private UniTaskCompletionSourceCore<AsyncUnit> core;

			// Token: 0x040001EA RID: 490
			private CancellationToken cancellationToken;

			// Token: 0x040001EB RID: 491
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040001EC RID: 492
			private bool cancelImmediately;
		}

		// Token: 0x02000169 RID: 361
		private sealed class WaitForEndOfFramePromise : IUniTaskSource, IValueTaskSource, ITaskPoolNode<UniTask.WaitForEndOfFramePromise>, IEnumerator
		{
			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600079A RID: 1946 RVA: 0x00011C9A File Offset: 0x0000FE9A
			public ref UniTask.WaitForEndOfFramePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600079B RID: 1947 RVA: 0x00011CA2 File Offset: 0x0000FEA2
			static WaitForEndOfFramePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitForEndOfFramePromise), () => UniTask.WaitForEndOfFramePromise.pool.Size);
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x00011CCD File Offset: 0x0000FECD
			private WaitForEndOfFramePromise()
			{
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x00011CDC File Offset: 0x0000FEDC
			public static IUniTaskSource Create(MonoBehaviour coroutineRunner, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitForEndOfFramePromise waitForEndOfFramePromise;
				if (!UniTask.WaitForEndOfFramePromise.pool.TryPop(out waitForEndOfFramePromise))
				{
					waitForEndOfFramePromise = new UniTask.WaitForEndOfFramePromise();
				}
				waitForEndOfFramePromise.cancellationToken = cancellationToken;
				waitForEndOfFramePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitForEndOfFramePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitForEndOfFramePromise waitForEndOfFramePromise2 = (UniTask.WaitForEndOfFramePromise)state;
						waitForEndOfFramePromise2.core.TrySetCanceled(waitForEndOfFramePromise2.cancellationToken);
					}, waitForEndOfFramePromise);
				}
				coroutineRunner.StartCoroutine(waitForEndOfFramePromise);
				token = waitForEndOfFramePromise.core.Version;
				return waitForEndOfFramePromise;
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x00011D6C File Offset: 0x0000FF6C
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x00011DB8 File Offset: 0x0000FFB8
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x00011DC6 File Offset: 0x0000FFC6
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x00011DD3 File Offset: 0x0000FFD3
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x00011DE3 File Offset: 0x0000FFE3
			private bool TryReturn()
			{
				this.core.Reset();
				this.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				return UniTask.WaitForEndOfFramePromise.pool.TryPush(this);
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00011E18 File Offset: 0x00010018
			object IEnumerator.Current
			{
				get
				{
					return UniTask.WaitForEndOfFramePromise.waitForEndOfFrameYieldInstruction;
				}
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x00011E20 File Offset: 0x00010020
			bool IEnumerator.MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return true;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x00011E6D File Offset: 0x0001006D
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x040001ED RID: 493
			private static TaskPool<UniTask.WaitForEndOfFramePromise> pool;

			// Token: 0x040001EE RID: 494
			private UniTask.WaitForEndOfFramePromise nextNode;

			// Token: 0x040001EF RID: 495
			private UniTaskCompletionSourceCore<object> core;

			// Token: 0x040001F0 RID: 496
			private CancellationToken cancellationToken;

			// Token: 0x040001F1 RID: 497
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040001F2 RID: 498
			private bool cancelImmediately;

			// Token: 0x040001F3 RID: 499
			private static readonly WaitForEndOfFrame waitForEndOfFrameYieldInstruction = new WaitForEndOfFrame();

			// Token: 0x040001F4 RID: 500
			private bool isFirst = true;
		}

		// Token: 0x0200016A RID: 362
		private sealed class DelayFramePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayFramePromise>
		{
			// Token: 0x17000060 RID: 96
			// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00011E76 File Offset: 0x00010076
			public ref UniTask.DelayFramePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060007A7 RID: 1959 RVA: 0x00011E7E File Offset: 0x0001007E
			static DelayFramePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayFramePromise), () => UniTask.DelayFramePromise.pool.Size);
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x00011E9F File Offset: 0x0001009F
			private DelayFramePromise()
			{
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x00011EA8 File Offset: 0x000100A8
			public static IUniTaskSource Create(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayFramePromise delayFramePromise;
				if (!UniTask.DelayFramePromise.pool.TryPop(out delayFramePromise))
				{
					delayFramePromise = new UniTask.DelayFramePromise();
				}
				delayFramePromise.delayFrameCount = delayFrameCount;
				delayFramePromise.cancellationToken = cancellationToken;
				delayFramePromise.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				delayFramePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					delayFramePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayFramePromise delayFramePromise2 = (UniTask.DelayFramePromise)state;
						delayFramePromise2.core.TrySetCanceled(delayFramePromise2.cancellationToken);
					}, delayFramePromise);
				}
				PlayerLoopHelper.AddAction(timing, delayFramePromise);
				token = delayFramePromise.core.Version;
				return delayFramePromise;
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x00011F54 File Offset: 0x00010154
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x00011FA0 File Offset: 0x000101A0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x00011FAE File Offset: 0x000101AE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x00011FBB File Offset: 0x000101BB
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x00011FCC File Offset: 0x000101CC
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.currentFrameCount == 0)
				{
					if (this.delayFrameCount == 0)
					{
						this.core.TrySetResult(AsyncUnit.Default);
						return false;
					}
					if (this.initialFrame == Time.frameCount)
					{
						return true;
					}
				}
				int num = this.currentFrameCount + 1;
				this.currentFrameCount = num;
				if (num >= this.delayFrameCount)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				return true;
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x0001205C File Offset: 0x0001025C
			private bool TryReturn()
			{
				this.core.Reset();
				this.currentFrameCount = 0;
				this.delayFrameCount = 0;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayFramePromise.pool.TryPush(this);
			}

			// Token: 0x040001F5 RID: 501
			private static TaskPool<UniTask.DelayFramePromise> pool;

			// Token: 0x040001F6 RID: 502
			private UniTask.DelayFramePromise nextNode;

			// Token: 0x040001F7 RID: 503
			private int initialFrame;

			// Token: 0x040001F8 RID: 504
			private int delayFrameCount;

			// Token: 0x040001F9 RID: 505
			private CancellationToken cancellationToken;

			// Token: 0x040001FA RID: 506
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040001FB RID: 507
			private bool cancelImmediately;

			// Token: 0x040001FC RID: 508
			private int currentFrameCount;

			// Token: 0x040001FD RID: 509
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x0200016B RID: 363
		private sealed class DelayPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayPromise>
		{
			// Token: 0x17000061 RID: 97
			// (get) Token: 0x060007B0 RID: 1968 RVA: 0x000120AB File Offset: 0x000102AB
			public ref UniTask.DelayPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060007B1 RID: 1969 RVA: 0x000120B3 File Offset: 0x000102B3
			static DelayPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayPromise), () => UniTask.DelayPromise.pool.Size);
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x000120D4 File Offset: 0x000102D4
			private DelayPromise()
			{
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x000120DC File Offset: 0x000102DC
			public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayPromise delayPromise;
				if (!UniTask.DelayPromise.pool.TryPop(out delayPromise))
				{
					delayPromise = new UniTask.DelayPromise();
				}
				delayPromise.elapsed = 0f;
				delayPromise.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
				delayPromise.cancellationToken = cancellationToken;
				delayPromise.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				delayPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					delayPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayPromise delayPromise2 = (UniTask.DelayPromise)state;
						delayPromise2.core.TrySetCanceled(delayPromise2.cancellationToken);
					}, delayPromise);
				}
				PlayerLoopHelper.AddAction(timing, delayPromise);
				token = delayPromise.core.Version;
				return delayPromise;
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x0001219C File Offset: 0x0001039C
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x000121E8 File Offset: 0x000103E8
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x000121F6 File Offset: 0x000103F6
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060007B7 RID: 1975 RVA: 0x00012203 File Offset: 0x00010403
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x00012214 File Offset: 0x00010414
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.elapsed == 0f && this.initialFrame == Time.frameCount)
				{
					return true;
				}
				this.elapsed += Time.deltaTime;
				if (this.elapsed >= this.delayTimeSpan)
				{
					this.core.TrySetResult(null);
					return false;
				}
				return true;
			}

			// Token: 0x060007B9 RID: 1977 RVA: 0x00012290 File Offset: 0x00010490
			private bool TryReturn()
			{
				this.core.Reset();
				this.delayTimeSpan = 0f;
				this.elapsed = 0f;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayPromise.pool.TryPush(this);
			}

			// Token: 0x040001FE RID: 510
			private static TaskPool<UniTask.DelayPromise> pool;

			// Token: 0x040001FF RID: 511
			private UniTask.DelayPromise nextNode;

			// Token: 0x04000200 RID: 512
			private int initialFrame;

			// Token: 0x04000201 RID: 513
			private float delayTimeSpan;

			// Token: 0x04000202 RID: 514
			private float elapsed;

			// Token: 0x04000203 RID: 515
			private CancellationToken cancellationToken;

			// Token: 0x04000204 RID: 516
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000205 RID: 517
			private bool cancelImmediately;

			// Token: 0x04000206 RID: 518
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200016C RID: 364
		private sealed class DelayIgnoreTimeScalePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayIgnoreTimeScalePromise>
		{
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x060007BA RID: 1978 RVA: 0x000122E7 File Offset: 0x000104E7
			public ref UniTask.DelayIgnoreTimeScalePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x000122EF File Offset: 0x000104EF
			static DelayIgnoreTimeScalePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayIgnoreTimeScalePromise), () => UniTask.DelayIgnoreTimeScalePromise.pool.Size);
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x00012310 File Offset: 0x00010510
			private DelayIgnoreTimeScalePromise()
			{
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x00012318 File Offset: 0x00010518
			public static IUniTaskSource Create(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayIgnoreTimeScalePromise delayIgnoreTimeScalePromise;
				if (!UniTask.DelayIgnoreTimeScalePromise.pool.TryPop(out delayIgnoreTimeScalePromise))
				{
					delayIgnoreTimeScalePromise = new UniTask.DelayIgnoreTimeScalePromise();
				}
				delayIgnoreTimeScalePromise.elapsed = 0f;
				delayIgnoreTimeScalePromise.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
				delayIgnoreTimeScalePromise.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				delayIgnoreTimeScalePromise.cancellationToken = cancellationToken;
				delayIgnoreTimeScalePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					delayIgnoreTimeScalePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayIgnoreTimeScalePromise delayIgnoreTimeScalePromise2 = (UniTask.DelayIgnoreTimeScalePromise)state;
						delayIgnoreTimeScalePromise2.core.TrySetCanceled(delayIgnoreTimeScalePromise2.cancellationToken);
					}, delayIgnoreTimeScalePromise);
				}
				PlayerLoopHelper.AddAction(timing, delayIgnoreTimeScalePromise);
				token = delayIgnoreTimeScalePromise.core.Version;
				return delayIgnoreTimeScalePromise;
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x000123D8 File Offset: 0x000105D8
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x00012424 File Offset: 0x00010624
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x00012432 File Offset: 0x00010632
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x0001243F File Offset: 0x0001063F
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x00012450 File Offset: 0x00010650
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.elapsed == 0f && this.initialFrame == Time.frameCount)
				{
					return true;
				}
				this.elapsed += Time.unscaledDeltaTime;
				if (this.elapsed >= this.delayFrameTimeSpan)
				{
					this.core.TrySetResult(null);
					return false;
				}
				return true;
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x000124CC File Offset: 0x000106CC
			private bool TryReturn()
			{
				this.core.Reset();
				this.delayFrameTimeSpan = 0f;
				this.elapsed = 0f;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayIgnoreTimeScalePromise.pool.TryPush(this);
			}

			// Token: 0x04000207 RID: 519
			private static TaskPool<UniTask.DelayIgnoreTimeScalePromise> pool;

			// Token: 0x04000208 RID: 520
			private UniTask.DelayIgnoreTimeScalePromise nextNode;

			// Token: 0x04000209 RID: 521
			private float delayFrameTimeSpan;

			// Token: 0x0400020A RID: 522
			private float elapsed;

			// Token: 0x0400020B RID: 523
			private int initialFrame;

			// Token: 0x0400020C RID: 524
			private CancellationToken cancellationToken;

			// Token: 0x0400020D RID: 525
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400020E RID: 526
			private bool cancelImmediately;

			// Token: 0x0400020F RID: 527
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200016D RID: 365
		private sealed class DelayRealtimePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayRealtimePromise>
		{
			// Token: 0x17000063 RID: 99
			// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00012523 File Offset: 0x00010723
			public ref UniTask.DelayRealtimePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x0001252B File Offset: 0x0001072B
			static DelayRealtimePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayRealtimePromise), () => UniTask.DelayRealtimePromise.pool.Size);
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x0001254C File Offset: 0x0001074C
			private DelayRealtimePromise()
			{
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x00012554 File Offset: 0x00010754
			public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayRealtimePromise delayRealtimePromise;
				if (!UniTask.DelayRealtimePromise.pool.TryPop(out delayRealtimePromise))
				{
					delayRealtimePromise = new UniTask.DelayRealtimePromise();
				}
				delayRealtimePromise.stopwatch = ValueStopwatch.StartNew();
				delayRealtimePromise.delayTimeSpanTicks = delayTimeSpan.Ticks;
				delayRealtimePromise.cancellationToken = cancellationToken;
				delayRealtimePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					delayRealtimePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayRealtimePromise delayRealtimePromise2 = (UniTask.DelayRealtimePromise)state;
						delayRealtimePromise2.core.TrySetCanceled(delayRealtimePromise2.cancellationToken);
					}, delayRealtimePromise);
				}
				PlayerLoopHelper.AddAction(timing, delayRealtimePromise);
				token = delayRealtimePromise.core.Version;
				return delayRealtimePromise;
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x000125FC File Offset: 0x000107FC
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x00012648 File Offset: 0x00010848
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x00012656 File Offset: 0x00010856
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x00012663 File Offset: 0x00010863
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060007CC RID: 1996 RVA: 0x00012674 File Offset: 0x00010874
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.stopwatch.IsInvalid)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				if (this.stopwatch.ElapsedTicks >= this.delayTimeSpanTicks)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				return true;
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x000126EC File Offset: 0x000108EC
			private bool TryReturn()
			{
				this.core.Reset();
				this.stopwatch = default(ValueStopwatch);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayRealtimePromise.pool.TryPush(this);
			}

			// Token: 0x04000210 RID: 528
			private static TaskPool<UniTask.DelayRealtimePromise> pool;

			// Token: 0x04000211 RID: 529
			private UniTask.DelayRealtimePromise nextNode;

			// Token: 0x04000212 RID: 530
			private long delayTimeSpanTicks;

			// Token: 0x04000213 RID: 531
			private ValueStopwatch stopwatch;

			// Token: 0x04000214 RID: 532
			private CancellationToken cancellationToken;

			// Token: 0x04000215 RID: 533
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000216 RID: 534
			private bool cancelImmediately;

			// Token: 0x04000217 RID: 535
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x0200016E RID: 366
		private static class CanceledUniTaskCache<T>
		{
			// Token: 0x04000218 RID: 536
			public static readonly UniTask<T> Task = new UniTask<T>(new UniTask.CanceledResultSource<T>(CancellationToken.None), 0);
		}

		// Token: 0x0200016F RID: 367
		private sealed class ExceptionResultSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x060007CF RID: 1999 RVA: 0x00012750 File Offset: 0x00010950
			public ExceptionResultSource(Exception exception)
			{
				this.exception = ExceptionDispatchInfo.Capture(exception);
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x00012764 File Offset: 0x00010964
			public void GetResult(short token)
			{
				if (!this.calledGet)
				{
					this.calledGet = true;
					GC.SuppressFinalize(this);
				}
				this.exception.Throw();
			}

			// Token: 0x060007D1 RID: 2001 RVA: 0x00012786 File Offset: 0x00010986
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x00012789 File Offset: 0x00010989
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x0001278C File Offset: 0x0001098C
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x00012798 File Offset: 0x00010998
			~ExceptionResultSource()
			{
				if (!this.calledGet)
				{
					UniTaskScheduler.PublishUnobservedTaskException(this.exception.SourceException);
				}
			}

			// Token: 0x04000219 RID: 537
			private readonly ExceptionDispatchInfo exception;

			// Token: 0x0400021A RID: 538
			private bool calledGet;
		}

		// Token: 0x02000170 RID: 368
		private sealed class ExceptionResultSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060007D5 RID: 2005 RVA: 0x000127D8 File Offset: 0x000109D8
			public ExceptionResultSource(Exception exception)
			{
				this.exception = ExceptionDispatchInfo.Capture(exception);
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x000127EC File Offset: 0x000109EC
			public T GetResult(short token)
			{
				if (!this.calledGet)
				{
					this.calledGet = true;
					GC.SuppressFinalize(this);
				}
				this.exception.Throw();
				return default(T);
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x00012822 File Offset: 0x00010A22
			void IUniTaskSource.GetResult(short token)
			{
				if (!this.calledGet)
				{
					this.calledGet = true;
					GC.SuppressFinalize(this);
				}
				this.exception.Throw();
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x00012844 File Offset: 0x00010A44
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x00012847 File Offset: 0x00010A47
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x060007DA RID: 2010 RVA: 0x0001284A File Offset: 0x00010A4A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00012854 File Offset: 0x00010A54
			~ExceptionResultSource()
			{
				if (!this.calledGet)
				{
					UniTaskScheduler.PublishUnobservedTaskException(this.exception.SourceException);
				}
			}

			// Token: 0x0400021B RID: 539
			private readonly ExceptionDispatchInfo exception;

			// Token: 0x0400021C RID: 540
			private bool calledGet;
		}

		// Token: 0x02000171 RID: 369
		private sealed class CanceledResultSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x060007DC RID: 2012 RVA: 0x00012894 File Offset: 0x00010A94
			public CanceledResultSource(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x000128A3 File Offset: 0x00010AA3
			public void GetResult(short token)
			{
				throw new OperationCanceledException(this.cancellationToken);
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x000128B0 File Offset: 0x00010AB0
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x000128B3 File Offset: 0x00010AB3
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060007E0 RID: 2016 RVA: 0x000128B6 File Offset: 0x00010AB6
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x0400021D RID: 541
			private readonly CancellationToken cancellationToken;
		}

		// Token: 0x02000172 RID: 370
		private sealed class CanceledResultSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060007E1 RID: 2017 RVA: 0x000128BF File Offset: 0x00010ABF
			public CanceledResultSource(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x000128CE File Offset: 0x00010ACE
			public T GetResult(short token)
			{
				throw new OperationCanceledException(this.cancellationToken);
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x000128DB File Offset: 0x00010ADB
			void IUniTaskSource.GetResult(short token)
			{
				throw new OperationCanceledException(this.cancellationToken);
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x000128E8 File Offset: 0x00010AE8
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x000128EB File Offset: 0x00010AEB
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060007E6 RID: 2022 RVA: 0x000128EE File Offset: 0x00010AEE
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x0400021E RID: 542
			private readonly CancellationToken cancellationToken;
		}

		// Token: 0x02000173 RID: 371
		private sealed class DeferPromise : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x000128F7 File Offset: 0x00010AF7
			public DeferPromise(Func<UniTask> factory)
			{
				this.factory = factory;
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x00012906 File Offset: 0x00010B06
			public void GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x00012914 File Offset: 0x00010B14
			public UniTaskStatus GetStatus(short token)
			{
				Func<UniTask> func = Interlocked.Exchange<Func<UniTask>>(ref this.factory, null);
				if (func != null)
				{
					this.task = func();
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x00012959 File Offset: 0x00010B59
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x00012968 File Offset: 0x00010B68
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x0400021F RID: 543
			private Func<UniTask> factory;

			// Token: 0x04000220 RID: 544
			private UniTask task;

			// Token: 0x04000221 RID: 545
			private UniTask.Awaiter awaiter;
		}

		// Token: 0x02000174 RID: 372
		private sealed class DeferPromise<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060007EC RID: 2028 RVA: 0x00012975 File Offset: 0x00010B75
			public DeferPromise(Func<UniTask<T>> factory)
			{
				this.factory = factory;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x00012984 File Offset: 0x00010B84
			public T GetResult(short token)
			{
				return this.awaiter.GetResult();
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00012991 File Offset: 0x00010B91
			void IUniTaskSource.GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x000129A0 File Offset: 0x00010BA0
			public UniTaskStatus GetStatus(short token)
			{
				Func<UniTask<T>> func = Interlocked.Exchange<Func<UniTask<T>>>(ref this.factory, null);
				if (func != null)
				{
					this.task = func();
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x000129E5 File Offset: 0x00010BE5
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x000129F4 File Offset: 0x00010BF4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x04000222 RID: 546
			private Func<UniTask<T>> factory;

			// Token: 0x04000223 RID: 547
			private UniTask<T> task;

			// Token: 0x04000224 RID: 548
			private UniTask<T>.Awaiter awaiter;
		}

		// Token: 0x02000175 RID: 373
		private sealed class DeferPromiseWithState<TState> : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x00012A01 File Offset: 0x00010C01
			public DeferPromiseWithState(TState argument, Func<TState, UniTask> factory)
			{
				this.argument = argument;
				this.factory = factory;
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x00012A17 File Offset: 0x00010C17
			public void GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00012A24 File Offset: 0x00010C24
			public UniTaskStatus GetStatus(short token)
			{
				Func<TState, UniTask> func = Interlocked.Exchange<Func<TState, UniTask>>(ref this.factory, null);
				if (func != null)
				{
					this.task = func(this.argument);
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00012A6F File Offset: 0x00010C6F
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00012A7E File Offset: 0x00010C7E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x04000225 RID: 549
			private Func<TState, UniTask> factory;

			// Token: 0x04000226 RID: 550
			private TState argument;

			// Token: 0x04000227 RID: 551
			private UniTask task;

			// Token: 0x04000228 RID: 552
			private UniTask.Awaiter awaiter;
		}

		// Token: 0x02000176 RID: 374
		private sealed class DeferPromiseWithState<TState, TResult> : IUniTaskSource<TResult>, IUniTaskSource, IValueTaskSource, IValueTaskSource<TResult>
		{
			// Token: 0x060007F7 RID: 2039 RVA: 0x00012A8B File Offset: 0x00010C8B
			public DeferPromiseWithState(TState argument, Func<TState, UniTask<TResult>> factory)
			{
				this.argument = argument;
				this.factory = factory;
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00012AA1 File Offset: 0x00010CA1
			public TResult GetResult(short token)
			{
				return this.awaiter.GetResult();
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x00012AAE File Offset: 0x00010CAE
			void IUniTaskSource.GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x00012ABC File Offset: 0x00010CBC
			public UniTaskStatus GetStatus(short token)
			{
				Func<TState, UniTask<TResult>> func = Interlocked.Exchange<Func<TState, UniTask<TResult>>>(ref this.factory, null);
				if (func != null)
				{
					this.task = func(this.argument);
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x00012B07 File Offset: 0x00010D07
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x00012B16 File Offset: 0x00010D16
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x04000229 RID: 553
			private Func<TState, UniTask<TResult>> factory;

			// Token: 0x0400022A RID: 554
			private TState argument;

			// Token: 0x0400022B RID: 555
			private UniTask<TResult> task;

			// Token: 0x0400022C RID: 556
			private UniTask<TResult>.Awaiter awaiter;
		}

		// Token: 0x02000177 RID: 375
		private sealed class NeverPromise<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060007FD RID: 2045 RVA: 0x00012B23 File Offset: 0x00010D23
			public NeverPromise(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
				if (this.cancellationToken.CanBeCanceled)
				{
					this.cancellationToken.RegisterWithoutCaptureExecutionContext(UniTask.NeverPromise<T>.cancellationCallback, this);
				}
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x00012B54 File Offset: 0x00010D54
			private static void CancellationCallback(object state)
			{
				UniTask.NeverPromise<T> neverPromise = (UniTask.NeverPromise<T>)state;
				neverPromise.core.TrySetCanceled(neverPromise.cancellationToken);
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00012B7A File Offset: 0x00010D7A
			public T GetResult(short token)
			{
				return this.core.GetResult(token);
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00012B88 File Offset: 0x00010D88
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x00012B96 File Offset: 0x00010D96
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x00012BA3 File Offset: 0x00010DA3
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x00012BB3 File Offset: 0x00010DB3
			void IUniTaskSource.GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x0400022D RID: 557
			private static readonly Action<object> cancellationCallback = new Action<object>(UniTask.NeverPromise<T>.CancellationCallback);

			// Token: 0x0400022E RID: 558
			private CancellationToken cancellationToken;

			// Token: 0x0400022F RID: 559
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x02000178 RID: 376
		private sealed class WaitUntilPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilPromise>
		{
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000805 RID: 2053 RVA: 0x00012BD5 File Offset: 0x00010DD5
			public ref UniTask.WaitUntilPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x00012BDD File Offset: 0x00010DDD
			static WaitUntilPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilPromise), () => UniTask.WaitUntilPromise.pool.Size);
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x00012BFE File Offset: 0x00010DFE
			private WaitUntilPromise()
			{
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x00012C08 File Offset: 0x00010E08
			public static IUniTaskSource Create(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilPromise waitUntilPromise;
				if (!UniTask.WaitUntilPromise.pool.TryPop(out waitUntilPromise))
				{
					waitUntilPromise = new UniTask.WaitUntilPromise();
				}
				waitUntilPromise.predicate = predicate;
				waitUntilPromise.cancellationToken = cancellationToken;
				waitUntilPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitUntilPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilPromise waitUntilPromise2 = (UniTask.WaitUntilPromise)state;
						waitUntilPromise2.core.TrySetCanceled(waitUntilPromise2.cancellationToken);
					}, waitUntilPromise);
				}
				PlayerLoopHelper.AddAction(timing, waitUntilPromise);
				token = waitUntilPromise.core.Version;
				return waitUntilPromise;
			}

			// Token: 0x06000809 RID: 2057 RVA: 0x00012CA0 File Offset: 0x00010EA0
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x00012CEC File Offset: 0x00010EEC
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x00012CFA File Offset: 0x00010EFA
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x00012D07 File Offset: 0x00010F07
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x00012D18 File Offset: 0x00010F18
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (!this.predicate())
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x00012D8C File Offset: 0x00010F8C
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilPromise.pool.TryPush(this);
			}

			// Token: 0x04000230 RID: 560
			private static TaskPool<UniTask.WaitUntilPromise> pool;

			// Token: 0x04000231 RID: 561
			private UniTask.WaitUntilPromise nextNode;

			// Token: 0x04000232 RID: 562
			private Func<bool> predicate;

			// Token: 0x04000233 RID: 563
			private CancellationToken cancellationToken;

			// Token: 0x04000234 RID: 564
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000235 RID: 565
			private bool cancelImmediately;

			// Token: 0x04000236 RID: 566
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000179 RID: 377
		private sealed class WaitUntilPromise<T> : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilPromise<T>>
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x0600080F RID: 2063 RVA: 0x00012DC9 File Offset: 0x00010FC9
			public ref UniTask.WaitUntilPromise<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x00012DD1 File Offset: 0x00010FD1
			static WaitUntilPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilPromise<T>), () => UniTask.WaitUntilPromise<T>.pool.Size);
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x00012DF2 File Offset: 0x00010FF2
			private WaitUntilPromise()
			{
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x00012DFC File Offset: 0x00010FFC
			public static IUniTaskSource Create(T argument, Func<T, bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilPromise<T> waitUntilPromise;
				if (!UniTask.WaitUntilPromise<T>.pool.TryPop(out waitUntilPromise))
				{
					waitUntilPromise = new UniTask.WaitUntilPromise<T>();
				}
				waitUntilPromise.predicate = predicate;
				waitUntilPromise.argument = argument;
				waitUntilPromise.cancellationToken = cancellationToken;
				waitUntilPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitUntilPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilPromise<T> waitUntilPromise2 = (UniTask.WaitUntilPromise<T>)state;
						waitUntilPromise2.core.TrySetCanceled(waitUntilPromise2.cancellationToken);
					}, waitUntilPromise);
				}
				PlayerLoopHelper.AddAction(timing, waitUntilPromise);
				token = waitUntilPromise.core.Version;
				return waitUntilPromise;
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x00012E9C File Offset: 0x0001109C
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x00012EE8 File Offset: 0x000110E8
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x00012EF6 File Offset: 0x000110F6
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x00012F03 File Offset: 0x00011103
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x00012F14 File Offset: 0x00011114
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (!this.predicate(this.argument))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x00012F90 File Offset: 0x00011190
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.argument = default(T);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilPromise<T>.pool.TryPush(this);
			}

			// Token: 0x04000237 RID: 567
			private static TaskPool<UniTask.WaitUntilPromise<T>> pool;

			// Token: 0x04000238 RID: 568
			private UniTask.WaitUntilPromise<T> nextNode;

			// Token: 0x04000239 RID: 569
			private Func<T, bool> predicate;

			// Token: 0x0400023A RID: 570
			private T argument;

			// Token: 0x0400023B RID: 571
			private CancellationToken cancellationToken;

			// Token: 0x0400023C RID: 572
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400023D RID: 573
			private bool cancelImmediately;

			// Token: 0x0400023E RID: 574
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200017A RID: 378
		private sealed class WaitWhilePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitWhilePromise>
		{
			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000819 RID: 2073 RVA: 0x00012FE4 File Offset: 0x000111E4
			public ref UniTask.WaitWhilePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x00012FEC File Offset: 0x000111EC
			static WaitWhilePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitWhilePromise), () => UniTask.WaitWhilePromise.pool.Size);
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x0001300D File Offset: 0x0001120D
			private WaitWhilePromise()
			{
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x00013018 File Offset: 0x00011218
			public static IUniTaskSource Create(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitWhilePromise waitWhilePromise;
				if (!UniTask.WaitWhilePromise.pool.TryPop(out waitWhilePromise))
				{
					waitWhilePromise = new UniTask.WaitWhilePromise();
				}
				waitWhilePromise.predicate = predicate;
				waitWhilePromise.cancellationToken = cancellationToken;
				waitWhilePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitWhilePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitWhilePromise waitWhilePromise2 = (UniTask.WaitWhilePromise)state;
						waitWhilePromise2.core.TrySetCanceled(waitWhilePromise2.cancellationToken);
					}, waitWhilePromise);
				}
				PlayerLoopHelper.AddAction(timing, waitWhilePromise);
				token = waitWhilePromise.core.Version;
				return waitWhilePromise;
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x000130B0 File Offset: 0x000112B0
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x000130FC File Offset: 0x000112FC
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x0001310A File Offset: 0x0001130A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x00013117 File Offset: 0x00011317
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x00013128 File Offset: 0x00011328
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (this.predicate())
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x0001319C File Offset: 0x0001139C
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitWhilePromise.pool.TryPush(this);
			}

			// Token: 0x0400023F RID: 575
			private static TaskPool<UniTask.WaitWhilePromise> pool;

			// Token: 0x04000240 RID: 576
			private UniTask.WaitWhilePromise nextNode;

			// Token: 0x04000241 RID: 577
			private Func<bool> predicate;

			// Token: 0x04000242 RID: 578
			private CancellationToken cancellationToken;

			// Token: 0x04000243 RID: 579
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000244 RID: 580
			private bool cancelImmediately;

			// Token: 0x04000245 RID: 581
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200017B RID: 379
		private sealed class WaitWhilePromise<T> : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitWhilePromise<T>>
		{
			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000823 RID: 2083 RVA: 0x000131D9 File Offset: 0x000113D9
			public ref UniTask.WaitWhilePromise<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x000131E1 File Offset: 0x000113E1
			static WaitWhilePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitWhilePromise<T>), () => UniTask.WaitWhilePromise<T>.pool.Size);
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x00013202 File Offset: 0x00011402
			private WaitWhilePromise()
			{
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x0001320C File Offset: 0x0001140C
			public static IUniTaskSource Create(T argument, Func<T, bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitWhilePromise<T> waitWhilePromise;
				if (!UniTask.WaitWhilePromise<T>.pool.TryPop(out waitWhilePromise))
				{
					waitWhilePromise = new UniTask.WaitWhilePromise<T>();
				}
				waitWhilePromise.predicate = predicate;
				waitWhilePromise.argument = argument;
				waitWhilePromise.cancellationToken = cancellationToken;
				waitWhilePromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitWhilePromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitWhilePromise<T> waitWhilePromise2 = (UniTask.WaitWhilePromise<T>)state;
						waitWhilePromise2.core.TrySetCanceled(waitWhilePromise2.cancellationToken);
					}, waitWhilePromise);
				}
				PlayerLoopHelper.AddAction(timing, waitWhilePromise);
				token = waitWhilePromise.core.Version;
				return waitWhilePromise;
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x000132AC File Offset: 0x000114AC
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x000132F8 File Offset: 0x000114F8
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000829 RID: 2089 RVA: 0x00013306 File Offset: 0x00011506
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x00013313 File Offset: 0x00011513
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x00013324 File Offset: 0x00011524
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (this.predicate(this.argument))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x000133A0 File Offset: 0x000115A0
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.argument = default(T);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitWhilePromise<T>.pool.TryPush(this);
			}

			// Token: 0x04000246 RID: 582
			private static TaskPool<UniTask.WaitWhilePromise<T>> pool;

			// Token: 0x04000247 RID: 583
			private UniTask.WaitWhilePromise<T> nextNode;

			// Token: 0x04000248 RID: 584
			private Func<T, bool> predicate;

			// Token: 0x04000249 RID: 585
			private T argument;

			// Token: 0x0400024A RID: 586
			private CancellationToken cancellationToken;

			// Token: 0x0400024B RID: 587
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400024C RID: 588
			private bool cancelImmediately;

			// Token: 0x0400024D RID: 589
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200017C RID: 380
		private sealed class WaitUntilCanceledPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilCanceledPromise>
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x000133F4 File Offset: 0x000115F4
			public ref UniTask.WaitUntilCanceledPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x000133FC File Offset: 0x000115FC
			static WaitUntilCanceledPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilCanceledPromise), () => UniTask.WaitUntilCanceledPromise.pool.Size);
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x0001341D File Offset: 0x0001161D
			private WaitUntilCanceledPromise()
			{
			}

			// Token: 0x06000830 RID: 2096 RVA: 0x00013428 File Offset: 0x00011628
			public static IUniTaskSource Create(CancellationToken cancellationToken, PlayerLoopTiming timing, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilCanceledPromise waitUntilCanceledPromise;
				if (!UniTask.WaitUntilCanceledPromise.pool.TryPop(out waitUntilCanceledPromise))
				{
					waitUntilCanceledPromise = new UniTask.WaitUntilCanceledPromise();
				}
				waitUntilCanceledPromise.cancellationToken = cancellationToken;
				waitUntilCanceledPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitUntilCanceledPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						((UniTask.WaitUntilCanceledPromise)state).core.TrySetResult(null);
					}, waitUntilCanceledPromise);
				}
				PlayerLoopHelper.AddAction(timing, waitUntilCanceledPromise);
				token = waitUntilCanceledPromise.core.Version;
				return waitUntilCanceledPromise;
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x000134B8 File Offset: 0x000116B8
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x00013504 File Offset: 0x00011704
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000833 RID: 2099 RVA: 0x00013512 File Offset: 0x00011712
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000834 RID: 2100 RVA: 0x0001351F File Offset: 0x0001171F
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x0001352F File Offset: 0x0001172F
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetResult(null);
					return false;
				}
				return true;
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x0001354E File Offset: 0x0001174E
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilCanceledPromise.pool.TryPush(this);
			}

			// Token: 0x0400024E RID: 590
			private static TaskPool<UniTask.WaitUntilCanceledPromise> pool;

			// Token: 0x0400024F RID: 591
			private UniTask.WaitUntilCanceledPromise nextNode;

			// Token: 0x04000250 RID: 592
			private CancellationToken cancellationToken;

			// Token: 0x04000251 RID: 593
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000252 RID: 594
			private bool cancelImmediately;

			// Token: 0x04000253 RID: 595
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200017D RID: 381
		private sealed class WaitUntilValueChangedUnityObjectPromise<T, U> : IUniTaskSource<U>, IUniTaskSource, IValueTaskSource, IValueTaskSource<U>, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>>
		{
			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000837 RID: 2103 RVA: 0x00013584 File Offset: 0x00011784
			public ref UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x0001358C File Offset: 0x0001178C
			static WaitUntilValueChangedUnityObjectPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>), () => UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.pool.Size);
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x000135AD File Offset: 0x000117AD
			private WaitUntilValueChangedUnityObjectPromise()
			{
			}

			// Token: 0x0600083A RID: 2106 RVA: 0x000135B8 File Offset: 0x000117B8
			public static IUniTaskSource<U> Create(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<U>.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> waitUntilValueChangedUnityObjectPromise;
				if (!UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.pool.TryPop(out waitUntilValueChangedUnityObjectPromise))
				{
					waitUntilValueChangedUnityObjectPromise = new UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>();
				}
				waitUntilValueChangedUnityObjectPromise.target = target;
				waitUntilValueChangedUnityObjectPromise.targetAsUnityObject = target as global::UnityEngine.Object;
				waitUntilValueChangedUnityObjectPromise.monitorFunction = monitorFunction;
				waitUntilValueChangedUnityObjectPromise.currentValue = monitorFunction(target);
				waitUntilValueChangedUnityObjectPromise.equalityComparer = equalityComparer ?? UnityEqualityComparer.GetDefault<U>();
				waitUntilValueChangedUnityObjectPromise.cancellationToken = cancellationToken;
				waitUntilValueChangedUnityObjectPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitUntilValueChangedUnityObjectPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> waitUntilValueChangedUnityObjectPromise2 = (UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>)state;
						waitUntilValueChangedUnityObjectPromise2.core.TrySetCanceled(waitUntilValueChangedUnityObjectPromise2.cancellationToken);
					}, waitUntilValueChangedUnityObjectPromise);
				}
				PlayerLoopHelper.AddAction(timing, waitUntilValueChangedUnityObjectPromise);
				token = waitUntilValueChangedUnityObjectPromise.core.Version;
				return waitUntilValueChangedUnityObjectPromise;
			}

			// Token: 0x0600083B RID: 2107 RVA: 0x00013688 File Offset: 0x00011888
			public U GetResult(short token)
			{
				U result;
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

			// Token: 0x0600083C RID: 2108 RVA: 0x000136D4 File Offset: 0x000118D4
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600083D RID: 2109 RVA: 0x000136DE File Offset: 0x000118DE
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600083E RID: 2110 RVA: 0x000136EC File Offset: 0x000118EC
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600083F RID: 2111 RVA: 0x000136F9 File Offset: 0x000118F9
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000840 RID: 2112 RVA: 0x0001370C File Offset: 0x0001190C
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested || this.targetAsUnityObject == null)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				U u = default(U);
				try
				{
					u = this.monitorFunction(this.target);
					if (this.equalityComparer.Equals(this.currentValue, u))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(u);
				return false;
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x000137B0 File Offset: 0x000119B0
			private bool TryReturn()
			{
				this.core.Reset();
				this.target = default(T);
				this.currentValue = default(U);
				this.monitorFunction = null;
				this.equalityComparer = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.pool.TryPush(this);
			}

			// Token: 0x04000254 RID: 596
			private static TaskPool<UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>> pool;

			// Token: 0x04000255 RID: 597
			private UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> nextNode;

			// Token: 0x04000256 RID: 598
			private T target;

			// Token: 0x04000257 RID: 599
			private global::UnityEngine.Object targetAsUnityObject;

			// Token: 0x04000258 RID: 600
			private U currentValue;

			// Token: 0x04000259 RID: 601
			private Func<T, U> monitorFunction;

			// Token: 0x0400025A RID: 602
			private IEqualityComparer<U> equalityComparer;

			// Token: 0x0400025B RID: 603
			private CancellationToken cancellationToken;

			// Token: 0x0400025C RID: 604
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400025D RID: 605
			private bool cancelImmediately;

			// Token: 0x0400025E RID: 606
			private UniTaskCompletionSourceCore<U> core;
		}

		// Token: 0x0200017E RID: 382
		private sealed class WaitUntilValueChangedStandardObjectPromise<T, U> : IUniTaskSource<U>, IUniTaskSource, IValueTaskSource, IValueTaskSource<U>, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>> where T : class
		{
			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000842 RID: 2114 RVA: 0x00013817 File Offset: 0x00011A17
			public ref UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0001381F File Offset: 0x00011A1F
			static WaitUntilValueChangedStandardObjectPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>), () => UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.pool.Size);
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x00013840 File Offset: 0x00011A40
			private WaitUntilValueChangedStandardObjectPromise()
			{
			}

			// Token: 0x06000845 RID: 2117 RVA: 0x00013848 File Offset: 0x00011A48
			public static IUniTaskSource<U> Create(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<U>.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> waitUntilValueChangedStandardObjectPromise;
				if (!UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.pool.TryPop(out waitUntilValueChangedStandardObjectPromise))
				{
					waitUntilValueChangedStandardObjectPromise = new UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>();
				}
				waitUntilValueChangedStandardObjectPromise.target = new WeakReference<T>(target, false);
				waitUntilValueChangedStandardObjectPromise.monitorFunction = monitorFunction;
				waitUntilValueChangedStandardObjectPromise.currentValue = monitorFunction(target);
				waitUntilValueChangedStandardObjectPromise.equalityComparer = equalityComparer ?? UnityEqualityComparer.GetDefault<U>();
				waitUntilValueChangedStandardObjectPromise.cancellationToken = cancellationToken;
				waitUntilValueChangedStandardObjectPromise.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					waitUntilValueChangedStandardObjectPromise.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> waitUntilValueChangedStandardObjectPromise2 = (UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>)state;
						waitUntilValueChangedStandardObjectPromise2.core.TrySetCanceled(waitUntilValueChangedStandardObjectPromise2.cancellationToken);
					}, waitUntilValueChangedStandardObjectPromise);
				}
				PlayerLoopHelper.AddAction(timing, waitUntilValueChangedStandardObjectPromise);
				token = waitUntilValueChangedStandardObjectPromise.core.Version;
				return waitUntilValueChangedStandardObjectPromise;
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x00013910 File Offset: 0x00011B10
			public U GetResult(short token)
			{
				U result;
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

			// Token: 0x06000847 RID: 2119 RVA: 0x0001395C File Offset: 0x00011B5C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x00013966 File Offset: 0x00011B66
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x00013974 File Offset: 0x00011B74
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x00013981 File Offset: 0x00011B81
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x00013994 File Offset: 0x00011B94
			public bool MoveNext()
			{
				T t;
				if (this.cancellationToken.IsCancellationRequested || !this.target.TryGetTarget(out t))
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				U u = default(U);
				try
				{
					u = this.monitorFunction(t);
					if (this.equalityComparer.Equals(this.currentValue, u))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(u);
				return false;
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x00013A34 File Offset: 0x00011C34
			private bool TryReturn()
			{
				this.core.Reset();
				this.target = null;
				this.currentValue = default(U);
				this.monitorFunction = null;
				this.equalityComparer = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.pool.TryPush(this);
			}

			// Token: 0x0400025F RID: 607
			private static TaskPool<UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>> pool;

			// Token: 0x04000260 RID: 608
			private UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> nextNode;

			// Token: 0x04000261 RID: 609
			private WeakReference<T> target;

			// Token: 0x04000262 RID: 610
			private U currentValue;

			// Token: 0x04000263 RID: 611
			private Func<T, U> monitorFunction;

			// Token: 0x04000264 RID: 612
			private IEqualityComparer<U> equalityComparer;

			// Token: 0x04000265 RID: 613
			private CancellationToken cancellationToken;

			// Token: 0x04000266 RID: 614
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000267 RID: 615
			private bool cancelImmediately;

			// Token: 0x04000268 RID: 616
			private UniTaskCompletionSourceCore<U> core;
		}

		// Token: 0x0200017F RID: 383
		private sealed class WhenAllPromise<T1, T2> : IUniTaskSource<ValueTuple<T1, T2>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2>>
		{
			// Token: 0x0600084D RID: 2125 RVA: 0x00013A98 File Offset: 0x00011C98
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT2(this, in awaiter2);
					return;
				}
				awaiter2.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T2>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2>, UniTask<T2>.Awaiter>(this, awaiter2));
			}

			// Token: 0x0600084E RID: 2126 RVA: 0x00013B44 File Offset: 0x00011D44
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 2)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2>(self.t1, self.t2));
				}
			}

			// Token: 0x0600084F RID: 2127 RVA: 0x00013BAC File Offset: 0x00011DAC
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 2)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2>(self.t1, self.t2));
				}
			}

			// Token: 0x06000850 RID: 2128 RVA: 0x00013C14 File Offset: 0x00011E14
			public ValueTuple<T1, T2> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x00013C28 File Offset: 0x00011E28
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x00013C32 File Offset: 0x00011E32
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x00013C40 File Offset: 0x00011E40
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00013C4D File Offset: 0x00011E4D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000269 RID: 617
			private T1 t1;

			// Token: 0x0400026A RID: 618
			private T2 t2;

			// Token: 0x0400026B RID: 619
			private int completedCount;

			// Token: 0x0400026C RID: 620
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2>> core;
		}

		// Token: 0x02000180 RID: 384
		private sealed class WhenAllPromise<T1, T2, T3> : IUniTaskSource<ValueTuple<T1, T2, T3>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3>>
		{
			// Token: 0x06000855 RID: 2133 RVA: 0x00013C60 File Offset: 0x00011E60
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT3(this, in awaiter3);
					return;
				}
				awaiter3.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T3>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T3>.Awaiter>(this, awaiter3));
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x00013D54 File Offset: 0x00011F54
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 3)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3>(self.t1, self.t2, self.t3));
				}
			}

			// Token: 0x06000857 RID: 2135 RVA: 0x00013DC0 File Offset: 0x00011FC0
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 3)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3>(self.t1, self.t2, self.t3));
				}
			}

			// Token: 0x06000858 RID: 2136 RVA: 0x00013E2C File Offset: 0x0001202C
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 3)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3>(self.t1, self.t2, self.t3));
				}
			}

			// Token: 0x06000859 RID: 2137 RVA: 0x00013E98 File Offset: 0x00012098
			public ValueTuple<T1, T2, T3> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x00013EAC File Offset: 0x000120AC
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x00013EB6 File Offset: 0x000120B6
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x00013EC4 File Offset: 0x000120C4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x00013ED1 File Offset: 0x000120D1
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400026D RID: 621
			private T1 t1;

			// Token: 0x0400026E RID: 622
			private T2 t2;

			// Token: 0x0400026F RID: 623
			private T3 t3;

			// Token: 0x04000270 RID: 624
			private int completedCount;

			// Token: 0x04000271 RID: 625
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3>> core;
		}

		// Token: 0x02000181 RID: 385
		private sealed class WhenAllPromise<T1, T2, T3, T4> : IUniTaskSource<ValueTuple<T1, T2, T3, T4>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4>>
		{
			// Token: 0x0600085E RID: 2142 RVA: 0x00013EE4 File Offset: 0x000120E4
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(this, in awaiter4);
					return;
				}
				awaiter4.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>(this, awaiter4));
			}

			// Token: 0x0600085F RID: 2143 RVA: 0x00014020 File Offset: 0x00012220
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x00014094 File Offset: 0x00012294
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x00014108 File Offset: 0x00012308
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000862 RID: 2146 RVA: 0x0001417C File Offset: 0x0001237C
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000863 RID: 2147 RVA: 0x000141F0 File Offset: 0x000123F0
			public ValueTuple<T1, T2, T3, T4> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x00014204 File Offset: 0x00012404
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x0001420E File Offset: 0x0001240E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000866 RID: 2150 RVA: 0x0001421C File Offset: 0x0001241C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x00014229 File Offset: 0x00012429
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000272 RID: 626
			private T1 t1;

			// Token: 0x04000273 RID: 627
			private T2 t2;

			// Token: 0x04000274 RID: 628
			private T3 t3;

			// Token: 0x04000275 RID: 629
			private T4 t4;

			// Token: 0x04000276 RID: 630
			private int completedCount;

			// Token: 0x04000277 RID: 631
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4>> core;
		}

		// Token: 0x02000182 RID: 386
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5>>
		{
			// Token: 0x06000868 RID: 2152 RVA: 0x0001423C File Offset: 0x0001243C
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(this, in awaiter5);
					return;
				}
				awaiter5.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>(this, awaiter5));
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x000143C0 File Offset: 0x000125C0
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x00014438 File Offset: 0x00012638
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x000144B0 File Offset: 0x000126B0
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x00014528 File Offset: 0x00012728
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x000145A0 File Offset: 0x000127A0
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x00014618 File Offset: 0x00012818
			public ValueTuple<T1, T2, T3, T4, T5> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600086F RID: 2159 RVA: 0x0001462C File Offset: 0x0001282C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x00014636 File Offset: 0x00012836
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x00014644 File Offset: 0x00012844
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x00014651 File Offset: 0x00012851
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000278 RID: 632
			private T1 t1;

			// Token: 0x04000279 RID: 633
			private T2 t2;

			// Token: 0x0400027A RID: 634
			private T3 t3;

			// Token: 0x0400027B RID: 635
			private T4 t4;

			// Token: 0x0400027C RID: 636
			private T5 t5;

			// Token: 0x0400027D RID: 637
			private int completedCount;

			// Token: 0x0400027E RID: 638
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5>> core;
		}

		// Token: 0x02000183 RID: 387
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6>>
		{
			// Token: 0x06000873 RID: 2163 RVA: 0x00014664 File Offset: 0x00012864
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(this, in awaiter6);
					return;
				}
				awaiter6.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>(this, awaiter6));
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x00014834 File Offset: 0x00012A34
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000875 RID: 2165 RVA: 0x000148B4 File Offset: 0x00012AB4
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x00014934 File Offset: 0x00012B34
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x000149B4 File Offset: 0x00012BB4
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x00014A34 File Offset: 0x00012C34
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x00014AB4 File Offset: 0x00012CB4
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x00014B34 File Offset: 0x00012D34
			public ValueTuple<T1, T2, T3, T4, T5, T6> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600087B RID: 2171 RVA: 0x00014B48 File Offset: 0x00012D48
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x00014B52 File Offset: 0x00012D52
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600087D RID: 2173 RVA: 0x00014B60 File Offset: 0x00012D60
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x00014B6D File Offset: 0x00012D6D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400027F RID: 639
			private T1 t1;

			// Token: 0x04000280 RID: 640
			private T2 t2;

			// Token: 0x04000281 RID: 641
			private T3 t3;

			// Token: 0x04000282 RID: 642
			private T4 t4;

			// Token: 0x04000283 RID: 643
			private T5 t5;

			// Token: 0x04000284 RID: 644
			private T6 t6;

			// Token: 0x04000285 RID: 645
			private int completedCount;

			// Token: 0x04000286 RID: 646
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6>> core;
		}

		// Token: 0x02000184 RID: 388
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
		{
			// Token: 0x0600087F RID: 2175 RVA: 0x00014B80 File Offset: 0x00012D80
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(this, in awaiter7);
					return;
				}
				awaiter7.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>(this, awaiter7));
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x00014D98 File Offset: 0x00012F98
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x00014E1C File Offset: 0x0001301C
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000882 RID: 2178 RVA: 0x00014EA0 File Offset: 0x000130A0
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000883 RID: 2179 RVA: 0x00014F24 File Offset: 0x00013124
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x00014FA8 File Offset: 0x000131A8
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x0001502C File Offset: 0x0001322C
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x000150B0 File Offset: 0x000132B0
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x00015134 File Offset: 0x00013334
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x00015148 File Offset: 0x00013348
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x00015152 File Offset: 0x00013352
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x00015160 File Offset: 0x00013360
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x0001516D File Offset: 0x0001336D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000287 RID: 647
			private T1 t1;

			// Token: 0x04000288 RID: 648
			private T2 t2;

			// Token: 0x04000289 RID: 649
			private T3 t3;

			// Token: 0x0400028A RID: 650
			private T4 t4;

			// Token: 0x0400028B RID: 651
			private T5 t5;

			// Token: 0x0400028C RID: 652
			private T6 t6;

			// Token: 0x0400028D RID: 653
			private T7 t7;

			// Token: 0x0400028E RID: 654
			private int completedCount;

			// Token: 0x0400028F RID: 655
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7>> core;
		}

		// Token: 0x02000185 RID: 389
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>
		{
			// Token: 0x0600088C RID: 2188 RVA: 0x00015180 File Offset: 0x00013380
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(this, in awaiter8);
					return;
				}
				awaiter8.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>(this, awaiter8));
			}

			// Token: 0x0600088D RID: 2189 RVA: 0x000153E4 File Offset: 0x000135E4
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x00015474 File Offset: 0x00013674
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x00015504 File Offset: 0x00013704
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x00015594 File Offset: 0x00013794
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x00015624 File Offset: 0x00013824
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x000156B4 File Offset: 0x000138B4
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x00015744 File Offset: 0x00013944
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x000157D4 File Offset: 0x000139D4
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x00015864 File Offset: 0x00013A64
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x00015878 File Offset: 0x00013A78
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x00015882 File Offset: 0x00013A82
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x00015890 File Offset: 0x00013A90
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x0001589D File Offset: 0x00013A9D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000290 RID: 656
			private T1 t1;

			// Token: 0x04000291 RID: 657
			private T2 t2;

			// Token: 0x04000292 RID: 658
			private T3 t3;

			// Token: 0x04000293 RID: 659
			private T4 t4;

			// Token: 0x04000294 RID: 660
			private T5 t5;

			// Token: 0x04000295 RID: 661
			private T6 t6;

			// Token: 0x04000296 RID: 662
			private T7 t7;

			// Token: 0x04000297 RID: 663
			private T8 t8;

			// Token: 0x04000298 RID: 664
			private int completedCount;

			// Token: 0x04000299 RID: 665
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>> core;
		}

		// Token: 0x02000186 RID: 390
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>
		{
			// Token: 0x0600089A RID: 2202 RVA: 0x000158B0 File Offset: 0x00013AB0
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(this, in awaiter9);
					return;
				}
				awaiter9.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>(this, awaiter9));
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x00015B5C File Offset: 0x00013D5C
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x00015BF4 File Offset: 0x00013DF4
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x00015C8C File Offset: 0x00013E8C
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x00015D24 File Offset: 0x00013F24
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x00015DBC File Offset: 0x00013FBC
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x00015E54 File Offset: 0x00014054
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x00015EEC File Offset: 0x000140EC
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x00015F84 File Offset: 0x00014184
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x0001601C File Offset: 0x0001421C
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x000160B4 File Offset: 0x000142B4
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x000160C8 File Offset: 0x000142C8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x000160D2 File Offset: 0x000142D2
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x000160E0 File Offset: 0x000142E0
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x000160ED File Offset: 0x000142ED
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400029A RID: 666
			private T1 t1;

			// Token: 0x0400029B RID: 667
			private T2 t2;

			// Token: 0x0400029C RID: 668
			private T3 t3;

			// Token: 0x0400029D RID: 669
			private T4 t4;

			// Token: 0x0400029E RID: 670
			private T5 t5;

			// Token: 0x0400029F RID: 671
			private T6 t6;

			// Token: 0x040002A0 RID: 672
			private T7 t7;

			// Token: 0x040002A1 RID: 673
			private T8 t8;

			// Token: 0x040002A2 RID: 674
			private T9 t9;

			// Token: 0x040002A3 RID: 675
			private int completedCount;

			// Token: 0x040002A4 RID: 676
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>> core;
		}

		// Token: 0x02000187 RID: 391
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>
		{
			// Token: 0x060008A9 RID: 2217 RVA: 0x00016100 File Offset: 0x00014300
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(this, in awaiter10);
					return;
				}
				awaiter10.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>(this, awaiter10));
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x000163F8 File Offset: 0x000145F8
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x00016494 File Offset: 0x00014694
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x00016530 File Offset: 0x00014730
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x000165CC File Offset: 0x000147CC
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x00016668 File Offset: 0x00014868
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00016704 File Offset: 0x00014904
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x000167A0 File Offset: 0x000149A0
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x0001683C File Offset: 0x00014A3C
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x000168D8 File Offset: 0x00014AD8
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00016974 File Offset: 0x00014B74
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x00016A10 File Offset: 0x00014C10
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x00016A24 File Offset: 0x00014C24
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008B6 RID: 2230 RVA: 0x00016A2E File Offset: 0x00014C2E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x00016A3C File Offset: 0x00014C3C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x00016A49 File Offset: 0x00014C49
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002A5 RID: 677
			private T1 t1;

			// Token: 0x040002A6 RID: 678
			private T2 t2;

			// Token: 0x040002A7 RID: 679
			private T3 t3;

			// Token: 0x040002A8 RID: 680
			private T4 t4;

			// Token: 0x040002A9 RID: 681
			private T5 t5;

			// Token: 0x040002AA RID: 682
			private T6 t6;

			// Token: 0x040002AB RID: 683
			private T7 t7;

			// Token: 0x040002AC RID: 684
			private T8 t8;

			// Token: 0x040002AD RID: 685
			private T9 t9;

			// Token: 0x040002AE RID: 686
			private T10 t10;

			// Token: 0x040002AF RID: 687
			private int completedCount;

			// Token: 0x040002B0 RID: 688
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>> core;
		}

		// Token: 0x02000188 RID: 392
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>
		{
			// Token: 0x060008B9 RID: 2233 RVA: 0x00016A5C File Offset: 0x00014C5C
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(this, in awaiter11);
					return;
				}
				awaiter11.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>(this, awaiter11));
			}

			// Token: 0x060008BA RID: 2234 RVA: 0x00016D9C File Offset: 0x00014F9C
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008BB RID: 2235 RVA: 0x00016E40 File Offset: 0x00015040
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008BC RID: 2236 RVA: 0x00016EE4 File Offset: 0x000150E4
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008BD RID: 2237 RVA: 0x00016F88 File Offset: 0x00015188
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x0001702C File Offset: 0x0001522C
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x000170D0 File Offset: 0x000152D0
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x00017174 File Offset: 0x00015374
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x00017218 File Offset: 0x00015418
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x000172BC File Offset: 0x000154BC
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x00017360 File Offset: 0x00015560
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008C4 RID: 2244 RVA: 0x00017404 File Offset: 0x00015604
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x000174A8 File Offset: 0x000156A8
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060008C6 RID: 2246 RVA: 0x000174BC File Offset: 0x000156BC
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x000174C6 File Offset: 0x000156C6
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x000174D4 File Offset: 0x000156D4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x000174E1 File Offset: 0x000156E1
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002B1 RID: 689
			private T1 t1;

			// Token: 0x040002B2 RID: 690
			private T2 t2;

			// Token: 0x040002B3 RID: 691
			private T3 t3;

			// Token: 0x040002B4 RID: 692
			private T4 t4;

			// Token: 0x040002B5 RID: 693
			private T5 t5;

			// Token: 0x040002B6 RID: 694
			private T6 t6;

			// Token: 0x040002B7 RID: 695
			private T7 t7;

			// Token: 0x040002B8 RID: 696
			private T8 t8;

			// Token: 0x040002B9 RID: 697
			private T9 t9;

			// Token: 0x040002BA RID: 698
			private T10 t10;

			// Token: 0x040002BB RID: 699
			private T11 t11;

			// Token: 0x040002BC RID: 700
			private int completedCount;

			// Token: 0x040002BD RID: 701
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>> core;
		}

		// Token: 0x02000189 RID: 393
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>
		{
			// Token: 0x060008CA RID: 2250 RVA: 0x000174F4 File Offset: 0x000156F4
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(this, in awaiter12);
					return;
				}
				awaiter12.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>(this, awaiter12));
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00017880 File Offset: 0x00015A80
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x00017928 File Offset: 0x00015B28
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x000179D0 File Offset: 0x00015BD0
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x00017A78 File Offset: 0x00015C78
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x00017B20 File Offset: 0x00015D20
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x00017BC8 File Offset: 0x00015DC8
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x00017C70 File Offset: 0x00015E70
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x00017D18 File Offset: 0x00015F18
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x00017DC0 File Offset: 0x00015FC0
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x00017E68 File Offset: 0x00016068
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x00017F10 File Offset: 0x00016110
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D6 RID: 2262 RVA: 0x00017FB8 File Offset: 0x000161B8
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060008D7 RID: 2263 RVA: 0x00018060 File Offset: 0x00016260
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060008D8 RID: 2264 RVA: 0x00018074 File Offset: 0x00016274
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008D9 RID: 2265 RVA: 0x0001807E File Offset: 0x0001627E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008DA RID: 2266 RVA: 0x0001808C File Offset: 0x0001628C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x00018099 File Offset: 0x00016299
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002BE RID: 702
			private T1 t1;

			// Token: 0x040002BF RID: 703
			private T2 t2;

			// Token: 0x040002C0 RID: 704
			private T3 t3;

			// Token: 0x040002C1 RID: 705
			private T4 t4;

			// Token: 0x040002C2 RID: 706
			private T5 t5;

			// Token: 0x040002C3 RID: 707
			private T6 t6;

			// Token: 0x040002C4 RID: 708
			private T7 t7;

			// Token: 0x040002C5 RID: 709
			private T8 t8;

			// Token: 0x040002C6 RID: 710
			private T9 t9;

			// Token: 0x040002C7 RID: 711
			private T10 t10;

			// Token: 0x040002C8 RID: 712
			private T11 t11;

			// Token: 0x040002C9 RID: 713
			private T12 t12;

			// Token: 0x040002CA RID: 714
			private int completedCount;

			// Token: 0x040002CB RID: 715
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>> core;
		}

		// Token: 0x0200018A RID: 394
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>
		{
			// Token: 0x060008DC RID: 2268 RVA: 0x000180AC File Offset: 0x000162AC
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(this, in awaiter13);
					return;
				}
				awaiter13.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter> stateTuple13 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(stateTuple13.Item1, in stateTuple13.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>(this, awaiter13));
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x00018480 File Offset: 0x00016680
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x00018530 File Offset: 0x00016730
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x000185E0 File Offset: 0x000167E0
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E0 RID: 2272 RVA: 0x00018690 File Offset: 0x00016890
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E1 RID: 2273 RVA: 0x00018740 File Offset: 0x00016940
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x000187F0 File Offset: 0x000169F0
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x000188A0 File Offset: 0x00016AA0
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x00018950 File Offset: 0x00016B50
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x00018A00 File Offset: 0x00016C00
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E6 RID: 2278 RVA: 0x00018AB0 File Offset: 0x00016CB0
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E7 RID: 2279 RVA: 0x00018B60 File Offset: 0x00016D60
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E8 RID: 2280 RVA: 0x00018C10 File Offset: 0x00016E10
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008E9 RID: 2281 RVA: 0x00018CC0 File Offset: 0x00016EC0
			private static void TryInvokeContinuationT13(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T13>.Awaiter awaiter)
			{
				try
				{
					self.t13 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x060008EA RID: 2282 RVA: 0x00018D70 File Offset: 0x00016F70
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060008EB RID: 2283 RVA: 0x00018D84 File Offset: 0x00016F84
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008EC RID: 2284 RVA: 0x00018D8E File Offset: 0x00016F8E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008ED RID: 2285 RVA: 0x00018D9C File Offset: 0x00016F9C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x00018DA9 File Offset: 0x00016FA9
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002CC RID: 716
			private T1 t1;

			// Token: 0x040002CD RID: 717
			private T2 t2;

			// Token: 0x040002CE RID: 718
			private T3 t3;

			// Token: 0x040002CF RID: 719
			private T4 t4;

			// Token: 0x040002D0 RID: 720
			private T5 t5;

			// Token: 0x040002D1 RID: 721
			private T6 t6;

			// Token: 0x040002D2 RID: 722
			private T7 t7;

			// Token: 0x040002D3 RID: 723
			private T8 t8;

			// Token: 0x040002D4 RID: 724
			private T9 t9;

			// Token: 0x040002D5 RID: 725
			private T10 t10;

			// Token: 0x040002D6 RID: 726
			private T11 t11;

			// Token: 0x040002D7 RID: 727
			private T12 t12;

			// Token: 0x040002D8 RID: 728
			private T13 t13;

			// Token: 0x040002D9 RID: 729
			private int completedCount;

			// Token: 0x040002DA RID: 730
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>> core;
		}

		// Token: 0x0200018B RID: 395
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>
		{
			// Token: 0x060008EF RID: 2287 RVA: 0x00018DBC File Offset: 0x00016FBC
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter> stateTuple13 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(stateTuple13.Item1, in stateTuple13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(this, in awaiter14);
					return;
				}
				awaiter14.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter> stateTuple14 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(stateTuple14.Item1, in stateTuple14.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>(this, awaiter14));
			}

			// Token: 0x060008F0 RID: 2288 RVA: 0x000191DC File Offset: 0x000173DC
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F1 RID: 2289 RVA: 0x00019290 File Offset: 0x00017490
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x00019344 File Offset: 0x00017544
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F3 RID: 2291 RVA: 0x000193F8 File Offset: 0x000175F8
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F4 RID: 2292 RVA: 0x000194AC File Offset: 0x000176AC
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F5 RID: 2293 RVA: 0x00019560 File Offset: 0x00017760
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F6 RID: 2294 RVA: 0x00019614 File Offset: 0x00017814
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F7 RID: 2295 RVA: 0x000196C8 File Offset: 0x000178C8
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x0001977C File Offset: 0x0001797C
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008F9 RID: 2297 RVA: 0x00019830 File Offset: 0x00017A30
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x000198E4 File Offset: 0x00017AE4
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x00019998 File Offset: 0x00017B98
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008FC RID: 2300 RVA: 0x00019A4C File Offset: 0x00017C4C
			private static void TryInvokeContinuationT13(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T13>.Awaiter awaiter)
			{
				try
				{
					self.t13 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008FD RID: 2301 RVA: 0x00019B00 File Offset: 0x00017D00
			private static void TryInvokeContinuationT14(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T14>.Awaiter awaiter)
			{
				try
				{
					self.t14 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x060008FE RID: 2302 RVA: 0x00019BB4 File Offset: 0x00017DB4
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060008FF RID: 2303 RVA: 0x00019BC8 File Offset: 0x00017DC8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000900 RID: 2304 RVA: 0x00019BD2 File Offset: 0x00017DD2
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000901 RID: 2305 RVA: 0x00019BE0 File Offset: 0x00017DE0
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000902 RID: 2306 RVA: 0x00019BED File Offset: 0x00017DED
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002DB RID: 731
			private T1 t1;

			// Token: 0x040002DC RID: 732
			private T2 t2;

			// Token: 0x040002DD RID: 733
			private T3 t3;

			// Token: 0x040002DE RID: 734
			private T4 t4;

			// Token: 0x040002DF RID: 735
			private T5 t5;

			// Token: 0x040002E0 RID: 736
			private T6 t6;

			// Token: 0x040002E1 RID: 737
			private T7 t7;

			// Token: 0x040002E2 RID: 738
			private T8 t8;

			// Token: 0x040002E3 RID: 739
			private T9 t9;

			// Token: 0x040002E4 RID: 740
			private T10 t10;

			// Token: 0x040002E5 RID: 741
			private T11 t11;

			// Token: 0x040002E6 RID: 742
			private T12 t12;

			// Token: 0x040002E7 RID: 743
			private T13 t13;

			// Token: 0x040002E8 RID: 744
			private T14 t14;

			// Token: 0x040002E9 RID: 745
			private int completedCount;

			// Token: 0x040002EA RID: 746
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>> core;
		}

		// Token: 0x0200018C RID: 396
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>
		{
			// Token: 0x06000903 RID: 2307 RVA: 0x00019C00 File Offset: 0x00017E00
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter> stateTuple13 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(stateTuple13.Item1, in stateTuple13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(this, in awaiter14);
				}
				else
				{
					awaiter14.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter> stateTuple14 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(stateTuple14.Item1, in stateTuple14.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>(this, awaiter14));
				}
				UniTask<T15>.Awaiter awaiter15 = task15.GetAwaiter();
				if (awaiter15.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(this, in awaiter15);
					return;
				}
				awaiter15.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter> stateTuple15 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(stateTuple15.Item1, in stateTuple15.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>(this, awaiter15));
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x0001A068 File Offset: 0x00018268
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000905 RID: 2309 RVA: 0x0001A12C File Offset: 0x0001832C
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x0001A1F0 File Offset: 0x000183F0
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x0001A2B4 File Offset: 0x000184B4
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x0001A378 File Offset: 0x00018578
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x0001A43C File Offset: 0x0001863C
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x0001A500 File Offset: 0x00018700
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x0001A5C4 File Offset: 0x000187C4
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x0001A688 File Offset: 0x00018888
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x0001A74C File Offset: 0x0001894C
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600090E RID: 2318 RVA: 0x0001A810 File Offset: 0x00018A10
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600090F RID: 2319 RVA: 0x0001A8D4 File Offset: 0x00018AD4
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x0001A998 File Offset: 0x00018B98
			private static void TryInvokeContinuationT13(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T13>.Awaiter awaiter)
			{
				try
				{
					self.t13 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000911 RID: 2321 RVA: 0x0001AA5C File Offset: 0x00018C5C
			private static void TryInvokeContinuationT14(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T14>.Awaiter awaiter)
			{
				try
				{
					self.t14 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000912 RID: 2322 RVA: 0x0001AB20 File Offset: 0x00018D20
			private static void TryInvokeContinuationT15(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T15>.Awaiter awaiter)
			{
				try
				{
					self.t15 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000913 RID: 2323 RVA: 0x0001ABE4 File Offset: 0x00018DE4
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000914 RID: 2324 RVA: 0x0001ABF8 File Offset: 0x00018DF8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000915 RID: 2325 RVA: 0x0001AC02 File Offset: 0x00018E02
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000916 RID: 2326 RVA: 0x0001AC10 File Offset: 0x00018E10
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000917 RID: 2327 RVA: 0x0001AC1D File Offset: 0x00018E1D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002EB RID: 747
			private T1 t1;

			// Token: 0x040002EC RID: 748
			private T2 t2;

			// Token: 0x040002ED RID: 749
			private T3 t3;

			// Token: 0x040002EE RID: 750
			private T4 t4;

			// Token: 0x040002EF RID: 751
			private T5 t5;

			// Token: 0x040002F0 RID: 752
			private T6 t6;

			// Token: 0x040002F1 RID: 753
			private T7 t7;

			// Token: 0x040002F2 RID: 754
			private T8 t8;

			// Token: 0x040002F3 RID: 755
			private T9 t9;

			// Token: 0x040002F4 RID: 756
			private T10 t10;

			// Token: 0x040002F5 RID: 757
			private T11 t11;

			// Token: 0x040002F6 RID: 758
			private T12 t12;

			// Token: 0x040002F7 RID: 759
			private T13 t13;

			// Token: 0x040002F8 RID: 760
			private T14 t14;

			// Token: 0x040002F9 RID: 761
			private T15 t15;

			// Token: 0x040002FA RID: 762
			private int completedCount;

			// Token: 0x040002FB RID: 763
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>> core;
		}

		// Token: 0x0200018D RID: 397
		private sealed class WhenAllPromise<T> : IUniTaskSource<T[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T[]>
		{
			// Token: 0x06000918 RID: 2328 RVA: 0x0001AC30 File Offset: 0x00018E30
			public WhenAllPromise(UniTask<T>[] tasks, int tasksLength)
			{
				this.completeCount = 0;
				if (tasksLength == 0)
				{
					this.result = Array.Empty<T>();
					this.core.TrySetResult(this.result);
					return;
				}
				this.result = new T[tasksLength];
				int i = 0;
				while (i < tasksLength)
				{
					UniTask<T>.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_00A0;
					}
					goto IL_005E;
					IL_00A0:
					i++;
					continue;
					IL_005E:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAllPromise<T>.TryInvokeContinuation(this, in awaiter, i);
						goto IL_00A0;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T>, UniTask<T>.Awaiter, int> stateTuple = (StateTuple<UniTask.WhenAllPromise<T>, UniTask<T>.Awaiter, int>)state)
						{
							UniTask.WhenAllPromise<T>.TryInvokeContinuation(stateTuple.Item1, in stateTuple.Item2, stateTuple.Item3);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T>, UniTask<T>.Awaiter, int>(this, awaiter, i));
					goto IL_00A0;
				}
			}

			// Token: 0x06000919 RID: 2329 RVA: 0x0001ACF8 File Offset: 0x00018EF8
			private static void TryInvokeContinuation(UniTask.WhenAllPromise<T> self, in UniTask<T>.Awaiter awaiter, int i)
			{
				try
				{
					self.result[i] = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completeCount) == self.result.Length)
				{
					self.core.TrySetResult(self.result);
				}
			}

			// Token: 0x0600091A RID: 2330 RVA: 0x0001AD60 File Offset: 0x00018F60
			public T[] GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600091B RID: 2331 RVA: 0x0001AD74 File Offset: 0x00018F74
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x0001AD7E File Offset: 0x00018F7E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x0001AD8C File Offset: 0x00018F8C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600091E RID: 2334 RVA: 0x0001AD99 File Offset: 0x00018F99
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002FC RID: 764
			private T[] result;

			// Token: 0x040002FD RID: 765
			private int completeCount;

			// Token: 0x040002FE RID: 766
			private UniTaskCompletionSourceCore<T[]> core;
		}

		// Token: 0x0200018E RID: 398
		private sealed class WhenAllPromise : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x0600091F RID: 2335 RVA: 0x0001ADAC File Offset: 0x00018FAC
			public WhenAllPromise(UniTask[] tasks, int tasksLength)
			{
				this.tasksLength = tasksLength;
				this.completeCount = 0;
				if (tasksLength == 0)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return;
				}
				int i = 0;
				while (i < tasksLength)
				{
					UniTask.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_008D;
					}
					goto IL_004D;
					IL_008D:
					i++;
					continue;
					IL_004D:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAllPromise.TryInvokeContinuation(this, in awaiter);
						goto IL_008D;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise, UniTask.Awaiter> stateTuple = (StateTuple<UniTask.WhenAllPromise, UniTask.Awaiter>)state)
						{
							UniTask.WhenAllPromise.TryInvokeContinuation(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise, UniTask.Awaiter>(this, awaiter));
					goto IL_008D;
				}
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x0001AE60 File Offset: 0x00019060
			private static void TryInvokeContinuation(UniTask.WhenAllPromise self, in UniTask.Awaiter awaiter)
			{
				try
				{
					awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completeCount) == self.tasksLength)
				{
					self.core.TrySetResult(AsyncUnit.Default);
				}
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x0001AEBC File Offset: 0x000190BC
			public void GetResult(short token)
			{
				GC.SuppressFinalize(this);
				this.core.GetResult(token);
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x0001AED1 File Offset: 0x000190D1
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x0001AEDF File Offset: 0x000190DF
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000924 RID: 2340 RVA: 0x0001AEEC File Offset: 0x000190EC
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002FF RID: 767
			private int completeCount;

			// Token: 0x04000300 RID: 768
			private int tasksLength;

			// Token: 0x04000301 RID: 769
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x0200018F RID: 399
		private sealed class WhenAnyPromise<T1, T2> : IUniTaskSource<ValueTuple<int, T1, T2>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2>>
		{
			// Token: 0x06000925 RID: 2341 RVA: 0x0001AEFC File Offset: 0x000190FC
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT2(this, in awaiter2);
					return;
				}
				awaiter2.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter>(this, awaiter2));
			}

			// Token: 0x06000926 RID: 2342 RVA: 0x0001AFA8 File Offset: 0x000191A8
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2>(0, result, default(T2)));
				}
			}

			// Token: 0x06000927 RID: 2343 RVA: 0x0001B008 File Offset: 0x00019208
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2>(1, default(T1), result));
				}
			}

			// Token: 0x06000928 RID: 2344 RVA: 0x0001B068 File Offset: 0x00019268
			[return: TupleElementNames(new string[] { null, "result1", "result2" })]
			public ValueTuple<int, T1, T2> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x0001B07C File Offset: 0x0001927C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x0001B08A File Offset: 0x0001928A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x0001B09A File Offset: 0x0001929A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600092C RID: 2348 RVA: 0x0001B0A7 File Offset: 0x000192A7
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000302 RID: 770
			private int completedCount;

			// Token: 0x04000303 RID: 771
			[TupleElementNames(new string[] { null, "result1", "result2" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2>> core;
		}

		// Token: 0x02000190 RID: 400
		private sealed class WhenAnyPromise<T1, T2, T3> : IUniTaskSource<ValueTuple<int, T1, T2, T3>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3>>
		{
			// Token: 0x0600092D RID: 2349 RVA: 0x0001B0B4 File Offset: 0x000192B4
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT3(this, in awaiter3);
					return;
				}
				awaiter3.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter>(this, awaiter3));
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x0001B1A8 File Offset: 0x000193A8
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3>(0, result, default(T2), default(T3)));
				}
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x0001B214 File Offset: 0x00019414
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3>(1, default(T1), result, default(T3)));
				}
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x0001B280 File Offset: 0x00019480
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3>(2, default(T1), default(T2), result));
				}
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x0001B2EC File Offset: 0x000194EC
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3" })]
			public ValueTuple<int, T1, T2, T3> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x0001B300 File Offset: 0x00019500
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x0001B30E File Offset: 0x0001950E
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000934 RID: 2356 RVA: 0x0001B31E File Offset: 0x0001951E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x0001B32B File Offset: 0x0001952B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000304 RID: 772
			private int completedCount;

			// Token: 0x04000305 RID: 773
			[TupleElementNames(new string[] { null, "result1", "result2", "result3" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3>> core;
		}

		// Token: 0x02000191 RID: 401
		private sealed class WhenAnyPromise<T1, T2, T3, T4> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4>>
		{
			// Token: 0x06000936 RID: 2358 RVA: 0x0001B338 File Offset: 0x00019538
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(this, in awaiter4);
					return;
				}
				awaiter4.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>(this, awaiter4));
			}

			// Token: 0x06000937 RID: 2359 RVA: 0x0001B474 File Offset: 0x00019674
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(0, result, default(T2), default(T3), default(T4)));
				}
			}

			// Token: 0x06000938 RID: 2360 RVA: 0x0001B4E8 File Offset: 0x000196E8
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(1, default(T1), result, default(T3), default(T4)));
				}
			}

			// Token: 0x06000939 RID: 2361 RVA: 0x0001B55C File Offset: 0x0001975C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(2, default(T1), default(T2), result, default(T4)));
				}
			}

			// Token: 0x0600093A RID: 2362 RVA: 0x0001B5D0 File Offset: 0x000197D0
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(3, default(T1), default(T2), default(T3), result));
				}
			}

			// Token: 0x0600093B RID: 2363 RVA: 0x0001B644 File Offset: 0x00019844
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4" })]
			public ValueTuple<int, T1, T2, T3, T4> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600093C RID: 2364 RVA: 0x0001B658 File Offset: 0x00019858
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600093D RID: 2365 RVA: 0x0001B666 File Offset: 0x00019866
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x0001B676 File Offset: 0x00019876
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600093F RID: 2367 RVA: 0x0001B683 File Offset: 0x00019883
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000306 RID: 774
			private int completedCount;

			// Token: 0x04000307 RID: 775
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4>> core;
		}

		// Token: 0x02000192 RID: 402
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5>>
		{
			// Token: 0x06000940 RID: 2368 RVA: 0x0001B690 File Offset: 0x00019890
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(this, in awaiter5);
					return;
				}
				awaiter5.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>(this, awaiter5));
			}

			// Token: 0x06000941 RID: 2369 RVA: 0x0001B814 File Offset: 0x00019A14
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(0, result, default(T2), default(T3), default(T4), default(T5)));
				}
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x0001B894 File Offset: 0x00019A94
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(1, default(T1), result, default(T3), default(T4), default(T5)));
				}
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x0001B914 File Offset: 0x00019B14
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(2, default(T1), default(T2), result, default(T4), default(T5)));
				}
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x0001B994 File Offset: 0x00019B94
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(3, default(T1), default(T2), default(T3), result, default(T5)));
				}
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x0001BA14 File Offset: 0x00019C14
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(4, default(T1), default(T2), default(T3), default(T4), result));
				}
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x0001BA94 File Offset: 0x00019C94
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5" })]
			public ValueTuple<int, T1, T2, T3, T4, T5> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x0001BAA8 File Offset: 0x00019CA8
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000948 RID: 2376 RVA: 0x0001BAB6 File Offset: 0x00019CB6
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000949 RID: 2377 RVA: 0x0001BAC6 File Offset: 0x00019CC6
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0001BAD3 File Offset: 0x00019CD3
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000308 RID: 776
			private int completedCount;

			// Token: 0x04000309 RID: 777
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5>> core;
		}

		// Token: 0x02000193 RID: 403
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6>>
		{
			// Token: 0x0600094B RID: 2379 RVA: 0x0001BAE0 File Offset: 0x00019CE0
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(this, in awaiter6);
					return;
				}
				awaiter6.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>(this, awaiter6));
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0001BCB0 File Offset: 0x00019EB0
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6)));
				}
			}

			// Token: 0x0600094D RID: 2381 RVA: 0x0001BD38 File Offset: 0x00019F38
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6)));
				}
			}

			// Token: 0x0600094E RID: 2382 RVA: 0x0001BDC0 File Offset: 0x00019FC0
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6)));
				}
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0001BE48 File Offset: 0x0001A048
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6)));
				}
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x0001BED0 File Offset: 0x0001A0D0
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6)));
				}
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x0001BF58 File Offset: 0x0001A158
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result));
				}
			}

			// Token: 0x06000952 RID: 2386 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6" })]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000954 RID: 2388 RVA: 0x0001C002 File Offset: 0x0001A202
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000955 RID: 2389 RVA: 0x0001C012 File Offset: 0x0001A212
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x0001C01F File Offset: 0x0001A21F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400030A RID: 778
			private int completedCount;

			// Token: 0x0400030B RID: 779
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6>> core;
		}

		// Token: 0x02000194 RID: 404
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>>
		{
			// Token: 0x06000957 RID: 2391 RVA: 0x0001C02C File Offset: 0x0001A22C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(this, in awaiter7);
					return;
				}
				awaiter7.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>(this, awaiter7));
			}

			// Token: 0x06000958 RID: 2392 RVA: 0x0001C244 File Offset: 0x0001A444
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x06000959 RID: 2393 RVA: 0x0001C2DC File Offset: 0x0001A4DC
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x0600095A RID: 2394 RVA: 0x0001C374 File Offset: 0x0001A574
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x0600095B RID: 2395 RVA: 0x0001C40C File Offset: 0x0001A60C
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x0600095C RID: 2396 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x0600095D RID: 2397 RVA: 0x0001C53C File Offset: 0x0001A73C
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7>(result)));
				}
			}

			// Token: 0x0600095F RID: 2399 RVA: 0x0001C66C File Offset: 0x0001A86C
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", null })]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000960 RID: 2400 RVA: 0x0001C680 File Offset: 0x0001A880
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000961 RID: 2401 RVA: 0x0001C68E File Offset: 0x0001A88E
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000962 RID: 2402 RVA: 0x0001C69E File Offset: 0x0001A89E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000963 RID: 2403 RVA: 0x0001C6AB File Offset: 0x0001A8AB
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400030C RID: 780
			private int completedCount;

			// Token: 0x0400030D RID: 781
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", null })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>> core;
		}

		// Token: 0x02000195 RID: 405
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>>
		{
			// Token: 0x06000964 RID: 2404 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(this, in awaiter8);
					return;
				}
				awaiter8.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>(this, awaiter8));
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x0001C91C File Offset: 0x0001AB1C
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000966 RID: 2406 RVA: 0x0001C9BC File Offset: 0x0001ABBC
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000967 RID: 2407 RVA: 0x0001CA5C File Offset: 0x0001AC5C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000968 RID: 2408 RVA: 0x0001CAFC File Offset: 0x0001ACFC
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000969 RID: 2409 RVA: 0x0001CB9C File Offset: 0x0001AD9C
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x0600096A RID: 2410 RVA: 0x0001CC3C File Offset: 0x0001AE3C
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x0001CCDC File Offset: 0x0001AEDC
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(result, default(T8))));
				}
			}

			// Token: 0x0600096C RID: 2412 RVA: 0x0001CD7C File Offset: 0x0001AF7C
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), result)));
				}
			}

			// Token: 0x0600096D RID: 2413 RVA: 0x0001CE1C File Offset: 0x0001B01C
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", null,
				null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600096E RID: 2414 RVA: 0x0001CE30 File Offset: 0x0001B030
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600096F RID: 2415 RVA: 0x0001CE3E File Offset: 0x0001B03E
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000970 RID: 2416 RVA: 0x0001CE4E File Offset: 0x0001B04E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000971 RID: 2417 RVA: 0x0001CE5B File Offset: 0x0001B05B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400030E RID: 782
			private int completedCount;

			// Token: 0x0400030F RID: 783
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", null,
				null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>> core;
		}

		// Token: 0x02000196 RID: 406
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>>
		{
			// Token: 0x06000972 RID: 2418 RVA: 0x0001CE68 File Offset: 0x0001B068
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(this, in awaiter9);
					return;
				}
				awaiter9.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>(this, awaiter9));
			}

			// Token: 0x06000973 RID: 2419 RVA: 0x0001D114 File Offset: 0x0001B314
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000974 RID: 2420 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000975 RID: 2421 RVA: 0x0001D26C File Offset: 0x0001B46C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000976 RID: 2422 RVA: 0x0001D318 File Offset: 0x0001B518
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000977 RID: 2423 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x0001D470 File Offset: 0x0001B670
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x0001D51C File Offset: 0x0001B71C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(result, default(T8), default(T9))));
				}
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), result, default(T9))));
				}
			}

			// Token: 0x0600097B RID: 2427 RVA: 0x0001D674 File Offset: 0x0001B874
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), result)));
				}
			}

			// Token: 0x0600097C RID: 2428 RVA: 0x0001D720 File Offset: 0x0001B920
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x0001D734 File Offset: 0x0001B934
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x0001D742 File Offset: 0x0001B942
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0001D752 File Offset: 0x0001B952
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0001D75F File Offset: 0x0001B95F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000310 RID: 784
			private int completedCount;

			// Token: 0x04000311 RID: 785
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>> core;
		}

		// Token: 0x02000197 RID: 407
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>>
		{
			// Token: 0x06000981 RID: 2433 RVA: 0x0001D76C File Offset: 0x0001B96C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(this, in awaiter10);
					return;
				}
				awaiter10.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>(this, awaiter10));
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x0001DA64 File Offset: 0x0001BC64
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x0001DB18 File Offset: 0x0001BD18
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000984 RID: 2436 RVA: 0x0001DBCC File Offset: 0x0001BDCC
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x0001DC80 File Offset: 0x0001BE80
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x0001DD34 File Offset: 0x0001BF34
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x0001DE9C File Offset: 0x0001C09C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(result, default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x0001DF50 File Offset: 0x0001C150
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), result, default(T9), default(T10))));
				}
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x0001E004 File Offset: 0x0001C204
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), result, default(T10))));
				}
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), result)));
				}
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x0001E170 File Offset: 0x0001C370
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x0001E184 File Offset: 0x0001C384
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x0001E192 File Offset: 0x0001C392
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x0001E1A2 File Offset: 0x0001C3A2
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x0001E1AF File Offset: 0x0001C3AF
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000312 RID: 786
			private int completedCount;

			// Token: 0x04000313 RID: 787
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>> core;
		}

		// Token: 0x02000198 RID: 408
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>>
		{
			// Token: 0x06000991 RID: 2449 RVA: 0x0001E1BC File Offset: 0x0001C3BC
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(this, in awaiter11);
					return;
				}
				awaiter11.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>(this, awaiter11));
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x0001E4FC File Offset: 0x0001C6FC
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0001E684 File Offset: 0x0001C884
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x0001E748 File Offset: 0x0001C948
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0001E80C File Offset: 0x0001CA0C
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x0001E994 File Offset: 0x0001CB94
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(result, default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x0001EA58 File Offset: 0x0001CC58
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), result, default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x0001EB1C File Offset: 0x0001CD1C
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), result, default(T10), default(T11))));
				}
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), result, default(T11))));
				}
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), result)));
				}
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x0001ED68 File Offset: 0x0001CF68
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", null, null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x0001ED7C File Offset: 0x0001CF7C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600099F RID: 2463 RVA: 0x0001ED8A File Offset: 0x0001CF8A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009A0 RID: 2464 RVA: 0x0001ED9A File Offset: 0x0001CF9A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009A1 RID: 2465 RVA: 0x0001EDA7 File Offset: 0x0001CFA7
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000314 RID: 788
			private int completedCount;

			// Token: 0x04000315 RID: 789
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", null, null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>> core;
		}

		// Token: 0x02000199 RID: 409
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>>
		{
			// Token: 0x060009A2 RID: 2466 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(this, in awaiter12);
					return;
				}
				awaiter12.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>(this, awaiter12));
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0001F140 File Offset: 0x0001D340
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x0001F210 File Offset: 0x0001D410
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009A5 RID: 2469 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009A6 RID: 2470 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009A7 RID: 2471 RVA: 0x0001F480 File Offset: 0x0001D680
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x0001F550 File Offset: 0x0001D750
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x0001F620 File Offset: 0x0001D820
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(result, default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009AA RID: 2474 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), result, default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009AB RID: 2475 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), result, default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x0001F890 File Offset: 0x0001DA90
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), result, default(T11), default(T12))));
				}
			}

			// Token: 0x060009AD RID: 2477 RVA: 0x0001F960 File Offset: 0x0001DB60
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), result, default(T12))));
				}
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x0001FA30 File Offset: 0x0001DC30
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), result)));
				}
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x0001FB00 File Offset: 0x0001DD00
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", null, null, null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x0001FB14 File Offset: 0x0001DD14
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060009B1 RID: 2481 RVA: 0x0001FB22 File Offset: 0x0001DD22
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009B2 RID: 2482 RVA: 0x0001FB32 File Offset: 0x0001DD32
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009B3 RID: 2483 RVA: 0x0001FB3F File Offset: 0x0001DD3F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000316 RID: 790
			private int completedCount;

			// Token: 0x04000317 RID: 791
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", null, null, null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>> core;
		}

		// Token: 0x0200019A RID: 410
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>>
		{
			// Token: 0x060009B4 RID: 2484 RVA: 0x0001FB4C File Offset: 0x0001DD4C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(this, in awaiter13);
					return;
				}
				awaiter13.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter> stateTuple13 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(stateTuple13.Item1, in stateTuple13.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>(this, awaiter13));
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x0001FF20 File Offset: 0x0001E120
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009B6 RID: 2486 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009B7 RID: 2487 RVA: 0x000200D0 File Offset: 0x0001E2D0
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009B8 RID: 2488 RVA: 0x000201A8 File Offset: 0x0001E3A8
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x00020280 File Offset: 0x0001E480
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x00020358 File Offset: 0x0001E558
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x00020430 File Offset: 0x0001E630
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(result, default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x00020508 File Offset: 0x0001E708
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), result, default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x000205E0 File Offset: 0x0001E7E0
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), result, default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x000206B8 File Offset: 0x0001E8B8
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), result, default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x00020794 File Offset: 0x0001E994
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), result, default(T12), default(T13))));
				}
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x00020870 File Offset: 0x0001EA70
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), result, default(T13))));
				}
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x0002094C File Offset: 0x0001EB4C
			private static void TryInvokeContinuationT13(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T13>.Awaiter awaiter)
			{
				T13 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(12, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), result)));
				}
			}

			// Token: 0x060009C2 RID: 2498 RVA: 0x00020A28 File Offset: 0x0001EC28
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", null, null, null, null, null, null,
				null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060009C3 RID: 2499 RVA: 0x00020A3C File Offset: 0x0001EC3C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x00020A4A File Offset: 0x0001EC4A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x00020A5A File Offset: 0x0001EC5A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x00020A67 File Offset: 0x0001EC67
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000318 RID: 792
			private int completedCount;

			// Token: 0x04000319 RID: 793
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", null, null, null, null, null, null,
				null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>> core;
		}

		// Token: 0x0200019B RID: 411
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>>
		{
			// Token: 0x060009C7 RID: 2503 RVA: 0x00020A74 File Offset: 0x0001EC74
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter> stateTuple13 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(stateTuple13.Item1, in stateTuple13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(this, in awaiter14);
					return;
				}
				awaiter14.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter> stateTuple14 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(stateTuple14.Item1, in stateTuple14.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>(this, awaiter14));
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x00020E94 File Offset: 0x0001F094
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009C9 RID: 2505 RVA: 0x00020F7C File Offset: 0x0001F17C
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x00021064 File Offset: 0x0001F264
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009CB RID: 2507 RVA: 0x0002114C File Offset: 0x0001F34C
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x00021234 File Offset: 0x0001F434
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x0002131C File Offset: 0x0001F51C
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x00021404 File Offset: 0x0001F604
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(result, default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x000214EC File Offset: 0x0001F6EC
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), result, default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x000215D4 File Offset: 0x0001F7D4
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), result, default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009D1 RID: 2513 RVA: 0x000216BC File Offset: 0x0001F8BC
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), result, default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x000217A4 File Offset: 0x0001F9A4
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), result, default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x0002188C File Offset: 0x0001FA8C
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), result, default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x00021974 File Offset: 0x0001FB74
			private static void TryInvokeContinuationT13(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T13>.Awaiter awaiter)
			{
				T13 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(12, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), result, new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060009D5 RID: 2517 RVA: 0x00021A5C File Offset: 0x0001FC5C
			private static void TryInvokeContinuationT14(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T14>.Awaiter awaiter)
			{
				T14 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(13, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(result))));
				}
			}

			// Token: 0x060009D6 RID: 2518 RVA: 0x00021B44 File Offset: 0x0001FD44
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", null, null, null, null, null,
				null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x00021B58 File Offset: 0x0001FD58
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x00021B66 File Offset: 0x0001FD66
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x00021B76 File Offset: 0x0001FD76
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x00021B83 File Offset: 0x0001FD83
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400031A RID: 794
			private int completedCount;

			// Token: 0x0400031B RID: 795
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", null, null, null, null, null,
				null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>> core;
		}

		// Token: 0x0200019C RID: 412
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>>
		{
			// Token: 0x060009DB RID: 2523 RVA: 0x00021B90 File Offset: 0x0001FD90
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(stateTuple2.Item1, in stateTuple2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter> stateTuple3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(stateTuple3.Item1, in stateTuple3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter> stateTuple4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(stateTuple4.Item1, in stateTuple4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter> stateTuple5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(stateTuple5.Item1, in stateTuple5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter> stateTuple6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(stateTuple6.Item1, in stateTuple6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter> stateTuple7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(stateTuple7.Item1, in stateTuple7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter> stateTuple8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(stateTuple8.Item1, in stateTuple8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter> stateTuple9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(stateTuple9.Item1, in stateTuple9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter> stateTuple10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(stateTuple10.Item1, in stateTuple10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter> stateTuple11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(stateTuple11.Item1, in stateTuple11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter> stateTuple12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(stateTuple12.Item1, in stateTuple12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter> stateTuple13 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(stateTuple13.Item1, in stateTuple13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(this, in awaiter14);
				}
				else
				{
					awaiter14.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter> stateTuple14 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(stateTuple14.Item1, in stateTuple14.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>(this, awaiter14));
				}
				UniTask<T15>.Awaiter awaiter15 = task15.GetAwaiter();
				if (awaiter15.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(this, in awaiter15);
					return;
				}
				awaiter15.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter> stateTuple15 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(stateTuple15.Item1, in stateTuple15.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>(this, awaiter15));
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x00021FF8 File Offset: 0x000201F8
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x000220EC File Offset: 0x000202EC
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x000221E0 File Offset: 0x000203E0
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x000222D4 File Offset: 0x000204D4
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x000223C8 File Offset: 0x000205C8
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x000224BC File Offset: 0x000206BC
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x000225B0 File Offset: 0x000207B0
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(result, default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E3 RID: 2531 RVA: 0x000226A4 File Offset: 0x000208A4
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), result, default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x00022798 File Offset: 0x00020998
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), result, default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x0002288C File Offset: 0x00020A8C
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), result, default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x00022980 File Offset: 0x00020B80
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), result, default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E7 RID: 2535 RVA: 0x00022A74 File Offset: 0x00020C74
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), result, default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E8 RID: 2536 RVA: 0x00022B68 File Offset: 0x00020D68
			private static void TryInvokeContinuationT13(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T13>.Awaiter awaiter)
			{
				T13 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(12, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), result, new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060009E9 RID: 2537 RVA: 0x00022C5C File Offset: 0x00020E5C
			private static void TryInvokeContinuationT14(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T14>.Awaiter awaiter)
			{
				T14 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(13, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(result, default(T15)))));
				}
			}

			// Token: 0x060009EA RID: 2538 RVA: 0x00022D50 File Offset: 0x00020F50
			private static void TryInvokeContinuationT15(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T15>.Awaiter awaiter)
			{
				T15 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(14, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), result))));
				}
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x00022E44 File Offset: 0x00021044
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", "result15", null, null, null, null,
				null, null, null, null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x00022E58 File Offset: 0x00021058
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x00022E66 File Offset: 0x00021066
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009EE RID: 2542 RVA: 0x00022E76 File Offset: 0x00021076
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009EF RID: 2543 RVA: 0x00022E83 File Offset: 0x00021083
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400031C RID: 796
			private int completedCount;

			// Token: 0x0400031D RID: 797
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", "result15", null, null, null, null,
				null, null, null, null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>> core;
		}

		// Token: 0x0200019D RID: 413
		private sealed class WhenAnyLRPromise<T> : IUniTaskSource<ValueTuple<bool, T>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<bool, T>>
		{
			// Token: 0x060009F0 RID: 2544 RVA: 0x00022E90 File Offset: 0x00021090
			public WhenAnyLRPromise(UniTask<T> leftTask, UniTask rightTask)
			{
				UniTask<T>.Awaiter awaiter;
				try
				{
					awaiter = leftTask.GetAwaiter();
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					goto IL_0060;
				}
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyLRPromise<T>.TryLeftInvokeContinuation(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask<T>.Awaiter> stateTuple = (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask<T>.Awaiter>)state)
						{
							UniTask.WhenAnyLRPromise<T>.TryLeftInvokeContinuation(stateTuple.Item1, in stateTuple.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyLRPromise<T>, UniTask<T>.Awaiter>(this, awaiter));
				}
				IL_0060:
				UniTask.Awaiter awaiter2;
				try
				{
					awaiter2 = rightTask.GetAwaiter();
				}
				catch (Exception ex2)
				{
					this.core.TrySetException(ex2);
					return;
				}
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyLRPromise<T>.TryRightInvokeContinuation(this, in awaiter2);
					return;
				}
				awaiter2.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask.Awaiter> stateTuple2 = (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask.Awaiter>)state)
					{
						UniTask.WhenAnyLRPromise<T>.TryRightInvokeContinuation(stateTuple2.Item1, in stateTuple2.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyLRPromise<T>, UniTask.Awaiter>(this, awaiter2));
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x00022F74 File Offset: 0x00021174
			private static void TryLeftInvokeContinuation(UniTask.WhenAnyLRPromise<T> self, in UniTask<T>.Awaiter awaiter)
			{
				T result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<bool, T>(true, result));
				}
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x00022FCC File Offset: 0x000211CC
			private static void TryRightInvokeContinuation(UniTask.WhenAnyLRPromise<T> self, in UniTask.Awaiter awaiter)
			{
				try
				{
					awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<bool, T>(false, default(T)));
				}
			}

			// Token: 0x060009F3 RID: 2547 RVA: 0x0002302C File Offset: 0x0002122C
			public ValueTuple<bool, T> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x00023040 File Offset: 0x00021240
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x0002304E File Offset: 0x0002124E
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x0002305E File Offset: 0x0002125E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x0002306B File Offset: 0x0002126B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400031E RID: 798
			private int completedCount;

			// Token: 0x0400031F RID: 799
			private UniTaskCompletionSourceCore<ValueTuple<bool, T>> core;
		}

		// Token: 0x0200019E RID: 414
		private sealed class WhenAnyPromise<T> : IUniTaskSource<ValueTuple<int, T>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T>>
		{
			// Token: 0x060009F8 RID: 2552 RVA: 0x00023078 File Offset: 0x00021278
			public WhenAnyPromise(UniTask<T>[] tasks, int tasksLength)
			{
				if (tasksLength == 0)
				{
					throw new ArgumentException("The tasks argument contains no tasks.");
				}
				int i = 0;
				while (i < tasksLength)
				{
					UniTask<T>.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_007A;
					}
					goto IL_0038;
					IL_007A:
					i++;
					continue;
					IL_0038:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAnyPromise<T>.TryInvokeContinuation(this, in awaiter, i);
						goto IL_007A;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T>, UniTask<T>.Awaiter, int> stateTuple = (StateTuple<UniTask.WhenAnyPromise<T>, UniTask<T>.Awaiter, int>)state)
						{
							UniTask.WhenAnyPromise<T>.TryInvokeContinuation(stateTuple.Item1, in stateTuple.Item2, stateTuple.Item3);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T>, UniTask<T>.Awaiter, int>(this, awaiter, i));
					goto IL_007A;
				}
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x00023118 File Offset: 0x00021318
			private static void TryInvokeContinuation(UniTask.WhenAnyPromise<T> self, in UniTask<T>.Awaiter awaiter, int i)
			{
				T result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T>(i, result));
				}
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x00023170 File Offset: 0x00021370
			public ValueTuple<int, T> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00023184 File Offset: 0x00021384
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00023192 File Offset: 0x00021392
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x000231A2 File Offset: 0x000213A2
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x000231AF File Offset: 0x000213AF
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000320 RID: 800
			private int completedCount;

			// Token: 0x04000321 RID: 801
			private UniTaskCompletionSourceCore<ValueTuple<int, T>> core;
		}

		// Token: 0x0200019F RID: 415
		private sealed class WhenAnyPromise : IUniTaskSource<int>, IUniTaskSource, IValueTaskSource, IValueTaskSource<int>
		{
			// Token: 0x060009FF RID: 2559 RVA: 0x000231BC File Offset: 0x000213BC
			public WhenAnyPromise(UniTask[] tasks, int tasksLength)
			{
				if (tasksLength == 0)
				{
					throw new ArgumentException("The tasks argument contains no tasks.");
				}
				int i = 0;
				while (i < tasksLength)
				{
					UniTask.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_007A;
					}
					goto IL_0038;
					IL_007A:
					i++;
					continue;
					IL_0038:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAnyPromise.TryInvokeContinuation(this, in awaiter, i);
						goto IL_007A;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise, UniTask.Awaiter, int> stateTuple = (StateTuple<UniTask.WhenAnyPromise, UniTask.Awaiter, int>)state)
						{
							UniTask.WhenAnyPromise.TryInvokeContinuation(stateTuple.Item1, in stateTuple.Item2, stateTuple.Item3);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise, UniTask.Awaiter, int>(this, awaiter, i));
					goto IL_007A;
				}
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x0002325C File Offset: 0x0002145C
			private static void TryInvokeContinuation(UniTask.WhenAnyPromise self, in UniTask.Awaiter awaiter, int i)
			{
				try
				{
					awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(i);
				}
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x000232AC File Offset: 0x000214AC
			public int GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x000232C0 File Offset: 0x000214C0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x000232CE File Offset: 0x000214CE
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x000232DE File Offset: 0x000214DE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x000232EB File Offset: 0x000214EB
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000322 RID: 802
			private int completedCount;

			// Token: 0x04000323 RID: 803
			private UniTaskCompletionSourceCore<int> core;
		}

		// Token: 0x020001A0 RID: 416
		private sealed class AsyncUnitSource : IUniTaskSource<AsyncUnit>, IUniTaskSource, IValueTaskSource, IValueTaskSource<AsyncUnit>
		{
			// Token: 0x06000A06 RID: 2566 RVA: 0x000232F5 File Offset: 0x000214F5
			public AsyncUnitSource(IUniTaskSource source)
			{
				this.source = source;
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x00023304 File Offset: 0x00021504
			public AsyncUnit GetResult(short token)
			{
				this.source.GetResult(token);
				return AsyncUnit.Default;
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x00023317 File Offset: 0x00021517
			public UniTaskStatus GetStatus(short token)
			{
				return this.source.GetStatus(token);
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x00023325 File Offset: 0x00021525
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x00023335 File Offset: 0x00021535
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x00023342 File Offset: 0x00021542
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000324 RID: 804
			private readonly IUniTaskSource source;
		}

		// Token: 0x020001A1 RID: 417
		private sealed class IsCanceledSource : IUniTaskSource<bool>, IUniTaskSource, IValueTaskSource, IValueTaskSource<bool>
		{
			// Token: 0x06000A0C RID: 2572 RVA: 0x0002334C File Offset: 0x0002154C
			public IsCanceledSource(IUniTaskSource source)
			{
				this.source = source;
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x0002335B File Offset: 0x0002155B
			public bool GetResult(short token)
			{
				if (this.source.GetStatus(token) == UniTaskStatus.Canceled)
				{
					return true;
				}
				this.source.GetResult(token);
				return false;
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0002337B File Offset: 0x0002157B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x00023385 File Offset: 0x00021585
			public UniTaskStatus GetStatus(short token)
			{
				return this.source.GetStatus(token);
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x00023393 File Offset: 0x00021593
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x000233A0 File Offset: 0x000215A0
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000325 RID: 805
			private readonly IUniTaskSource source;
		}

		// Token: 0x020001A2 RID: 418
		private sealed class MemoizeSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x06000A12 RID: 2578 RVA: 0x000233B0 File Offset: 0x000215B0
			public MemoizeSource(IUniTaskSource source)
			{
				this.source = source;
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x000233C0 File Offset: 0x000215C0
			public void GetResult(short token)
			{
				if (this.source == null)
				{
					if (this.exception != null)
					{
						this.exception.Throw();
						return;
					}
				}
				else
				{
					try
					{
						this.source.GetResult(token);
						this.status = UniTaskStatus.Succeeded;
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
				}
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x0002344C File Offset: 0x0002164C
			public UniTaskStatus GetStatus(short token)
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.GetStatus(token);
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00023469 File Offset: 0x00021669
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				if (this.source == null)
				{
					continuation(state);
					return;
				}
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000A16 RID: 2582 RVA: 0x00023489 File Offset: 0x00021689
			public UniTaskStatus UnsafeGetStatus()
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x04000326 RID: 806
			private IUniTaskSource source;

			// Token: 0x04000327 RID: 807
			private ExceptionDispatchInfo exception;

			// Token: 0x04000328 RID: 808
			private UniTaskStatus status;
		}

		// Token: 0x020001A3 RID: 419
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x000234A5 File Offset: 0x000216A5
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Awaiter(in UniTask task)
			{
				this.task = task;
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000234B3 File Offset: 0x000216B3
			public bool IsCompleted
			{
				[DebuggerHidden]
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.task.Status.IsCompleted();
				}
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x000234C5 File Offset: 0x000216C5
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				if (this.task.source == null)
				{
					return;
				}
				this.task.source.GetResult(this.task.token);
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x000234F0 File Offset: 0x000216F0
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				if (this.task.source == null)
				{
					continuation();
					return;
				}
				this.task.source.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x00023527 File Offset: 0x00021727
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void UnsafeOnCompleted(Action continuation)
			{
				if (this.task.source == null)
				{
					continuation();
					return;
				}
				this.task.source.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x0002355E File Offset: 0x0002175E
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SourceOnCompleted(Action<object> continuation, object state)
			{
				if (this.task.source == null)
				{
					continuation(state);
					return;
				}
				this.task.source.OnCompleted(continuation, state, this.task.token);
			}

			// Token: 0x04000329 RID: 809
			private readonly UniTask task;
		}
	}
}
