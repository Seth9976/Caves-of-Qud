using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B6 RID: 694
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SizeOfExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x0010BEE1 File Offset: 0x0010A0E1
		internal SizeOfExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x0010BEEC File Offset: 0x0010A0EC
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((SizeOfExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x0010BF0B File Offset: 0x0010A10B
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((SizeOfExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x0010BF31 File Offset: 0x0010A131
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x0010BF40 File Offset: 0x0010A140
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((SizeOfExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0010BF66 File Offset: 0x0010A166
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<TypeSyntax>(ref this.type, 2);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x0010BF7B File Offset: 0x0010A17B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x0010BF89 File Offset: 0x0010A189
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSizeOfExpression(this);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x0010BF92 File Offset: 0x0010A192
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSizeOfExpression(this);
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0010BF9C File Offset: 0x0010A19C
		public SizeOfExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && type == this.Type && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			SizeOfExpressionSyntax sizeOfExpressionSyntax = SyntaxFactory.SizeOfExpression(keyword, openParenToken, type, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return sizeOfExpressionSyntax;
			}
			return sizeOfExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0010C001 File Offset: 0x0010A201
		public SizeOfExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x0010C01C File Offset: 0x0010A21C
		public SizeOfExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x0010C037 File Offset: 0x0010A237
		public SizeOfExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.Keyword, this.OpenParenToken, type, this.CloseParenToken);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x0010C052 File Offset: 0x0010A252
		public SizeOfExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Type, closeParenToken);
		}

		// Token: 0x04001474 RID: 5236
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
