using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E6 RID: 998
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InterpolationFormatClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060056D4 RID: 22228 RVA: 0x001560B1 File Offset: 0x001542B1
		internal InterpolationFormatClauseSyntax(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken formatStringToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(formatStringToken);
			this.formatStringToken = formatStringToken;
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x001560E1 File Offset: 0x001542E1
		internal InterpolationFormatClauseSyntax(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken formatStringToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(formatStringToken);
			this.formatStringToken = formatStringToken;
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x00156115 File Offset: 0x00154315
		internal InterpolationFormatClauseSyntax(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken formatStringToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(formatStringToken);
			this.formatStringToken = formatStringToken;
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x00156141 File Offset: 0x00154341
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060056D8 RID: 22232 RVA: 0x00156149 File Offset: 0x00154349
		public SyntaxToken FormatStringToken
		{
			get
			{
				return this.formatStringToken;
			}
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x00156154 File Offset: 0x00154354
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxToken = null;
				}
				else
				{
					syntaxToken = this.formatStringToken;
				}
			}
			else
			{
				syntaxToken = this.colonToken;
			}
			return syntaxToken;
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x0015617F File Offset: 0x0015437F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InterpolationFormatClauseSyntax(this, parent, position);
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00156189 File Offset: 0x00154389
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolationFormatClause(this);
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x00156192 File Offset: 0x00154392
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolationFormatClause(this);
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x0015619C File Offset: 0x0015439C
		public InterpolationFormatClauseSyntax Update(SyntaxToken colonToken, SyntaxToken formatStringToken)
		{
			if (colonToken != this.ColonToken || formatStringToken != this.FormatStringToken)
			{
				InterpolationFormatClauseSyntax interpolationFormatClauseSyntax = SyntaxFactory.InterpolationFormatClause(colonToken, formatStringToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					interpolationFormatClauseSyntax = interpolationFormatClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					interpolationFormatClauseSyntax = interpolationFormatClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return interpolationFormatClauseSyntax;
			}
			return this;
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x001561F2 File Offset: 0x001543F2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InterpolationFormatClauseSyntax(base.Kind, this.colonToken, this.formatStringToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x00156212 File Offset: 0x00154412
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InterpolationFormatClauseSyntax(base.Kind, this.colonToken, this.formatStringToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x00156234 File Offset: 0x00154434
		internal InterpolationFormatClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.colonToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.formatStringToken = syntaxToken2;
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x00156283 File Offset: 0x00154483
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.colonToken);
			writer.WriteValue(this.formatStringToken);
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x001562A4 File Offset: 0x001544A4
		static InterpolationFormatClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InterpolationFormatClauseSyntax), (ObjectReader r) => new InterpolationFormatClauseSyntax(r));
		}

		// Token: 0x040016EE RID: 5870
		internal readonly SyntaxToken colonToken;

		// Token: 0x040016EF RID: 5871
		internal readonly SyntaxToken formatStringToken;
	}
}
