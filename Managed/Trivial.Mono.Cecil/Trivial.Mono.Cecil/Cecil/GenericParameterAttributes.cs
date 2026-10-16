using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000045 RID: 69
	[Flags]
	public enum GenericParameterAttributes : ushort
	{
		// Token: 0x04000094 RID: 148
		VarianceMask = 3,
		// Token: 0x04000095 RID: 149
		NonVariant = 0,
		// Token: 0x04000096 RID: 150
		Covariant = 1,
		// Token: 0x04000097 RID: 151
		Contravariant = 2,
		// Token: 0x04000098 RID: 152
		SpecialConstraintMask = 28,
		// Token: 0x04000099 RID: 153
		ReferenceTypeConstraint = 4,
		// Token: 0x0400009A RID: 154
		NotNullableValueTypeConstraint = 8,
		// Token: 0x0400009B RID: 155
		DefaultConstructorConstraint = 16
	}
}
