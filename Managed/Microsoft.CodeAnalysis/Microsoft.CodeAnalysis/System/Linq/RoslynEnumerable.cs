using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Collections.Internal;

namespace System.Linq
{
	// Token: 0x0200001E RID: 30
	internal static class RoslynEnumerable
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002EBC File Offset: 0x000010BC
		[NullableContext(1)]
		public static SegmentedList<TSource> ToSegmentedList<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			return new SegmentedList<TSource>(source);
		}
	}
}
