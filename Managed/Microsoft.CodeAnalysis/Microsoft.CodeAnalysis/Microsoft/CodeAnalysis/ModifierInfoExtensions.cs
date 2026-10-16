using System;
using System.Collections.Immutable;
using System.Linq;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200019F RID: 415
	internal static class ModifierInfoExtensions
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x0003026C File Offset: 0x0002E46C
		internal static bool AnyRequired<TypeSymbol>(this ImmutableArray<ModifierInfo<TypeSymbol>> modifiers) where TypeSymbol : class
		{
			if (!modifiers.IsDefaultOrEmpty)
			{
				return modifiers.Any<ModifierInfo<TypeSymbol>>((ModifierInfo<TypeSymbol> m) => !m.IsOptional);
			}
			return false;
		}
	}
}
