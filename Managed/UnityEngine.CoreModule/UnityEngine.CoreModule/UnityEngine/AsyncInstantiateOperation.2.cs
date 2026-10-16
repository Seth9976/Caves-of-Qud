using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000253 RID: 595
	public class AsyncInstantiateOperation<T> : AsyncInstantiateOperation
	{
		// Token: 0x06001F2A RID: 7978 RVA: 0x0003BDD1 File Offset: 0x00039FD1
		internal AsyncInstantiateOperation(IntPtr ptr, CancellationToken cancellationToken)
			: base(ptr, cancellationToken)
		{
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		public new T[] Result
		{
			get
			{
				return (T[])this.m_Result;
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0003BE00 File Offset: 0x0003A000
		internal override Object[] CreateResultArray(int size)
		{
			this.m_Result = (Object[])new T[size];
			return this.m_Result;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0003BE2C File Offset: 0x0003A02C
		[ExcludeFromDocs]
		public AsyncInstantiateOperation<T>.Awaiter GetAwaiter()
		{
			return new AsyncInstantiateOperation<T>.Awaiter(this);
		}

		// Token: 0x02000254 RID: 596
		internal new static class BindingsMarshaller
		{
			// Token: 0x06001F2E RID: 7982 RVA: 0x0003BE44 File Offset: 0x0003A044
			public static AsyncInstantiateOperation<T> ConvertToManaged(IntPtr ptr)
			{
				return new AsyncInstantiateOperation<T>(ptr, CancellationToken.None);
			}

			// Token: 0x06001F2F RID: 7983 RVA: 0x0003BDC9 File Offset: 0x00039FC9
			public static IntPtr ConvertToNative(AsyncInstantiateOperation<T> obj)
			{
				return obj.m_Ptr;
			}
		}

		// Token: 0x02000255 RID: 597
		[ExcludeFromDocs]
		public struct Awaiter : INotifyCompletion
		{
			// Token: 0x06001F30 RID: 7984 RVA: 0x0003BE54 File Offset: 0x0003A054
			public Awaiter(AsyncInstantiateOperation<T> op)
			{
				this._awaitable = Awaitable.FromAsyncOperation(op, default(CancellationToken));
				this._op = op;
			}

			// Token: 0x06001F31 RID: 7985 RVA: 0x0003BE7E File Offset: 0x0003A07E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				this._awaitable.SetContinuation(continuation);
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0003BE8E File Offset: 0x0003A08E
			public bool IsCompleted
			{
				get
				{
					return this._awaitable.IsCompleted;
				}
			}

			// Token: 0x06001F33 RID: 7987 RVA: 0x0003BE9C File Offset: 0x0003A09C
			public T[] GetResult()
			{
				this._awaitable.GetAwaiter().GetResult();
				return this._op.Result;
			}

			// Token: 0x04000933 RID: 2355
			private readonly Awaitable _awaitable;

			// Token: 0x04000934 RID: 2356
			private readonly AsyncInstantiateOperation<T> _op;
		}
	}
}
