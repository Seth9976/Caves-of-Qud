using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000352 RID: 850
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PragmaChecksumDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x060047B4 RID: 18356 RVA: 0x0011A756 File Offset: 0x00118956
		internal PragmaChecksumDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x060047B5 RID: 18357 RVA: 0x0011A761 File Offset: 0x00118961
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x060047B6 RID: 18358 RVA: 0x0011A780 File Offset: 0x00118980
		public SyntaxToken PragmaKeyword
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).pragmaKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x0011A7A6 File Offset: 0x001189A6
		public SyntaxToken ChecksumKeyword
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).checksumKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x0011A7CC File Offset: 0x001189CC
		public SyntaxToken File
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).file, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x0011A7F2 File Offset: 0x001189F2
		public SyntaxToken Guid
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).guid, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x0011A818 File Offset: 0x00118A18
		public SyntaxToken Bytes
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).bytes, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x0011A83E File Offset: 0x00118A3E
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((PragmaChecksumDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(6), base.GetChildIndex(6));
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x0011A864 File Offset: 0x00118A64
		public override bool IsActive
		{
			get
			{
				return ((PragmaChecksumDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x0011A876 File Offset: 0x00118A76
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0011A879 File Offset: 0x00118A79
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0011A87C File Offset: 0x00118A7C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPragmaChecksumDirectiveTrivia(this);
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0011A885 File Offset: 0x00118A85
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPragmaChecksumDirectiveTrivia(this);
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x0011A890 File Offset: 0x00118A90
		public PragmaChecksumDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(pragmaKeyword != this.PragmaKeyword) && !(checksumKeyword != this.ChecksumKeyword) && !(file != this.File) && !(guid != this.Guid) && !(bytes != this.Bytes) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			PragmaChecksumDirectiveTriviaSyntax pragmaChecksumDirectiveTriviaSyntax = SyntaxFactory.PragmaChecksumDirectiveTrivia(hashToken, pragmaKeyword, checksumKeyword, file, guid, bytes, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return pragmaChecksumDirectiveTriviaSyntax;
			}
			return pragmaChecksumDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x0011A92F File Offset: 0x00118B2F
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x0011A938 File Offset: 0x00118B38
		public new PragmaChecksumDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.PragmaKeyword, this.ChecksumKeyword, this.File, this.Guid, this.Bytes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0011A978 File Offset: 0x00118B78
		public PragmaChecksumDirectiveTriviaSyntax WithPragmaKeyword(SyntaxToken pragmaKeyword)
		{
			return this.Update(this.HashToken, pragmaKeyword, this.ChecksumKeyword, this.File, this.Guid, this.Bytes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0011A9B8 File Offset: 0x00118BB8
		public PragmaChecksumDirectiveTriviaSyntax WithChecksumKeyword(SyntaxToken checksumKeyword)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, checksumKeyword, this.File, this.Guid, this.Bytes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0011A9F8 File Offset: 0x00118BF8
		public PragmaChecksumDirectiveTriviaSyntax WithFile(SyntaxToken file)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.ChecksumKeyword, file, this.Guid, this.Bytes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x0011AA38 File Offset: 0x00118C38
		public PragmaChecksumDirectiveTriviaSyntax WithGuid(SyntaxToken guid)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.ChecksumKeyword, this.File, guid, this.Bytes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x0011AA78 File Offset: 0x00118C78
		public PragmaChecksumDirectiveTriviaSyntax WithBytes(SyntaxToken bytes)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.ChecksumKeyword, this.File, this.Guid, bytes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x0011AAB6 File Offset: 0x00118CB6
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x0011AAC0 File Offset: 0x00118CC0
		public new PragmaChecksumDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.ChecksumKeyword, this.File, this.Guid, this.Bytes, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0011AB00 File Offset: 0x00118D00
		public PragmaChecksumDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.ChecksumKeyword, this.File, this.Guid, this.Bytes, this.EndOfDirectiveToken, isActive);
		}
	}
}
