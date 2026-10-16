using System;
using System.Text;

namespace NAudio.Utils
{
	// Token: 0x02000068 RID: 104
	public static class ByteArrayExtensions
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00007540 File Offset: 0x00005740
		public static bool IsEntirelyNull(byte[] buffer)
		{
			foreach (byte b in buffer)
			{
				if (b != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000756C File Offset: 0x0000576C
		public static string DescribeAsHex(byte[] buffer, string separator, int bytesPerLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (byte b in buffer)
			{
				stringBuilder.AppendFormat("{0:X2}{1}", b, separator);
				if (++num % bytesPerLine == 0)
				{
					stringBuilder.Append("\r\n");
				}
			}
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000075D4 File Offset: 0x000057D4
		public static string DecodeAsString(byte[] buffer, int offset, int length, Encoding encoding)
		{
			for (int i = 0; i < length; i++)
			{
				if (buffer[offset + i] == 0)
				{
					length = i;
				}
			}
			return encoding.GetString(buffer, offset, length);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007600 File Offset: 0x00005800
		public static byte[] Concat(params byte[][] byteArrays)
		{
			int num = 0;
			foreach (byte[] array in byteArrays)
			{
				num += array.Length;
			}
			if (num <= 0)
			{
				return new byte[0];
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			foreach (byte[] array3 in byteArrays)
			{
				Array.Copy(array3, 0, array2, num2, array3.Length);
				num2 += array3.Length;
			}
			return array2;
		}
	}
}
