using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200023C RID: 572
	[NullableContext(1)]
	public interface IDiscardSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001620 RID: 5664
		ITypeSymbol Type { get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001621 RID: 5665
		NullableAnnotation NullableAnnotation { get; }
	}
}
