using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000C3 RID: 195
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal readonly struct SequencePoint
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x000179F1 File Offset: 0x00015BF1
		public SequencePoint(DebugSourceDocument document, int offset, int startLine, ushort startColumn, int endLine, ushort endColumn)
		{
			this.Offset = offset;
			this.StartLine = startLine;
			this.StartColumn = startColumn;
			this.EndLine = endLine;
			this.EndColumn = endColumn;
			this.Document = document;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00017A20 File Offset: 0x00015C20
		public bool IsHidden
		{
			get
			{
				return this.StartLine == 16707566;
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017A2F File Offset: 0x00015C2F
		public override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017A36 File Offset: 0x00015C36
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00017A40 File Offset: 0x00015C40
		private string GetDebuggerDisplay()
		{
			if (!this.IsHidden)
			{
				return string.Format("{0}: ({1}, {2}) - ({3}, {4})", new object[] { this.Offset, this.StartLine, this.StartColumn, this.EndLine, this.EndColumn });
			}
			return "<hidden>";
		}

		// Token: 0x040001FB RID: 507
		public const int HiddenLine = 16707566;

		// Token: 0x040001FC RID: 508
		public readonly int Offset;

		// Token: 0x040001FD RID: 509
		public readonly int StartLine;

		// Token: 0x040001FE RID: 510
		public readonly int EndLine;

		// Token: 0x040001FF RID: 511
		public readonly ushort StartColumn;

		// Token: 0x04000200 RID: 512
		public readonly ushort EndColumn;

		// Token: 0x04000201 RID: 513
		public readonly DebugSourceDocument Document;
	}
}
