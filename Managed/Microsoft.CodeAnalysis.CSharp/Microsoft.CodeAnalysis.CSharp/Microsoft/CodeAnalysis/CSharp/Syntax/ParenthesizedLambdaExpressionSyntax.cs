using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000274 RID: 628
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ParenthesizedLambdaExpressionSyntax : LambdaExpressionSyntax
	{
		// Token: 0x06003BAA RID: 15274 RVA: 0x0010367C File Offset: 0x0010187C
		public new ParenthesizedLambdaExpressionSyntax WithBody(CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.WithExpressionBody((ExpressionSyntax)body).WithBlock(null);
			}
			return this.WithBlock(blockSyntax).WithExpressionBody(null);
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x001036B4 File Offset: 0x001018B4
		public ParenthesizedLambdaExpressionSyntax Update(SyntaxToken asyncKeyword, ParameterListSyntax parameterList, SyntaxToken arrowToken, CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.Update(asyncKeyword, parameterList, arrowToken, null, (ExpressionSyntax)body);
			}
			return this.Update(asyncKeyword, parameterList, arrowToken, blockSyntax, null);
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x001036E9 File Offset: 0x001018E9
		public override SyntaxToken AsyncKeyword
		{
			get
			{
				return this.Modifiers.FirstOrDefault(SyntaxKind.AsyncKeyword);
			}
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x001036FB File Offset: 0x001018FB
		internal override AnonymousFunctionExpressionSyntax WithAsyncKeywordCore(SyntaxToken asyncKeyword)
		{
			return this.WithAsyncKeyword(asyncKeyword);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x00103704 File Offset: 0x00101904
		public new ParenthesizedLambdaExpressionSyntax WithAsyncKeyword(SyntaxToken asyncKeyword)
		{
			return this.Update(asyncKeyword, this.ParameterList, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x00103725 File Offset: 0x00101925
		public ParenthesizedLambdaExpressionSyntax Update(SyntaxToken asyncKeyword, ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(SyntaxFactory.TokenList(asyncKeyword), parameterList, arrowToken, block, expressionBody);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x00103739 File Offset: 0x00101939
		public ParenthesizedLambdaExpressionSyntax Update(SyntaxTokenList modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, modifiers, parameterList, arrowToken, block, expressionBody);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x0010374E File Offset: 0x0010194E
		internal ParenthesizedLambdaExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x00103759 File Offset: 0x00101959
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x00103770 File Offset: 0x00101970
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

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x001037AC File Offset: 0x001019AC
		public ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 2);
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x001037BB File Offset: 0x001019BB
		public override SyntaxToken ArrowToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedLambdaExpressionSyntax)base.Green).arrowToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x001037E1 File Offset: 0x001019E1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Block
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 4);
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x001037F0 File Offset: 0x001019F0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expressionBody, 5);
			}
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x00103800 File Offset: 0x00101A00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 2);
			case 4:
				return base.GetRed<BlockSyntax>(ref this.block, 4);
			case 5:
				return base.GetRed<ExpressionSyntax>(ref this.expressionBody, 5);
			}
			return null;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x00103870 File Offset: 0x00101A70
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.parameterList;
			case 4:
				return this.block;
			case 5:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x001038C4 File Offset: 0x00101AC4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedLambdaExpression(this);
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x001038CD File Offset: 0x00101ACD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedLambdaExpression(this);
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x001038D8 File Offset: 0x00101AD8
		public ParenthesizedLambdaExpressionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && parameterList == this.ParameterList && !(arrowToken != this.ArrowToken) && block == this.Block && expressionBody == this.ExpressionBody)
			{
				return this;
			}
			ParenthesizedLambdaExpressionSyntax parenthesizedLambdaExpressionSyntax = SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, parameterList, arrowToken, block, expressionBody);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return parenthesizedLambdaExpressionSyntax;
			}
			return parenthesizedLambdaExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00103955 File Offset: 0x00101B55
		internal override LambdaExpressionSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x0010395E File Offset: 0x00101B5E
		public new ParenthesizedLambdaExpressionSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.ParameterList, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00103985 File Offset: 0x00101B85
		internal override AnonymousFunctionExpressionSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x0010398E File Offset: 0x00101B8E
		public new ParenthesizedLambdaExpressionSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.ParameterList, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x001039B5 File Offset: 0x00101BB5
		public ParenthesizedLambdaExpressionSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, parameterList, this.ArrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x001039DC File Offset: 0x00101BDC
		internal override LambdaExpressionSyntax WithArrowTokenCore(SyntaxToken arrowToken)
		{
			return this.WithArrowToken(arrowToken);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x001039E5 File Offset: 0x00101BE5
		public new ParenthesizedLambdaExpressionSyntax WithArrowToken(SyntaxToken arrowToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ParameterList, arrowToken, this.Block, this.ExpressionBody);
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x00103A0C File Offset: 0x00101C0C
		internal override AnonymousFunctionExpressionSyntax WithBlockCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			return this.WithBlock(block);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x00103A15 File Offset: 0x00101C15
		public new ParenthesizedLambdaExpressionSyntax WithBlock([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ParameterList, this.ArrowToken, block, this.ExpressionBody);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x00103A3C File Offset: 0x00101C3C
		internal override AnonymousFunctionExpressionSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00103A45 File Offset: 0x00101C45
		public new ParenthesizedLambdaExpressionSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ParameterList, this.ArrowToken, this.Block, expressionBody);
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x00103A6C File Offset: 0x00101C6C
		internal override LambdaExpressionSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00103A78 File Offset: 0x00101C78
		public new ParenthesizedLambdaExpressionSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x00103A9A File Offset: 0x00101C9A
		internal override AnonymousFunctionExpressionSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x00103AA4 File Offset: 0x00101CA4
		public new ParenthesizedLambdaExpressionSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x00103AC8 File Offset: 0x00101CC8
		public ParenthesizedLambdaExpressionSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00103AFA File Offset: 0x00101CFA
		internal override AnonymousFunctionExpressionSyntax AddBlockAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBlockAttributeLists(items);
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x00103B04 File Offset: 0x00101D04
		public new ParenthesizedLambdaExpressionSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Block) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBlock(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x00103B45 File Offset: 0x00101D45
		internal override AnonymousFunctionExpressionSyntax AddBlockStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBlockStatements(items);
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x00103B50 File Offset: 0x00101D50
		public new ParenthesizedLambdaExpressionSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Block) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBlock(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x04001406 RID: 5126
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001407 RID: 5127
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x04001408 RID: 5128
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;

		// Token: 0x04001409 RID: 5129
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expressionBody;
	}
}
