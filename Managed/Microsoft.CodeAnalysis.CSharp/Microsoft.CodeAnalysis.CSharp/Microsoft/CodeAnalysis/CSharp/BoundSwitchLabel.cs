using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001BA RID: 442
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSwitchLabel : BoundNode
	{
		// Token: 0x06001F7B RID: 8059 RVA: 0x000A2828 File Offset: 0x000A0A28
		public BoundSwitchLabel(SyntaxNode syntax, LabelSymbol label, BoundPattern pattern, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenClause, bool hasErrors = false)
			: base(BoundKind.SwitchLabel, syntax, hasErrors || pattern.HasErrors() || whenClause.HasErrors())
		{
			this.Label = label;
			this.Pattern = pattern;
			this.WhenClause = whenClause;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000A2862 File Offset: 0x000A0A62
		public LabelSymbol Label { get; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000A286A File Offset: 0x000A0A6A
		public BoundPattern Pattern { get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000A2872 File Offset: 0x000A0A72
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression WhenClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000A287A File Offset: 0x000A0A7A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSwitchLabel(this);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000A2884 File Offset: 0x000A0A84
		public BoundSwitchLabel Update(LabelSymbol label, BoundPattern pattern, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenClause)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label) || pattern != this.Pattern || whenClause != this.WhenClause)
			{
				BoundSwitchLabel boundSwitchLabel = new BoundSwitchLabel(this.Syntax, label, pattern, whenClause, base.HasErrors);
				boundSwitchLabel.CopyAttributes(this);
				return boundSwitchLabel;
			}
			return this;
		}
	}
}
