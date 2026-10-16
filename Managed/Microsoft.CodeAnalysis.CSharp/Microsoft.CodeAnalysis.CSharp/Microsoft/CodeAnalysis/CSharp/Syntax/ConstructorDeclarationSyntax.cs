using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200024F RID: 591
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConstructorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x06003871 RID: 14449 RVA: 0x000FACA0 File Offset: 0x000F8EA0
		public ConstructorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, SyntaxToken semicolonToken)
		{
			return this.Update(attributeLists, modifiers, identifier, parameterList, initializer, body, null, semicolonToken);
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000FACBF File Offset: 0x000F8EBF
		internal ConstructorDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000FACCA File Offset: 0x000F8ECA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000FACE0 File Offset: 0x000F8EE0
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

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000FAD1C File Offset: 0x000F8F1C
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((ConstructorDeclarationSyntax)base.Green).identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000FAD42 File Offset: 0x000F8F42
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 3);
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000FAD51 File Offset: 0x000F8F51
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstructorInitializerSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ConstructorInitializerSyntax>(ref this.initializer, 4);
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000FAD60 File Offset: 0x000F8F60
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 5);
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000FAD6F File Offset: 0x000F8F6F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 6);
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000FAD80 File Offset: 0x000F8F80
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((ConstructorDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000FADC0 File Offset: 0x000F8FC0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 3);
			case 4:
				return base.GetRed<ConstructorInitializerSyntax>(ref this.initializer, 4);
			case 5:
				return base.GetRed<BlockSyntax>(ref this.body, 5);
			case 6:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 6);
			}
			return null;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000FAE44 File Offset: 0x000F9044
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.parameterList;
			case 4:
				return this.initializer;
			case 5:
				return this.body;
			case 6:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000FAEA5 File Offset: 0x000F90A5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstructorDeclaration(this);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000FAEAE File Offset: 0x000F90AE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstructorDeclaration(this);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000FAEB8 File Offset: 0x000F90B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public ConstructorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(identifier != this.Identifier) && parameterList == this.ParameterList && initializer == this.Initializer && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ConstructorDeclarationSyntax constructorDeclarationSyntax = SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return constructorDeclarationSyntax;
			}
			return constructorDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000FAF52 File Offset: 0x000F9152
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000FAF5C File Offset: 0x000F915C
		public new ConstructorDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Identifier, this.ParameterList, this.Initializer, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000FAF9A File Offset: 0x000F919A
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000FAFA4 File Offset: 0x000F91A4
		public new ConstructorDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Identifier, this.ParameterList, this.Initializer, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000FAFE4 File Offset: 0x000F91E4
		public ConstructorDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, identifier, this.ParameterList, this.Initializer, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000FB022 File Offset: 0x000F9222
		internal override BaseMethodDeclarationSyntax WithParameterListCore(ParameterListSyntax parameterList)
		{
			return this.WithParameterList(parameterList);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000FB02C File Offset: 0x000F922C
		public new ConstructorDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Identifier, parameterList, this.Initializer, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000FB06C File Offset: 0x000F926C
		public ConstructorDeclarationSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] ConstructorInitializerSyntax initializer)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Identifier, this.ParameterList, initializer, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000FB0AA File Offset: 0x000F92AA
		internal override BaseMethodDeclarationSyntax WithBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.WithBody(body);
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000FB0B4 File Offset: 0x000F92B4
		public new ConstructorDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Identifier, this.ParameterList, this.Initializer, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000FB0F2 File Offset: 0x000F92F2
		internal override BaseMethodDeclarationSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000FB0FC File Offset: 0x000F92FC
		public new ConstructorDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Identifier, this.ParameterList, this.Initializer, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000FB13A File Offset: 0x000F933A
		internal override BaseMethodDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000FB144 File Offset: 0x000F9344
		public new ConstructorDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Identifier, this.ParameterList, this.Initializer, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000FB182 File Offset: 0x000F9382
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000FB18C File Offset: 0x000F938C
		public new ConstructorDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000FB1AE File Offset: 0x000F93AE
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000FB1B8 File Offset: 0x000F93B8
		public new ConstructorDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000FB1DA File Offset: 0x000F93DA
		internal override BaseMethodDeclarationSyntax AddParameterListParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameterListParameters(items);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000FB1E4 File Offset: 0x000F93E4
		public new ConstructorDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000FB216 File Offset: 0x000F9416
		internal override BaseMethodDeclarationSyntax AddBodyAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBodyAttributeLists(items);
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000FB220 File Offset: 0x000F9420
		public new ConstructorDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000FB261 File Offset: 0x000F9461
		internal override BaseMethodDeclarationSyntax AddBodyStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBodyStatements(items);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000FB26C File Offset: 0x000F946C
		public new ConstructorDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x040013A2 RID: 5026
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013A3 RID: 5027
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x040013A4 RID: 5028
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ConstructorInitializerSyntax initializer;

		// Token: 0x040013A5 RID: 5029
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x040013A6 RID: 5030
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
