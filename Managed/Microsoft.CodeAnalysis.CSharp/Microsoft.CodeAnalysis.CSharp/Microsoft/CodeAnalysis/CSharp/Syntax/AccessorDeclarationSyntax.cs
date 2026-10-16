using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000276 RID: 630
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AccessorDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003BF6 RID: 15350 RVA: 0x001041B5 File Offset: 0x001023B5
		public AccessorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, BlockSyntax body, SyntaxToken semicolonToken)
		{
			return this.Update(attributeLists, modifiers, keyword, body, null, semicolonToken);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x001041C5 File Offset: 0x001023C5
		internal AccessorDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x001041D0 File Offset: 0x001023D0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x001041E4 File Offset: 0x001023E4
		public SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x00104220 File Offset: 0x00102420
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((AccessorDeclarationSyntax)base.Green).keyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x00104246 File Offset: 0x00102446
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 3);
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x00104255 File Offset: 0x00102455
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 4);
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x00104264 File Offset: 0x00102464
		public SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((AccessorDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x001042A4 File Offset: 0x001024A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<BlockSyntax>(ref this.body, 3);
			case 4:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 4);
			}
			return null;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x00104300 File Offset: 0x00102500
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.body;
			case 4:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x00104347 File Offset: 0x00102547
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAccessorDeclaration(this);
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x00104350 File Offset: 0x00102550
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAccessorDeclaration(this);
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x0010435C File Offset: 0x0010255C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public AccessorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(keyword != this.Keyword) && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			AccessorDeclarationSyntax accessorDeclarationSyntax = SyntaxFactory.AccessorDeclaration(base.Kind(), attributeLists, modifiers, keyword, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return accessorDeclarationSyntax;
			}
			return accessorDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x001043E4 File Offset: 0x001025E4
		public AccessorDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Keyword, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0010440B File Offset: 0x0010260B
		public AccessorDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Keyword, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x00104432 File Offset: 0x00102632
		public AccessorDeclarationSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, keyword, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x00104459 File Offset: 0x00102659
		public AccessorDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x00104480 File Offset: 0x00102680
		public AccessorDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x001044A7 File Offset: 0x001026A7
		public AccessorDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x001044D0 File Offset: 0x001026D0
		public AccessorDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x001044F4 File Offset: 0x001026F4
		public AccessorDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x00104518 File Offset: 0x00102718
		public AccessorDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x0010455C File Offset: 0x0010275C
		public AccessorDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x04001410 RID: 5136
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001411 RID: 5137
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x04001412 RID: 5138
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
