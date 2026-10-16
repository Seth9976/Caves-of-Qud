using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200047C RID: 1148
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ReferenceDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06006013 RID: 24595 RVA: 0x001739B8 File Offset: 0x00171BB8
		internal ReferenceDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(referenceKeyword);
			this.referenceKeyword = referenceKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x00173A1C File Offset: 0x00171C1C
		internal ReferenceDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(referenceKeyword);
			this.referenceKeyword = referenceKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x00173A84 File Offset: 0x00171C84
		internal ReferenceDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(referenceKeyword);
			this.referenceKeyword = referenceKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x00173AE3 File Offset: 0x00171CE3
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x00173AEB File Offset: 0x00171CEB
		public SyntaxToken ReferenceKeyword
		{
			get
			{
				return this.referenceKeyword;
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06006018 RID: 24600 RVA: 0x00173AF3 File Offset: 0x00171CF3
		public SyntaxToken File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x00173AFB File Offset: 0x00171CFB
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x0600601A RID: 24602 RVA: 0x00173B03 File Offset: 0x00171D03
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x00173B0C File Offset: 0x00171D0C
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
				syntaxToken = this.referenceKeyword;
				break;
			case 2:
				syntaxToken = this.file;
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

		// Token: 0x0600601C RID: 24604 RVA: 0x00173B58 File Offset: 0x00171D58
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ReferenceDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x00173B62 File Offset: 0x00171D62
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitReferenceDirectiveTrivia(this);
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x00173B6B File Offset: 0x00171D6B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitReferenceDirectiveTrivia(this);
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x00173B74 File Offset: 0x00171D74
		public ReferenceDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || referenceKeyword != this.ReferenceKeyword || file != this.File || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				ReferenceDirectiveTriviaSyntax referenceDirectiveTriviaSyntax = SyntaxFactory.ReferenceDirectiveTrivia(hashToken, referenceKeyword, file, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					referenceDirectiveTriviaSyntax = referenceDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					referenceDirectiveTriviaSyntax = referenceDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return referenceDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x00173BE2 File Offset: 0x00171DE2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ReferenceDirectiveTriviaSyntax(base.Kind, this.hashToken, this.referenceKeyword, this.file, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x00173C14 File Offset: 0x00171E14
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ReferenceDirectiveTriviaSyntax(base.Kind, this.hashToken, this.referenceKeyword, this.file, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x00173C48 File Offset: 0x00171E48
		internal ReferenceDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.referenceKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.file = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.endOfDirectiveToken = syntaxToken4;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x00173CD8 File Offset: 0x00171ED8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.referenceKeyword);
			writer.WriteValue(this.file);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x00173D28 File Offset: 0x00171F28
		static ReferenceDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ReferenceDirectiveTriviaSyntax), (ObjectReader r) => new ReferenceDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001936 RID: 6454
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001937 RID: 6455
		internal readonly SyntaxToken referenceKeyword;

		// Token: 0x04001938 RID: 6456
		internal readonly SyntaxToken file;

		// Token: 0x04001939 RID: 6457
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x0400193A RID: 6458
		internal readonly bool isActive;
	}
}
