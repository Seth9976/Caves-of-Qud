using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000144 RID: 324
	internal struct OneOrMore<TValue, TList> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue> where TList : IReadOnlyList<TValue>
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00054EC0 File Offset: 0x000530C0
		public int Count
		{
			get
			{
				if (!this.m_IsSingle)
				{
					TList multiple = this.m_Multiple;
					return multiple.Count;
				}
				return 1;
			}
		}

		// Token: 0x170004E9 RID: 1257
		public TValue this[int index]
		{
			get
			{
				if (!this.m_IsSingle)
				{
					TList multiple = this.m_Multiple;
					return multiple[index];
				}
				if (index < 0 || index > 1)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Single;
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00054F30 File Offset: 0x00053130
		public OneOrMore(TValue single)
		{
			this.m_IsSingle = true;
			this.m_Single = single;
			this.m_Multiple = default(TList);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00054F4C File Offset: 0x0005314C
		public OneOrMore(TList multiple)
		{
			this.m_IsSingle = false;
			this.m_Single = default(TValue);
			this.m_Multiple = multiple;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00054F68 File Offset: 0x00053168
		public static implicit operator OneOrMore<TValue, TList>(TValue single)
		{
			return new OneOrMore<TValue, TList>(single);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00054F70 File Offset: 0x00053170
		public static implicit operator OneOrMore<TValue, TList>(TList multiple)
		{
			return new OneOrMore<TValue, TList>(multiple);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00054F78 File Offset: 0x00053178
		public IEnumerator<TValue> GetEnumerator()
		{
			return new OneOrMore<TValue, TList>.Enumerator
			{
				m_List = this
			};
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00054F8B File Offset: 0x0005318B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000731 RID: 1841
		private readonly bool m_IsSingle;

		// Token: 0x04000732 RID: 1842
		private readonly TValue m_Single;

		// Token: 0x04000733 RID: 1843
		private readonly TList m_Multiple;

		// Token: 0x02000251 RID: 593
		private class Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06001671 RID: 5745 RVA: 0x0006743A File Offset: 0x0006563A
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Count;
			}

			// Token: 0x06001672 RID: 5746 RVA: 0x00067460 File Offset: 0x00065660
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06001673 RID: 5747 RVA: 0x00067469 File Offset: 0x00065669
			public TValue Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06001674 RID: 5748 RVA: 0x0006747C File Offset: 0x0006567C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001675 RID: 5749 RVA: 0x00067489 File Offset: 0x00065689
			public void Dispose()
			{
			}

			// Token: 0x04000CA7 RID: 3239
			internal int m_Index = -1;

			// Token: 0x04000CA8 RID: 3240
			internal OneOrMore<TValue, TList> m_List;
		}
	}
}
