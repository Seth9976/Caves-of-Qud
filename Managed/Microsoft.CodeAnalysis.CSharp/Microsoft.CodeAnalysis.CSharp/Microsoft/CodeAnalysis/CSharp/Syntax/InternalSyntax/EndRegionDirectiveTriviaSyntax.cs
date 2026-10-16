using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000473 RID: 1139
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EndRegionDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005F6F RID: 24431 RVA: 0x00171814 File Offset: 0x0016FA14
		internal EndRegionDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(endRegionKeyword);
			this.endRegionKeyword = endRegionKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x00171868 File Offset: 0x0016FA68
		internal EndRegionDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(endRegionKeyword);
			this.endRegionKeyword = endRegionKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x001718C0 File Offset: 0x0016FAC0
		internal EndRegionDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(endRegionKeyword);
			this.endRegionKeyword = endRegionKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06005F72 RID: 24434 RVA: 0x0017190F File Offset: 0x0016FB0F
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06005F73 RID: 24435 RVA: 0x00171917 File Offset: 0x0016FB17
		public SyntaxToken EndRegionKeyword
		{
			get
			{
				return this.endRegionKeyword;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06005F74 RID: 24436 RVA: 0x0017191F File Offset: 0x0016FB1F
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06005F75 RID: 24437 RVA: 0x00171927 File Offset: 0x0016FB27
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x00171930 File Offset: 0x0016FB30
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
				syntaxToken = this.endRegionKeyword;
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

		// Token: 0x06005F77 RID: 24439 RVA: 0x0017196F File Offset: 0x0016FB6F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EndRegionDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x00171979 File Offset: 0x0016FB79
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEndRegionDirectiveTrivia(this);
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x00171982 File Offset: 0x0016FB82
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEndRegionDirectiveTrivia(this);
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x0017198C File Offset: 0x0016FB8C
		public EndRegionDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || endRegionKeyword != this.EndRegionKeyword || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				EndRegionDirectiveTriviaSyntax endRegionDirectiveTriviaSyntax = SyntaxFactory.EndRegionDirectiveTrivia(hashToken, endRegionKeyword, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					endRegionDirectiveTriviaSyntax = endRegionDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					endRegionDirectiveTriviaSyntax = endRegionDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return endRegionDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x001719EE File Offset: 0x0016FBEE
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EndRegionDirectiveTriviaSyntax(base.Kind, this.hashToken, this.endRegionKeyword, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x00171A1A File Offset: 0x0016FC1A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EndRegionDirectiveTriviaSyntax(base.Kind, this.hashToken, this.endRegionKeyword, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x00171A48 File Offset: 0x0016FC48
		internal EndRegionDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.endRegionKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x00171ABD File Offset: 0x0016FCBD
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.endRegionKeyword);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x00171AF6 File Offset: 0x0016FCF6
		static EndRegionDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EndRegionDirectiveTriviaSyntax), (ObjectReader r) => new EndRegionDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001907 RID: 6407
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001908 RID: 6408
		internal readonly SyntaxToken endRegionKeyword;

		// Token: 0x04001909 RID: 6409
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x0400190A RID: 6410
		internal readonly bool isActive;
	}
}
