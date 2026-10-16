using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F2 RID: 242
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBadStatement : BoundStatement, IBoundInvalidNode
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00063B4A File Offset: 0x00061D4A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return this.ChildBoundNodes;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00063B52 File Offset: 0x00061D52
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ChildBoundNodes;
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00063B5A File Offset: 0x00061D5A
		public BoundBadStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundNode> childBoundNodes, bool hasErrors = false)
			: base(BoundKind.BadStatement, syntax, hasErrors || childBoundNodes.HasErrors<BoundNode>())
		{
			this.ChildBoundNodes = childBoundNodes;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00063B78 File Offset: 0x00061D78
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundNode> ChildBoundNodes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00063B80 File Offset: 0x00061D80
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBadStatement(this);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00063B89 File Offset: 0x00061D89
		public BoundBadStatement Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundNode> childBoundNodes)
		{
			if (childBoundNodes != this.ChildBoundNodes)
			{
				BoundBadStatement boundBadStatement = new BoundBadStatement(this.Syntax, childBoundNodes, base.HasErrors);
				boundBadStatement.CopyAttributes(this);
				return boundBadStatement;
			}
			return this;
		}
	}
}
