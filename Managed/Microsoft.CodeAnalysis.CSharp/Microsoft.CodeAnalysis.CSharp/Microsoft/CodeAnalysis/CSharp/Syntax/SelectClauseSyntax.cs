using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D5 RID: 725
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SelectClauseSyntax : SelectOrGroupClauseSyntax
	{
		// Token: 0x060040EA RID: 16618 RVA: 0x0010EA39 File Offset: 0x0010CC39
		internal SelectClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x0010EA44 File Offset: 0x0010CC44
		public SyntaxToken SelectKeyword
		{
			get
			{
				return new SyntaxToken(this, ((SelectClauseSyntax)base.Green).selectKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x0010EA63 File Offset: 0x0010CC63
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0010EA72 File Offset: 0x0010CC72
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0010EA87 File Offset: 0x0010CC87
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x0010EA95 File Offset: 0x0010CC95
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSelectClause(this);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0010EA9E File Offset: 0x0010CC9E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSelectClause(this);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x0010EAA8 File Offset: 0x0010CCA8
		public SelectClauseSyntax Update(SyntaxToken selectKeyword, ExpressionSyntax expression)
		{
			if (!(selectKeyword != this.SelectKeyword) && expression == this.Expression)
			{
				return this;
			}
			SelectClauseSyntax selectClauseSyntax = SyntaxFactory.SelectClause(selectKeyword, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return selectClauseSyntax;
			}
			return selectClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x0010EAED File Offset: 0x0010CCED
		public SelectClauseSyntax WithSelectKeyword(SyntaxToken selectKeyword)
		{
			return this.Update(selectKeyword, this.Expression);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x0010EAFC File Offset: 0x0010CCFC
		public SelectClauseSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.SelectKeyword, expression);
		}

		// Token: 0x040014A0 RID: 5280
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
