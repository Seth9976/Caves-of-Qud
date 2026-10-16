using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000354 RID: 852
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LoadDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x060047DE RID: 18398 RVA: 0x0011AD27 File Offset: 0x00118F27
		internal LoadDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x0011AD32 File Offset: 0x00118F32
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((LoadDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x0011AD51 File Offset: 0x00118F51
		public SyntaxToken LoadKeyword
		{
			get
			{
				return new SyntaxToken(this, ((LoadDirectiveTriviaSyntax)base.Green).loadKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x0011AD77 File Offset: 0x00118F77
		public SyntaxToken File
		{
			get
			{
				return new SyntaxToken(this, ((LoadDirectiveTriviaSyntax)base.Green).file, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x0011AD9D File Offset: 0x00118F9D
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((LoadDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x0011ADC3 File Offset: 0x00118FC3
		public override bool IsActive
		{
			get
			{
				return ((LoadDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0011ADD5 File Offset: 0x00118FD5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0011ADD8 File Offset: 0x00118FD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x0011ADDB File Offset: 0x00118FDB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLoadDirectiveTrivia(this);
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x0011ADE4 File Offset: 0x00118FE4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLoadDirectiveTrivia(this);
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x0011ADF0 File Offset: 0x00118FF0
		public LoadDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(loadKeyword != this.LoadKeyword) && !(file != this.File) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			LoadDirectiveTriviaSyntax loadDirectiveTriviaSyntax = SyntaxFactory.LoadDirectiveTrivia(hashToken, loadKeyword, file, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return loadDirectiveTriviaSyntax;
			}
			return loadDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0011AE5C File Offset: 0x0011905C
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0011AE65 File Offset: 0x00119065
		public new LoadDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.LoadKeyword, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0011AE86 File Offset: 0x00119086
		public LoadDirectiveTriviaSyntax WithLoadKeyword(SyntaxToken loadKeyword)
		{
			return this.Update(this.HashToken, loadKeyword, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0011AEA7 File Offset: 0x001190A7
		public LoadDirectiveTriviaSyntax WithFile(SyntaxToken file)
		{
			return this.Update(this.HashToken, this.LoadKeyword, file, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0011AEC8 File Offset: 0x001190C8
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x0011AED1 File Offset: 0x001190D1
		public new LoadDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.LoadKeyword, this.File, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x0011AEF2 File Offset: 0x001190F2
		public LoadDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.LoadKeyword, this.File, this.EndOfDirectiveToken, isActive);
		}
	}
}
