using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000478 RID: 1144
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UndefDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005FC5 RID: 24517 RVA: 0x001727B8 File Offset: 0x001709B8
		internal UndefDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(undefKeyword);
			this.undefKeyword = undefKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x0017281C File Offset: 0x00170A1C
		internal UndefDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(undefKeyword);
			this.undefKeyword = undefKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x00172884 File Offset: 0x00170A84
		internal UndefDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(undefKeyword);
			this.undefKeyword = undefKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x001728E3 File Offset: 0x00170AE3
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x001728EB File Offset: 0x00170AEB
		public SyntaxToken UndefKeyword
		{
			get
			{
				return this.undefKeyword;
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06005FCA RID: 24522 RVA: 0x001728F3 File Offset: 0x00170AF3
		public SyntaxToken Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x001728FB File Offset: 0x00170AFB
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x00172903 File Offset: 0x00170B03
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x0017290C File Offset: 0x00170B0C
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
				syntaxToken = this.undefKeyword;
				break;
			case 2:
				syntaxToken = this.name;
				break;
			case 3:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x00172958 File Offset: 0x00170B58
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new UndefDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x00172962 File Offset: 0x00170B62
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUndefDirectiveTrivia(this);
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x0017296B File Offset: 0x00170B6B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUndefDirectiveTrivia(this);
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x00172974 File Offset: 0x00170B74
		public UndefDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || undefKeyword != this.UndefKeyword || name != this.Name || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				UndefDirectiveTriviaSyntax undefDirectiveTriviaSyntax = SyntaxFactory.UndefDirectiveTrivia(hashToken, undefKeyword, name, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					undefDirectiveTriviaSyntax = undefDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					undefDirectiveTriviaSyntax = undefDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return undefDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005FD2 RID: 24530 RVA: 0x001729E2 File Offset: 0x00170BE2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new UndefDirectiveTriviaSyntax(base.Kind, this.hashToken, this.undefKeyword, this.name, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x00172A14 File Offset: 0x00170C14
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new UndefDirectiveTriviaSyntax(base.Kind, this.hashToken, this.undefKeyword, this.name, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x00172A48 File Offset: 0x00170C48
		internal UndefDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.undefKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.name = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.endOfDirectiveToken = syntaxToken4;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005FD5 RID: 24533 RVA: 0x00172AD8 File Offset: 0x00170CD8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.undefKeyword);
			writer.WriteValue(this.name);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x00172B28 File Offset: 0x00170D28
		static UndefDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(UndefDirectiveTriviaSyntax), (ObjectReader r) => new UndefDirectiveTriviaSyntax(r));
		}

		// Token: 0x0400191C RID: 6428
		internal readonly SyntaxToken hashToken;

		// Token: 0x0400191D RID: 6429
		internal readonly SyntaxToken undefKeyword;

		// Token: 0x0400191E RID: 6430
		internal readonly SyntaxToken name;

		// Token: 0x0400191F RID: 6431
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001920 RID: 6432
		internal readonly bool isActive;
	}
}
