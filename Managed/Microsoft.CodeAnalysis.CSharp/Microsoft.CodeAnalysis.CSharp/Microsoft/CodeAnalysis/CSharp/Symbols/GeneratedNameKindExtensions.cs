using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200056D RID: 1389
	internal static class GeneratedNameKindExtensions
	{
		// Token: 0x060074F1 RID: 29937 RVA: 0x001B3F3E File Offset: 0x001B213E
		internal static bool IsTypeName(this GeneratedNameKind kind)
		{
			return kind - GeneratedNameKind.LambdaDisplayClass <= 1 || kind == GeneratedNameKind.DynamicCallSiteContainerType;
		}
	}
}
