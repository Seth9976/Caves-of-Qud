using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B0 RID: 432
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLeafDecisionDagNode : BoundDecisionDagNode
	{
		// Token: 0x06001F52 RID: 8018 RVA: 0x000A23FE File Offset: 0x000A05FE
		public BoundLeafDecisionDagNode(SyntaxNode syntax, LabelSymbol label, bool hasErrors)
			: base(BoundKind.LeafDecisionDagNode, syntax, hasErrors)
		{
			this.Label = label;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000A2411 File Offset: 0x000A0611
		public BoundLeafDecisionDagNode(SyntaxNode syntax, LabelSymbol label)
			: base(BoundKind.LeafDecisionDagNode, syntax)
		{
			this.Label = label;
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x000A2423 File Offset: 0x000A0623
		public LabelSymbol Label { get; }

		// Token: 0x06001F55 RID: 8021 RVA: 0x000A242B File Offset: 0x000A062B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLeafDecisionDagNode(this);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000A2434 File Offset: 0x000A0634
		public BoundLeafDecisionDagNode Update(LabelSymbol label)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label))
			{
				BoundLeafDecisionDagNode boundLeafDecisionDagNode = new BoundLeafDecisionDagNode(this.Syntax, label, base.HasErrors);
				boundLeafDecisionDagNode.CopyAttributes(this);
				return boundLeafDecisionDagNode;
			}
			return this;
		}
	}
}
