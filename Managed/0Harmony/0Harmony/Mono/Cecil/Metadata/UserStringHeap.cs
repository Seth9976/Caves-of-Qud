using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002B0 RID: 688
	internal sealed class UserStringHeap : StringHeap
	{
		// Token: 0x0600112E RID: 4398 RVA: 0x00037598 File Offset: 0x00035798
		public UserStringHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000375A4 File Offset: 0x000357A4
		protected override string ReadStringAt(uint index)
		{
			int num = (int)index;
			uint num2 = (uint)((ulong)this.data.ReadCompressedUInt32(ref num) & 18446744073709551614UL);
			if (num2 < 1U)
			{
				return string.Empty;
			}
			char[] array = new char[num2 / 2U];
			int num3 = num;
			int num4 = 0;
			while ((long)num3 < (long)num + (long)((ulong)num2))
			{
				array[num4++] = (char)((int)this.data[num3] | ((int)this.data[num3 + 1] << 8));
				num3 += 2;
			}
			return new string(array);
		}
	}
}
