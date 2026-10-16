using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200025B RID: 603
	internal static class NullableFlowStateExtensions
	{
		// Token: 0x06001720 RID: 5920 RVA: 0x0004597C File Offset: 0x00043B7C
		public static NullableAnnotation ToAnnotation(this NullableFlowState nullableFlowState)
		{
			if (nullableFlowState == NullableFlowState.NotNull)
			{
				return NullableAnnotation.NotAnnotated;
			}
			if (nullableFlowState == NullableFlowState.MaybeNull)
			{
				return NullableAnnotation.Annotated;
			}
			return NullableAnnotation.None;
		}
	}
}
