using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D6 RID: 726
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class GroupClauseSyntax : SelectOrGroupClauseSyntax
	{
		// Token: 0x060040F4 RID: 16628 RVA: 0x0010EB0B File Offset: 0x0010CD0B
		internal GroupClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x0010EB16 File Offset: 0x0010CD16
		public SyntaxToken GroupKeyword
		{
			get
			{
				return new SyntaxToken(this, ((GroupClauseSyntax)base.Green).groupKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x0010EB35 File Offset: 0x0010CD35
		public ExpressionSyntax GroupExpression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.groupExpression, 1);
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x0010EB44 File Offset: 0x0010CD44
		public SyntaxToken ByKeyword
		{
			get
			{
				return new SyntaxToken(this, ((GroupClauseSyntax)base.Green).byKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x0010EB6A File Offset: 0x0010CD6A
		public ExpressionSyntax ByExpression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.byExpression, 3);
			}
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x0010EB7C File Offset: 0x0010CD7C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 1)
			{
				if (index != 3)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.byExpression, 3);
				}
			}
			else
			{
				expressionSyntax = base.GetRed<ExpressionSyntax>(ref this.groupExpression, 1);
			}
			return expressionSyntax;
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0010EBB8 File Offset: 0x0010CDB8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 1)
			{
				if (index != 3)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = this.byExpression;
				}
			}
			else
			{
				expressionSyntax = this.groupExpression;
			}
			return expressionSyntax;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0010EBE4 File Offset: 0x0010CDE4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGroupClause(this);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0010EBED File Offset: 0x0010CDED
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGroupClause(this);
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x0010EBF8 File Offset: 0x0010CDF8
		public GroupClauseSyntax Update(SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
		{
			if (!(groupKeyword != this.GroupKeyword) && groupExpression == this.GroupExpression && !(byKeyword != this.ByKeyword) && byExpression == this.ByExpression)
			{
				return this;
			}
			GroupClauseSyntax groupClauseSyntax = SyntaxFactory.GroupClause(groupKeyword, groupExpression, byKeyword, byExpression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return groupClauseSyntax;
			}
			return groupClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x0010EC58 File Offset: 0x0010CE58
		public GroupClauseSyntax WithGroupKeyword(SyntaxToken groupKeyword)
		{
			return this.Update(groupKeyword, this.GroupExpression, this.ByKeyword, this.ByExpression);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x0010EC73 File Offset: 0x0010CE73
		public GroupClauseSyntax WithGroupExpression(ExpressionSyntax groupExpression)
		{
			return this.Update(this.GroupKeyword, groupExpression, this.ByKeyword, this.ByExpression);
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0010EC8E File Offset: 0x0010CE8E
		public GroupClauseSyntax WithByKeyword(SyntaxToken byKeyword)
		{
			return this.Update(this.GroupKeyword, this.GroupExpression, byKeyword, this.ByExpression);
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0010ECA9 File Offset: 0x0010CEA9
		public GroupClauseSyntax WithByExpression(ExpressionSyntax byExpression)
		{
			return this.Update(this.GroupKeyword, this.GroupExpression, this.ByKeyword, byExpression);
		}

		// Token: 0x040014A1 RID: 5281
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax groupExpression;

		// Token: 0x040014A2 RID: 5282
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax byExpression;
	}
}
