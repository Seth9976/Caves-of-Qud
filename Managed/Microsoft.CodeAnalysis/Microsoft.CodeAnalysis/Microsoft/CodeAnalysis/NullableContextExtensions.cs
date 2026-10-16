using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000145 RID: 325
	public static class NullableContextExtensions
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x00026ED4 File Offset: 0x000250D4
		private static bool IsFlagSet(NullableContext context, NullableContext flag)
		{
			return (context & flag) == flag;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00026EDC File Offset: 0x000250DC
		public static bool WarningsEnabled(this NullableContext context)
		{
			return NullableContextExtensions.IsFlagSet(context, NullableContext.WarningsEnabled);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00026EE5 File Offset: 0x000250E5
		public static bool AnnotationsEnabled(this NullableContext context)
		{
			return NullableContextExtensions.IsFlagSet(context, NullableContext.AnnotationsEnabled);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00026EEE File Offset: 0x000250EE
		public static bool WarningsInherited(this NullableContext context)
		{
			return NullableContextExtensions.IsFlagSet(context, NullableContext.WarningsContextInherited);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00026EF7 File Offset: 0x000250F7
		public static bool AnnotationsInherited(this NullableContext context)
		{
			return NullableContextExtensions.IsFlagSet(context, NullableContext.AnnotationsContextInherited);
		}
	}
}
