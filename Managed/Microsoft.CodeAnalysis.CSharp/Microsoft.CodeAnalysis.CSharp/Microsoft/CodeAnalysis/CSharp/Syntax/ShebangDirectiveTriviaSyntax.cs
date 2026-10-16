using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000355 RID: 853
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ShebangDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x060047F0 RID: 18416 RVA: 0x0011AF13 File Offset: 0x00119113
		internal ShebangDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x0011AF1E File Offset: 0x0011911E
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((ShebangDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x0011AF3D File Offset: 0x0011913D
		public SyntaxToken ExclamationToken
		{
			get
			{
				return new SyntaxToken(this, ((ShebangDirectiveTriviaSyntax)base.Green).exclamationToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x0011AF63 File Offset: 0x00119163
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((ShebangDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x060047F4 RID: 18420 RVA: 0x0011AF89 File Offset: 0x00119189
		public override bool IsActive
		{
			get
			{
				return ((ShebangDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0011AF9B File Offset: 0x0011919B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0011AF9E File Offset: 0x0011919E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x0011AFA1 File Offset: 0x001191A1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitShebangDirectiveTrivia(this);
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x0011AFAA File Offset: 0x001191AA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitShebangDirectiveTrivia(this);
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x0011AFB4 File Offset: 0x001191B4
		public ShebangDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(exclamationToken != this.ExclamationToken) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			ShebangDirectiveTriviaSyntax shebangDirectiveTriviaSyntax = SyntaxFactory.ShebangDirectiveTrivia(hashToken, exclamationToken, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return shebangDirectiveTriviaSyntax;
			}
			return shebangDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x0011B00F File Offset: 0x0011920F
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x0011B018 File Offset: 0x00119218
		public new ShebangDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.ExclamationToken, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0011B033 File Offset: 0x00119233
		public ShebangDirectiveTriviaSyntax WithExclamationToken(SyntaxToken exclamationToken)
		{
			return this.Update(this.HashToken, exclamationToken, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0011B04E File Offset: 0x0011924E
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0011B057 File Offset: 0x00119257
		public new ShebangDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.ExclamationToken, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x0011B072 File Offset: 0x00119272
		public ShebangDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.ExclamationToken, this.EndOfDirectiveToken, isActive);
		}
	}
}
