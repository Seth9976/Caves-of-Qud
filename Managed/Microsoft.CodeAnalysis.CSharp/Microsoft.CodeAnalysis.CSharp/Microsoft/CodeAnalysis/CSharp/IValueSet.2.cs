using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200023A RID: 570
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IValueSet<[global::System.Runtime.CompilerServices.Nullable(2)] T> : IValueSet
	{
		// Token: 0x06002F0A RID: 12042
		IValueSet<T> Intersect(IValueSet<T> other);

		// Token: 0x06002F0B RID: 12043
		IValueSet<T> Union(IValueSet<T> other);

		// Token: 0x06002F0C RID: 12044
		IValueSet<T> Complement();

		// Token: 0x06002F0D RID: 12045
		bool Any(BinaryOperatorKind relation, T value);

		// Token: 0x06002F0E RID: 12046
		bool All(BinaryOperatorKind relation, T value);
	}
}
