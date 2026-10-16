using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200037E RID: 894
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PredefinedTypeSyntax : TypeSyntax
	{
		// Token: 0x060050E9 RID: 20713 RVA: 0x00146B6F File Offset: 0x00144D6F
		internal PredefinedTypeSyntax(SyntaxKind kind, SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00146B90 File Offset: 0x00144D90
		internal PredefinedTypeSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x00146BB5 File Offset: 0x00144DB5
		internal PredefinedTypeSyntax(SyntaxKind kind, SyntaxToken keyword)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x00146BD3 File Offset: 0x00144DD3
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00146BDB File Offset: 0x00144DDB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.keyword;
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x00146BE8 File Offset: 0x00144DE8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PredefinedTypeSyntax(this, parent, position);
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x00146BF2 File Offset: 0x00144DF2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPredefinedType(this);
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x00146BFB File Offset: 0x00144DFB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPredefinedType(this);
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x00146C04 File Offset: 0x00144E04
		public PredefinedTypeSyntax Update(SyntaxToken keyword)
		{
			if (keyword != this.Keyword)
			{
				PredefinedTypeSyntax predefinedTypeSyntax = SyntaxFactory.PredefinedType(keyword);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					predefinedTypeSyntax = predefinedTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					predefinedTypeSyntax = predefinedTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return predefinedTypeSyntax;
			}
			return this;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x00146C50 File Offset: 0x00144E50
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PredefinedTypeSyntax(base.Kind, this.keyword, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00146C6A File Offset: 0x00144E6A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PredefinedTypeSyntax(base.Kind, this.keyword, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x00146C84 File Offset: 0x00144E84
		internal PredefinedTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00146CB9 File Offset: 0x00144EB9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x00146CCE File Offset: 0x00144ECE
		static PredefinedTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PredefinedTypeSyntax), (ObjectReader r) => new PredefinedTypeSyntax(r));
		}

		// Token: 0x040015E6 RID: 5606
		internal readonly SyntaxToken keyword;
	}
}
