using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B5 RID: 693
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeOfExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F5C RID: 16220 RVA: 0x0010BD55 File Offset: 0x00109F55
		internal TypeOfExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x0010BD60 File Offset: 0x00109F60
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((TypeOfExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x0010BD7F File Offset: 0x00109F7F
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeOfExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x0010BDA5 File Offset: 0x00109FA5
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x0010BDB4 File Offset: 0x00109FB4
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeOfExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0010BDDA File Offset: 0x00109FDA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<TypeSyntax>(ref this.type, 2);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x0010BDEF File Offset: 0x00109FEF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0010BDFD File Offset: 0x00109FFD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeOfExpression(this);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x0010BE06 File Offset: 0x0010A006
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeOfExpression(this);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0010BE10 File Offset: 0x0010A010
		public TypeOfExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && type == this.Type && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			TypeOfExpressionSyntax typeOfExpressionSyntax = SyntaxFactory.TypeOfExpression(keyword, openParenToken, type, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeOfExpressionSyntax;
			}
			return typeOfExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0010BE75 File Offset: 0x0010A075
		public TypeOfExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x0010BE90 File Offset: 0x0010A090
		public TypeOfExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x0010BEAB File Offset: 0x0010A0AB
		public TypeOfExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.Keyword, this.OpenParenToken, type, this.CloseParenToken);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0010BEC6 File Offset: 0x0010A0C6
		public TypeOfExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Type, closeParenToken);
		}

		// Token: 0x04001473 RID: 5235
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
