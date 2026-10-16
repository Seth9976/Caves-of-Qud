using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000470 RID: 1136
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ElseDirectiveTriviaSyntax : BranchingDirectiveTriviaSyntax
	{
		// Token: 0x06005F3B RID: 24379 RVA: 0x00170EB4 File Offset: 0x0016F0B4
		internal ElseDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(elseKeyword);
			this.elseKeyword = elseKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x00170F10 File Offset: 0x0016F110
		internal ElseDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(elseKeyword);
			this.elseKeyword = elseKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x00170F70 File Offset: 0x0016F170
		internal ElseDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(elseKeyword);
			this.elseKeyword = elseKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06005F3E RID: 24382 RVA: 0x00170FC7 File Offset: 0x0016F1C7
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x00170FCF File Offset: 0x0016F1CF
		public SyntaxToken ElseKeyword
		{
			get
			{
				return this.elseKeyword;
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06005F40 RID: 24384 RVA: 0x00170FD7 File Offset: 0x0016F1D7
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x00170FDF File Offset: 0x0016F1DF
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06005F42 RID: 24386 RVA: 0x00170FE7 File Offset: 0x0016F1E7
		public override bool BranchTaken
		{
			get
			{
				return this.branchTaken;
			}
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x00170FF0 File Offset: 0x0016F1F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.hashToken;
				break;
			case 1:
				syntaxToken = this.elseKeyword;
				break;
			case 2:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x0017102F File Offset: 0x0016F22F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ElseDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x00171039 File Offset: 0x0016F239
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElseDirectiveTrivia(this);
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x00171042 File Offset: 0x0016F242
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElseDirectiveTrivia(this);
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x0017104C File Offset: 0x0016F24C
		public ElseDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
		{
			if (hashToken != this.HashToken || elseKeyword != this.ElseKeyword || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				ElseDirectiveTriviaSyntax elseDirectiveTriviaSyntax = SyntaxFactory.ElseDirectiveTrivia(hashToken, elseKeyword, endOfDirectiveToken, isActive, branchTaken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					elseDirectiveTriviaSyntax = elseDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					elseDirectiveTriviaSyntax = elseDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return elseDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x001710B0 File Offset: 0x0016F2B0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ElseDirectiveTriviaSyntax(base.Kind, this.hashToken, this.elseKeyword, this.endOfDirectiveToken, this.isActive, this.branchTaken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x001710E2 File Offset: 0x0016F2E2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ElseDirectiveTriviaSyntax(base.Kind, this.hashToken, this.elseKeyword, this.endOfDirectiveToken, this.isActive, this.branchTaken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x00171114 File Offset: 0x0016F314
		internal ElseDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.elseKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
			this.branchTaken = reader.ReadBoolean();
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x00171198 File Offset: 0x0016F398
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.elseKeyword);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
			writer.WriteBoolean(this.branchTaken);
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x001711E8 File Offset: 0x0016F3E8
		static ElseDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ElseDirectiveTriviaSyntax), (ObjectReader r) => new ElseDirectiveTriviaSyntax(r));
		}

		// Token: 0x040018FA RID: 6394
		internal readonly SyntaxToken hashToken;

		// Token: 0x040018FB RID: 6395
		internal readonly SyntaxToken elseKeyword;

		// Token: 0x040018FC RID: 6396
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x040018FD RID: 6397
		internal readonly bool isActive;

		// Token: 0x040018FE RID: 6398
		internal readonly bool branchTaken;
	}
}
