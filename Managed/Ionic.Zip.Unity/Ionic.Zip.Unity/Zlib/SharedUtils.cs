using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000065 RID: 101
	internal class SharedUtils
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0001F91A File Offset: 0x0001DB1A
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001F924 File Offset: 0x0001DB24
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001F966 File Offset: 0x0001DB66
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001F973 File Offset: 0x0001DB73
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
