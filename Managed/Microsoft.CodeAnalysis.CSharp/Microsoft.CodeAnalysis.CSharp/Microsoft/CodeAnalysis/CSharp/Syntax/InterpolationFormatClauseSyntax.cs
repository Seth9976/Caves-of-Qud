using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002EF RID: 751
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InterpolationFormatClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060041F4 RID: 16884 RVA: 0x0011047B File Offset: 0x0010E67B
		internal InterpolationFormatClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x00110486 File Offset: 0x0010E686
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolationFormatClauseSyntax)base.Green).colonToken, base.Position, 0);
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x001104A5 File Offset: 0x0010E6A5
		public SyntaxToken FormatStringToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolationFormatClauseSyntax)base.Green).formatStringToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x001104CB File Offset: 0x0010E6CB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x001104CE File Offset: 0x0010E6CE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x001104D1 File Offset: 0x0010E6D1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolationFormatClause(this);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x001104DA File Offset: 0x0010E6DA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolationFormatClause(this);
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x001104E4 File Offset: 0x0010E6E4
		public InterpolationFormatClauseSyntax Update(SyntaxToken colonToken, SyntaxToken formatStringToken)
		{
			if (!(colonToken != this.ColonToken) && !(formatStringToken != this.FormatStringToken))
			{
				return this;
			}
			InterpolationFormatClauseSyntax interpolationFormatClauseSyntax = SyntaxFactory.InterpolationFormatClause(colonToken, formatStringToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return interpolationFormatClauseSyntax;
			}
			return interpolationFormatClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x0011052E File Offset: 0x0010E72E
		public InterpolationFormatClauseSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(colonToken, this.FormatStringToken);
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x0011053D File Offset: 0x0010E73D
		public InterpolationFormatClauseSyntax WithFormatStringToken(SyntaxToken formatStringToken)
		{
			return this.Update(this.ColonToken, formatStringToken);
		}
	}
}
