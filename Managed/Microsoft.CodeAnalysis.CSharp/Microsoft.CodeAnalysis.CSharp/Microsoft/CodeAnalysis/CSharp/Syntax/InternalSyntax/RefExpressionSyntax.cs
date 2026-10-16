using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B4 RID: 948
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RefExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060053F6 RID: 21494 RVA: 0x0014E82D File Offset: 0x0014CA2D
		internal RefExpressionSyntax(SyntaxKind kind, SyntaxToken refKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(refKeyword);
			this.refKeyword = refKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x0014E85D File Offset: 0x0014CA5D
		internal RefExpressionSyntax(SyntaxKind kind, SyntaxToken refKeyword, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(refKeyword);
			this.refKeyword = refKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0014E891 File Offset: 0x0014CA91
		internal RefExpressionSyntax(SyntaxKind kind, SyntaxToken refKeyword, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(refKeyword);
			this.refKeyword = refKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x0014E8BD File Offset: 0x0014CABD
		public SyntaxToken RefKeyword
		{
			get
			{
				return this.refKeyword;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x0014E8C5 File Offset: 0x0014CAC5
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0014E8D0 File Offset: 0x0014CAD0
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
					greenNode = this.expression;
				}
			}
			else
			{
				greenNode = this.refKeyword;
			}
			return greenNode;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0014E8FB File Offset: 0x0014CAFB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RefExpressionSyntax(this, parent, position);
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0014E905 File Offset: 0x0014CB05
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefExpression(this);
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0014E90E File Offset: 0x0014CB0E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefExpression(this);
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0014E918 File Offset: 0x0014CB18
		public RefExpressionSyntax Update(SyntaxToken refKeyword, ExpressionSyntax expression)
		{
			if (refKeyword != this.RefKeyword || expression != this.Expression)
			{
				RefExpressionSyntax refExpressionSyntax = SyntaxFactory.RefExpression(refKeyword, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					refExpressionSyntax = refExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					refExpressionSyntax = refExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return refExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0014E96E File Offset: 0x0014CB6E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RefExpressionSyntax(base.Kind, this.refKeyword, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0014E98E File Offset: 0x0014CB8E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RefExpressionSyntax(base.Kind, this.refKeyword, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0014E9B0 File Offset: 0x0014CBB0
		internal RefExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.refKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0014E9FF File Offset: 0x0014CBFF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.refKeyword);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0014EA20 File Offset: 0x0014CC20
		static RefExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RefExpressionSyntax), (ObjectReader r) => new RefExpressionSyntax(r));
		}

		// Token: 0x0400166B RID: 5739
		internal readonly SyntaxToken refKeyword;

		// Token: 0x0400166C RID: 5740
		internal readonly ExpressionSyntax expression;
	}
}
