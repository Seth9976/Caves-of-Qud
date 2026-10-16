using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200047D RID: 1149
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LoadDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06006025 RID: 24613 RVA: 0x00173D4C File Offset: 0x00171F4C
		internal LoadDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(loadKeyword);
			this.loadKeyword = loadKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x00173DB0 File Offset: 0x00171FB0
		internal LoadDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(loadKeyword);
			this.loadKeyword = loadKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x00173E18 File Offset: 0x00172018
		internal LoadDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(loadKeyword);
			this.loadKeyword = loadKeyword;
			base.AdjustFlagsAndWidth(file);
			this.file = file;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06006028 RID: 24616 RVA: 0x00173E77 File Offset: 0x00172077
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x00173E7F File Offset: 0x0017207F
		public SyntaxToken LoadKeyword
		{
			get
			{
				return this.loadKeyword;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x00173E87 File Offset: 0x00172087
		public SyntaxToken File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x00173E8F File Offset: 0x0017208F
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x00173E97 File Offset: 0x00172097
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x00173EA0 File Offset: 0x001720A0
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
				syntaxToken = this.loadKeyword;
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

		// Token: 0x0600602E RID: 24622 RVA: 0x00173EEC File Offset: 0x001720EC
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LoadDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x00173EF6 File Offset: 0x001720F6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLoadDirectiveTrivia(this);
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x00173EFF File Offset: 0x001720FF
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLoadDirectiveTrivia(this);
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x00173F08 File Offset: 0x00172108
		public LoadDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || loadKeyword != this.LoadKeyword || file != this.File || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				LoadDirectiveTriviaSyntax loadDirectiveTriviaSyntax = SyntaxFactory.LoadDirectiveTrivia(hashToken, loadKeyword, file, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					loadDirectiveTriviaSyntax = loadDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					loadDirectiveTriviaSyntax = loadDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return loadDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x00173F76 File Offset: 0x00172176
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LoadDirectiveTriviaSyntax(base.Kind, this.hashToken, this.loadKeyword, this.file, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x00173FA8 File Offset: 0x001721A8
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LoadDirectiveTriviaSyntax(base.Kind, this.hashToken, this.loadKeyword, this.file, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x00173FDC File Offset: 0x001721DC
		internal LoadDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.loadKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.file = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.endOfDirectiveToken = syntaxToken4;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x0017406C File Offset: 0x0017226C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.loadKeyword);
			writer.WriteValue(this.file);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x001740BC File Offset: 0x001722BC
		static LoadDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LoadDirectiveTriviaSyntax), (ObjectReader r) => new LoadDirectiveTriviaSyntax(r));
		}

		// Token: 0x0400193B RID: 6459
		internal readonly SyntaxToken hashToken;

		// Token: 0x0400193C RID: 6460
		internal readonly SyntaxToken loadKeyword;

		// Token: 0x0400193D RID: 6461
		internal readonly SyntaxToken file;

		// Token: 0x0400193E RID: 6462
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x0400193F RID: 6463
		internal readonly bool isActive;
	}
}
