using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E4 RID: 996
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InterpolationSyntax : InterpolatedStringContentSyntax
	{
		// Token: 0x060056B3 RID: 22195 RVA: 0x00155AAC File Offset: 0x00153CAC
		internal InterpolationSyntax(SyntaxKind kind, SyntaxToken openBraceToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (alignmentClause != null)
			{
				base.AdjustFlagsAndWidth(alignmentClause);
				this.alignmentClause = alignmentClause;
			}
			if (formatClause != null)
			{
				base.AdjustFlagsAndWidth(formatClause);
				this.formatClause = formatClause;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x00155B20 File Offset: 0x00153D20
		internal InterpolationSyntax(SyntaxKind kind, SyntaxToken openBraceToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (alignmentClause != null)
			{
				base.AdjustFlagsAndWidth(alignmentClause);
				this.alignmentClause = alignmentClause;
			}
			if (formatClause != null)
			{
				base.AdjustFlagsAndWidth(formatClause);
				this.formatClause = formatClause;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x00155B98 File Offset: 0x00153D98
		internal InterpolationSyntax(SyntaxKind kind, SyntaxToken openBraceToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (alignmentClause != null)
			{
				base.AdjustFlagsAndWidth(alignmentClause);
				this.alignmentClause = alignmentClause;
			}
			if (formatClause != null)
			{
				base.AdjustFlagsAndWidth(formatClause);
				this.formatClause = formatClause;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060056B6 RID: 22198 RVA: 0x00155C07 File Offset: 0x00153E07
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00155C0F File Offset: 0x00153E0F
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x00155C17 File Offset: 0x00153E17
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InterpolationAlignmentClauseSyntax AlignmentClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.alignmentClause;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060056B9 RID: 22201 RVA: 0x00155C1F File Offset: 0x00153E1F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InterpolationFormatClauseSyntax FormatClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.formatClause;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x00155C27 File Offset: 0x00153E27
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x00155C30 File Offset: 0x00153E30
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBraceToken;
				break;
			case 1:
				greenNode = this.expression;
				break;
			case 2:
				greenNode = this.alignmentClause;
				break;
			case 3:
				greenNode = this.formatClause;
				break;
			case 4:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x00155C89 File Offset: 0x00153E89
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InterpolationSyntax(this, parent, position);
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x00155C93 File Offset: 0x00153E93
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolation(this);
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x00155C9C File Offset: 0x00153E9C
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolation(this);
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x00155CA8 File Offset: 0x00153EA8
		public InterpolationSyntax Update(SyntaxToken openBraceToken, ExpressionSyntax expression, InterpolationAlignmentClauseSyntax alignmentClause, InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
		{
			if (openBraceToken != this.OpenBraceToken || expression != this.Expression || alignmentClause != this.AlignmentClause || formatClause != this.FormatClause || closeBraceToken != this.CloseBraceToken)
			{
				InterpolationSyntax interpolationSyntax = SyntaxFactory.Interpolation(openBraceToken, expression, alignmentClause, formatClause, closeBraceToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					interpolationSyntax = interpolationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					interpolationSyntax = interpolationSyntax.WithAnnotationsGreen(annotations);
				}
				return interpolationSyntax;
			}
			return this;
		}

		// Token: 0x060056C0 RID: 22208 RVA: 0x00155D20 File Offset: 0x00153F20
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InterpolationSyntax(base.Kind, this.openBraceToken, this.expression, this.alignmentClause, this.formatClause, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x00155D52 File Offset: 0x00153F52
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InterpolationSyntax(base.Kind, this.openBraceToken, this.expression, this.alignmentClause, this.formatClause, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x00155D84 File Offset: 0x00153F84
		internal InterpolationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBraceToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax = (InterpolationAlignmentClauseSyntax)reader.ReadValue();
			if (interpolationAlignmentClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(interpolationAlignmentClauseSyntax);
				this.alignmentClause = interpolationAlignmentClauseSyntax;
			}
			InterpolationFormatClauseSyntax interpolationFormatClauseSyntax = (InterpolationFormatClauseSyntax)reader.ReadValue();
			if (interpolationFormatClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(interpolationFormatClauseSyntax);
				this.formatClause = interpolationFormatClauseSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBraceToken = syntaxToken2;
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x00155E2C File Offset: 0x0015402C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.alignmentClause);
			writer.WriteValue(this.formatClause);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x060056C4 RID: 22212 RVA: 0x00155E7C File Offset: 0x0015407C
		static InterpolationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InterpolationSyntax), (ObjectReader r) => new InterpolationSyntax(r));
		}

		// Token: 0x040016E7 RID: 5863
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040016E8 RID: 5864
		internal readonly ExpressionSyntax expression;

		// Token: 0x040016E9 RID: 5865
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly InterpolationAlignmentClauseSyntax alignmentClause;

		// Token: 0x040016EA RID: 5866
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly InterpolationFormatClauseSyntax formatClause;

		// Token: 0x040016EB RID: 5867
		internal readonly SyntaxToken closeBraceToken;
	}
}
