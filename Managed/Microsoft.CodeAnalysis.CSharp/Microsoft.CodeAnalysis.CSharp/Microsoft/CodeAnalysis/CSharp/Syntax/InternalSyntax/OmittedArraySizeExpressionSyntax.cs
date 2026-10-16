using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003CF RID: 975
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OmittedArraySizeExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600558D RID: 21901 RVA: 0x0015306F File Offset: 0x0015126F
		internal OmittedArraySizeExpressionSyntax(SyntaxKind kind, SyntaxToken omittedArraySizeExpressionToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(omittedArraySizeExpressionToken);
			this.omittedArraySizeExpressionToken = omittedArraySizeExpressionToken;
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00153090 File Offset: 0x00151290
		internal OmittedArraySizeExpressionSyntax(SyntaxKind kind, SyntaxToken omittedArraySizeExpressionToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(omittedArraySizeExpressionToken);
			this.omittedArraySizeExpressionToken = omittedArraySizeExpressionToken;
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x001530B5 File Offset: 0x001512B5
		internal OmittedArraySizeExpressionSyntax(SyntaxKind kind, SyntaxToken omittedArraySizeExpressionToken)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(omittedArraySizeExpressionToken);
			this.omittedArraySizeExpressionToken = omittedArraySizeExpressionToken;
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06005590 RID: 21904 RVA: 0x001530D3 File Offset: 0x001512D3
		public SyntaxToken OmittedArraySizeExpressionToken
		{
			get
			{
				return this.omittedArraySizeExpressionToken;
			}
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x001530DB File Offset: 0x001512DB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.omittedArraySizeExpressionToken;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x001530E8 File Offset: 0x001512E8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new OmittedArraySizeExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x001530F2 File Offset: 0x001512F2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOmittedArraySizeExpression(this);
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x001530FB File Offset: 0x001512FB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOmittedArraySizeExpression(this);
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x00153104 File Offset: 0x00151304
		public OmittedArraySizeExpressionSyntax Update(SyntaxToken omittedArraySizeExpressionToken)
		{
			if (omittedArraySizeExpressionToken != this.OmittedArraySizeExpressionToken)
			{
				OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax = SyntaxFactory.OmittedArraySizeExpression(omittedArraySizeExpressionToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					omittedArraySizeExpressionSyntax = omittedArraySizeExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					omittedArraySizeExpressionSyntax = omittedArraySizeExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return omittedArraySizeExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x00153150 File Offset: 0x00151350
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new OmittedArraySizeExpressionSyntax(base.Kind, this.omittedArraySizeExpressionToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0015316A File Offset: 0x0015136A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new OmittedArraySizeExpressionSyntax(base.Kind, this.omittedArraySizeExpressionToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00153184 File Offset: 0x00151384
		internal OmittedArraySizeExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.omittedArraySizeExpressionToken = syntaxToken;
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x001531B9 File Offset: 0x001513B9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.omittedArraySizeExpressionToken);
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x001531CE File Offset: 0x001513CE
		static OmittedArraySizeExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(OmittedArraySizeExpressionSyntax), (ObjectReader r) => new OmittedArraySizeExpressionSyntax(r));
		}

		// Token: 0x040016BE RID: 5822
		internal readonly SyntaxToken omittedArraySizeExpressionToken;
	}
}
