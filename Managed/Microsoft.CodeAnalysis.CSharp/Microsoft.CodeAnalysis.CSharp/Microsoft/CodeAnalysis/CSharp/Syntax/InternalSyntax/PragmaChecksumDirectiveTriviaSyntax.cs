using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200047B RID: 1147
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PragmaChecksumDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005FFE RID: 24574 RVA: 0x00173478 File Offset: 0x00171678
		internal PragmaChecksumDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 7;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(pragmaKeyword);
			this.pragmaKeyword = pragmaKeyword;
			base.AdjustFlagsAndWidth(checksumKeyword);
			this.checksumKeyword = checksumKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(guid);
			this.guid = guid;
			base.AdjustFlagsAndWidth(bytes);
			this.bytes = bytes;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x0017350C File Offset: 0x0017170C
		internal PragmaChecksumDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 7;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(pragmaKeyword);
			this.pragmaKeyword = pragmaKeyword;
			base.AdjustFlagsAndWidth(checksumKeyword);
			this.checksumKeyword = checksumKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(guid);
			this.guid = guid;
			base.AdjustFlagsAndWidth(bytes);
			this.bytes = bytes;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x001735A4 File Offset: 0x001717A4
		internal PragmaChecksumDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 7;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(pragmaKeyword);
			this.pragmaKeyword = pragmaKeyword;
			base.AdjustFlagsAndWidth(checksumKeyword);
			this.checksumKeyword = checksumKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(guid);
			this.guid = guid;
			base.AdjustFlagsAndWidth(bytes);
			this.bytes = bytes;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x00173633 File Offset: 0x00171833
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06006002 RID: 24578 RVA: 0x0017363B File Offset: 0x0017183B
		public SyntaxToken PragmaKeyword
		{
			get
			{
				return this.pragmaKeyword;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06006003 RID: 24579 RVA: 0x00173643 File Offset: 0x00171843
		public SyntaxToken ChecksumKeyword
		{
			get
			{
				return this.checksumKeyword;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x0017364B File Offset: 0x0017184B
		public SyntaxToken File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06006005 RID: 24581 RVA: 0x00173653 File Offset: 0x00171853
		public SyntaxToken Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x0017365B File Offset: 0x0017185B
		public SyntaxToken Bytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x00173663 File Offset: 0x00171863
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x0017366B File Offset: 0x0017186B
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x00173674 File Offset: 0x00171874
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
				syntaxToken = this.pragmaKeyword;
				break;
			case 2:
				syntaxToken = this.checksumKeyword;
				break;
			case 3:
				syntaxToken = this.file;
				break;
			case 4:
				syntaxToken = this.guid;
				break;
			case 5:
				syntaxToken = this.bytes;
				break;
			case 6:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x001736E7 File Offset: 0x001718E7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PragmaChecksumDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x001736F1 File Offset: 0x001718F1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPragmaChecksumDirectiveTrivia(this);
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x001736FA File Offset: 0x001718FA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPragmaChecksumDirectiveTrivia(this);
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x00173704 File Offset: 0x00171904
		public PragmaChecksumDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || pragmaKeyword != this.PragmaKeyword || checksumKeyword != this.ChecksumKeyword || file != this.File || guid != this.Guid || bytes != this.Bytes || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				PragmaChecksumDirectiveTriviaSyntax pragmaChecksumDirectiveTriviaSyntax = SyntaxFactory.PragmaChecksumDirectiveTrivia(hashToken, pragmaKeyword, checksumKeyword, file, guid, bytes, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					pragmaChecksumDirectiveTriviaSyntax = pragmaChecksumDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					pragmaChecksumDirectiveTriviaSyntax = pragmaChecksumDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return pragmaChecksumDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x00173798 File Offset: 0x00171998
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PragmaChecksumDirectiveTriviaSyntax(base.Kind, this.hashToken, this.pragmaKeyword, this.checksumKeyword, this.file, this.guid, this.bytes, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x001737E8 File Offset: 0x001719E8
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PragmaChecksumDirectiveTriviaSyntax(base.Kind, this.hashToken, this.pragmaKeyword, this.checksumKeyword, this.file, this.guid, this.bytes, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x00173838 File Offset: 0x00171A38
		internal PragmaChecksumDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 7;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.pragmaKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.checksumKeyword = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.file = syntaxToken4;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.guid = syntaxToken5;
			SyntaxToken syntaxToken6 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken6);
			this.bytes = syntaxToken6;
			SyntaxToken syntaxToken7 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken7);
			this.endOfDirectiveToken = syntaxToken7;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x00173920 File Offset: 0x00171B20
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.pragmaKeyword);
			writer.WriteValue(this.checksumKeyword);
			writer.WriteValue(this.file);
			writer.WriteValue(this.guid);
			writer.WriteValue(this.bytes);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x00173994 File Offset: 0x00171B94
		static PragmaChecksumDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PragmaChecksumDirectiveTriviaSyntax), (ObjectReader r) => new PragmaChecksumDirectiveTriviaSyntax(r));
		}

		// Token: 0x0400192E RID: 6446
		internal readonly SyntaxToken hashToken;

		// Token: 0x0400192F RID: 6447
		internal readonly SyntaxToken pragmaKeyword;

		// Token: 0x04001930 RID: 6448
		internal readonly SyntaxToken checksumKeyword;

		// Token: 0x04001931 RID: 6449
		internal readonly SyntaxToken file;

		// Token: 0x04001932 RID: 6450
		internal readonly SyntaxToken guid;

		// Token: 0x04001933 RID: 6451
		internal readonly SyntaxToken bytes;

		// Token: 0x04001934 RID: 6452
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001935 RID: 6453
		internal readonly bool isActive;
	}
}
