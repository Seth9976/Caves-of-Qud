using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F3 RID: 755
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EqualsValueClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600421C RID: 16924 RVA: 0x00110851 File Offset: 0x0010EA51
		internal EqualsValueClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x0011085C File Offset: 0x0010EA5C
		public SyntaxToken EqualsToken
		{
			get
			{
				return new SyntaxToken(this, ((EqualsValueClauseSyntax)base.Green).equalsToken, base.Position, 0);
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x0011087B File Offset: 0x0010EA7B
		public ExpressionSyntax Value
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.value, 1);
			}
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x0011088A File Offset: 0x0010EA8A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.value, 1);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x0011089F File Offset: 0x0010EA9F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.value;
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x001108AD File Offset: 0x0010EAAD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEqualsValueClause(this);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x001108B6 File Offset: 0x0010EAB6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEqualsValueClause(this);
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x001108C0 File Offset: 0x0010EAC0
		public EqualsValueClauseSyntax Update(SyntaxToken equalsToken, ExpressionSyntax value)
		{
			if (!(equalsToken != this.EqualsToken) && value == this.Value)
			{
				return this;
			}
			EqualsValueClauseSyntax equalsValueClauseSyntax = SyntaxFactory.EqualsValueClause(equalsToken, value);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return equalsValueClauseSyntax;
			}
			return equalsValueClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x00110905 File Offset: 0x0010EB05
		public EqualsValueClauseSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.Update(equalsToken, this.Value);
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x00110914 File Offset: 0x0010EB14
		public EqualsValueClauseSyntax WithValue(ExpressionSyntax value)
		{
			return this.Update(this.EqualsToken, value);
		}

		// Token: 0x040014C3 RID: 5315
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax value;
	}
}
