using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002B4 RID: 692
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DefaultExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F4E RID: 16206 RVA: 0x0010BBC7 File Offset: 0x00109DC7
		internal DefaultExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x0010BBD2 File Offset: 0x00109DD2
		public SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((DefaultExpressionSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x0010BBF1 File Offset: 0x00109DF1
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((DefaultExpressionSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x0010BC17 File Offset: 0x00109E17
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x0010BC26 File Offset: 0x00109E26
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((DefaultExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0010BC4C File Offset: 0x00109E4C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<TypeSyntax>(ref this.type, 2);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0010BC61 File Offset: 0x00109E61
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0010BC6F File Offset: 0x00109E6F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefaultExpression(this);
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x0010BC78 File Offset: 0x00109E78
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefaultExpression(this);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0010BC84 File Offset: 0x00109E84
		public DefaultExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (!(keyword != this.Keyword) && !(openParenToken != this.OpenParenToken) && type == this.Type && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			DefaultExpressionSyntax defaultExpressionSyntax = SyntaxFactory.DefaultExpression(keyword, openParenToken, type, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return defaultExpressionSyntax;
			}
			return defaultExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0010BCE9 File Offset: 0x00109EE9
		public DefaultExpressionSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.OpenParenToken, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0010BD04 File Offset: 0x00109F04
		public DefaultExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.Keyword, openParenToken, this.Type, this.CloseParenToken);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0010BD1F File Offset: 0x00109F1F
		public DefaultExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.Keyword, this.OpenParenToken, type, this.CloseParenToken);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0010BD3A File Offset: 0x00109F3A
		public DefaultExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.Keyword, this.OpenParenToken, this.Type, closeParenToken);
		}

		// Token: 0x04001472 RID: 5234
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
