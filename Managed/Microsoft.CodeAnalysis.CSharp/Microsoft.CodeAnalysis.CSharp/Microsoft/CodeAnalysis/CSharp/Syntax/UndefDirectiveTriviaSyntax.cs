using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200034F RID: 847
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class UndefDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004777 RID: 18295 RVA: 0x00119FA7 File Offset: 0x001181A7
		internal UndefDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x00119FB2 File Offset: 0x001181B2
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((UndefDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x00119FD1 File Offset: 0x001181D1
		public SyntaxToken UndefKeyword
		{
			get
			{
				return new SyntaxToken(this, ((UndefDirectiveTriviaSyntax)base.Green).undefKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x00119FF7 File Offset: 0x001181F7
		public SyntaxToken Name
		{
			get
			{
				return new SyntaxToken(this, ((UndefDirectiveTriviaSyntax)base.Green).name, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x0011A01D File Offset: 0x0011821D
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((UndefDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x0011A043 File Offset: 0x00118243
		public override bool IsActive
		{
			get
			{
				return ((UndefDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x0011A055 File Offset: 0x00118255
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x0011A058 File Offset: 0x00118258
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x0011A05B File Offset: 0x0011825B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUndefDirectiveTrivia(this);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x0011A064 File Offset: 0x00118264
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUndefDirectiveTrivia(this);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x0011A070 File Offset: 0x00118270
		public UndefDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(undefKeyword != this.UndefKeyword) && !(name != this.Name) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			UndefDirectiveTriviaSyntax undefDirectiveTriviaSyntax = SyntaxFactory.UndefDirectiveTrivia(hashToken, undefKeyword, name, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return undefDirectiveTriviaSyntax;
			}
			return undefDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x0011A0DC File Offset: 0x001182DC
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x0011A0E5 File Offset: 0x001182E5
		public new UndefDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.UndefKeyword, this.Name, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x0011A106 File Offset: 0x00118306
		public UndefDirectiveTriviaSyntax WithUndefKeyword(SyntaxToken undefKeyword)
		{
			return this.Update(this.HashToken, undefKeyword, this.Name, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x0011A127 File Offset: 0x00118327
		public UndefDirectiveTriviaSyntax WithName(SyntaxToken name)
		{
			return this.Update(this.HashToken, this.UndefKeyword, name, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x0011A148 File Offset: 0x00118348
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x0011A151 File Offset: 0x00118351
		public new UndefDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.UndefKeyword, this.Name, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x0011A172 File Offset: 0x00118372
		public UndefDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.UndefKeyword, this.Name, this.EndOfDirectiveToken, isActive);
		}
	}
}
