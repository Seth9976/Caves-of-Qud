using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000196 RID: 406
	internal static class SpanUtilities
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x00030190 File Offset: 0x0002E390
		[NullableContext(1)]
		public unsafe static bool All<[Nullable(2)] TElement, [Nullable(2)] TParam>([Nullable(new byte[] { 0, 1 })] this ReadOnlySpan<TElement> span, TParam param, Func<TElement, TParam, bool> predicate)
		{
			ReadOnlySpan<TElement> readOnlySpan = span;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				TElement telement = *readOnlySpan[i];
				if (!predicate.Invoke(telement, param))
				{
					return false;
				}
			}
			return true;
		}
	}
}
