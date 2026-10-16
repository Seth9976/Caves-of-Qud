using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000200 RID: 512
	internal static class ObjectDisplayExtensions
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x00040E0C File Offset: 0x0003F00C
		internal static bool IncludesOption(this ObjectDisplayOptions options, ObjectDisplayOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
