using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001AB RID: 427
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSwitchExpressionArm : BoundNode
	{
		// Token: 0x06001F31 RID: 7985 RVA: 0x000A1FF0 File Offset: 0x000A01F0
		public BoundSwitchExpressionArm(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundPattern pattern, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenClause, BoundExpression value, LabelSymbol label, bool hasErrors = false)
			: base(BoundKind.SwitchExpressionArm, syntax, hasErrors || pattern.HasErrors() || whenClause.HasErrors() || value.HasErrors())
		{
			this.Locals = locals;
			this.Pattern = pattern;
			this.WhenClause = whenClause;
			this.Value = value;
			this.Label = label;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x000A204B File Offset: 0x000A024B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000A2053 File Offset: 0x000A0253
		public BoundPattern Pattern { get; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x000A205B File Offset: 0x000A025B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression WhenClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000A2063 File Offset: 0x000A0263
		public BoundExpression Value { get; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000A206B File Offset: 0x000A026B
		public LabelSymbol Label { get; }

		// Token: 0x06001F37 RID: 7991 RVA: 0x000A2073 File Offset: 0x000A0273
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSwitchExpressionArm(this);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000A207C File Offset: 0x000A027C
		public BoundSwitchExpressionArm Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundPattern pattern, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenClause, BoundExpression value, LabelSymbol label)
		{
			if (locals != this.Locals || pattern != this.Pattern || whenClause != this.WhenClause || value != this.Value || !SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label))
			{
				BoundSwitchExpressionArm boundSwitchExpressionArm = new BoundSwitchExpressionArm(this.Syntax, locals, pattern, whenClause, value, label, base.HasErrors);
				boundSwitchExpressionArm.CopyAttributes(this);
				return boundSwitchExpressionArm;
			}
			return this;
		}
	}
}
