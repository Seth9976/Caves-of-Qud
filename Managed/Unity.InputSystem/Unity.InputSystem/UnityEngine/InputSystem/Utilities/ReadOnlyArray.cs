using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000147 RID: 327
	public struct ReadOnlyArray<TValue> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue>
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x00056127 File Offset: 0x00054327
		public ReadOnlyArray(TValue[] array)
		{
			this.m_Array = array;
			this.m_StartIndex = 0;
			this.m_Length = ((array != null) ? array.Length : 0);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00056146 File Offset: 0x00054346
		public ReadOnlyArray(TValue[] array, int index, int length)
		{
			this.m_Array = array;
			this.m_StartIndex = index;
			this.m_Length = length;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00056160 File Offset: 0x00054360
		public TValue[] ToArray()
		{
			TValue[] array = new TValue[this.m_Length];
			if (this.m_Length > 0)
			{
				Array.Copy(this.m_Array, this.m_StartIndex, array, 0, this.m_Length);
			}
			return array;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0005619C File Offset: 0x0005439C
		public int IndexOf(Predicate<TValue> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = 0; i < this.m_Length; i++)
			{
				if (predicate(this.m_Array[this.m_StartIndex + i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000561E6 File Offset: 0x000543E6
		public ReadOnlyArray<TValue>.Enumerator GetEnumerator()
		{
			return new ReadOnlyArray<TValue>.Enumerator(this.m_Array, this.m_StartIndex, this.m_Length);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000561FF File Offset: 0x000543FF
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0005620C File Offset: 0x0005440C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00056219 File Offset: 0x00054419
		public static implicit operator ReadOnlyArray<TValue>(TValue[] array)
		{
			return new ReadOnlyArray<TValue>(array);
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00056221 File Offset: 0x00054421
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170004EE RID: 1262
		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.m_Array == null)
				{
					throw new InvalidOperationException();
				}
				return this.m_Array[this.m_StartIndex + index];
			}
		}

		// Token: 0x04000742 RID: 1858
		internal TValue[] m_Array;

		// Token: 0x04000743 RID: 1859
		internal int m_StartIndex;

		// Token: 0x04000744 RID: 1860
		internal int m_Length;

		// Token: 0x02000252 RID: 594
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06001677 RID: 5751 RVA: 0x0006749A File Offset: 0x0006569A
			internal Enumerator(TValue[] array, int index, int length)
			{
				this.m_Array = array;
				this.m_IndexStart = index - 1;
				this.m_IndexEnd = index + length;
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x06001678 RID: 5752 RVA: 0x000674C1 File Offset: 0x000656C1
			public void Dispose()
			{
			}

			// Token: 0x06001679 RID: 5753 RVA: 0x000674C3 File Offset: 0x000656C3
			public bool MoveNext()
			{
				if (this.m_Index < this.m_IndexEnd)
				{
					this.m_Index++;
				}
				return this.m_Index != this.m_IndexEnd;
			}

			// Token: 0x0600167A RID: 5754 RVA: 0x000674F2 File Offset: 0x000656F2
			public void Reset()
			{
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x0600167B RID: 5755 RVA: 0x00067500 File Offset: 0x00065700
			public TValue Current
			{
				get
				{
					if (this.m_Index == this.m_IndexEnd)
					{
						throw new InvalidOperationException("Iterated beyond end");
					}
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x0600167C RID: 5756 RVA: 0x0006752C File Offset: 0x0006572C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000CA9 RID: 3241
			private readonly TValue[] m_Array;

			// Token: 0x04000CAA RID: 3242
			private readonly int m_IndexStart;

			// Token: 0x04000CAB RID: 3243
			private readonly int m_IndexEnd;

			// Token: 0x04000CAC RID: 3244
			private int m_Index;
		}
	}
}
