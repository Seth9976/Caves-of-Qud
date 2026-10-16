using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200034D RID: 845
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BadDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004755 RID: 18261 RVA: 0x00119C41 File Offset: 0x00117E41
		internal BadDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06004756 RID: 18262 RVA: 0x00119C4C File Offset: 0x00117E4C
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((BadDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06004757 RID: 18263 RVA: 0x00119C6B File Offset: 0x00117E6B
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((BadDirectiveTriviaSyntax)base.Green).identifier, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x00119C91 File Offset: 0x00117E91
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((BadDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x00119CB7 File Offset: 0x00117EB7
		public override bool IsActive
		{
			get
			{
				return ((BadDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x00119CC9 File Offset: 0x00117EC9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x00119CCC File Offset: 0x00117ECC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x00119CCF File Offset: 0x00117ECF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBadDirectiveTrivia(this);
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x00119CD8 File Offset: 0x00117ED8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBadDirectiveTrivia(this);
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x00119CE4 File Offset: 0x00117EE4
		public BadDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(identifier != this.Identifier) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			BadDirectiveTriviaSyntax badDirectiveTriviaSyntax = SyntaxFactory.BadDirectiveTrivia(hashToken, identifier, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return badDirectiveTriviaSyntax;
			}
			return badDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00119D3F File Offset: 0x00117F3F
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x00119D48 File Offset: 0x00117F48
		public new BadDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.Identifier, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x00119D63 File Offset: 0x00117F63
		public BadDirectiveTriviaSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.HashToken, identifier, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x00119D7E File Offset: 0x00117F7E
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x00119D87 File Offset: 0x00117F87
		public new BadDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.Identifier, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x00119DA2 File Offset: 0x00117FA2
		public BadDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.Identifier, this.EndOfDirectiveToken, isActive);
		}
	}
}
