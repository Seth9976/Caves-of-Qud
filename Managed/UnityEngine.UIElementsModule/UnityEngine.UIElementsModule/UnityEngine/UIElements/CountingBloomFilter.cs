using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D2 RID: 466
	internal struct CountingBloomFilter
	{
		// Token: 0x06000E55 RID: 3669 RVA: 0x00040658 File Offset: 0x0003E858
		private unsafe void AdjustSlot(uint index, bool increment)
		{
			if (increment)
			{
				bool flag = *((ref this.m_Counters.FixedElementField) + (UIntPtr)index) != byte.MaxValue;
				if (flag)
				{
					ref byte ptr = (ref this.m_Counters.FixedElementField) + (UIntPtr)index;
					ptr += 1;
				}
			}
			else
			{
				bool flag2 = *((ref this.m_Counters.FixedElementField) + (UIntPtr)index) > 0;
				if (flag2)
				{
					ref byte ptr2 = (ref this.m_Counters.FixedElementField) + (UIntPtr)index;
					ptr2 -= 1;
				}
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000406CC File Offset: 0x0003E8CC
		private uint Hash1(uint hash)
		{
			return hash & 16383U;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000406E8 File Offset: 0x0003E8E8
		private uint Hash2(uint hash)
		{
			return (hash >> 14) & 16383U;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00040704 File Offset: 0x0003E904
		private unsafe bool IsSlotEmpty(uint index)
		{
			return *((ref this.m_Counters.FixedElementField) + (UIntPtr)index) == 0;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00040728 File Offset: 0x0003E928
		public void InsertHash(uint hash)
		{
			this.AdjustSlot(this.Hash1(hash), true);
			this.AdjustSlot(this.Hash2(hash), true);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00040749 File Offset: 0x0003E949
		public void RemoveHash(uint hash)
		{
			this.AdjustSlot(this.Hash1(hash), false);
			this.AdjustSlot(this.Hash2(hash), false);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0004076C File Offset: 0x0003E96C
		public bool ContainsHash(uint hash)
		{
			return !this.IsSlotEmpty(this.Hash1(hash)) && !this.IsSlotEmpty(this.Hash2(hash));
		}

		// Token: 0x040008E8 RID: 2280
		private const int KEY_SIZE = 14;

		// Token: 0x040008E9 RID: 2281
		private const uint ARRAY_SIZE = 16384U;

		// Token: 0x040008EA RID: 2282
		private const int KEY_MASK = 16383;

		// Token: 0x040008EB RID: 2283
		[FixedBuffer(typeof(byte), 16384)]
		private CountingBloomFilter.<m_Counters>e__FixedBuffer m_Counters;

		// Token: 0x020001D3 RID: 467
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 16384)]
		public struct <m_Counters>e__FixedBuffer
		{
			// Token: 0x040008EC RID: 2284
			public byte FixedElementField;
		}
	}
}
