using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200034A RID: 842
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EndRegionDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004725 RID: 18213 RVA: 0x001197CD File Offset: 0x001179CD
		internal EndRegionDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x001197D8 File Offset: 0x001179D8
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((EndRegionDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x001197F7 File Offset: 0x001179F7
		public SyntaxToken EndRegionKeyword
		{
			get
			{
				return new SyntaxToken(this, ((EndRegionDirectiveTriviaSyntax)base.Green).endRegionKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x0011981D File Offset: 0x00117A1D
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((EndRegionDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004729 RID: 18217 RVA: 0x00119843 File Offset: 0x00117A43
		public override bool IsActive
		{
			get
			{
				return ((EndRegionDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00119855 File Offset: 0x00117A55
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x00119858 File Offset: 0x00117A58
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x0011985B File Offset: 0x00117A5B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEndRegionDirectiveTrivia(this);
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00119864 File Offset: 0x00117A64
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEndRegionDirectiveTrivia(this);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00119870 File Offset: 0x00117A70
		public EndRegionDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(endRegionKeyword != this.EndRegionKeyword) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			EndRegionDirectiveTriviaSyntax endRegionDirectiveTriviaSyntax = SyntaxFactory.EndRegionDirectiveTrivia(hashToken, endRegionKeyword, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return endRegionDirectiveTriviaSyntax;
			}
			return endRegionDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x001198CB File Offset: 0x00117ACB
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x001198D4 File Offset: 0x00117AD4
		public new EndRegionDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.EndRegionKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x001198EF File Offset: 0x00117AEF
		public EndRegionDirectiveTriviaSyntax WithEndRegionKeyword(SyntaxToken endRegionKeyword)
		{
			return this.Update(this.HashToken, endRegionKeyword, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x0011990A File Offset: 0x00117B0A
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00119913 File Offset: 0x00117B13
		public new EndRegionDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.EndRegionKeyword, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x0011992E File Offset: 0x00117B2E
		public EndRegionDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.EndRegionKeyword, this.EndOfDirectiveToken, isActive);
		}
	}
}
