using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000475 RID: 1141
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class WarningDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005F91 RID: 24465 RVA: 0x00171E1C File Offset: 0x0017001C
		internal WarningDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(warningKeyword);
			this.warningKeyword = warningKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x00171E70 File Offset: 0x00170070
		internal WarningDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(warningKeyword);
			this.warningKeyword = warningKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x00171EC8 File Offset: 0x001700C8
		internal WarningDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(warningKeyword);
			this.warningKeyword = warningKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06005F94 RID: 24468 RVA: 0x00171F17 File Offset: 0x00170117
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06005F95 RID: 24469 RVA: 0x00171F1F File Offset: 0x0017011F
		public SyntaxToken WarningKeyword
		{
			get
			{
				return this.warningKeyword;
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x00171F27 File Offset: 0x00170127
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06005F97 RID: 24471 RVA: 0x00171F2F File Offset: 0x0017012F
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x00171F38 File Offset: 0x00170138
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
				syntaxToken = this.warningKeyword;
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

		// Token: 0x06005F99 RID: 24473 RVA: 0x00171F77 File Offset: 0x00170177
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new WarningDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x00171F81 File Offset: 0x00170181
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWarningDirectiveTrivia(this);
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x00171F8A File Offset: 0x0017018A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWarningDirectiveTrivia(this);
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x00171F94 File Offset: 0x00170194
		public WarningDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || warningKeyword != this.WarningKeyword || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				WarningDirectiveTriviaSyntax warningDirectiveTriviaSyntax = SyntaxFactory.WarningDirectiveTrivia(hashToken, warningKeyword, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					warningDirectiveTriviaSyntax = warningDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					warningDirectiveTriviaSyntax = warningDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return warningDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x00171FF6 File Offset: 0x001701F6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new WarningDirectiveTriviaSyntax(base.Kind, this.hashToken, this.warningKeyword, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x00172022 File Offset: 0x00170222
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new WarningDirectiveTriviaSyntax(base.Kind, this.hashToken, this.warningKeyword, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x00172050 File Offset: 0x00170250
		internal WarningDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.warningKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x001720C5 File Offset: 0x001702C5
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.warningKeyword);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x001720FE File Offset: 0x001702FE
		static WarningDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(WarningDirectiveTriviaSyntax), (ObjectReader r) => new WarningDirectiveTriviaSyntax(r));
		}

		// Token: 0x0400190F RID: 6415
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001910 RID: 6416
		internal readonly SyntaxToken warningKeyword;

		// Token: 0x04001911 RID: 6417
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001912 RID: 6418
		internal readonly bool isActive;
	}
}
