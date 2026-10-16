using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E9 RID: 1257
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct SourceSpan
	{
		// Token: 0x0600329B RID: 12955 RVA: 0x0008FD48 File Offset: 0x0008DF48
		public SourceSpan(DebugSourceDocument document, int startLine, int startColumn, int endLine, int endColumn)
		{
			this.StartLine = startLine;
			this.StartColumn = startColumn;
			this.EndLine = endLine;
			this.EndColumn = endColumn;
			this.Document = document;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x0008FD6F File Offset: 0x0008DF6F
		public override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x0008FD76 File Offset: 0x0008DF76
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x0008FD80 File Offset: 0x0008DF80
		private string GetDebuggerDisplay()
		{
			return string.Format("({0}, {1}) - ({2}, {3})", new object[] { this.StartLine, this.StartColumn, this.EndLine, this.EndColumn });
		}

		// Token: 0x04001386 RID: 4998
		public readonly int StartLine;

		// Token: 0x04001387 RID: 4999
		public readonly int StartColumn;

		// Token: 0x04001388 RID: 5000
		public readonly int EndLine;

		// Token: 0x04001389 RID: 5001
		public readonly int EndColumn;

		// Token: 0x0400138A RID: 5002
		public readonly DebugSourceDocument Document;
	}
}
