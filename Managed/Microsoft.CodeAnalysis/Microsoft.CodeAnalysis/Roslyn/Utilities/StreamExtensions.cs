using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000062 RID: 98
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StreamExtensions
	{
		// Token: 0x0600021C RID: 540 RVA: 0x000072F4 File Offset: 0x000054F4
		public static int TryReadAll(this Stream stream, byte[] buffer, int offset, int count)
		{
			int i;
			int num;
			for (i = 0; i < count; i += num)
			{
				num = stream.Read(buffer, offset + i, count - i);
				if (num == 0)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007320 File Offset: 0x00005520
		public static byte[] ReadAllBytes(this Stream stream)
		{
			if (!stream.CanSeek)
			{
				MemoryStream memoryStream = new MemoryStream();
				stream.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
			long num = stream.Length - stream.Position;
			if (num == 0L)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[num];
			int num2 = stream.TryReadAll(array, 0, array.Length);
			Array.Resize<byte>(ref array, num2);
			return array;
		}
	}
}
