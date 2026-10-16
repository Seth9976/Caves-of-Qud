using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C8 RID: 712
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ImplicitArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600404E RID: 16462 RVA: 0x0010D5EE File Offset: 0x0010B7EE
		internal ImplicitArrayCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x0010D5F9 File Offset: 0x0010B7F9
		public SyntaxToken NewKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitArrayCreationExpressionSyntax)base.Green).newKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x0010D618 File Offset: 0x0010B818
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitArrayCreationExpressionSyntax)base.Green).openBracketToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x0010D640 File Offset: 0x0010B840
		public SyntaxTokenList Commas
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(2);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x0010D67C File Offset: 0x0010B87C
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitArrayCreationExpressionSyntax)base.Green).closeBracketToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x0010D6A2 File Offset: 0x0010B8A2
		public InitializerExpressionSyntax Initializer
		{
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 4);
			}
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x0010D6B1 File Offset: 0x0010B8B1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 4)
			{
				return null;
			}
			return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 4);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0010D6C6 File Offset: 0x0010B8C6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 4)
			{
				return null;
			}
			return this.initializer;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0010D6D4 File Offset: 0x0010B8D4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitArrayCreationExpression(this);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0010D6DD File Offset: 0x0010B8DD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitArrayCreationExpression(this);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x0010D6E8 File Offset: 0x0010B8E8
		public ImplicitArrayCreationExpressionSyntax Update(SyntaxToken newKeyword, SyntaxToken openBracketToken, SyntaxTokenList commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			if (!(newKeyword != this.NewKeyword) && !(openBracketToken != this.OpenBracketToken) && !(commas != this.Commas) && !(closeBracketToken != this.CloseBracketToken) && initializer == this.Initializer)
			{
				return this;
			}
			ImplicitArrayCreationExpressionSyntax implicitArrayCreationExpressionSyntax = SyntaxFactory.ImplicitArrayCreationExpression(newKeyword, openBracketToken, commas, closeBracketToken, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return implicitArrayCreationExpressionSyntax;
			}
			return implicitArrayCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x0010D75E File Offset: 0x0010B95E
		public ImplicitArrayCreationExpressionSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.Update(newKeyword, this.OpenBracketToken, this.Commas, this.CloseBracketToken, this.Initializer);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x0010D77F File Offset: 0x0010B97F
		public ImplicitArrayCreationExpressionSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(this.NewKeyword, openBracketToken, this.Commas, this.CloseBracketToken, this.Initializer);
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x0010D7A0 File Offset: 0x0010B9A0
		public ImplicitArrayCreationExpressionSyntax WithCommas(SyntaxTokenList commas)
		{
			return this.Update(this.NewKeyword, this.OpenBracketToken, commas, this.CloseBracketToken, this.Initializer);
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x0010D7C1 File Offset: 0x0010B9C1
		public ImplicitArrayCreationExpressionSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.NewKeyword, this.OpenBracketToken, this.Commas, closeBracketToken, this.Initializer);
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x0010D7E2 File Offset: 0x0010B9E2
		public ImplicitArrayCreationExpressionSyntax WithInitializer(InitializerExpressionSyntax initializer)
		{
			return this.Update(this.NewKeyword, this.OpenBracketToken, this.Commas, this.CloseBracketToken, initializer);
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x0010D804 File Offset: 0x0010BA04
		public ImplicitArrayCreationExpressionSyntax AddCommas(params SyntaxToken[] items)
		{
			return this.WithCommas(this.Commas.AddRange(items));
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x0010D828 File Offset: 0x0010BA28
		public ImplicitArrayCreationExpressionSyntax AddInitializerExpressions(params ExpressionSyntax[] items)
		{
			return this.WithInitializer(this.Initializer.WithExpressions(this.Initializer.Expressions.AddRange(items)));
		}

		// Token: 0x0400148E RID: 5262
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
