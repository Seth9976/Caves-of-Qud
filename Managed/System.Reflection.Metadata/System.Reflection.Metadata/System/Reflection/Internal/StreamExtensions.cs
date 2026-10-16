using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000189 RID: 393
	internal static class StreamExtensions
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x00024754 File Offset: 0x00022954
		internal unsafe static void CopyTo([Nullable(1)] this Stream source, byte* destination, int size)
		{
			byte[] array = new byte[Math.Min(81920, size)];
			while (size > 0)
			{
				int num = Math.Min(size, array.Length);
				int num2 = source.Read(array, 0, num);
				if (num2 <= 0 || num2 > num)
				{
					throw new IOException(SR.UnexpectedStreamEnd);
				}
				Marshal.Copy(array, 0, (IntPtr)((void*)destination), num2);
				destination += num2;
				size -= num2;
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000247B8 File Offset: 0x000229B8
		[NullableContext(1)]
		internal static int TryReadAll(this Stream stream, byte[] buffer, int offset, int count)
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

		// Token: 0x06000C16 RID: 3094 RVA: 0x000247E8 File Offset: 0x000229E8
		[NullableContext(1)]
		internal static int GetAndValidateSize(Stream stream, int size, string streamParameterName)
		{
			long num = stream.Length - stream.Position;
			if (size < 0 || (long)size > num)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size != 0)
			{
				return size;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentException(SR.StreamTooLarge, streamParameterName);
			}
			return (int)num;
		}

		// Token: 0x04000971 RID: 2417
		internal const int StreamCopyBufferSize = 81920;
	}
}
