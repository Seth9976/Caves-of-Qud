using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000147 RID: 327
	public static class NullableContextOptionsExtensions
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00026F00 File Offset: 0x00025100
		private static bool IsFlagSet(NullableContextOptions context, NullableContextOptions flag)
		{
			return (context & flag) == flag;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00026F08 File Offset: 0x00025108
		public static bool WarningsEnabled(this NullableContextOptions context)
		{
			return NullableContextOptionsExtensions.IsFlagSet(context, NullableContextOptions.Warnings);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00026F11 File Offset: 0x00025111
		public static bool AnnotationsEnabled(this NullableContextOptions context)
		{
			return NullableContextOptionsExtensions.IsFlagSet(context, NullableContextOptions.Annotations);
		}
	}
}
