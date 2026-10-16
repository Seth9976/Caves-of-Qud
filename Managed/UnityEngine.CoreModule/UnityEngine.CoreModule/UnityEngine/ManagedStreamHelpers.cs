using System;
using System.IO;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002A0 RID: 672
	internal static class ManagedStreamHelpers
	{
		// Token: 0x06002148 RID: 8520 RVA: 0x000409C8 File Offset: 0x0003EBC8
		internal static void ValidateLoadFromStream(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			bool flag2 = !stream.CanRead;
			if (flag2)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			bool flag3 = !stream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00040A28 File Offset: 0x0003EC28
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamRead(byte[] buffer, int offset, int count, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(int*)(void*)returnValueAddress = stream.Read(buffer, offset, count);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00040A70 File Offset: 0x0003EC70
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamSeek(long offset, uint origin, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)(void*)returnValueAddress = stream.Seek(offset, (SeekOrigin)origin);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00040AB4 File Offset: 0x0003ECB4
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamLength(Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)(void*)returnValueAddress = stream.Length;
		}
	}
}
