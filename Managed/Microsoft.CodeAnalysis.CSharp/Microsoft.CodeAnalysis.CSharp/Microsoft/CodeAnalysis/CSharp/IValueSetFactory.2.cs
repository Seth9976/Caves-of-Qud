using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200023C RID: 572
	internal interface IValueSetFactory<[global::System.Runtime.CompilerServices.Nullable(2)] T> : IValueSetFactory
	{
		// Token: 0x06002F15 RID: 12053
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		IValueSet<T> Related(BinaryOperatorKind relation, T value);
	}
}
