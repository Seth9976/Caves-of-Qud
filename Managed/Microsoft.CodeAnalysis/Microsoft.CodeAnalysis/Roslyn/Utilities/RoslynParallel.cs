using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Roslyn.Utilities
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RoslynParallel
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00006FAC File Offset: 0x000051AC
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body, CancellationToken cancellationToken)
		{
			RoslynParallel.<>c__DisplayClass1_0 CS$<>8__locals1 = new RoslynParallel.<>c__DisplayClass1_0();
			CS$<>8__locals1.body = body;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			ParallelOptions parallelOptions;
			if (!CS$<>8__locals1.cancellationToken.CanBeCanceled)
			{
				parallelOptions = RoslynParallel.DefaultParallelOptions;
			}
			else
			{
				(parallelOptions = new ParallelOptions()).CancellationToken = CS$<>8__locals1.cancellationToken;
			}
			ParallelOptions parallelOptions2 = parallelOptions;
			return Parallel.For(fromInclusive, toExclusive, parallelOptions2, new Action<int>(CS$<>8__locals1.<For>g__errorHandlingBody|0));
		}

		// Token: 0x04000086 RID: 134
		internal static readonly ParallelOptions DefaultParallelOptions = new ParallelOptions();
	}
}
