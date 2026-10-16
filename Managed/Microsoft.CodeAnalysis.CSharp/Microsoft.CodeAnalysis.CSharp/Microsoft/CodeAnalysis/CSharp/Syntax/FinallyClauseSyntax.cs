using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000304 RID: 772
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FinallyClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x00111C01 File Offset: 0x0010FE01
		internal FinallyClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x00111C0C File Offset: 0x0010FE0C
		public SyntaxToken FinallyKeyword
		{
			get
			{
				return new SyntaxToken(this, ((FinallyClauseSyntax)base.Green).finallyKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x00111C2B File Offset: 0x0010FE2B
		public BlockSyntax Block
		{
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 1);
			}
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x00111C3A File Offset: 0x0010FE3A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<BlockSyntax>(ref this.block, 1);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x00111C4F File Offset: 0x0010FE4F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.block;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x00111C5D File Offset: 0x0010FE5D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFinallyClause(this);
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x00111C66 File Offset: 0x0010FE66
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFinallyClause(this);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x00111C70 File Offset: 0x0010FE70
		public FinallyClauseSyntax Update(SyntaxToken finallyKeyword, BlockSyntax block)
		{
			if (!(finallyKeyword != this.FinallyKeyword) && block == this.Block)
			{
				return this;
			}
			FinallyClauseSyntax finallyClauseSyntax = SyntaxFactory.FinallyClause(finallyKeyword, block);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return finallyClauseSyntax;
			}
			return finallyClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x00111CB5 File Offset: 0x0010FEB5
		public FinallyClauseSyntax WithFinallyKeyword(SyntaxToken finallyKeyword)
		{
			return this.Update(finallyKeyword, this.Block);
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x00111CC4 File Offset: 0x0010FEC4
		public FinallyClauseSyntax WithBlock(BlockSyntax block)
		{
			return this.Update(this.FinallyKeyword, block);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x00111CD4 File Offset: 0x0010FED4
		public FinallyClauseSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x00111D08 File Offset: 0x0010FF08
		public FinallyClauseSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
		}

		// Token: 0x040014D5 RID: 5333
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;
	}
}
