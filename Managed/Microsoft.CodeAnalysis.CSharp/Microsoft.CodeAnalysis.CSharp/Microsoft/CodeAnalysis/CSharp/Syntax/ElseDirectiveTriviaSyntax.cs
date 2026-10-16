using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000347 RID: 839
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ElseDirectiveTriviaSyntax : BranchingDirectiveTriviaSyntax
	{
		// Token: 0x060046F3 RID: 18163 RVA: 0x00119311 File Offset: 0x00117511
		internal ElseDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x0011931C File Offset: 0x0011751C
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((ElseDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x0011933B File Offset: 0x0011753B
		public SyntaxToken ElseKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ElseDirectiveTriviaSyntax)base.Green).elseKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x00119361 File Offset: 0x00117561
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((ElseDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x00119387 File Offset: 0x00117587
		public override bool IsActive
		{
			get
			{
				return ((ElseDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x00119399 File Offset: 0x00117599
		public override bool BranchTaken
		{
			get
			{
				return ((ElseDirectiveTriviaSyntax)base.Green).BranchTaken;
			}
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x001193AB File Offset: 0x001175AB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x001193AE File Offset: 0x001175AE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x001193B1 File Offset: 0x001175B1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElseDirectiveTrivia(this);
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x001193BA File Offset: 0x001175BA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElseDirectiveTrivia(this);
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x001193C4 File Offset: 0x001175C4
		public ElseDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
		{
			if (!(hashToken != this.HashToken) && !(elseKeyword != this.ElseKeyword) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			ElseDirectiveTriviaSyntax elseDirectiveTriviaSyntax = SyntaxFactory.ElseDirectiveTrivia(hashToken, elseKeyword, endOfDirectiveToken, isActive, branchTaken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return elseDirectiveTriviaSyntax;
			}
			return elseDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x00119421 File Offset: 0x00117621
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x0011942A File Offset: 0x0011762A
		public new ElseDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.ElseKeyword, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x0011944B File Offset: 0x0011764B
		public ElseDirectiveTriviaSyntax WithElseKeyword(SyntaxToken elseKeyword)
		{
			return this.Update(this.HashToken, elseKeyword, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken);
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x0011946C File Offset: 0x0011766C
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x00119475 File Offset: 0x00117675
		public new ElseDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.ElseKeyword, endOfDirectiveToken, this.IsActive, this.BranchTaken);
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x00119496 File Offset: 0x00117696
		public ElseDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.ElseKeyword, this.EndOfDirectiveToken, isActive, this.BranchTaken);
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x001194B7 File Offset: 0x001176B7
		public ElseDirectiveTriviaSyntax WithBranchTaken(bool branchTaken)
		{
			return this.Update(this.HashToken, this.ElseKeyword, this.EndOfDirectiveToken, this.IsActive, branchTaken);
		}
	}
}
