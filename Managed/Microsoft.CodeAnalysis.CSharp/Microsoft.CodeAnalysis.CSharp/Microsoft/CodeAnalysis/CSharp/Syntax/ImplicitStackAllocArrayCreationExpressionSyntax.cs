using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C9 RID: 713
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ImplicitStackAllocArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004060 RID: 16480 RVA: 0x0010D85A File Offset: 0x0010BA5A
		internal ImplicitStackAllocArrayCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x0010D865 File Offset: 0x0010BA65
		public SyntaxToken StackAllocKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitStackAllocArrayCreationExpressionSyntax)base.Green).stackAllocKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x0010D884 File Offset: 0x0010BA84
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitStackAllocArrayCreationExpressionSyntax)base.Green).openBracketToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x0010D8AA File Offset: 0x0010BAAA
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitStackAllocArrayCreationExpressionSyntax)base.Green).closeBracketToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x0010D8D0 File Offset: 0x0010BAD0
		public InitializerExpressionSyntax Initializer
		{
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 3);
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x0010D8DF File Offset: 0x0010BADF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 3)
			{
				return null;
			}
			return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 3);
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0010D8F4 File Offset: 0x0010BAF4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 3)
			{
				return null;
			}
			return this.initializer;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x0010D902 File Offset: 0x0010BB02
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0010D90B File Offset: 0x0010BB0B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x0010D914 File Offset: 0x0010BB14
		public ImplicitStackAllocArrayCreationExpressionSyntax Update(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			if (!(stackAllocKeyword != this.StackAllocKeyword) && !(openBracketToken != this.OpenBracketToken) && !(closeBracketToken != this.CloseBracketToken) && initializer == this.Initializer)
			{
				return this;
			}
			ImplicitStackAllocArrayCreationExpressionSyntax implicitStackAllocArrayCreationExpressionSyntax = SyntaxFactory.ImplicitStackAllocArrayCreationExpression(stackAllocKeyword, openBracketToken, closeBracketToken, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return implicitStackAllocArrayCreationExpressionSyntax;
			}
			return implicitStackAllocArrayCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x0010D979 File Offset: 0x0010BB79
		public ImplicitStackAllocArrayCreationExpressionSyntax WithStackAllocKeyword(SyntaxToken stackAllocKeyword)
		{
			return this.Update(stackAllocKeyword, this.OpenBracketToken, this.CloseBracketToken, this.Initializer);
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x0010D994 File Offset: 0x0010BB94
		public ImplicitStackAllocArrayCreationExpressionSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(this.StackAllocKeyword, openBracketToken, this.CloseBracketToken, this.Initializer);
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x0010D9AF File Offset: 0x0010BBAF
		public ImplicitStackAllocArrayCreationExpressionSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.StackAllocKeyword, this.OpenBracketToken, closeBracketToken, this.Initializer);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x0010D9CA File Offset: 0x0010BBCA
		public ImplicitStackAllocArrayCreationExpressionSyntax WithInitializer(InitializerExpressionSyntax initializer)
		{
			return this.Update(this.StackAllocKeyword, this.OpenBracketToken, this.CloseBracketToken, initializer);
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0010D9E8 File Offset: 0x0010BBE8
		public ImplicitStackAllocArrayCreationExpressionSyntax AddInitializerExpressions(params ExpressionSyntax[] items)
		{
			return this.WithInitializer(this.Initializer.WithExpressions(this.Initializer.Expressions.AddRange(items)));
		}

		// Token: 0x0400148F RID: 5263
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
