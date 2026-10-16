using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005F5 RID: 1525
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal readonly struct VisualNodeChildrenAlloc
	{
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000A1ED5 File Offset: 0x000A00D5
		public bool IsCreated
		{
			get
			{
				return (this.m_Reserved & int.MinValue) != 0;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000A1EE6 File Offset: 0x000A00E6
		public int Count
		{
			get
			{
				return this.m_Size;
			}
		}

		// Token: 0x17000B27 RID: 2855
		public unsafe VisualNodeHandle this[int index]
		{
			get
			{
				bool flag = (ulong)index >= (ulong)((long)this.m_Size);
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				return this.GetUnsafePtr()[index];
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000A1F34 File Offset: 0x000A0134
		public unsafe VisualNodeHandle* GetUnsafePtr()
		{
			return (VisualNodeHandle*)this.m_Ptr.ToPointer();
		}

		// Token: 0x0400147D RID: 5245
		private const int k_VisualNodeChildrenIsAllocBit = -2147483648;

		// Token: 0x0400147E RID: 5246
		[FieldOffset(0)]
		private readonly IntPtr m_Ptr;

		// Token: 0x0400147F RID: 5247
		[FieldOffset(8)]
		private readonly int m_Size;

		// Token: 0x04001480 RID: 5248
		[FieldOffset(12)]
		private readonly int m_Capacity;

		// Token: 0x04001481 RID: 5249
		[FieldOffset(16)]
		private readonly int m_Reserved;
	}
}
