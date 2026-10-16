using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002CE RID: 718
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FromClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x06004089 RID: 16521 RVA: 0x0010DCFA File Offset: 0x0010BEFA
		internal FromClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x0010DD05 File Offset: 0x0010BF05
		public SyntaxToken FromKeyword
		{
			get
			{
				return new SyntaxToken(this, ((FromClauseSyntax)base.Green).fromKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x0010DD24 File Offset: 0x0010BF24
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x0010DD33 File Offset: 0x0010BF33
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((FromClauseSyntax)base.Green).identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x0010DD59 File Offset: 0x0010BF59
		public SyntaxToken InKeyword
		{
			get
			{
				return new SyntaxToken(this, ((FromClauseSyntax)base.Green).inKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x0010DD7F File Offset: 0x0010BF7F
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 4);
			}
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0010DD90 File Offset: 0x0010BF90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 1)
			{
				if (index != 4)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.expression, 4);
				}
			}
			else
			{
				expressionSyntax = base.GetRed<TypeSyntax>(ref this.type, 1);
			}
			return expressionSyntax;
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0010DDCC File Offset: 0x0010BFCC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 1)
			{
				if (index != 4)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = this.expression;
				}
			}
			else
			{
				expressionSyntax = this.type;
			}
			return expressionSyntax;
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0010DDF8 File Offset: 0x0010BFF8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFromClause(this);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x0010DE01 File Offset: 0x0010C001
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFromClause(this);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x0010DE0C File Offset: 0x0010C00C
		public FromClauseSyntax Update(SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
		{
			if (!(fromKeyword != this.FromKeyword) && type == this.Type && !(identifier != this.Identifier) && !(inKeyword != this.InKeyword) && expression == this.Expression)
			{
				return this;
			}
			FromClauseSyntax fromClauseSyntax = SyntaxFactory.FromClause(fromKeyword, type, identifier, inKeyword, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return fromClauseSyntax;
			}
			return fromClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0010DE7D File Offset: 0x0010C07D
		public FromClauseSyntax WithFromKeyword(SyntaxToken fromKeyword)
		{
			return this.Update(fromKeyword, this.Type, this.Identifier, this.InKeyword, this.Expression);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0010DE9E File Offset: 0x0010C09E
		public FromClauseSyntax WithType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.Update(this.FromKeyword, type, this.Identifier, this.InKeyword, this.Expression);
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x0010DEBF File Offset: 0x0010C0BF
		public FromClauseSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.FromKeyword, this.Type, identifier, this.InKeyword, this.Expression);
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0010DEE0 File Offset: 0x0010C0E0
		public FromClauseSyntax WithInKeyword(SyntaxToken inKeyword)
		{
			return this.Update(this.FromKeyword, this.Type, this.Identifier, inKeyword, this.Expression);
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0010DF01 File Offset: 0x0010C101
		public FromClauseSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.FromKeyword, this.Type, this.Identifier, this.InKeyword, expression);
		}

		// Token: 0x04001495 RID: 5269
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001496 RID: 5270
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
