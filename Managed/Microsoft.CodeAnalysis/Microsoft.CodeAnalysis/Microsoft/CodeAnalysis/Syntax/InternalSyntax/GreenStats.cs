using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B6 RID: 694
	internal class GreenStats
	{
		// Token: 0x06001D72 RID: 7538 RVA: 0x00055D06 File Offset: 0x00053F06
		[NullableContext(1)]
		internal static void NoteGreen(GreenNode node)
		{
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00055D08 File Offset: 0x00053F08
		[Conditional("DEBUG")]
		internal static void ItemAdded()
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00055D0A File Offset: 0x00053F0A
		[Conditional("DEBUG")]
		internal static void ItemCacheable()
		{
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00055D0C File Offset: 0x00053F0C
		[Conditional("DEBUG")]
		internal static void CacheHit()
		{
		}
	}
}
