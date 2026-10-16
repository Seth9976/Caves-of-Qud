using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000350 RID: 848
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LineDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004789 RID: 18313 RVA: 0x0011A193 File Offset: 0x00118393
		internal LineDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x0011A19E File Offset: 0x0011839E
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((LineDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x0600478B RID: 18315 RVA: 0x0011A1BD File Offset: 0x001183BD
		public SyntaxToken LineKeyword
		{
			get
			{
				return new SyntaxToken(this, ((LineDirectiveTriviaSyntax)base.Green).lineKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x0011A1E3 File Offset: 0x001183E3
		public SyntaxToken Line
		{
			get
			{
				return new SyntaxToken(this, ((LineDirectiveTriviaSyntax)base.Green).line, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x0600478D RID: 18317 RVA: 0x0011A20C File Offset: 0x0011840C
		public SyntaxToken File
		{
			get
			{
				SyntaxToken file = ((LineDirectiveTriviaSyntax)base.Green).file;
				if (file == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, file, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x0011A24C File Offset: 0x0011844C
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((LineDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x0011A272 File Offset: 0x00118472
		public override bool IsActive
		{
			get
			{
				return ((LineDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x0011A284 File Offset: 0x00118484
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0011A287 File Offset: 0x00118487
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x0011A28A File Offset: 0x0011848A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLineDirectiveTrivia(this);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x0011A293 File Offset: 0x00118493
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLineDirectiveTrivia(this);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x0011A29C File Offset: 0x0011849C
		public LineDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(lineKeyword != this.LineKeyword) && !(line != this.Line) && !(file != this.File) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			LineDirectiveTriviaSyntax lineDirectiveTriviaSyntax = SyntaxFactory.LineDirectiveTrivia(hashToken, lineKeyword, line, file, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return lineDirectiveTriviaSyntax;
			}
			return lineDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x0011A319 File Offset: 0x00118519
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x0011A322 File Offset: 0x00118522
		public new LineDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.LineKeyword, this.Line, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x0011A349 File Offset: 0x00118549
		public LineDirectiveTriviaSyntax WithLineKeyword(SyntaxToken lineKeyword)
		{
			return this.Update(this.HashToken, lineKeyword, this.Line, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x0011A370 File Offset: 0x00118570
		public LineDirectiveTriviaSyntax WithLine(SyntaxToken line)
		{
			return this.Update(this.HashToken, this.LineKeyword, line, this.File, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x0011A397 File Offset: 0x00118597
		public LineDirectiveTriviaSyntax WithFile(SyntaxToken file)
		{
			return this.Update(this.HashToken, this.LineKeyword, this.Line, file, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0011A3BE File Offset: 0x001185BE
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x0011A3C7 File Offset: 0x001185C7
		public new LineDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.LineKeyword, this.Line, this.File, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0011A3EE File Offset: 0x001185EE
		public LineDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.LineKeyword, this.Line, this.File, this.EndOfDirectiveToken, isActive);
		}
	}
}
