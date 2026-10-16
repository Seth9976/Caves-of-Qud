using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C7 RID: 1223
	internal sealed class BidirectionalDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>, IEnumerable
	{
		// Token: 0x06002792 RID: 10130 RVA: 0x00089024 File Offset: 0x00087224
		public BidirectionalDictionary(int capacity)
		{
			this._forward = new Dictionary<T1, T2>(capacity);
			this._backward = new Dictionary<T2, T1>(capacity);
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x00089044 File Offset: 0x00087244
		public int Count
		{
			get
			{
				return this._forward.Count;
			}
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00089051 File Offset: 0x00087251
		public void Add(T1 item1, T2 item2)
		{
			this._forward.Add(item1, item2);
			this._backward.Add(item2, item1);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0008906D File Offset: 0x0008726D
		public bool TryGetForward(T1 item1, out T2 item2)
		{
			return this._forward.TryGetValue(item1, out item2);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0008907C File Offset: 0x0008727C
		public bool TryGetBackward(T2 item2, out T1 item1)
		{
			return this._backward.TryGetValue(item2, out item1);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0008908B File Offset: 0x0008728B
		public Dictionary<T1, T2>.Enumerator GetEnumerator()
		{
			return this._forward.GetEnumerator();
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00089098 File Offset: 0x00087298
		IEnumerator<KeyValuePair<T1, T2>> IEnumerable<KeyValuePair<T1, T2>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00089098 File Offset: 0x00087298
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001572 RID: 5490
		private readonly Dictionary<T1, T2> _forward;

		// Token: 0x04001573 RID: 5491
		private readonly Dictionary<T2, T1> _backward;
	}
}
