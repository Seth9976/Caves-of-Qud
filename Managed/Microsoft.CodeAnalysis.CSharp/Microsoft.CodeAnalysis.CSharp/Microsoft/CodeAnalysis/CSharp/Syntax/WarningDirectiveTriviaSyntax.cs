using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200034C RID: 844
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class WarningDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004745 RID: 18245 RVA: 0x00119AC5 File Offset: 0x00117CC5
		internal WarningDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x00119AD0 File Offset: 0x00117CD0
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((WarningDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x00119AEF File Offset: 0x00117CEF
		public SyntaxToken WarningKeyword
		{
			get
			{
				return new SyntaxToken(this, ((WarningDirectiveTriviaSyntax)base.Green).warningKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x00119B15 File Offset: 0x00117D15
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((WarningDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06004749 RID: 18249 RVA: 0x00119B3B File Offset: 0x00117D3B
		public override bool IsActive
		{
			get
			{
				return ((WarningDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00119B4D File Offset: 0x00117D4D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00119B50 File Offset: 0x00117D50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x00119B53 File Offset: 0x00117D53
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWarningDirectiveTrivia(this);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00119B5C File Offset: 0x00117D5C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWarningDirectiveTrivia(this);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00119B68 File Offset: 0x00117D68
		public WarningDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(warningKeyword != this.WarningKeyword) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			WarningDirectiveTriviaSyntax warningDirectiveTriviaSyntax = SyntaxFactory.WarningDirectiveTrivia(hashToken, warningKeyword, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return warningDirectiveTriviaSyntax;
			}
			return warningDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00119BC3 File Offset: 0x00117DC3
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x00119BCC File Offset: 0x00117DCC
		public new WarningDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.WarningKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00119BE7 File Offset: 0x00117DE7
		public WarningDirectiveTriviaSyntax WithWarningKeyword(SyntaxToken warningKeyword)
		{
			return this.Update(this.HashToken, warningKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00119C02 File Offset: 0x00117E02
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00119C0B File Offset: 0x00117E0B
		public new WarningDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.WarningKeyword, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00119C26 File Offset: 0x00117E26
		public WarningDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.WarningKeyword, this.EndOfDirectiveToken, isActive);
		}
	}
}
