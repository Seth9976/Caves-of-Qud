using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F1 RID: 1009
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DiscardDesignationSyntax : VariableDesignationSyntax
	{
		// Token: 0x06005771 RID: 22385 RVA: 0x00157DDF File Offset: 0x00155FDF
		internal DiscardDesignationSyntax(SyntaxKind kind, SyntaxToken underscoreToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(underscoreToken);
			this.underscoreToken = underscoreToken;
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x00157E00 File Offset: 0x00156000
		internal DiscardDesignationSyntax(SyntaxKind kind, SyntaxToken underscoreToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(underscoreToken);
			this.underscoreToken = underscoreToken;
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x00157E25 File Offset: 0x00156025
		internal DiscardDesignationSyntax(SyntaxKind kind, SyntaxToken underscoreToken)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(underscoreToken);
			this.underscoreToken = underscoreToken;
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06005774 RID: 22388 RVA: 0x00157E43 File Offset: 0x00156043
		public SyntaxToken UnderscoreToken
		{
			get
			{
				return this.underscoreToken;
			}
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x00157E4B File Offset: 0x0015604B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.underscoreToken;
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x00157E58 File Offset: 0x00156058
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DiscardDesignationSyntax(this, parent, position);
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x00157E62 File Offset: 0x00156062
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDiscardDesignation(this);
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00157E6B File Offset: 0x0015606B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDiscardDesignation(this);
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00157E74 File Offset: 0x00156074
		public DiscardDesignationSyntax Update(SyntaxToken underscoreToken)
		{
			if (underscoreToken != this.UnderscoreToken)
			{
				DiscardDesignationSyntax discardDesignationSyntax = SyntaxFactory.DiscardDesignation(underscoreToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					discardDesignationSyntax = discardDesignationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					discardDesignationSyntax = discardDesignationSyntax.WithAnnotationsGreen(annotations);
				}
				return discardDesignationSyntax;
			}
			return this;
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x00157EC0 File Offset: 0x001560C0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DiscardDesignationSyntax(base.Kind, this.underscoreToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x00157EDA File Offset: 0x001560DA
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DiscardDesignationSyntax(base.Kind, this.underscoreToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x00157EF4 File Offset: 0x001560F4
		internal DiscardDesignationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.underscoreToken = syntaxToken;
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x00157F29 File Offset: 0x00156129
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.underscoreToken);
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00157F3E File Offset: 0x0015613E
		static DiscardDesignationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DiscardDesignationSyntax), (ObjectReader r) => new DiscardDesignationSyntax(r));
		}

		// Token: 0x0400170F RID: 5903
		internal readonly SyntaxToken underscoreToken;
	}
}
