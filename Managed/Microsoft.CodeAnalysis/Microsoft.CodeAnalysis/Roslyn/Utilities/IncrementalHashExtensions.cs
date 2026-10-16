using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Roslyn.Utilities
{
	// Token: 0x02000049 RID: 73
	internal static class IncrementalHashExtensions
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00006150 File Offset: 0x00004350
		[NullableContext(1)]
		internal static void AppendData(this IncrementalHash hash, IEnumerable<Blob> blobs)
		{
			foreach (Blob blob in blobs)
			{
				hash.AppendData(blob.GetBytes());
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000061A0 File Offset: 0x000043A0
		[NullableContext(1)]
		internal static void AppendData(this IncrementalHash hash, [Nullable(new byte[] { 1, 0 })] IEnumerable<ArraySegment<byte>> blobs)
		{
			foreach (ArraySegment<byte> arraySegment in blobs)
			{
				hash.AppendData(arraySegment);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000061E8 File Offset: 0x000043E8
		internal static void AppendData([Nullable(1)] this IncrementalHash hash, ArraySegment<byte> segment)
		{
			hash.AppendData(segment.Array, segment.Offset, segment.Count);
		}
	}
}
