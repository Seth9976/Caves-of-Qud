using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000239 RID: 569
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IValueSet
	{
		// Token: 0x06002F03 RID: 12035
		IValueSet Intersect(IValueSet other);

		// Token: 0x06002F04 RID: 12036
		IValueSet Union(IValueSet other);

		// Token: 0x06002F05 RID: 12037
		IValueSet Complement();

		// Token: 0x06002F06 RID: 12038
		bool Any(BinaryOperatorKind relation, ConstantValue value);

		// Token: 0x06002F07 RID: 12039
		bool All(BinaryOperatorKind relation, ConstantValue value);

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002F08 RID: 12040
		bool IsEmpty { get; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002F09 RID: 12041
		[global::System.Runtime.CompilerServices.Nullable(2)]
		ConstantValue Sample
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}
	}
}
