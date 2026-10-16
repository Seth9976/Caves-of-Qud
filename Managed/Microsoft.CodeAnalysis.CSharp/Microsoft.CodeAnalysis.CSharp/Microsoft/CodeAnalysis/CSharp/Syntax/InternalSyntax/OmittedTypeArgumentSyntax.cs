using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200038A RID: 906
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OmittedTypeArgumentSyntax : TypeSyntax
	{
		// Token: 0x060051A1 RID: 20897 RVA: 0x001487A4 File Offset: 0x001469A4
		internal OmittedTypeArgumentSyntax(SyntaxKind kind, SyntaxToken omittedTypeArgumentToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(omittedTypeArgumentToken);
			this.omittedTypeArgumentToken = omittedTypeArgumentToken;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x001487C5 File Offset: 0x001469C5
		internal OmittedTypeArgumentSyntax(SyntaxKind kind, SyntaxToken omittedTypeArgumentToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(omittedTypeArgumentToken);
			this.omittedTypeArgumentToken = omittedTypeArgumentToken;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x001487EA File Offset: 0x001469EA
		internal OmittedTypeArgumentSyntax(SyntaxKind kind, SyntaxToken omittedTypeArgumentToken)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(omittedTypeArgumentToken);
			this.omittedTypeArgumentToken = omittedTypeArgumentToken;
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x060051A4 RID: 20900 RVA: 0x00148808 File Offset: 0x00146A08
		public SyntaxToken OmittedTypeArgumentToken
		{
			get
			{
				return this.omittedTypeArgumentToken;
			}
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00148810 File Offset: 0x00146A10
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.omittedTypeArgumentToken;
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x0014881D File Offset: 0x00146A1D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new OmittedTypeArgumentSyntax(this, parent, position);
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x00148827 File Offset: 0x00146A27
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOmittedTypeArgument(this);
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00148830 File Offset: 0x00146A30
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOmittedTypeArgument(this);
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0014883C File Offset: 0x00146A3C
		public OmittedTypeArgumentSyntax Update(SyntaxToken omittedTypeArgumentToken)
		{
			if (omittedTypeArgumentToken != this.OmittedTypeArgumentToken)
			{
				OmittedTypeArgumentSyntax omittedTypeArgumentSyntax = SyntaxFactory.OmittedTypeArgument(omittedTypeArgumentToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					omittedTypeArgumentSyntax = omittedTypeArgumentSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					omittedTypeArgumentSyntax = omittedTypeArgumentSyntax.WithAnnotationsGreen(annotations);
				}
				return omittedTypeArgumentSyntax;
			}
			return this;
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x00148888 File Offset: 0x00146A88
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new OmittedTypeArgumentSyntax(base.Kind, this.omittedTypeArgumentToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x001488A2 File Offset: 0x00146AA2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new OmittedTypeArgumentSyntax(base.Kind, this.omittedTypeArgumentToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x001488BC File Offset: 0x00146ABC
		internal OmittedTypeArgumentSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.omittedTypeArgumentToken = syntaxToken;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x001488F1 File Offset: 0x00146AF1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.omittedTypeArgumentToken);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00148906 File Offset: 0x00146B06
		static OmittedTypeArgumentSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(OmittedTypeArgumentSyntax), (ObjectReader r) => new OmittedTypeArgumentSyntax(r));
		}

		// Token: 0x04001602 RID: 5634
		internal readonly SyntaxToken omittedTypeArgumentToken;
	}
}
