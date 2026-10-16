using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009E RID: 158
	internal static class HandleKindExtensions
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0000D10D File Offset: 0x0000B30D
		internal static bool IsHeapHandle(this HandleKind kind)
		{
			return kind >= HandleKind.NamespaceDefinition;
		}
	}
}
