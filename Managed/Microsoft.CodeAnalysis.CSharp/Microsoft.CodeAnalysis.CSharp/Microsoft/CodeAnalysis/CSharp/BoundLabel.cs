using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A8 RID: 168
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLabel : BoundExpression
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x000556B5 File Offset: 0x000538B5
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.Label;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000556BD File Offset: 0x000538BD
		public BoundLabel(SyntaxNode syntax, LabelSymbol label, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(BoundKind.Label, syntax, type, hasErrors)
		{
			this.Label = label;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000556D2 File Offset: 0x000538D2
		public BoundLabel(SyntaxNode syntax, LabelSymbol label, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(BoundKind.Label, syntax, type)
		{
			this.Label = label;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000556E5 File Offset: 0x000538E5
		public LabelSymbol Label { get; }

		// Token: 0x06000CDF RID: 3295 RVA: 0x000556ED File Offset: 0x000538ED
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLabel(this);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000556F6 File Offset: 0x000538F6
		public BoundLabel Update(LabelSymbol label, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label) || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundLabel boundLabel = new BoundLabel(this.Syntax, label, type, base.HasErrors);
				boundLabel.CopyAttributes(this);
				return boundLabel;
			}
			return this;
		}
	}
}
