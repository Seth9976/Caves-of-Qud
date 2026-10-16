using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005F2 RID: 1522
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal readonly struct VisualNodeChildrenData : IEnumerable<VisualNodeHandle>, IEnumerable
	{
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000A1CCD File Offset: 0x0009FECD
		public int Count
		{
			get
			{
				return this.m_Alloc.IsCreated ? this.m_Alloc.Count : this.m_Fixed.Count;
			}
		}

		// Token: 0x17000B20 RID: 2848
		public VisualNodeHandle this[int index]
		{
			get
			{
				return this.m_Alloc.IsCreated ? this.m_Alloc[index] : this.m_Fixed[index];
			}
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000A1D20 File Offset: 0x0009FF20
		public VisualNodeHandle ElementAt(int index)
		{
			return this.m_Alloc.IsCreated ? this.m_Alloc[index] : this.m_Fixed[index];
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000A1D5C File Offset: 0x0009FF5C
		public unsafe VisualNodeHandle* GetUnsafePtr()
		{
			fixed (VisualNodeChildrenData* ptr = &this)
			{
				return (VisualNodeHandle*)ptr;
			}
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000A1D74 File Offset: 0x0009FF74
		public VisualNodeChildrenData.Enumerator GetEnumerator()
		{
			return new VisualNodeChildrenData.Enumerator(this.GetUnsafePtr(), this.Count);
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000A1D87 File Offset: 0x0009FF87
		IEnumerator<VisualNodeHandle> IEnumerable<VisualNodeHandle>.GetEnumerator()
		{
			return new VisualNodeChildrenData.Enumerator(this.GetUnsafePtr(), this.Count);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000A1D87 File Offset: 0x0009FF87
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new VisualNodeChildrenData.Enumerator(this.GetUnsafePtr(), this.Count);
		}

		// Token: 0x04001473 RID: 5235
		[FieldOffset(0)]
		private readonly VisualNodeChildrenFixed m_Fixed;

		// Token: 0x04001474 RID: 5236
		[FieldOffset(0)]
		private readonly VisualNodeChildrenAlloc m_Alloc;

		// Token: 0x020005F3 RID: 1523
		public struct Enumerator : IEnumerator<VisualNodeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06002D7C RID: 11644 RVA: 0x000A1D9F File Offset: 0x0009FF9F
			public unsafe Enumerator(VisualNodeHandle* ptr, int count)
			{
				this.m_Ptr = ptr;
				this.m_Count = count;
				this.m_Index = -1;
			}

			// Token: 0x06002D7D RID: 11645 RVA: 0x000A1DB8 File Offset: 0x0009FFB8
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Count;
			}

			// Token: 0x06002D7E RID: 11646 RVA: 0x000A1DE3 File Offset: 0x0009FFE3
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000B21 RID: 2849
			// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000A1DED File Offset: 0x0009FFED
			public unsafe VisualNodeHandle Current
			{
				get
				{
					return this.m_Ptr[this.m_Index];
				}
			}

			// Token: 0x17000B22 RID: 2850
			// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000A1E09 File Offset: 0x000A0009
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002D81 RID: 11649 RVA: 0x000A1E16 File Offset: 0x000A0016
			public void Dispose()
			{
				this.m_Ptr = null;
			}

			// Token: 0x04001475 RID: 5237
			private unsafe VisualNodeHandle* m_Ptr;

			// Token: 0x04001476 RID: 5238
			private int m_Count;

			// Token: 0x04001477 RID: 5239
			private int m_Index;
		}
	}
}
