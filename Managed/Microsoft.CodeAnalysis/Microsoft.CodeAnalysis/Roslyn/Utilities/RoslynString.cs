using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200005D RID: 93
	[NullableContext(2)]
	[Nullable(0)]
	internal static class RoslynString
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00007013 File Offset: 0x00005213
		public static bool IsNullOrEmpty([NotNullWhen(false)] string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000701B File Offset: 0x0000521B
		public static bool IsNullOrWhiteSpace([NotNullWhen(false)] string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}
	}
}
