using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000301 RID: 769
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CatchClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060042C2 RID: 17090 RVA: 0x0011169B File Offset: 0x0010F89B
		internal CatchClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x001116A6 File Offset: 0x0010F8A6
		public SyntaxToken CatchKeyword
		{
			get
			{
				return new SyntaxToken(this, ((CatchClauseSyntax)base.Green).catchKeyword, base.Position, 0);
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x001116C5 File Offset: 0x0010F8C5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CatchDeclarationSyntax Declaration
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<CatchDeclarationSyntax>(ref this.declaration, 1);
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x001116D4 File Offset: 0x0010F8D4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CatchFilterClauseSyntax Filter
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<CatchFilterClauseSyntax>(ref this.filter, 2);
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x001116E3 File Offset: 0x0010F8E3
		public BlockSyntax Block
		{
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 3);
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x001116F4 File Offset: 0x0010F8F4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 1:
				syntaxNode = base.GetRed<CatchDeclarationSyntax>(ref this.declaration, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<CatchFilterClauseSyntax>(ref this.filter, 2);
				break;
			case 3:
				syntaxNode = base.GetRed<BlockSyntax>(ref this.block, 3);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0011174C File Offset: 0x0010F94C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 1:
				syntaxNode = this.declaration;
				break;
			case 2:
				syntaxNode = this.filter;
				break;
			case 3:
				syntaxNode = this.block;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0011178D File Offset: 0x0010F98D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCatchClause(this);
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x00111796 File Offset: 0x0010F996
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCatchClause(this);
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x001117A0 File Offset: 0x0010F9A0
		public CatchClauseSyntax Update(SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter, BlockSyntax block)
		{
			if (!(catchKeyword != this.CatchKeyword) && declaration == this.Declaration && filter == this.Filter && block == this.Block)
			{
				return this;
			}
			CatchClauseSyntax catchClauseSyntax = SyntaxFactory.CatchClause(catchKeyword, declaration, filter, block);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return catchClauseSyntax;
			}
			return catchClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x001117FB File Offset: 0x0010F9FB
		public CatchClauseSyntax WithCatchKeyword(SyntaxToken catchKeyword)
		{
			return this.Update(catchKeyword, this.Declaration, this.Filter, this.Block);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x00111816 File Offset: 0x0010FA16
		public CatchClauseSyntax WithDeclaration([global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration)
		{
			return this.Update(this.CatchKeyword, declaration, this.Filter, this.Block);
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x00111831 File Offset: 0x0010FA31
		public CatchClauseSyntax WithFilter([global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter)
		{
			return this.Update(this.CatchKeyword, this.Declaration, filter, this.Block);
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0011184C File Offset: 0x0010FA4C
		public CatchClauseSyntax WithBlock(BlockSyntax block)
		{
			return this.Update(this.CatchKeyword, this.Declaration, this.Filter, block);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x00111868 File Offset: 0x0010FA68
		public CatchClauseSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0011189C File Offset: 0x0010FA9C
		public CatchClauseSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
		}

		// Token: 0x040014D0 RID: 5328
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CatchDeclarationSyntax declaration;

		// Token: 0x040014D1 RID: 5329
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CatchFilterClauseSyntax filter;

		// Token: 0x040014D2 RID: 5330
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;
	}
}
