using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200023B RID: 571
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IValueSetFactory
	{
		// Token: 0x06002F0F RID: 12047
		IValueSet Related(BinaryOperatorKind relation, ConstantValue value);

		// Token: 0x06002F10 RID: 12048
		bool Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right);

		// Token: 0x06002F11 RID: 12049
		IValueSet Random(int expectedSize, Random random);

		// Token: 0x06002F12 RID: 12050
		ConstantValue RandomValue(Random random);

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002F13 RID: 12051
		IValueSet AllValues { get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002F14 RID: 12052
		IValueSet NoValues { get; }
	}
}
