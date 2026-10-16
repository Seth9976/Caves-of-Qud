using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C6 RID: 710
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AnonymousObjectCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004032 RID: 16434 RVA: 0x0010D28A File Offset: 0x0010B48A
		internal AnonymousObjectCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x0010D295 File Offset: 0x0010B495
		public SyntaxToken NewKeyword
		{
			get
			{
				return new SyntaxToken(this, ((AnonymousObjectCreationExpressionSyntax)base.Green).newKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x0010D2B4 File Offset: 0x0010B4B4
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((AnonymousObjectCreationExpressionSyntax)base.Green).openBraceToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x0010D2DC File Offset: 0x0010B4DC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> Initializers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.initializers, 2);
				if (red == null)
				{
					return default(SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax>);
				}
				return new SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax>(red, base.GetChildIndex(2));
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x0010D311 File Offset: 0x0010B511
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((AnonymousObjectCreationExpressionSyntax)base.Green).closeBraceToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0010D337 File Offset: 0x0010B537
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed(ref this.initializers, 2);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x0010D34C File Offset: 0x0010B54C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.initializers;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x0010D35A File Offset: 0x0010B55A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAnonymousObjectCreationExpression(this);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x0010D363 File Offset: 0x0010B563
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAnonymousObjectCreationExpression(this);
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x0010D36C File Offset: 0x0010B56C
		public AnonymousObjectCreationExpressionSyntax Update(SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
		{
			if (!(newKeyword != this.NewKeyword) && !(openBraceToken != this.OpenBraceToken) && !(initializers != this.Initializers) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax = SyntaxFactory.AnonymousObjectCreationExpression(newKeyword, openBraceToken, initializers, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return anonymousObjectCreationExpressionSyntax;
			}
			return anonymousObjectCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x0010D3D6 File Offset: 0x0010B5D6
		public AnonymousObjectCreationExpressionSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.Update(newKeyword, this.OpenBraceToken, this.Initializers, this.CloseBraceToken);
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0010D3F1 File Offset: 0x0010B5F1
		public AnonymousObjectCreationExpressionSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.NewKeyword, openBraceToken, this.Initializers, this.CloseBraceToken);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0010D40C File Offset: 0x0010B60C
		public AnonymousObjectCreationExpressionSyntax WithInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers)
		{
			return this.Update(this.NewKeyword, this.OpenBraceToken, initializers, this.CloseBraceToken);
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x0010D427 File Offset: 0x0010B627
		public AnonymousObjectCreationExpressionSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.NewKeyword, this.OpenBraceToken, this.Initializers, closeBraceToken);
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0010D444 File Offset: 0x0010B644
		public AnonymousObjectCreationExpressionSyntax AddInitializers(params AnonymousObjectMemberDeclaratorSyntax[] items)
		{
			return this.WithInitializers(this.Initializers.AddRange(items));
		}

		// Token: 0x0400148B RID: 5259
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode initializers;
	}
}
