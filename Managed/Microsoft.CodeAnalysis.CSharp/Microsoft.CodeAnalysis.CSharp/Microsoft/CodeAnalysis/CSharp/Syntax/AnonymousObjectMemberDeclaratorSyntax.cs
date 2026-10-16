using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C5 RID: 709
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AnonymousObjectMemberDeclaratorSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004028 RID: 16424 RVA: 0x0010D18A File Offset: 0x0010B38A
		internal AnonymousObjectMemberDeclaratorSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06004029 RID: 16425 RVA: 0x0010D195 File Offset: 0x0010B395
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameEqualsSyntax NameEquals
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRedAtZero<NameEqualsSyntax>(ref this.nameEquals);
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x0010D1A3 File Offset: 0x0010B3A3
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x0010D1B4 File Offset: 0x0010B3B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<NameEqualsSyntax>(ref this.nameEquals);
			}
			return syntaxNode;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x0010D1EC File Offset: 0x0010B3EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.expression;
				}
			}
			else
			{
				syntaxNode = this.nameEquals;
			}
			return syntaxNode;
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x0010D217 File Offset: 0x0010B417
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAnonymousObjectMemberDeclarator(this);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x0010D220 File Offset: 0x0010B420
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAnonymousObjectMemberDeclarator(this);
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x0010D22C File Offset: 0x0010B42C
		public AnonymousObjectMemberDeclaratorSyntax Update([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, ExpressionSyntax expression)
		{
			if (nameEquals == this.NameEquals && expression == this.Expression)
			{
				return this;
			}
			AnonymousObjectMemberDeclaratorSyntax anonymousObjectMemberDeclaratorSyntax = SyntaxFactory.AnonymousObjectMemberDeclarator(nameEquals, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return anonymousObjectMemberDeclaratorSyntax;
			}
			return anonymousObjectMemberDeclaratorSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x0010D26C File Offset: 0x0010B46C
		public AnonymousObjectMemberDeclaratorSyntax WithNameEquals([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals)
		{
			return this.Update(nameEquals, this.Expression);
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x0010D27B File Offset: 0x0010B47B
		public AnonymousObjectMemberDeclaratorSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.NameEquals, expression);
		}

		// Token: 0x04001489 RID: 5257
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameEqualsSyntax nameEquals;

		// Token: 0x0400148A RID: 5258
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
