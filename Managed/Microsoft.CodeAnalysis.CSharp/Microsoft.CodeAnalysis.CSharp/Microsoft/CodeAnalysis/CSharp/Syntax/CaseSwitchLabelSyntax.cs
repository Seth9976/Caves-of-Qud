using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002FD RID: 765
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CaseSwitchLabelSyntax : SwitchLabelSyntax
	{
		// Token: 0x06004289 RID: 17033 RVA: 0x0011102E File Offset: 0x0010F22E
		internal CaseSwitchLabelSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x0600428A RID: 17034 RVA: 0x00111039 File Offset: 0x0010F239
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((CaseSwitchLabelSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x00111058 File Offset: 0x0010F258
		public ExpressionSyntax Value
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.value, 1);
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x00111067 File Offset: 0x0010F267
		public override SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((CaseSwitchLabelSyntax)base.Green).colonToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x0011108D File Offset: 0x0010F28D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.value, 1);
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x001110A2 File Offset: 0x0010F2A2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.value;
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x001110B0 File Offset: 0x0010F2B0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCaseSwitchLabel(this);
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x001110B9 File Offset: 0x0010F2B9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCaseSwitchLabel(this);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x001110C4 File Offset: 0x0010F2C4
		public CaseSwitchLabelSyntax Update(SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
		{
			if (!(keyword != this.Keyword) && value == this.Value && !(colonToken != this.ColonToken))
			{
				return this;
			}
			CaseSwitchLabelSyntax caseSwitchLabelSyntax = SyntaxFactory.CaseSwitchLabel(keyword, value, colonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return caseSwitchLabelSyntax;
			}
			return caseSwitchLabelSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x00111118 File Offset: 0x0010F318
		internal override SwitchLabelSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x00111121 File Offset: 0x0010F321
		public new CaseSwitchLabelSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.Value, this.ColonToken);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x00111136 File Offset: 0x0010F336
		public CaseSwitchLabelSyntax WithValue(ExpressionSyntax value)
		{
			return this.Update(this.Keyword, value, this.ColonToken);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x0011114B File Offset: 0x0010F34B
		internal override SwitchLabelSyntax WithColonTokenCore(SyntaxToken colonToken)
		{
			return this.WithColonToken(colonToken);
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x00111154 File Offset: 0x0010F354
		public new CaseSwitchLabelSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Keyword, this.Value, colonToken);
		}

		// Token: 0x040014CA RID: 5322
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax value;
	}
}
