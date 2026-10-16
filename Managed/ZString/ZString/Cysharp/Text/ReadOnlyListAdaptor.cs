using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct ReadOnlyListAdaptor<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000F759 File Offset: 0x0000D959
		public ReadOnlyListAdaptor(IList<T> list)
		{
			this._list = list;
		}

		// Token: 0x1700004A RID: 74
		public T this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000F770 File Offset: 0x0000D970
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F77D File Offset: 0x0000D97D
		public IEnumerator<T> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F78A File Offset: 0x0000D98A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000BA RID: 186
		private readonly IList<T> _list;
	}
}
