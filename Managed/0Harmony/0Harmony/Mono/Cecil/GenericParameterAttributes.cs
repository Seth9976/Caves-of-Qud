using System;

namespace Mono.Cecil
{
	// Token: 0x02000210 RID: 528
	[Flags]
	public enum GenericParameterAttributes : ushort
	{
		// Token: 0x0400033F RID: 831
		VarianceMask = 3,
		// Token: 0x04000340 RID: 832
		NonVariant = 0,
		// Token: 0x04000341 RID: 833
		Covariant = 1,
		// Token: 0x04000342 RID: 834
		Contravariant = 2,
		// Token: 0x04000343 RID: 835
		SpecialConstraintMask = 28,
		// Token: 0x04000344 RID: 836
		ReferenceTypeConstraint = 4,
		// Token: 0x04000345 RID: 837
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04000346 RID: 838
		DefaultConstructorConstraint = 16
	}
}
