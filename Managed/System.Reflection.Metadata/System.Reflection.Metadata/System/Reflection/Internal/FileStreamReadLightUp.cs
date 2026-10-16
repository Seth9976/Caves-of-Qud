using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x0200017F RID: 383
	internal static class FileStreamReadLightUp
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x00023530 File Offset: 0x00021730
		[NullableContext(1)]
		internal static bool IsFileStream(Stream stream)
		{
			return stream is FileStream;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002353C File Offset: 0x0002173C
		[NullableContext(1)]
		[return: Nullable(2)]
		internal static SafeHandle GetSafeFileHandle(Stream stream)
		{
			SafeHandle safeFileHandle;
			try
			{
				safeFileHandle = ((FileStream)stream).SafeFileHandle;
			}
			catch
			{
				return null;
			}
			if (safeFileHandle != null && safeFileHandle.IsInvalid)
			{
				return null;
			}
			return safeFileHandle;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002357C File Offset: 0x0002177C
		internal unsafe static bool TryReadFile([Nullable(1)] Stream stream, byte* buffer, long start, int size)
		{
			if (FileStreamReadLightUp.readFileNotAvailable)
			{
				return false;
			}
			SafeHandle safeFileHandle = FileStreamReadLightUp.GetSafeFileHandle(stream);
			if (safeFileHandle == null)
			{
				return false;
			}
			int num2;
			int num;
			try
			{
				num = Interop.Kernel32.ReadFile(safeFileHandle, buffer, size, out num2, IntPtr.Zero);
			}
			catch
			{
				FileStreamReadLightUp.readFileNotAvailable = true;
				return false;
			}
			return num != 0 && num2 == size;
		}

		// Token: 0x04000961 RID: 2401
		internal static bool readFileNotAvailable = Path.DirectorySeparatorChar != '\\';

		// Token: 0x04000962 RID: 2402
		internal static bool safeFileHandleNotAvailable;
	}
}
