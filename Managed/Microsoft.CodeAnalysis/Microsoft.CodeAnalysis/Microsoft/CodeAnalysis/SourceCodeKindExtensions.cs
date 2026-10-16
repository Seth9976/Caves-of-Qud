using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D6 RID: 470
	internal static class SourceCodeKindExtensions
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0003D88C File Offset: 0x0003BA8C
		internal static SourceCodeKind MapSpecifiedToEffectiveKind(this SourceCodeKind kind)
		{
			if (kind != SourceCodeKind.Regular && kind - SourceCodeKind.Script <= 1)
			{
				return SourceCodeKind.Script;
			}
			return SourceCodeKind.Regular;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0003D89A File Offset: 0x0003BA9A
		internal static bool IsValid(this SourceCodeKind value)
		{
			return value >= SourceCodeKind.Regular && value <= SourceCodeKind.Script;
		}
	}
}
