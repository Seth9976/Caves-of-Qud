using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C3 RID: 963
	[NullableContext(1)]
	public interface IPatternOperation : IOperation
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060025BE RID: 9662
		ITypeSymbol InputType { get; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060025BF RID: 9663
		ITypeSymbol NarrowedType { get; }
	}
}
