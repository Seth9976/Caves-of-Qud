using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E3 RID: 995
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InterpolatedStringTextSyntax : InterpolatedStringContentSyntax
	{
		// Token: 0x060056A5 RID: 22181 RVA: 0x0015592A File Offset: 0x00153B2A
		internal InterpolatedStringTextSyntax(SyntaxKind kind, SyntaxToken textToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(textToken);
			this.textToken = textToken;
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x0015594B File Offset: 0x00153B4B
		internal InterpolatedStringTextSyntax(SyntaxKind kind, SyntaxToken textToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(textToken);
			this.textToken = textToken;
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x00155970 File Offset: 0x00153B70
		internal InterpolatedStringTextSyntax(SyntaxKind kind, SyntaxToken textToken)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(textToken);
			this.textToken = textToken;
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060056A8 RID: 22184 RVA: 0x0015598E File Offset: 0x00153B8E
		public SyntaxToken TextToken
		{
			get
			{
				return this.textToken;
			}
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x00155996 File Offset: 0x00153B96
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.textToken;
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x001559A3 File Offset: 0x00153BA3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InterpolatedStringTextSyntax(this, parent, position);
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x001559AD File Offset: 0x00153BAD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolatedStringText(this);
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x001559B6 File Offset: 0x00153BB6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolatedStringText(this);
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x001559C0 File Offset: 0x00153BC0
		public InterpolatedStringTextSyntax Update(SyntaxToken textToken)
		{
			if (textToken != this.TextToken)
			{
				InterpolatedStringTextSyntax interpolatedStringTextSyntax = SyntaxFactory.InterpolatedStringText(textToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					interpolatedStringTextSyntax = interpolatedStringTextSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					interpolatedStringTextSyntax = interpolatedStringTextSyntax.WithAnnotationsGreen(annotations);
				}
				return interpolatedStringTextSyntax;
			}
			return this;
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x00155A0C File Offset: 0x00153C0C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InterpolatedStringTextSyntax(base.Kind, this.textToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x00155A26 File Offset: 0x00153C26
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InterpolatedStringTextSyntax(base.Kind, this.textToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x00155A40 File Offset: 0x00153C40
		internal InterpolatedStringTextSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.textToken = syntaxToken;
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x00155A75 File Offset: 0x00153C75
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.textToken);
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00155A8A File Offset: 0x00153C8A
		static InterpolatedStringTextSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InterpolatedStringTextSyntax), (ObjectReader r) => new InterpolatedStringTextSyntax(r));
		}

		// Token: 0x040016E6 RID: 5862
		internal readonly SyntaxToken textToken;
	}
}
