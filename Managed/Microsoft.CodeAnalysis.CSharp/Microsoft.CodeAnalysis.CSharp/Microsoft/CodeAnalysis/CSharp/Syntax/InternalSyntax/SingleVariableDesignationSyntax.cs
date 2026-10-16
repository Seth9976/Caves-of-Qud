using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F0 RID: 1008
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SingleVariableDesignationSyntax : VariableDesignationSyntax
	{
		// Token: 0x06005763 RID: 22371 RVA: 0x00157C5E File Offset: 0x00155E5E
		internal SingleVariableDesignationSyntax(SyntaxKind kind, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x00157C7F File Offset: 0x00155E7F
		internal SingleVariableDesignationSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x00157CA4 File Offset: 0x00155EA4
		internal SingleVariableDesignationSyntax(SyntaxKind kind, SyntaxToken identifier)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x00157CC2 File Offset: 0x00155EC2
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x00157CCA File Offset: 0x00155ECA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.identifier;
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00157CD7 File Offset: 0x00155ED7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SingleVariableDesignationSyntax(this, parent, position);
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x00157CE1 File Offset: 0x00155EE1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSingleVariableDesignation(this);
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x00157CEA File Offset: 0x00155EEA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSingleVariableDesignation(this);
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x00157CF4 File Offset: 0x00155EF4
		public SingleVariableDesignationSyntax Update(SyntaxToken identifier)
		{
			if (identifier != this.Identifier)
			{
				SingleVariableDesignationSyntax singleVariableDesignationSyntax = SyntaxFactory.SingleVariableDesignation(identifier);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					singleVariableDesignationSyntax = singleVariableDesignationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					singleVariableDesignationSyntax = singleVariableDesignationSyntax.WithAnnotationsGreen(annotations);
				}
				return singleVariableDesignationSyntax;
			}
			return this;
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x00157D40 File Offset: 0x00155F40
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SingleVariableDesignationSyntax(base.Kind, this.identifier, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00157D5A File Offset: 0x00155F5A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SingleVariableDesignationSyntax(base.Kind, this.identifier, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00157D74 File Offset: 0x00155F74
		internal SingleVariableDesignationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x00157DA9 File Offset: 0x00155FA9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.identifier);
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00157DBE File Offset: 0x00155FBE
		static SingleVariableDesignationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SingleVariableDesignationSyntax), (ObjectReader r) => new SingleVariableDesignationSyntax(r));
		}

		// Token: 0x0400170E RID: 5902
		internal readonly SyntaxToken identifier;
	}
}
