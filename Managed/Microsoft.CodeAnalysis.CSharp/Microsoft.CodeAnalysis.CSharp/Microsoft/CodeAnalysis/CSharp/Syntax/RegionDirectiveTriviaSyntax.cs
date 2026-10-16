using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000349 RID: 841
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RegionDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004715 RID: 18197 RVA: 0x00119651 File Offset: 0x00117851
		internal RegionDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x0011965C File Offset: 0x0011785C
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((RegionDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x0011967B File Offset: 0x0011787B
		public SyntaxToken RegionKeyword
		{
			get
			{
				return new SyntaxToken(this, ((RegionDirectiveTriviaSyntax)base.Green).regionKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x001196A1 File Offset: 0x001178A1
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((RegionDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x001196C7 File Offset: 0x001178C7
		public override bool IsActive
		{
			get
			{
				return ((RegionDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x001196D9 File Offset: 0x001178D9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x001196DC File Offset: 0x001178DC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x001196DF File Offset: 0x001178DF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRegionDirectiveTrivia(this);
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x001196E8 File Offset: 0x001178E8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRegionDirectiveTrivia(this);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x001196F4 File Offset: 0x001178F4
		public RegionDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(regionKeyword != this.RegionKeyword) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			RegionDirectiveTriviaSyntax regionDirectiveTriviaSyntax = SyntaxFactory.RegionDirectiveTrivia(hashToken, regionKeyword, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return regionDirectiveTriviaSyntax;
			}
			return regionDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x0011974F File Offset: 0x0011794F
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00119758 File Offset: 0x00117958
		public new RegionDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.RegionKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x00119773 File Offset: 0x00117973
		public RegionDirectiveTriviaSyntax WithRegionKeyword(SyntaxToken regionKeyword)
		{
			return this.Update(this.HashToken, regionKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x0011978E File Offset: 0x0011798E
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x00119797 File Offset: 0x00117997
		public new RegionDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.RegionKeyword, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x001197B2 File Offset: 0x001179B2
		public RegionDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.RegionKeyword, this.EndOfDirectiveToken, isActive);
		}
	}
}
