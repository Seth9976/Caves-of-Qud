using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000353 RID: 851
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ReferenceDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x060047CC RID: 18380 RVA: 0x0011AB3E File Offset: 0x00118D3E
		internal ReferenceDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x060047CD RID: 18381 RVA: 0x0011AB49 File Offset: 0x00118D49
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((ReferenceDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x0011AB68 File Offset: 0x00118D68
		public SyntaxToken ReferenceKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ReferenceDirectiveTriviaSyntax)base.Green).referenceKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x060047CF RID: 18383 RVA: 0x0011AB8E File Offset: 0x00118D8E
		public SyntaxToken File
		{
			get
			{
				return new SyntaxToken(this, ((ReferenceDirectiveTriviaSyntax)base.Green).file, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x0011ABB4 File Offset: 0x00118DB4
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((ReferenceDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x0011ABDA File Offset: 0x00118DDA
		public override bool IsActive
		{
			get
			{
				return ((ReferenceDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x0011ABEC File Offset: 0x00118DEC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0011ABEF File Offset: 0x00118DEF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0011ABF2 File Offset: 0x00118DF2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitReferenceDirectiveTrivia(this);
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x0011ABFB File Offset: 0x00118DFB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitReferenceDirectiveTrivia(this);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x0011AC04 File Offset: 0x00118E04
		public ReferenceDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(referenceKeyword != this.ReferenceKeyword) && !(file != this.File) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			ReferenceDirectiveTriviaSyntax referenceDirectiveTriviaSyntax = SyntaxFactory.ReferenceDirectiveTrivia(hashToken, referenceKeyword, file, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return referenceDirectiveTriviaSyntax;
			}
			return referenceDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x0011AC70 File Offset: 0x00118E70
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x0011AC79 File Offset: 0x00118E79
		public new ReferenceDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.ReferenceKeyword, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0011AC9A File Offset: 0x00118E9A
		public ReferenceDirectiveTriviaSyntax WithReferenceKeyword(SyntaxToken referenceKeyword)
		{
			return this.Update(this.HashToken, referenceKeyword, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0011ACBB File Offset: 0x00118EBB
		public ReferenceDirectiveTriviaSyntax WithFile(SyntaxToken file)
		{
			return this.Update(this.HashToken, this.ReferenceKeyword, file, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0011ACDC File Offset: 0x00118EDC
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0011ACE5 File Offset: 0x00118EE5
		public new ReferenceDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.ReferenceKeyword, this.File, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x0011AD06 File Offset: 0x00118F06
		public ReferenceDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.ReferenceKeyword, this.File, this.EndOfDirectiveToken, isActive);
		}
	}
}
