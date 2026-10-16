using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000286 RID: 646
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TryStatementSyntax : StatementSyntax
	{
		// Token: 0x06003D24 RID: 15652 RVA: 0x0010801E File Offset: 0x0010621E
		public TryStatementSyntax Update(SyntaxToken tryKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
		{
			return this.Update(this.AttributeLists, tryKeyword, block, catches, @finally);
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x00108031 File Offset: 0x00106231
		internal TryStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x0010803C File Offset: 0x0010623C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x00108050 File Offset: 0x00106250
		public SyntaxToken TryKeyword
		{
			get
			{
				return new SyntaxToken(this, ((TryStatementSyntax)base.Green).tryKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x00108076 File Offset: 0x00106276
		public BlockSyntax Block
		{
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 2);
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x00108085 File Offset: 0x00106285
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<CatchClauseSyntax> Catches
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<CatchClauseSyntax>(base.GetRed(ref this.catches, 3));
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x00108099 File Offset: 0x00106299
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public FinallyClauseSyntax Finally
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<FinallyClauseSyntax>(ref this.@finally, 4);
			}
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x001080A8 File Offset: 0x001062A8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<BlockSyntax>(ref this.block, 2);
			case 3:
				return base.GetRed(ref this.catches, 3);
			case 4:
				return base.GetRed<FinallyClauseSyntax>(ref this.@finally, 4);
			}
			return null;
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x00108114 File Offset: 0x00106314
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.block;
			case 3:
				return this.catches;
			case 4:
				return this.@finally;
			}
			return null;
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x00108164 File Offset: 0x00106364
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTryStatement(this);
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x0010816D File Offset: 0x0010636D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTryStatement(this);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x00108178 File Offset: 0x00106378
		public TryStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<CatchClauseSyntax> catches, [global::System.Runtime.CompilerServices.Nullable(2)] FinallyClauseSyntax @finally)
		{
			if (!(attributeLists != this.AttributeLists) && !(tryKeyword != this.TryKeyword) && block == this.Block && !(catches != this.Catches) && @finally == this.Finally)
			{
				return this;
			}
			TryStatementSyntax tryStatementSyntax = SyntaxFactory.TryStatement(attributeLists, tryKeyword, block, catches, @finally);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return tryStatementSyntax;
			}
			return tryStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x001081E9 File Offset: 0x001063E9
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x001081F2 File Offset: 0x001063F2
		public new TryStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.TryKeyword, this.Block, this.Catches, this.Finally);
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x00108213 File Offset: 0x00106413
		public TryStatementSyntax WithTryKeyword(SyntaxToken tryKeyword)
		{
			return this.Update(this.AttributeLists, tryKeyword, this.Block, this.Catches, this.Finally);
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x00108234 File Offset: 0x00106434
		public TryStatementSyntax WithBlock(BlockSyntax block)
		{
			return this.Update(this.AttributeLists, this.TryKeyword, block, this.Catches, this.Finally);
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x00108255 File Offset: 0x00106455
		public TryStatementSyntax WithCatches([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<CatchClauseSyntax> catches)
		{
			return this.Update(this.AttributeLists, this.TryKeyword, this.Block, catches, this.Finally);
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x00108276 File Offset: 0x00106476
		public TryStatementSyntax WithFinally([global::System.Runtime.CompilerServices.Nullable(2)] FinallyClauseSyntax @finally)
		{
			return this.Update(this.AttributeLists, this.TryKeyword, this.Block, this.Catches, @finally);
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x00108297 File Offset: 0x00106497
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x001082A0 File Offset: 0x001064A0
		public new TryStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x001082C4 File Offset: 0x001064C4
		public TryStatementSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x001082F8 File Offset: 0x001064F8
		public TryStatementSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x0010832C File Offset: 0x0010652C
		public TryStatementSyntax AddCatches(params CatchClauseSyntax[] items)
		{
			return this.WithCatches(this.Catches.AddRange(items));
		}

		// Token: 0x04001436 RID: 5174
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001437 RID: 5175
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;

		// Token: 0x04001438 RID: 5176
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode catches;

		// Token: 0x04001439 RID: 5177
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private FinallyClauseSyntax @finally;
	}
}
