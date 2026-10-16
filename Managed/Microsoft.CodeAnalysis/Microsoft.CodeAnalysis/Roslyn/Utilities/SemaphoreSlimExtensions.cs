using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Roslyn.Utilities
{
	// Token: 0x0200005E RID: 94
	internal static class SemaphoreSlimExtensions
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00007023 File Offset: 0x00005223
		[NullableContext(1)]
		public static SemaphoreSlimExtensions.SemaphoreDisposer DisposableWait(this SemaphoreSlim semaphore, CancellationToken cancellationToken = default(CancellationToken))
		{
			semaphore.Wait(cancellationToken);
			return new SemaphoreSlimExtensions.SemaphoreDisposer(semaphore);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007034 File Offset: 0x00005234
		public static ValueTask<SemaphoreSlimExtensions.SemaphoreDisposer> DisposableWaitAsync([Nullable(1)] this SemaphoreSlim semaphore, CancellationToken cancellationToken = default(CancellationToken))
		{
			SemaphoreSlimExtensions.<DisposableWaitAsync>d__1 <DisposableWaitAsync>d__;
			<DisposableWaitAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<SemaphoreSlimExtensions.SemaphoreDisposer>.Create();
			<DisposableWaitAsync>d__.semaphore = semaphore;
			<DisposableWaitAsync>d__.cancellationToken = cancellationToken;
			<DisposableWaitAsync>d__.<>1__state = -1;
			<DisposableWaitAsync>d__.<>t__builder.Start<SemaphoreSlimExtensions.<DisposableWaitAsync>d__1>(ref <DisposableWaitAsync>d__);
			return <DisposableWaitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x02000508 RID: 1288
		[NonCopyable]
		internal struct SemaphoreDisposer : IDisposable
		{
			// Token: 0x0600330B RID: 13067 RVA: 0x00090C14 File Offset: 0x0008EE14
			[NullableContext(1)]
			public SemaphoreDisposer(SemaphoreSlim semaphore)
			{
				this._semaphore = semaphore;
			}

			// Token: 0x0600330C RID: 13068 RVA: 0x00090C1D File Offset: 0x0008EE1D
			public void Dispose()
			{
				this._semaphore.Release();
			}

			// Token: 0x04001415 RID: 5141
			[Nullable(1)]
			private readonly SemaphoreSlim _semaphore;
		}
	}
}
