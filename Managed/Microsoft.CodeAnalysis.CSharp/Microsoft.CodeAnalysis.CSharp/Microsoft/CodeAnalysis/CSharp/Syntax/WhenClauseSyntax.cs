using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002DC RID: 732
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class WhenClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600413A RID: 16698 RVA: 0x0010F21F File Offset: 0x0010D41F
		internal WhenClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x0010F22A File Offset: 0x0010D42A
		public SyntaxToken WhenKeyword
		{
			get
			{
				return new SyntaxToken(this, ((WhenClauseSyntax)base.Green).whenKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x0010F249 File Offset: 0x0010D449
		public ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 1);
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x0010F258 File Offset: 0x0010D458
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.condition, 1);
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x0010F26D File Offset: 0x0010D46D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.condition;
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0010F27B File Offset: 0x0010D47B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWhenClause(this);
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x0010F284 File Offset: 0x0010D484
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWhenClause(this);
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0010F290 File Offset: 0x0010D490
		public WhenClauseSyntax Update(SyntaxToken whenKeyword, ExpressionSyntax condition)
		{
			if (!(whenKeyword != this.WhenKeyword) && condition == this.Condition)
			{
				return this;
			}
			WhenClauseSyntax whenClauseSyntax = SyntaxFactory.WhenClause(whenKeyword, condition);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return whenClauseSyntax;
			}
			return whenClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x0010F2D5 File Offset: 0x0010D4D5
		public WhenClauseSyntax WithWhenKeyword(SyntaxToken whenKeyword)
		{
			return this.Update(whenKeyword, this.Condition);
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x0010F2E4 File Offset: 0x0010D4E4
		public WhenClauseSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.WhenKeyword, condition);
		}

		// Token: 0x040014A8 RID: 5288
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;
	}
}
