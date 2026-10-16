using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000185 RID: 389
	[NullableContext(1)]
	[Nullable(0)]
	internal static class MemoryMapLightUp
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000243A8 File Offset: 0x000225A8
		internal static bool IsAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000243AB File Offset: 0x000225AB
		internal static IDisposable CreateMemoryMap(Stream stream)
		{
			return MemoryMappedFile.CreateFromFile((FileStream)stream, null, 0L, 1, 0, true);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000243C0 File Offset: 0x000225C0
		internal static IDisposable CreateViewAccessor(object memoryMap, long start, int size)
		{
			IDisposable disposable;
			try
			{
				disposable = ((MemoryMappedFile)memoryMap).CreateViewAccessor(start, (long)size, 1);
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new IOException(ex.Message, ex);
			}
			return disposable;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00024400 File Offset: 0x00022600
		internal static bool TryGetSafeBufferAndPointerOffset(object accessor, out SafeBuffer safeBuffer, out long offset)
		{
			MemoryMappedViewAccessor memoryMappedViewAccessor = (MemoryMappedViewAccessor)accessor;
			safeBuffer = memoryMappedViewAccessor.SafeMemoryMappedViewHandle;
			offset = memoryMappedViewAccessor.PointerOffset;
			return true;
		}
	}
}
