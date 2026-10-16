using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000019 RID: 25
	internal struct DisposableEnumeratorAdapter<[Nullable(2)] T, TEnumerator> : IDisposable where TEnumerator : struct, IEnumerator<T>
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002E08 File Offset: 0x00001008
		internal DisposableEnumeratorAdapter(TEnumerator enumerator)
		{
			this._enumeratorStruct = enumerator;
			this._enumeratorObject = null;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E18 File Offset: 0x00001018
		[NullableContext(1)]
		internal DisposableEnumeratorAdapter(IEnumerator<T> enumerator)
		{
			this._enumeratorStruct = default(TEnumerator);
			this._enumeratorObject = enumerator;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E2D File Offset: 0x0000102D
		[Nullable(1)]
		public T Current
		{
			[NullableContext(1)]
			get
			{
				if (this._enumeratorObject == null)
				{
					return this._enumeratorStruct.Current;
				}
				return this._enumeratorObject.Current;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E54 File Offset: 0x00001054
		public bool MoveNext()
		{
			if (this._enumeratorObject == null)
			{
				return this._enumeratorStruct.MoveNext();
			}
			return this._enumeratorObject.MoveNext();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E7B File Offset: 0x0000107B
		public void Dispose()
		{
			if (this._enumeratorObject != null)
			{
				this._enumeratorObject.Dispose();
				return;
			}
			this._enumeratorStruct.Dispose();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EA2 File Offset: 0x000010A2
		[return: Nullable(new byte[] { 0, 1, 0 })]
		public DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04000011 RID: 17
		private readonly IEnumerator<T> _enumeratorObject;

		// Token: 0x04000012 RID: 18
		private TEnumerator _enumeratorStruct;
	}
}
