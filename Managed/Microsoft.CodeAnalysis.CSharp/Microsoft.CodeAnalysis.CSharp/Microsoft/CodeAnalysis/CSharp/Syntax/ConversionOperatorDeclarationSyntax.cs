using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200031F RID: 799
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConversionOperatorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x060044E8 RID: 17640 RVA: 0x00115CB1 File Offset: 0x00113EB1
		internal ConversionOperatorDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x00115CBC File Offset: 0x00113EBC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x00115CD0 File Offset: 0x00113ED0
		public override SyntaxTokenList Modifiers
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

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x00115D0C File Offset: 0x00113F0C
		public SyntaxToken ImplicitOrExplicitKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ConversionOperatorDeclarationSyntax)base.Green).implicitOrExplicitKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x00115D32 File Offset: 0x00113F32
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ConversionOperatorDeclarationSyntax)base.Green).operatorKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x00115D58 File Offset: 0x00113F58
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 4);
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x00115D67 File Offset: 0x00113F67
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 5);
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x00115D76 File Offset: 0x00113F76
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 6);
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x00115D85 File Offset: 0x00113F85
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 7);
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x00115D94 File Offset: 0x00113F94
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((ConversionOperatorDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x00115DD4 File Offset: 0x00113FD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 4:
				return base.GetRed<TypeSyntax>(ref this.type, 4);
			case 5:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 5);
			case 6:
				return base.GetRed<BlockSyntax>(ref this.body, 6);
			case 7:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 7);
			}
			return null;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x00115E5C File Offset: 0x0011405C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 4:
				return this.type;
			case 5:
				return this.parameterList;
			case 6:
				return this.body;
			case 7:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x00115EC1 File Offset: 0x001140C1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConversionOperatorDeclaration(this);
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x00115ECA File Offset: 0x001140CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConversionOperatorDeclaration(this);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x00115ED4 File Offset: 0x001140D4
		public ConversionOperatorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(implicitOrExplicitKeyword != this.ImplicitOrExplicitKeyword) && !(operatorKeyword != this.OperatorKeyword) && type == this.Type && parameterList == this.ParameterList && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ConversionOperatorDeclarationSyntax conversionOperatorDeclarationSyntax = SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return conversionOperatorDeclarationSyntax;
			}
			return conversionOperatorDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x00115F7F File Offset: 0x0011417F
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x00115F88 File Offset: 0x00114188
		public new ConversionOperatorDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x00115FCC File Offset: 0x001141CC
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x00115FD8 File Offset: 0x001141D8
		public new ConversionOperatorDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x0011601C File Offset: 0x0011421C
		public ConversionOperatorDeclarationSyntax WithImplicitOrExplicitKeyword(SyntaxToken implicitOrExplicitKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, implicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00116060 File Offset: 0x00114260
		public ConversionOperatorDeclarationSyntax WithOperatorKeyword(SyntaxToken operatorKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, operatorKeyword, this.Type, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x001160A4 File Offset: 0x001142A4
		public ConversionOperatorDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, type, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x001160E8 File Offset: 0x001142E8
		internal override BaseMethodDeclarationSyntax WithParameterListCore(ParameterListSyntax parameterList)
		{
			return this.WithParameterList(parameterList);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x001160F4 File Offset: 0x001142F4
		public new ConversionOperatorDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, parameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x00116138 File Offset: 0x00114338
		internal override BaseMethodDeclarationSyntax WithBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.WithBody(body);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x00116144 File Offset: 0x00114344
		public new ConversionOperatorDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.ParameterList, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x00116188 File Offset: 0x00114388
		internal override BaseMethodDeclarationSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x00116194 File Offset: 0x00114394
		public new ConversionOperatorDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.ParameterList, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x001161D8 File Offset: 0x001143D8
		internal override BaseMethodDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x001161E4 File Offset: 0x001143E4
		public new ConversionOperatorDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.ParameterList, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00116228 File Offset: 0x00114428
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00116234 File Offset: 0x00114434
		public new ConversionOperatorDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00116256 File Offset: 0x00114456
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x00116260 File Offset: 0x00114460
		public new ConversionOperatorDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x00116282 File Offset: 0x00114482
		internal override BaseMethodDeclarationSyntax AddParameterListParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameterListParameters(items);
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x0011628C File Offset: 0x0011448C
		public new ConversionOperatorDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x001162BE File Offset: 0x001144BE
		internal override BaseMethodDeclarationSyntax AddBodyAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBodyAttributeLists(items);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x001162C8 File Offset: 0x001144C8
		public new ConversionOperatorDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00116309 File Offset: 0x00114509
		internal override BaseMethodDeclarationSyntax AddBodyStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBodyStatements(items);
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00116314 File Offset: 0x00114514
		public new ConversionOperatorDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x04001502 RID: 5378
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001503 RID: 5379
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001504 RID: 5380
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x04001505 RID: 5381
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x04001506 RID: 5382
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
