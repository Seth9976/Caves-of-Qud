using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E5 RID: 997
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InterpolationAlignmentClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060056C5 RID: 22213 RVA: 0x00155E9D File Offset: 0x0015409D
		internal InterpolationAlignmentClauseSyntax(SyntaxKind kind, SyntaxToken commaToken, ExpressionSyntax value, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(commaToken);
			this.commaToken = commaToken;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x00155ECD File Offset: 0x001540CD
		internal InterpolationAlignmentClauseSyntax(SyntaxKind kind, SyntaxToken commaToken, ExpressionSyntax value, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(commaToken);
			this.commaToken = commaToken;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x00155F01 File Offset: 0x00154101
		internal InterpolationAlignmentClauseSyntax(SyntaxKind kind, SyntaxToken commaToken, ExpressionSyntax value)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(commaToken);
			this.commaToken = commaToken;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x00155F2D File Offset: 0x0015412D
		public SyntaxToken CommaToken
		{
			get
			{
				return this.commaToken;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060056C9 RID: 22217 RVA: 0x00155F35 File Offset: 0x00154135
		public ExpressionSyntax Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x00155F40 File Offset: 0x00154140
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.value;
				}
			}
			else
			{
				greenNode = this.commaToken;
			}
			return greenNode;
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x00155F6B File Offset: 0x0015416B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InterpolationAlignmentClauseSyntax(this, parent, position);
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x00155F75 File Offset: 0x00154175
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolationAlignmentClause(this);
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x00155F7E File Offset: 0x0015417E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolationAlignmentClause(this);
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x00155F88 File Offset: 0x00154188
		public InterpolationAlignmentClauseSyntax Update(SyntaxToken commaToken, ExpressionSyntax value)
		{
			if (commaToken != this.CommaToken || value != this.Value)
			{
				InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax = SyntaxFactory.InterpolationAlignmentClause(commaToken, value);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					interpolationAlignmentClauseSyntax = interpolationAlignmentClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					interpolationAlignmentClauseSyntax = interpolationAlignmentClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return interpolationAlignmentClauseSyntax;
			}
			return this;
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x00155FDE File Offset: 0x001541DE
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InterpolationAlignmentClauseSyntax(base.Kind, this.commaToken, this.value, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x00155FFE File Offset: 0x001541FE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InterpolationAlignmentClauseSyntax(base.Kind, this.commaToken, this.value, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x00156020 File Offset: 0x00154220
		internal InterpolationAlignmentClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.commaToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.value = expressionSyntax;
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x0015606F File Offset: 0x0015426F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.commaToken);
			writer.WriteValue(this.value);
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x00156090 File Offset: 0x00154290
		static InterpolationAlignmentClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InterpolationAlignmentClauseSyntax), (ObjectReader r) => new InterpolationAlignmentClauseSyntax(r));
		}

		// Token: 0x040016EC RID: 5868
		internal readonly SyntaxToken commaToken;

		// Token: 0x040016ED RID: 5869
		internal readonly ExpressionSyntax value;
	}
}
