using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000242 RID: 578
	[NullableContext(1)]
	public interface ILabelSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600163B RID: 5691
		IMethodSymbol ContainingMethod { get; }
	}
}
