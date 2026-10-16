using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D5 RID: 981
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DiscardPatternSyntax : PatternSyntax
	{
		// Token: 0x060055DC RID: 21980 RVA: 0x00153BA2 File Offset: 0x00151DA2
		internal DiscardPatternSyntax(SyntaxKind kind, SyntaxToken underscoreToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(underscoreToken);
			this.underscoreToken = underscoreToken;
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x00153BC3 File Offset: 0x00151DC3
		internal DiscardPatternSyntax(SyntaxKind kind, SyntaxToken underscoreToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(underscoreToken);
			this.underscoreToken = underscoreToken;
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x00153BE8 File Offset: 0x00151DE8
		internal DiscardPatternSyntax(SyntaxKind kind, SyntaxToken underscoreToken)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(underscoreToken);
			this.underscoreToken = underscoreToken;
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x00153C06 File Offset: 0x00151E06
		public SyntaxToken UnderscoreToken
		{
			get
			{
				return this.underscoreToken;
			}
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x00153C0E File Offset: 0x00151E0E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.underscoreToken;
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x00153C1B File Offset: 0x00151E1B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DiscardPatternSyntax(this, parent, position);
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x00153C25 File Offset: 0x00151E25
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDiscardPattern(this);
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x00153C2E File Offset: 0x00151E2E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDiscardPattern(this);
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x00153C38 File Offset: 0x00151E38
		public DiscardPatternSyntax Update(SyntaxToken underscoreToken)
		{
			if (underscoreToken != this.UnderscoreToken)
			{
				DiscardPatternSyntax discardPatternSyntax = SyntaxFactory.DiscardPattern(underscoreToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					discardPatternSyntax = discardPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					discardPatternSyntax = discardPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return discardPatternSyntax;
			}
			return this;
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x00153C84 File Offset: 0x00151E84
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DiscardPatternSyntax(base.Kind, this.underscoreToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00153C9E File Offset: 0x00151E9E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DiscardPatternSyntax(base.Kind, this.underscoreToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x00153CB8 File Offset: 0x00151EB8
		internal DiscardPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.underscoreToken = syntaxToken;
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00153CED File Offset: 0x00151EED
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.underscoreToken);
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x00153D02 File Offset: 0x00151F02
		static DiscardPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DiscardPatternSyntax), (ObjectReader r) => new DiscardPatternSyntax(r));
		}

		// Token: 0x040016C9 RID: 5833
		internal readonly SyntaxToken underscoreToken;
	}
}
