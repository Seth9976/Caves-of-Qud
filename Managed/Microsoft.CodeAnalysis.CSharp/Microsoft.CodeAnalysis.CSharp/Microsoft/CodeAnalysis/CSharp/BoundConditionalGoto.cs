using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A9 RID: 425
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConditionalGoto : BoundStatement
	{
		// Token: 0x06001F25 RID: 7973 RVA: 0x000A1EF4 File Offset: 0x000A00F4
		public BoundConditionalGoto(SyntaxNode syntax, BoundExpression condition, bool jumpIfTrue, LabelSymbol label, bool hasErrors = false)
			: base(BoundKind.ConditionalGoto, syntax, hasErrors || condition.HasErrors())
		{
			this.Condition = condition;
			this.JumpIfTrue = jumpIfTrue;
			this.Label = label;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000A1F22 File Offset: 0x000A0122
		public BoundExpression Condition { get; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x000A1F2A File Offset: 0x000A012A
		public bool JumpIfTrue { get; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x000A1F32 File Offset: 0x000A0132
		public LabelSymbol Label { get; }

		// Token: 0x06001F29 RID: 7977 RVA: 0x000A1F3A File Offset: 0x000A013A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConditionalGoto(this);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000A1F44 File Offset: 0x000A0144
		public BoundConditionalGoto Update(BoundExpression condition, bool jumpIfTrue, LabelSymbol label)
		{
			if (condition != this.Condition || jumpIfTrue != this.JumpIfTrue || !SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label))
			{
				BoundConditionalGoto boundConditionalGoto = new BoundConditionalGoto(this.Syntax, condition, jumpIfTrue, label, base.HasErrors);
				boundConditionalGoto.CopyAttributes(this);
				return boundConditionalGoto;
			}
			return this;
		}
	}
}
