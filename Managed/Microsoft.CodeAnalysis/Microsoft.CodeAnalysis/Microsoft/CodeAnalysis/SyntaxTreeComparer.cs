using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000285 RID: 645
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxTreeComparer : IEqualityComparer<SyntaxTree>
	{
		// Token: 0x06001A78 RID: 6776 RVA: 0x0004E820 File Offset: 0x0004CA20
		[NullableContext(2)]
		public bool Equals(SyntaxTree x, SyntaxTree y)
		{
			if (x == null)
			{
				return y == null;
			}
			return y != null && string.Equals(x.FilePath, y.FilePath, 5) && SourceTextComparer.Instance.Equals(x.GetText(default(CancellationToken)), y.GetText(default(CancellationToken)));
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0004E878 File Offset: 0x0004CA78
		public int GetHashCode(SyntaxTree obj)
		{
			return Hash.Combine(obj.FilePath.GetHashCode(), SourceTextComparer.Instance.GetHashCode(obj.GetText(default(CancellationToken))));
		}

		// Token: 0x04000A39 RID: 2617
		public static readonly SyntaxTreeComparer Instance = new SyntaxTreeComparer();
	}
}
