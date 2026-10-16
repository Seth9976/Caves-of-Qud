using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000020 RID: 32
	[StructLayout(LayoutKind.Auto)]
	public readonly struct UniTaskCancelableAsyncEnumerable<T>
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002CE1 File Offset: 0x00000EE1
		internal UniTaskCancelableAsyncEnumerable(IUniTaskAsyncEnumerable<T> enumerable, CancellationToken cancellationToken)
		{
			this.enumerable = enumerable;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public UniTaskCancelableAsyncEnumerable<T>.Enumerator GetAsyncEnumerator()
		{
			return new UniTaskCancelableAsyncEnumerable<T>.Enumerator(this.enumerable.GetAsyncEnumerator(this.cancellationToken));
		}

		// Token: 0x04000023 RID: 35
		private readonly IUniTaskAsyncEnumerable<T> enumerable;

		// Token: 0x04000024 RID: 36
		private readonly CancellationToken cancellationToken;

		// Token: 0x0200013E RID: 318
		[StructLayout(LayoutKind.Auto)]
		public readonly struct Enumerator
		{
			// Token: 0x06000766 RID: 1894 RVA: 0x0001146F File Offset: 0x0000F66F
			internal Enumerator(IUniTaskAsyncEnumerator<T> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x00011478 File Offset: 0x0000F678
			public T Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x00011485 File Offset: 0x0000F685
			public UniTask<bool> MoveNextAsync()
			{
				return this.enumerator.MoveNextAsync();
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x00011492 File Offset: 0x0000F692
			public UniTask DisposeAsync()
			{
				return this.enumerator.DisposeAsync();
			}

			// Token: 0x040001C5 RID: 453
			private readonly IUniTaskAsyncEnumerator<T> enumerator;
		}
	}
}
