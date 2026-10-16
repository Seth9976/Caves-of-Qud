using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000062 RID: 98
	internal struct DynamicBitfield
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x00037224 File Offset: 0x00035424
		public void SetLength(int newLength)
		{
			int num = DynamicBitfield.BitCountToULongCount(newLength);
			if (this.array.length < num)
			{
				this.array.SetLength(num);
			}
			this.length = newLength;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0003725C File Offset: 0x0003545C
		public void SetBit(int bitIndex)
		{
			ref InlinedArray<ulong> ptr = ref this.array;
			int num = bitIndex / 64;
			ptr[num] |= 1UL << bitIndex % 64;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003728E File Offset: 0x0003548E
		public bool TestBit(int bitIndex)
		{
			return (this.array[bitIndex / 64] & (1UL << bitIndex % 64)) > 0UL;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000372B0 File Offset: 0x000354B0
		public void ClearBit(int bitIndex)
		{
			ref InlinedArray<ulong> ptr = ref this.array;
			int num = bitIndex / 64;
			ptr[num] &= ~(1UL << bitIndex % 64);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000372E4 File Offset: 0x000354E4
		public bool AnyBitIsSet()
		{
			for (int i = 0; i < this.array.length; i++)
			{
				if (this.array[i] != 0UL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00037318 File Offset: 0x00035518
		private static int BitCountToULongCount(int bitCount)
		{
			return (bitCount + 63) / 64;
		}

		// Token: 0x0400033E RID: 830
		public InlinedArray<ulong> array;

		// Token: 0x0400033F RID: 831
		public int length;
	}
}
