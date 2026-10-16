using System;
using Microsoft.CodeAnalysis;

namespace Roslyn.Utilities
{
	// Token: 0x02000027 RID: 39
	internal static class CompilerPathUtilities
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000032E2 File Offset: 0x000014E2
		internal static void RequireAbsolutePath(string path, string argumentName)
		{
			if (path == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (!PathUtilities.IsAbsolute(path))
			{
				throw new ArgumentException(CodeAnalysisResources.AbsolutePathExpected, argumentName);
			}
		}
	}
}
