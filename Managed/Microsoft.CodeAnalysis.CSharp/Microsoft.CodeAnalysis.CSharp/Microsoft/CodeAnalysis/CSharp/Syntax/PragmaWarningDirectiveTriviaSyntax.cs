using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000351 RID: 849
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PragmaWarningDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x0600479D RID: 18333 RVA: 0x0011A415 File Offset: 0x00118615
		internal PragmaWarningDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x0600479E RID: 18334 RVA: 0x0011A420 File Offset: 0x00118620
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((PragmaWarningDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x0011A43F File Offset: 0x0011863F
		public SyntaxToken PragmaKeyword
		{
			get
			{
				return new SyntaxToken(this, ((PragmaWarningDirectiveTriviaSyntax)base.Green).pragmaKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x060047A0 RID: 18336 RVA: 0x0011A465 File Offset: 0x00118665
		public SyntaxToken WarningKeyword
		{
			get
			{
				return new SyntaxToken(this, ((PragmaWarningDirectiveTriviaSyntax)base.Green).warningKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x060047A1 RID: 18337 RVA: 0x0011A48B File Offset: 0x0011868B
		public SyntaxToken DisableOrRestoreKeyword
		{
			get
			{
				return new SyntaxToken(this, ((PragmaWarningDirectiveTriviaSyntax)base.Green).disableOrRestoreKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x0011A4B4 File Offset: 0x001186B4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> ErrorCodes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.errorCodes, 4);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ExpressionSyntax>);
				}
				return new SeparatedSyntaxList<ExpressionSyntax>(red, base.GetChildIndex(4));
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x0011A4E9 File Offset: 0x001186E9
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((PragmaWarningDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x0011A50F File Offset: 0x0011870F
		public override bool IsActive
		{
			get
			{
				return ((PragmaWarningDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0011A521 File Offset: 0x00118721
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 4)
			{
				return null;
			}
			return base.GetRed(ref this.errorCodes, 4);
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0011A536 File Offset: 0x00118736
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 4)
			{
				return null;
			}
			return this.errorCodes;
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0011A544 File Offset: 0x00118744
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPragmaWarningDirectiveTrivia(this);
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x0011A54D File Offset: 0x0011874D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPragmaWarningDirectiveTrivia(this);
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0011A558 File Offset: 0x00118758
		public PragmaWarningDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(pragmaKeyword != this.PragmaKeyword) && !(warningKeyword != this.WarningKeyword) && !(disableOrRestoreKeyword != this.DisableOrRestoreKeyword) && !(errorCodes != this.ErrorCodes) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			PragmaWarningDirectiveTriviaSyntax pragmaWarningDirectiveTriviaSyntax = SyntaxFactory.PragmaWarningDirectiveTrivia(hashToken, pragmaKeyword, warningKeyword, disableOrRestoreKeyword, errorCodes, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return pragmaWarningDirectiveTriviaSyntax;
			}
			return pragmaWarningDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0011A5E6 File Offset: 0x001187E6
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0011A5EF File Offset: 0x001187EF
		public new PragmaWarningDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.PragmaKeyword, this.WarningKeyword, this.DisableOrRestoreKeyword, this.ErrorCodes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x0011A61C File Offset: 0x0011881C
		public PragmaWarningDirectiveTriviaSyntax WithPragmaKeyword(SyntaxToken pragmaKeyword)
		{
			return this.Update(this.HashToken, pragmaKeyword, this.WarningKeyword, this.DisableOrRestoreKeyword, this.ErrorCodes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0011A649 File Offset: 0x00118849
		public PragmaWarningDirectiveTriviaSyntax WithWarningKeyword(SyntaxToken warningKeyword)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, warningKeyword, this.DisableOrRestoreKeyword, this.ErrorCodes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0011A676 File Offset: 0x00118876
		public PragmaWarningDirectiveTriviaSyntax WithDisableOrRestoreKeyword(SyntaxToken disableOrRestoreKeyword)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.WarningKeyword, disableOrRestoreKeyword, this.ErrorCodes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x0011A6A3 File Offset: 0x001188A3
		public PragmaWarningDirectiveTriviaSyntax WithErrorCodes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> errorCodes)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.WarningKeyword, this.DisableOrRestoreKeyword, errorCodes, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x0011A6D0 File Offset: 0x001188D0
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0011A6D9 File Offset: 0x001188D9
		public new PragmaWarningDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.WarningKeyword, this.DisableOrRestoreKeyword, this.ErrorCodes, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x0011A706 File Offset: 0x00118906
		public PragmaWarningDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.PragmaKeyword, this.WarningKeyword, this.DisableOrRestoreKeyword, this.ErrorCodes, this.EndOfDirectiveToken, isActive);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x0011A734 File Offset: 0x00118934
		public PragmaWarningDirectiveTriviaSyntax AddErrorCodes(params ExpressionSyntax[] items)
		{
			return this.WithErrorCodes(this.ErrorCodes.AddRange(items));
		}

		// Token: 0x0400152C RID: 5420
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode errorCodes;
	}
}
