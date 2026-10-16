using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200047E RID: 1150
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ShebangDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06006037 RID: 24631 RVA: 0x001740E0 File Offset: 0x001722E0
		internal ShebangDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(exclamationToken);
			this.exclamationToken = exclamationToken;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x00174134 File Offset: 0x00172334
		internal ShebangDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(exclamationToken);
			this.exclamationToken = exclamationToken;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x0017418C File Offset: 0x0017238C
		internal ShebangDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(exclamationToken);
			this.exclamationToken = exclamationToken;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x001741DB File Offset: 0x001723DB
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x001741E3 File Offset: 0x001723E3
		public SyntaxToken ExclamationToken
		{
			get
			{
				return this.exclamationToken;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x0600603C RID: 24636 RVA: 0x001741EB File Offset: 0x001723EB
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x001741F3 File Offset: 0x001723F3
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x001741FC File Offset: 0x001723FC
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
				syntaxToken = this.exclamationToken;
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

		// Token: 0x0600603F RID: 24639 RVA: 0x0017423B File Offset: 0x0017243B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ShebangDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x00174245 File Offset: 0x00172445
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitShebangDirectiveTrivia(this);
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0017424E File Offset: 0x0017244E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitShebangDirectiveTrivia(this);
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x00174258 File Offset: 0x00172458
		public ShebangDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || exclamationToken != this.ExclamationToken || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				ShebangDirectiveTriviaSyntax shebangDirectiveTriviaSyntax = SyntaxFactory.ShebangDirectiveTrivia(hashToken, exclamationToken, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					shebangDirectiveTriviaSyntax = shebangDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					shebangDirectiveTriviaSyntax = shebangDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return shebangDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x001742BA File Offset: 0x001724BA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ShebangDirectiveTriviaSyntax(base.Kind, this.hashToken, this.exclamationToken, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x001742E6 File Offset: 0x001724E6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ShebangDirectiveTriviaSyntax(base.Kind, this.hashToken, this.exclamationToken, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x00174314 File Offset: 0x00172514
		internal ShebangDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.exclamationToken = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x00174389 File Offset: 0x00172589
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.exclamationToken);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x001743C2 File Offset: 0x001725C2
		static ShebangDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ShebangDirectiveTriviaSyntax), (ObjectReader r) => new ShebangDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001940 RID: 6464
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001941 RID: 6465
		internal readonly SyntaxToken exclamationToken;

		// Token: 0x04001942 RID: 6466
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001943 RID: 6467
		internal readonly bool isActive;
	}
}
