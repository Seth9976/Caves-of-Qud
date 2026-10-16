using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200003A RID: 58
	internal static class PEStreamOptionsExtensions
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00006D38 File Offset: 0x00004F38
		public static bool IsValid(this PEStreamOptions options)
		{
			return (options & ~(PEStreamOptions.LeaveOpen | PEStreamOptions.PrefetchMetadata | PEStreamOptions.PrefetchEntireImage | PEStreamOptions.IsLoadedImage)) == PEStreamOptions.Default;
		}
	}
}
