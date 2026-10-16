using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200034B RID: 843
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ErrorDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004735 RID: 18229 RVA: 0x00119949 File Offset: 0x00117B49
		internal ErrorDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004736 RID: 18230 RVA: 0x00119954 File Offset: 0x00117B54
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((ErrorDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x00119973 File Offset: 0x00117B73
		public SyntaxToken ErrorKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ErrorDirectiveTriviaSyntax)base.Green).errorKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x00119999 File Offset: 0x00117B99
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((ErrorDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x001199BF File Offset: 0x00117BBF
		public override bool IsActive
		{
			get
			{
				return ((ErrorDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x001199D1 File Offset: 0x00117BD1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x001199D4 File Offset: 0x00117BD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x001199D7 File Offset: 0x00117BD7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitErrorDirectiveTrivia(this);
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x001199E0 File Offset: 0x00117BE0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitErrorDirectiveTrivia(this);
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x001199EC File Offset: 0x00117BEC
		public ErrorDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(errorKeyword != this.ErrorKeyword) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			ErrorDirectiveTriviaSyntax errorDirectiveTriviaSyntax = SyntaxFactory.ErrorDirectiveTrivia(hashToken, errorKeyword, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return errorDirectiveTriviaSyntax;
			}
			return errorDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00119A47 File Offset: 0x00117C47
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00119A50 File Offset: 0x00117C50
		public new ErrorDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.ErrorKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00119A6B File Offset: 0x00117C6B
		public ErrorDirectiveTriviaSyntax WithErrorKeyword(SyntaxToken errorKeyword)
		{
			return this.Update(this.HashToken, errorKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x00119A86 File Offset: 0x00117C86
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x00119A8F File Offset: 0x00117C8F
		public new ErrorDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.ErrorKeyword, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x00119AAA File Offset: 0x00117CAA
		public ErrorDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.ErrorKeyword, this.EndOfDirectiveToken, isActive);
		}
	}
}
