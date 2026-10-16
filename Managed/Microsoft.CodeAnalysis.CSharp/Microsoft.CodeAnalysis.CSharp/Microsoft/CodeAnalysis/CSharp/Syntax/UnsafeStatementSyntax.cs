using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000289 RID: 649
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class UnsafeStatementSyntax : StatementSyntax
	{
		// Token: 0x06003D5F RID: 15711 RVA: 0x001084B0 File Offset: 0x001066B0
		public UnsafeStatementSyntax Update(SyntaxToken unsafeKeyword, BlockSyntax block)
		{
			return this.Update(this.AttributeLists, unsafeKeyword, block);
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x001084C0 File Offset: 0x001066C0
		internal UnsafeStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003D61 RID: 15713 RVA: 0x001084CB File Offset: 0x001066CB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x001084DF File Offset: 0x001066DF
		public SyntaxToken UnsafeKeyword
		{
			get
			{
				return new SyntaxToken(this, ((UnsafeStatementSyntax)base.Green).unsafeKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x00108505 File Offset: 0x00106705
		public BlockSyntax Block
		{
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 2);
			}
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x00108514 File Offset: 0x00106714
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<BlockSyntax>(ref this.block, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0010854C File Offset: 0x0010674C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.block;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x00108577 File Offset: 0x00106777
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUnsafeStatement(this);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x00108580 File Offset: 0x00106780
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUnsafeStatement(this);
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x0010858C File Offset: 0x0010678C
		public UnsafeStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
		{
			if (!(attributeLists != this.AttributeLists) && !(unsafeKeyword != this.UnsafeKeyword) && block == this.Block)
			{
				return this;
			}
			UnsafeStatementSyntax unsafeStatementSyntax = SyntaxFactory.UnsafeStatement(attributeLists, unsafeKeyword, block);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return unsafeStatementSyntax;
			}
			return unsafeStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x001085E0 File Offset: 0x001067E0
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x001085E9 File Offset: 0x001067E9
		public new UnsafeStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.UnsafeKeyword, this.Block);
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x001085FE File Offset: 0x001067FE
		public UnsafeStatementSyntax WithUnsafeKeyword(SyntaxToken unsafeKeyword)
		{
			return this.Update(this.AttributeLists, unsafeKeyword, this.Block);
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x00108613 File Offset: 0x00106813
		public UnsafeStatementSyntax WithBlock(BlockSyntax block)
		{
			return this.Update(this.AttributeLists, this.UnsafeKeyword, block);
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x00108628 File Offset: 0x00106828
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00108634 File Offset: 0x00106834
		public new UnsafeStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x00108658 File Offset: 0x00106858
		public UnsafeStatementSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x0010868C File Offset: 0x0010688C
		public UnsafeStatementSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
		}

		// Token: 0x0400143A RID: 5178
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400143B RID: 5179
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;
	}
}
