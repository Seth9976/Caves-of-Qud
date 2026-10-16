using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005F6 RID: 1526
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal readonly struct VisualNodeClassData : IEnumerable<int>, IEnumerable
	{
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000A1F41 File Offset: 0x000A0141
		public int Count
		{
			get
			{
				return this.m_Alloc.IsCreated ? this.m_Alloc.Count : this.m_Fixed.Count;
			}
		}

		// Token: 0x17000B29 RID: 2857
		public int this[int index]
		{
			get
			{
				return this.m_Alloc.IsCreated ? this.m_Alloc[index] : this.m_Fixed[index];
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000A1F94 File Offset: 0x000A0194
		public unsafe int* GetUnsafePtr()
		{
			fixed (VisualNodeClassData* ptr = &this)
			{
				return (int*)ptr;
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000A1FAC File Offset: 0x000A01AC
		public VisualNodeClassData.Enumerator GetEnumerator()
		{
			return new VisualNodeClassData.Enumerator(this.GetUnsafePtr(), this.Count);
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000A1FBF File Offset: 0x000A01BF
		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			return new VisualNodeClassData.Enumerator(this.GetUnsafePtr(), this.Count);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000A1FBF File Offset: 0x000A01BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new VisualNodeClassData.Enumerator(this.GetUnsafePtr(), this.Count);
		}

		// Token: 0x04001482 RID: 5250
		[FieldOffset(0)]
		private readonly VisualNodeClassDataFixed m_Fixed;

		// Token: 0x04001483 RID: 5251
		[FieldOffset(0)]
		private readonly VisualNodeClassDataAlloc m_Alloc;

		// Token: 0x020005F7 RID: 1527
		public struct Enumerator : IEnumerator<int>, IEnumerator, IDisposable
		{
			// Token: 0x06002D8F RID: 11663 RVA: 0x000A1FD7 File Offset: 0x000A01D7
			public unsafe Enumerator(int* ptr, int count)
			{
				this.m_Ptr = ptr;
				this.m_Count = count;
				this.m_Index = -1;
			}

			// Token: 0x06002D90 RID: 11664 RVA: 0x000A1FF0 File Offset: 0x000A01F0
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Count;
			}

			// Token: 0x06002D91 RID: 11665 RVA: 0x000A201B File Offset: 0x000A021B
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000B2A RID: 2858
			// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000A2025 File Offset: 0x000A0225
			public unsafe int Current
			{
				get
				{
					return this.m_Ptr[this.m_Index];
				}
			}

			// Token: 0x17000B2B RID: 2859
			// (get) Token: 0x06002D93 RID: 11667 RVA: 0x000A2038 File Offset: 0x000A0238
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002D94 RID: 11668 RVA: 0x000A2045 File Offset: 0x000A0245
			public void Dispose()
			{
				this.m_Ptr = null;
			}

			// Token: 0x04001484 RID: 5252
			private unsafe int* m_Ptr;

			// Token: 0x04001485 RID: 5253
			private int m_Count;

			// Token: 0x04001486 RID: 5254
			private int m_Index;
		}
	}
}
