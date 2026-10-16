using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000056 RID: 86
	public static class UniTaskObservableExtensions
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000916C File Offset: 0x0000736C
		public static UniTask<T> ToUniTask<T>(this IObservable<T> source, bool useFirstValue = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			UniTaskCompletionSource<T> uniTaskCompletionSource = new UniTaskCompletionSource<T>();
			SingleAssignmentDisposable singleAssignmentDisposable = new SingleAssignmentDisposable();
			IObserver<T> observer2;
			if (!useFirstValue)
			{
				IObserver<T> observer = new UniTaskObservableExtensions.ToUniTaskObserver<T>(uniTaskCompletionSource, singleAssignmentDisposable, cancellationToken);
				observer2 = observer;
			}
			else
			{
				IObserver<T> observer = new UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>(uniTaskCompletionSource, singleAssignmentDisposable, cancellationToken);
				observer2 = observer;
			}
			IObserver<T> observer3 = observer2;
			try
			{
				singleAssignmentDisposable.Disposable = source.Subscribe(observer3);
			}
			catch (Exception ex)
			{
				uniTaskCompletionSource.TrySetException(ex);
			}
			return uniTaskCompletionSource.Task;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000091D4 File Offset: 0x000073D4
		public static IObservable<T> ToObservable<T>(this UniTask<T> task)
		{
			if (task.Status.IsCompleted())
			{
				try
				{
					return new UniTaskObservableExtensions.ReturnObservable<T>(task.GetAwaiter().GetResult());
				}
				catch (Exception ex)
				{
					return new UniTaskObservableExtensions.ThrowObservable<T>(ex);
				}
			}
			AsyncSubject<T> asyncSubject = new AsyncSubject<T>();
			UniTaskObservableExtensions.Fire<T>(asyncSubject, task).Forget();
			return asyncSubject;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009234 File Offset: 0x00007434
		public static IObservable<AsyncUnit> ToObservable(this UniTask task)
		{
			if (task.Status.IsCompleted())
			{
				try
				{
					task.GetAwaiter().GetResult();
					return new UniTaskObservableExtensions.ReturnObservable<AsyncUnit>(AsyncUnit.Default);
				}
				catch (Exception ex)
				{
					return new UniTaskObservableExtensions.ThrowObservable<AsyncUnit>(ex);
				}
			}
			AsyncSubject<AsyncUnit> asyncSubject = new AsyncSubject<AsyncUnit>();
			UniTaskObservableExtensions.Fire(asyncSubject, task).Forget();
			return asyncSubject;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000929C File Offset: 0x0000749C
		private static async UniTaskVoid Fire<T>(AsyncSubject<T> subject, UniTask<T> task)
		{
			T t;
			try
			{
				t = await task;
			}
			catch (Exception ex)
			{
				subject.OnError(ex);
				return;
			}
			subject.OnNext(t);
			subject.OnCompleted();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000092E8 File Offset: 0x000074E8
		private static async UniTaskVoid Fire(AsyncSubject<AsyncUnit> subject, UniTask task)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				subject.OnError(ex);
				return;
			}
			subject.OnNext(AsyncUnit.Default);
			subject.OnCompleted();
		}

		// Token: 0x020001E9 RID: 489
		private class ToUniTaskObserver<T> : IObserver<T>
		{
			// Token: 0x06000AF0 RID: 2800 RVA: 0x00027800 File Offset: 0x00025A00
			public ToUniTaskObserver(UniTaskCompletionSource<T> promise, SingleAssignmentDisposable disposable, CancellationToken cancellationToken)
			{
				this.promise = promise;
				this.disposable = disposable;
				this.cancellationToken = cancellationToken;
				if (this.cancellationToken.CanBeCanceled)
				{
					this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskObservableExtensions.ToUniTaskObserver<T>.callback, this);
				}
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x0002784C File Offset: 0x00025A4C
			private static void OnCanceled(object state)
			{
				UniTaskObservableExtensions.ToUniTaskObserver<T> toUniTaskObserver = (UniTaskObservableExtensions.ToUniTaskObserver<T>)state;
				toUniTaskObserver.disposable.Dispose();
				toUniTaskObserver.promise.TrySetCanceled(toUniTaskObserver.cancellationToken);
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x0002787D File Offset: 0x00025A7D
			public void OnNext(T value)
			{
				this.hasValue = true;
				this.latestValue = value;
			}

			// Token: 0x06000AF3 RID: 2803 RVA: 0x00027890 File Offset: 0x00025A90
			public void OnError(Exception error)
			{
				try
				{
					this.promise.TrySetException(error);
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x06000AF4 RID: 2804 RVA: 0x000278D8 File Offset: 0x00025AD8
			public void OnCompleted()
			{
				try
				{
					if (this.hasValue)
					{
						this.promise.TrySetResult(this.latestValue);
					}
					else
					{
						this.promise.TrySetException(new InvalidOperationException("Sequence has no elements"));
					}
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x04000470 RID: 1136
			private static readonly Action<object> callback = new Action<object>(UniTaskObservableExtensions.ToUniTaskObserver<T>.OnCanceled);

			// Token: 0x04000471 RID: 1137
			private readonly UniTaskCompletionSource<T> promise;

			// Token: 0x04000472 RID: 1138
			private readonly SingleAssignmentDisposable disposable;

			// Token: 0x04000473 RID: 1139
			private readonly CancellationToken cancellationToken;

			// Token: 0x04000474 RID: 1140
			private readonly CancellationTokenRegistration registration;

			// Token: 0x04000475 RID: 1141
			private bool hasValue;

			// Token: 0x04000476 RID: 1142
			private T latestValue;
		}

		// Token: 0x020001EA RID: 490
		private class FirstValueToUniTaskObserver<T> : IObserver<T>
		{
			// Token: 0x06000AF6 RID: 2806 RVA: 0x00027958 File Offset: 0x00025B58
			public FirstValueToUniTaskObserver(UniTaskCompletionSource<T> promise, SingleAssignmentDisposable disposable, CancellationToken cancellationToken)
			{
				this.promise = promise;
				this.disposable = disposable;
				this.cancellationToken = cancellationToken;
				if (this.cancellationToken.CanBeCanceled)
				{
					this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>.callback, this);
				}
			}

			// Token: 0x06000AF7 RID: 2807 RVA: 0x000279A4 File Offset: 0x00025BA4
			private static void OnCanceled(object state)
			{
				UniTaskObservableExtensions.FirstValueToUniTaskObserver<T> firstValueToUniTaskObserver = (UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>)state;
				firstValueToUniTaskObserver.disposable.Dispose();
				firstValueToUniTaskObserver.promise.TrySetCanceled(firstValueToUniTaskObserver.cancellationToken);
			}

			// Token: 0x06000AF8 RID: 2808 RVA: 0x000279D8 File Offset: 0x00025BD8
			public void OnNext(T value)
			{
				this.hasValue = true;
				try
				{
					this.promise.TrySetResult(value);
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x06000AF9 RID: 2809 RVA: 0x00027A28 File Offset: 0x00025C28
			public void OnError(Exception error)
			{
				try
				{
					this.promise.TrySetException(error);
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x06000AFA RID: 2810 RVA: 0x00027A70 File Offset: 0x00025C70
			public void OnCompleted()
			{
				try
				{
					if (!this.hasValue)
					{
						this.promise.TrySetException(new InvalidOperationException("Sequence has no elements"));
					}
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x04000477 RID: 1143
			private static readonly Action<object> callback = new Action<object>(UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>.OnCanceled);

			// Token: 0x04000478 RID: 1144
			private readonly UniTaskCompletionSource<T> promise;

			// Token: 0x04000479 RID: 1145
			private readonly SingleAssignmentDisposable disposable;

			// Token: 0x0400047A RID: 1146
			private readonly CancellationToken cancellationToken;

			// Token: 0x0400047B RID: 1147
			private readonly CancellationTokenRegistration registration;

			// Token: 0x0400047C RID: 1148
			private bool hasValue;
		}

		// Token: 0x020001EB RID: 491
		private class ReturnObservable<T> : IObservable<T>
		{
			// Token: 0x06000AFC RID: 2812 RVA: 0x00027ADB File Offset: 0x00025CDB
			public ReturnObservable(T value)
			{
				this.value = value;
			}

			// Token: 0x06000AFD RID: 2813 RVA: 0x00027AEA File Offset: 0x00025CEA
			public IDisposable Subscribe(IObserver<T> observer)
			{
				observer.OnNext(this.value);
				observer.OnCompleted();
				return EmptyDisposable.Instance;
			}

			// Token: 0x0400047D RID: 1149
			private readonly T value;
		}

		// Token: 0x020001EC RID: 492
		private class ThrowObservable<T> : IObservable<T>
		{
			// Token: 0x06000AFE RID: 2814 RVA: 0x00027B03 File Offset: 0x00025D03
			public ThrowObservable(Exception value)
			{
				this.value = value;
			}

			// Token: 0x06000AFF RID: 2815 RVA: 0x00027B12 File Offset: 0x00025D12
			public IDisposable Subscribe(IObserver<T> observer)
			{
				observer.OnError(this.value);
				return EmptyDisposable.Instance;
			}

			// Token: 0x0400047E RID: 1150
			private readonly Exception value;
		}
	}
}
