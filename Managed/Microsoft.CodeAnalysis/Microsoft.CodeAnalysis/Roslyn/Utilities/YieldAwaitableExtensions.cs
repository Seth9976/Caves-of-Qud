using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200006F RID: 111
	internal static class YieldAwaitableExtensions
	{
		// Token: 0x06000298 RID: 664 RVA: 0x000095B8 File Offset: 0x000077B8
		public static ConfiguredYieldAwaitable ConfigureAwait(this YieldAwaitable awaitable, bool continueOnCapturedContext)
		{
			return new ConfiguredYieldAwaitable(awaitable, continueOnCapturedContext);
		}
	}
}
