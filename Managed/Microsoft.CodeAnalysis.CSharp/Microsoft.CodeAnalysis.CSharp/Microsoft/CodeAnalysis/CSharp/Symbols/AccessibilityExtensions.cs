using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004BD RID: 1213
	internal static class AccessibilityExtensions
	{
		// Token: 0x060067C1 RID: 26561 RVA: 0x00186DD3 File Offset: 0x00184FD3
		public static bool HasProtected(this Accessibility accessibility)
		{
			return accessibility - Accessibility.ProtectedAndInternal <= 1 || accessibility == Accessibility.ProtectedOrInternal;
		}
	}
}
