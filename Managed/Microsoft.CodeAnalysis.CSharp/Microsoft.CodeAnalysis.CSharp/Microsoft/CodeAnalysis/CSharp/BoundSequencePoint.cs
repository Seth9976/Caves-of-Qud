using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000BC RID: 188
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSequencePoint : BoundStatement
	{
		// Token: 0x06000D82 RID: 3458 RVA: 0x00056D28 File Offset: 0x00054F28
		public static BoundStatement Create([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntax, TextSpan? part, BoundStatement statement, bool hasErrors = false)
		{
			if (part != null)
			{
				return new BoundSequencePointWithSpan(syntax, statement, part.Value, hasErrors);
			}
			return new BoundSequencePoint(syntax, statement, hasErrors);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00056D4B File Offset: 0x00054F4B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static BoundStatement Create(SyntaxNode syntax, BoundStatement statementOpt, bool hasErrors = false, bool wasCompilerGenerated = false)
		{
			return new BoundSequencePoint(syntax, statementOpt, hasErrors)
			{
				WasCompilerGenerated = wasCompilerGenerated
			};
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00056D5C File Offset: 0x00054F5C
		public static BoundStatement CreateHidden([global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement statementOpt = null, bool hasErrors = false)
		{
			return new BoundSequencePoint(null, statementOpt, hasErrors)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00056D6D File Offset: 0x00054F6D
		public BoundSequencePoint(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement statementOpt, bool hasErrors = false)
			: base(BoundKind.SequencePoint, syntax, hasErrors || statementOpt.HasErrors())
		{
			this.StatementOpt = statementOpt;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00056D8B File Offset: 0x00054F8B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundStatement StatementOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00056D93 File Offset: 0x00054F93
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSequencePoint(this);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00056D9C File Offset: 0x00054F9C
		public BoundSequencePoint Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement statementOpt)
		{
			if (statementOpt != this.StatementOpt)
			{
				BoundSequencePoint boundSequencePoint = new BoundSequencePoint(this.Syntax, statementOpt, base.HasErrors);
				boundSequencePoint.CopyAttributes(this);
				return boundSequencePoint;
			}
			return this;
		}
	}
}
