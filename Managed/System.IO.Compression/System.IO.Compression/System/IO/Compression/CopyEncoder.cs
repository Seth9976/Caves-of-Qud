using System;

namespace System.IO.Compression
{
	// Token: 0x02000015 RID: 21
	internal sealed class CopyEncoder
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000361C File Offset: 0x0000181C
		public void GetBlock(DeflateInput input, OutputBuffer output, bool isFinal)
		{
			int num = 0;
			if (input != null)
			{
				num = Math.Min(input.Count, output.FreeBytes - 5 - output.BitsInBuffer);
				if (num > 65531)
				{
					num = 65531;
				}
			}
			if (isFinal)
			{
				output.WriteBits(3, 1U);
			}
			else
			{
				output.WriteBits(3, 0U);
			}
			output.FlushBits();
			this.WriteLenNLen((ushort)num, output);
			if (input != null && num > 0)
			{
				output.WriteBytes(input.Buffer, input.StartIndex, num);
				input.ConsumeBytes(num);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000369C File Offset: 0x0000189C
		private void WriteLenNLen(ushort len, OutputBuffer output)
		{
			output.WriteUInt16(len);
			ushort num = ~len;
			output.WriteUInt16(num);
		}

		// Token: 0x040000B9 RID: 185
		private const int PaddingSize = 5;

		// Token: 0x040000BA RID: 186
		private const int MaxUncompressedBlockSize = 65536;
	}
}
