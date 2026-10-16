using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004D RID: 77
	internal static class MetadataStreamOptionsExtensions
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00008F8E File Offset: 0x0000718E
		public static bool IsValid(this MetadataStreamOptions options)
		{
			return (options & ~(MetadataStreamOptions.LeaveOpen | MetadataStreamOptions.PrefetchMetadata)) == MetadataStreamOptions.Default;
		}
	}
}
