using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200027B RID: 635
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SimpleLambdaExpressionSyntax : LambdaExpressionSyntax
	{
		// Token: 0x06003C74 RID: 15476 RVA: 0x001055E4 File Offset: 0x001037E4
		public new SimpleLambdaExpressionSyntax WithBody(CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.WithExpressionBody((ExpressionSyntax)body).WithBlock(null);
			}
			return this.WithBlock(blockSyntax).WithExpressionBody(null);
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x0010561C File Offset: 0x0010381C
		public SimpleLambdaExpressionSyntax Update(SyntaxToken asyncKeyword, ParameterSyntax parameter, SyntaxToken arrowToken, CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.Update(asyncKeyword, parameter, arrowToken, null, (ExpressionSyntax)body);
			}
			return this.Update(asyncKeyword, parameter, arrowToken, blockSyntax, null);
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x00105651 File Offset: 0x00103851
		public override SyntaxToken AsyncKeyword
		{
			get
			{
				return this.Modifiers.FirstOrDefault(SyntaxKind.AsyncKeyword);
			}
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x00105663 File Offset: 0x00103863
		internal override AnonymousFunctionExpressionSyntax WithAsyncKeywordCore(SyntaxToken asyncKeyword)
		{
			return this.WithAsyncKeyword(asyncKeyword);
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x0010566C File Offset: 0x0010386C
		public new SimpleLambdaExpressionSyntax WithAsyncKeyword(SyntaxToken asyncKeyword)
		{
			return this.Update(asyncKeyword, this.Parameter, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x0010568D File Offset: 0x0010388D
		public SimpleLambdaExpressionSyntax Update(SyntaxToken asyncKeyword, ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(SyntaxFactory.TokenList(asyncKeyword), parameter, arrowToken, block, expressionBody);
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x001056A1 File Offset: 0x001038A1
		public SimpleLambdaExpressionSyntax Update(SyntaxTokenList modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, modifiers, parameter, arrowToken, block, expressionBody);
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x001056B6 File Offset: 0x001038B6
		internal SimpleLambdaExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x001056C1 File Offset: 0x001038C1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x001056D8 File Offset: 0x001038D8
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

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x00105714 File Offset: 0x00103914
		public ParameterSyntax Parameter
		{
			get
			{
				return base.GetRed<ParameterSyntax>(ref this.parameter, 2);
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x00105723 File Offset: 0x00103923
		public override SyntaxToken ArrowToken
		{
			get
			{
				return new SyntaxToken(this, ((SimpleLambdaExpressionSyntax)base.Green).arrowToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x00105749 File Offset: 0x00103949
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Block
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 4);
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x00105758 File Offset: 0x00103958
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expressionBody, 5);
			}
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x00105768 File Offset: 0x00103968
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<ParameterSyntax>(ref this.parameter, 2);
			case 4:
				return base.GetRed<BlockSyntax>(ref this.block, 4);
			case 5:
				return base.GetRed<ExpressionSyntax>(ref this.expressionBody, 5);
			}
			return null;
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x001057D8 File Offset: 0x001039D8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.parameter;
			case 4:
				return this.block;
			case 5:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x0010582C File Offset: 0x00103A2C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSimpleLambdaExpression(this);
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x00105835 File Offset: 0x00103A35
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSimpleLambdaExpression(this);
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x00105840 File Offset: 0x00103A40
		public SimpleLambdaExpressionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && parameter == this.Parameter && !(arrowToken != this.ArrowToken) && block == this.Block && expressionBody == this.ExpressionBody)
			{
				return this;
			}
			SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax = SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, arrowToken, block, expressionBody);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return simpleLambdaExpressionSyntax;
			}
			return simpleLambdaExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x001058BD File Offset: 0x00103ABD
		internal override LambdaExpressionSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x001058C6 File Offset: 0x00103AC6
		public new SimpleLambdaExpressionSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Parameter, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x001058ED File Offset: 0x00103AED
		internal override AnonymousFunctionExpressionSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x001058F6 File Offset: 0x00103AF6
		public new SimpleLambdaExpressionSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Parameter, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x0010591D File Offset: 0x00103B1D
		public SimpleLambdaExpressionSyntax WithParameter(ParameterSyntax parameter)
		{
			return this.Update(this.AttributeLists, this.Modifiers, parameter, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x00105944 File Offset: 0x00103B44
		internal override LambdaExpressionSyntax WithArrowTokenCore(SyntaxToken arrowToken)
		{
			return this.WithArrowToken(arrowToken);
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x0010594D File Offset: 0x00103B4D
		public new SimpleLambdaExpressionSyntax WithArrowToken(SyntaxToken arrowToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Parameter, arrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00105974 File Offset: 0x00103B74
		internal override AnonymousFunctionExpressionSyntax WithBlockCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			return this.WithBlock(block);
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x0010597D File Offset: 0x00103B7D
		public new SimpleLambdaExpressionSyntax WithBlock([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Parameter, this.ArrowToken, block, this.ExpressionBody);
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x001059A4 File Offset: 0x00103BA4
		internal override AnonymousFunctionExpressionSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x001059AD File Offset: 0x00103BAD
		public new SimpleLambdaExpressionSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Parameter, this.ArrowToken, this.Block, expressionBody);
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x001059D4 File Offset: 0x00103BD4
		internal override LambdaExpressionSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x001059E0 File Offset: 0x00103BE0
		public new SimpleLambdaExpressionSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x00105A02 File Offset: 0x00103C02
		internal override AnonymousFunctionExpressionSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x00105A0C File Offset: 0x00103C0C
		public new SimpleLambdaExpressionSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x00105A30 File Offset: 0x00103C30
		public SimpleLambdaExpressionSyntax AddParameterAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithParameter(this.Parameter.WithAttributeLists(this.Parameter.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x00105A64 File Offset: 0x00103C64
		public SimpleLambdaExpressionSyntax AddParameterModifiers(params SyntaxToken[] items)
		{
			return this.WithParameter(this.Parameter.WithModifiers(this.Parameter.Modifiers.AddRange(items)));
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x00105A96 File Offset: 0x00103C96
		internal override AnonymousFunctionExpressionSyntax AddBlockAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBlockAttributeLists(items);
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x00105AA0 File Offset: 0x00103CA0
		public new SimpleLambdaExpressionSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Block) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBlock(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x00105AE1 File Offset: 0x00103CE1
		internal override AnonymousFunctionExpressionSyntax AddBlockStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBlockStatements(items);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x00105AEC File Offset: 0x00103CEC
		public new SimpleLambdaExpressionSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Block) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBlock(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x0400141E RID: 5150
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400141F RID: 5151
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterSyntax parameter;

		// Token: 0x04001420 RID: 5152
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;

		// Token: 0x04001421 RID: 5153
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expressionBody;
	}
}
