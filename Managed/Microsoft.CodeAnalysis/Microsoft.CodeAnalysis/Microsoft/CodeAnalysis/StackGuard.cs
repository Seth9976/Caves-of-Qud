using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000197 RID: 407
	internal static class StackGuard
	{
		// Token: 0x06001076 RID: 4214 RVA: 0x000301CC File Offset: 0x0002E3CC
		[DebuggerStepThrough]
		public static void EnsureSufficientExecutionStack(int recursionDepth)
		{
			if (recursionDepth > 20)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
			}
		}

		// Token: 0x0400050B RID: 1291
		public const int MaxUncheckedRecursionDepth = 20;
	}
}
