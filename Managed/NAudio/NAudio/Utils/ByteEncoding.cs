using System;
using System.Text;

namespace NAudio.Utils
{
	// Token: 0x0200010F RID: 271
	public class ByteEncoding : Encoding
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00012EBE File Offset: 0x000110BE
		private ByteEncoding()
		{
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00012EC6 File Offset: 0x000110C6
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00012ECC File Offset: 0x000110CC
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				bytes[byteIndex + i] = (byte)chars[charIndex + i];
			}
			return charCount;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00012EF4 File Offset: 0x000110F4
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (bytes[index + i] == 0)
				{
					return i;
				}
			}
			return count;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00012F18 File Offset: 0x00011118
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				byte b = bytes[byteIndex + i];
				if (b == 0)
				{
					return i;
				}
				chars[charIndex + i] = (char)b;
			}
			return byteCount;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00012F45 File Offset: 0x00011145
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00012F48 File Offset: 0x00011148
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x0400069D RID: 1693
		public static readonly ByteEncoding Instance = new ByteEncoding();
	}
}
