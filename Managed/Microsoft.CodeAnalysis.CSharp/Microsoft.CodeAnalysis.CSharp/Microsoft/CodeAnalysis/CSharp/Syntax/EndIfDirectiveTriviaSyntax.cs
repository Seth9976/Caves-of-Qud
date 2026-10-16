using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000348 RID: 840
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EndIfDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004705 RID: 18181 RVA: 0x001194D8 File Offset: 0x001176D8
		internal EndIfDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x001194E3 File Offset: 0x001176E3
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((EndIfDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x00119502 File Offset: 0x00117702
		public SyntaxToken EndIfKeyword
		{
			get
			{
				return new SyntaxToken(this, ((EndIfDirectiveTriviaSyntax)base.Green).endIfKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x00119528 File Offset: 0x00117728
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((EndIfDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x0011954E File Offset: 0x0011774E
		public override bool IsActive
		{
			get
			{
				return ((EndIfDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00119560 File Offset: 0x00117760
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00119563 File Offset: 0x00117763
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x00119566 File Offset: 0x00117766
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEndIfDirectiveTrivia(this);
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x0011956F File Offset: 0x0011776F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEndIfDirectiveTrivia(this);
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x00119578 File Offset: 0x00117778
		public EndIfDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(endIfKeyword != this.EndIfKeyword) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			EndIfDirectiveTriviaSyntax endIfDirectiveTriviaSyntax = SyntaxFactory.EndIfDirectiveTrivia(hashToken, endIfKeyword, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return endIfDirectiveTriviaSyntax;
			}
			return endIfDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x001195D3 File Offset: 0x001177D3
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x001195DC File Offset: 0x001177DC
		public new EndIfDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.EndIfKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x001195F7 File Offset: 0x001177F7
		public EndIfDirectiveTriviaSyntax WithEndIfKeyword(SyntaxToken endIfKeyword)
		{
			return this.Update(this.HashToken, endIfKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00119612 File Offset: 0x00117812
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x0011961B File Offset: 0x0011781B
		public new EndIfDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.EndIfKeyword, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00119636 File Offset: 0x00117836
		public EndIfDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.EndIfKeyword, this.EndOfDirectiveToken, isActive);
		}
	}
}
