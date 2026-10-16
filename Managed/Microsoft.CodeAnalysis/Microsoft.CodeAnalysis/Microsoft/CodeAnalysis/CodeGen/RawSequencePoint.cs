using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004DE RID: 1246
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal readonly struct RawSequencePoint
	{
		// Token: 0x06003245 RID: 12869 RVA: 0x0008E9C6 File Offset: 0x0008CBC6
		internal RawSequencePoint(SyntaxTree syntaxTree, int ilMarker, TextSpan span)
		{
			this.SyntaxTree = syntaxTree;
			this.ILMarker = ilMarker;
			this.Span = span;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x0008E9E0 File Offset: 0x0008CBE0
		private string GetDebuggerDisplay()
		{
			return string.Format("#{0}: {1}", this.ILMarker, (this.Span == RawSequencePoint.HiddenSequencePointSpan) ? "hidden" : this.Span.ToString());
		}

		// Token: 0x0400135B RID: 4955
		internal readonly SyntaxTree SyntaxTree;

		// Token: 0x0400135C RID: 4956
		internal readonly int ILMarker;

		// Token: 0x0400135D RID: 4957
		internal readonly TextSpan Span;

		// Token: 0x0400135E RID: 4958
		internal static readonly TextSpan HiddenSequencePointSpan = new TextSpan(int.MaxValue, 0);
	}
}
