using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A6 RID: 1446
	internal static class TypeParameterBoundsExtensions
	{
		// Token: 0x060078C8 RID: 30920 RVA: 0x001B9C5A File Offset: 0x001B7E5A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static bool IsSet(this TypeParameterBounds boundsOpt)
		{
			return boundsOpt != TypeParameterBounds.Unset;
		}
	}
}
