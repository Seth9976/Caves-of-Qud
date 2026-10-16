using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003EE RID: 1006
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EqualsValueClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005751 RID: 22353 RVA: 0x00157A2D File Offset: 0x00155C2D
		internal EqualsValueClauseSyntax(SyntaxKind kind, SyntaxToken equalsToken, ExpressionSyntax value, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x00157A5D File Offset: 0x00155C5D
		internal EqualsValueClauseSyntax(SyntaxKind kind, SyntaxToken equalsToken, ExpressionSyntax value, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x00157A91 File Offset: 0x00155C91
		internal EqualsValueClauseSyntax(SyntaxKind kind, SyntaxToken equalsToken, ExpressionSyntax value)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06005754 RID: 22356 RVA: 0x00157ABD File Offset: 0x00155CBD
		public SyntaxToken EqualsToken
		{
			get
			{
				return this.equalsToken;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06005755 RID: 22357 RVA: 0x00157AC5 File Offset: 0x00155CC5
		public ExpressionSyntax Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x00157AD0 File Offset: 0x00155CD0
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
				greenNode = this.equalsToken;
			}
			return greenNode;
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x00157AFB File Offset: 0x00155CFB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EqualsValueClauseSyntax(this, parent, position);
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x00157B05 File Offset: 0x00155D05
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEqualsValueClause(this);
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x00157B0E File Offset: 0x00155D0E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEqualsValueClause(this);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x00157B18 File Offset: 0x00155D18
		public EqualsValueClauseSyntax Update(SyntaxToken equalsToken, ExpressionSyntax value)
		{
			if (equalsToken != this.EqualsToken || value != this.Value)
			{
				EqualsValueClauseSyntax equalsValueClauseSyntax = SyntaxFactory.EqualsValueClause(equalsToken, value);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					equalsValueClauseSyntax = equalsValueClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					equalsValueClauseSyntax = equalsValueClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return equalsValueClauseSyntax;
			}
			return this;
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00157B6E File Offset: 0x00155D6E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EqualsValueClauseSyntax(base.Kind, this.equalsToken, this.value, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x00157B8E File Offset: 0x00155D8E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EqualsValueClauseSyntax(base.Kind, this.equalsToken, this.value, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x00157BB0 File Offset: 0x00155DB0
		internal EqualsValueClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.equalsToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.value = expressionSyntax;
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x00157BFF File Offset: 0x00155DFF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.equalsToken);
			writer.WriteValue(this.value);
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x00157C20 File Offset: 0x00155E20
		static EqualsValueClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EqualsValueClauseSyntax), (ObjectReader r) => new EqualsValueClauseSyntax(r));
		}

		// Token: 0x0400170C RID: 5900
		internal readonly SyntaxToken equalsToken;

		// Token: 0x0400170D RID: 5901
		internal readonly ExpressionSyntax value;
	}
}
