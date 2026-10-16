using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005F9 RID: 1529
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal readonly struct VisualNodeClassDataAlloc
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000A20F1 File Offset: 0x000A02F1
		public bool IsCreated
		{
			get
			{
				return (this.m_Reserved & int.MinValue) != 0;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000A2102 File Offset: 0x000A0302
		public int Count
		{
			get
			{
				return this.m_Size;
			}
		}

		// Token: 0x17000B30 RID: 2864
		public unsafe int this[int index]
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

		// Token: 0x06002D9B RID: 11675 RVA: 0x000A2147 File Offset: 0x000A0347
		public unsafe int* GetUnsafePtr()
		{
			return (int*)this.m_Ptr.ToPointer();
		}

		// Token: 0x04001490 RID: 5264
		private const int k_VisualNodeClassDataIsAllocBit = -2147483648;

		// Token: 0x04001491 RID: 5265
		[FieldOffset(0)]
		private readonly IntPtr m_Ptr;

		// Token: 0x04001492 RID: 5266
		[FieldOffset(8)]
		private readonly int m_Size;

		// Token: 0x04001493 RID: 5267
		[FieldOffset(12)]
		private readonly int m_Capacity;

		// Token: 0x04001494 RID: 5268
		[FieldOffset(16)]
		private readonly int m_Reserved;
	}
}
