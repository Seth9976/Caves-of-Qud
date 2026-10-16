using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200004E RID: 78
	internal enum LambdaConversionResult
	{
		// Token: 0x0400011B RID: 283
		Success,
		// Token: 0x0400011C RID: 284
		BadTargetType,
		// Token: 0x0400011D RID: 285
		BadParameterCount,
		// Token: 0x0400011E RID: 286
		MissingSignatureWithOutParameter,
		// Token: 0x0400011F RID: 287
		MismatchedParameterType,
		// Token: 0x04000120 RID: 288
		RefInImplicitlyTypedLambda,
		// Token: 0x04000121 RID: 289
		StaticTypeInImplicitlyTypedLambda,
		// Token: 0x04000122 RID: 290
		ExpressionTreeMustHaveDelegateTypeArgument,
		// Token: 0x04000123 RID: 291
		ExpressionTreeFromAnonymousMethod,
		// Token: 0x04000124 RID: 292
		BindingFailed
	}
}
