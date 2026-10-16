using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200024C RID: 588
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CheckedStatementSyntax : StatementSyntax
	{
		// Token: 0x0600383B RID: 14395 RVA: 0x000FA58E File Offset: 0x000F878E
		public CheckedStatementSyntax Update(SyntaxToken keyword, BlockSyntax block)
		{
			return this.Update(this.AttributeLists, keyword, block);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000FA59E File Offset: 0x000F879E
		internal CheckedStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000FA5A9 File Offset: 0x000F87A9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000FA5BD File Offset: 0x000F87BD
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((CheckedStatementSyntax)base.Green).keyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000FA5E3 File Offset: 0x000F87E3
		public BlockSyntax Block
		{
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 2);
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000FA5F4 File Offset: 0x000F87F4
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

		// Token: 0x06003841 RID: 14401 RVA: 0x000FA62C File Offset: 0x000F882C
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

		// Token: 0x06003842 RID: 14402 RVA: 0x000FA657 File Offset: 0x000F8857
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCheckedStatement(this);
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000FA660 File Offset: 0x000F8860
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCheckedStatement(this);
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000FA66C File Offset: 0x000F886C
		public CheckedStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken keyword, BlockSyntax block)
		{
			if (!(attributeLists != this.AttributeLists) && !(keyword != this.Keyword) && block == this.Block)
			{
				return this;
			}
			CheckedStatementSyntax checkedStatementSyntax = SyntaxFactory.CheckedStatement(base.Kind(), attributeLists, keyword, block);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return checkedStatementSyntax;
			}
			return checkedStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000FA6C6 File Offset: 0x000F88C6
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000FA6CF File Offset: 0x000F88CF
		public new CheckedStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Keyword, this.Block);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000FA6E4 File Offset: 0x000F88E4
		public CheckedStatementSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(this.AttributeLists, keyword, this.Block);
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000FA6F9 File Offset: 0x000F88F9
		public CheckedStatementSyntax WithBlock(BlockSyntax block)
		{
			return this.Update(this.AttributeLists, this.Keyword, block);
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000FA70E File Offset: 0x000F890E
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000FA718 File Offset: 0x000F8918
		public new CheckedStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000FA73C File Offset: 0x000F893C
		public CheckedStatementSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000FA770 File Offset: 0x000F8970
		public CheckedStatementSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
		}

		// Token: 0x0400139C RID: 5020
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400139D RID: 5021
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;
	}
}
