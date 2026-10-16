using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000242 RID: 578
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AnonymousMethodExpressionSyntax : AnonymousFunctionExpressionSyntax
	{
		// Token: 0x0600378A RID: 14218 RVA: 0x000F96C0 File Offset: 0x000F78C0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public new AnonymousMethodExpressionSyntax WithBody(CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.WithExpressionBody((ExpressionSyntax)body).WithBlock(null);
			}
			return this.WithBlock(blockSyntax).WithExpressionBody(null);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000F96F8 File Offset: 0x000F78F8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public AnonymousMethodExpressionSyntax Update(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, CSharpSyntaxNode body)
		{
			BlockSyntax blockSyntax = body as BlockSyntax;
			if (blockSyntax == null)
			{
				return this.Update(asyncKeyword, delegateKeyword, parameterList, null, (ExpressionSyntax)body);
			}
			return this.Update(asyncKeyword, delegateKeyword, parameterList, blockSyntax, null);
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x000F972D File Offset: 0x000F792D
		public override SyntaxToken AsyncKeyword
		{
			get
			{
				return this.Modifiers.FirstOrDefault(SyntaxKind.AsyncKeyword);
			}
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000F973F File Offset: 0x000F793F
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal override AnonymousFunctionExpressionSyntax WithAsyncKeywordCore(SyntaxToken asyncKeyword)
		{
			return this.WithAsyncKeyword(asyncKeyword);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000F9748 File Offset: 0x000F7948
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public new AnonymousMethodExpressionSyntax WithAsyncKeyword(SyntaxToken asyncKeyword)
		{
			return this.Update(asyncKeyword, this.DelegateKeyword, this.ParameterList, this.Block, this.ExpressionBody);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000F9769 File Offset: 0x000F7969
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public AnonymousMethodExpressionSyntax Update(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
		{
			return this.Update(SyntaxFactory.TokenList(asyncKeyword), delegateKeyword, parameterList, block, expressionBody);
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000F977D File Offset: 0x000F797D
		internal AnonymousMethodExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000F9788 File Offset: 0x000F7988
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(0);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, base.Position, 0);
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000F97BD File Offset: 0x000F79BD
		public SyntaxToken DelegateKeyword
		{
			get
			{
				return new SyntaxToken(this, ((AnonymousMethodExpressionSyntax)base.Green).delegateKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000F97E3 File Offset: 0x000F79E3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 2);
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000F97F2 File Offset: 0x000F79F2
		public override BlockSyntax Block
		{
			get
			{
				return base.GetRed<BlockSyntax>(ref this.block, 3);
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000F9801 File Offset: 0x000F7A01
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expressionBody, 4);
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000F9810 File Offset: 0x000F7A10
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 2:
				syntaxNode = base.GetRed<ParameterListSyntax>(ref this.parameterList, 2);
				break;
			case 3:
				syntaxNode = base.GetRed<BlockSyntax>(ref this.block, 3);
				break;
			case 4:
				syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expressionBody, 4);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000F9868 File Offset: 0x000F7A68
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 2:
				syntaxNode = this.parameterList;
				break;
			case 3:
				syntaxNode = this.block;
				break;
			case 4:
				syntaxNode = this.expressionBody;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000F98A9 File Offset: 0x000F7AA9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAnonymousMethodExpression(this);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000F98B2 File Offset: 0x000F7AB2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAnonymousMethodExpression(this);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000F98BC File Offset: 0x000F7ABC
		public AnonymousMethodExpressionSyntax Update(SyntaxTokenList modifiers, SyntaxToken delegateKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			if (!(modifiers != this.Modifiers) && !(delegateKeyword != this.DelegateKeyword) && parameterList == this.ParameterList && block == this.Block && expressionBody == this.ExpressionBody)
			{
				return this;
			}
			AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax = SyntaxFactory.AnonymousMethodExpression(modifiers, delegateKeyword, parameterList, block, expressionBody);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return anonymousMethodExpressionSyntax;
			}
			return anonymousMethodExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000F9928 File Offset: 0x000F7B28
		internal override AnonymousFunctionExpressionSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000F9931 File Offset: 0x000F7B31
		public new AnonymousMethodExpressionSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(modifiers, this.DelegateKeyword, this.ParameterList, this.Block, this.ExpressionBody);
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000F9952 File Offset: 0x000F7B52
		public AnonymousMethodExpressionSyntax WithDelegateKeyword(SyntaxToken delegateKeyword)
		{
			return this.Update(this.Modifiers, delegateKeyword, this.ParameterList, this.Block, this.ExpressionBody);
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000F9973 File Offset: 0x000F7B73
		public AnonymousMethodExpressionSyntax WithParameterList([global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList)
		{
			return this.Update(this.Modifiers, this.DelegateKeyword, parameterList, this.Block, this.ExpressionBody);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000F9994 File Offset: 0x000F7B94
		internal override AnonymousFunctionExpressionSyntax WithBlockCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return this.WithBlock(block);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000F99AC File Offset: 0x000F7BAC
		public new AnonymousMethodExpressionSyntax WithBlock(BlockSyntax block)
		{
			return this.Update(this.Modifiers, this.DelegateKeyword, this.ParameterList, block, this.ExpressionBody);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000F99CD File Offset: 0x000F7BCD
		internal override AnonymousFunctionExpressionSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000F99D6 File Offset: 0x000F7BD6
		public new AnonymousMethodExpressionSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return this.Update(this.Modifiers, this.DelegateKeyword, this.ParameterList, this.Block, expressionBody);
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000F99F7 File Offset: 0x000F7BF7
		internal override AnonymousFunctionExpressionSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000F9A00 File Offset: 0x000F7C00
		public new AnonymousMethodExpressionSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000F9A24 File Offset: 0x000F7C24
		public AnonymousMethodExpressionSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			ParameterListSyntax parameterListSyntax;
			if ((parameterListSyntax = this.ParameterList) == null)
			{
				parameterListSyntax = SyntaxFactory.ParameterList(default(SeparatedSyntaxList<ParameterSyntax>));
			}
			ParameterListSyntax parameterListSyntax2 = parameterListSyntax;
			return this.WithParameterList(parameterListSyntax2.WithParameters(parameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000F9A65 File Offset: 0x000F7C65
		internal override AnonymousFunctionExpressionSyntax AddBlockAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBlockAttributeLists(items);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000F9A70 File Offset: 0x000F7C70
		public new AnonymousMethodExpressionSyntax AddBlockAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000F9AA2 File Offset: 0x000F7CA2
		internal override AnonymousFunctionExpressionSyntax AddBlockStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBlockStatements(items);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000F9AAC File Offset: 0x000F7CAC
		public new AnonymousMethodExpressionSyntax AddBlockStatements(params StatementSyntax[] items)
		{
			return this.WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
		}

		// Token: 0x04001391 RID: 5009
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x04001392 RID: 5010
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax block;

		// Token: 0x04001393 RID: 5011
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expressionBody;
	}
}
