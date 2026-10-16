using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000397 RID: 919
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RangeExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600524F RID: 21071 RVA: 0x0014A0C4 File Offset: 0x001482C4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal RangeExpressionSyntax(SyntaxKind kind, ExpressionSyntax leftOperand, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorToken, ExpressionSyntax rightOperand, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (leftOperand != null)
			{
				base.AdjustFlagsAndWidth(leftOperand);
				this.leftOperand = leftOperand;
			}
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			if (rightOperand != null)
			{
				base.AdjustFlagsAndWidth(rightOperand);
				this.rightOperand = rightOperand;
			}
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x0014A118 File Offset: 0x00148318
		internal RangeExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax leftOperand, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax rightOperand, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (leftOperand != null)
			{
				base.AdjustFlagsAndWidth(leftOperand);
				this.leftOperand = leftOperand;
			}
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			if (rightOperand != null)
			{
				base.AdjustFlagsAndWidth(rightOperand);
				this.rightOperand = rightOperand;
			}
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x0014A170 File Offset: 0x00148370
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal RangeExpressionSyntax(SyntaxKind kind, ExpressionSyntax leftOperand, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorToken, ExpressionSyntax rightOperand)
			: base(kind)
		{
			base.SlotCount = 3;
			if (leftOperand != null)
			{
				base.AdjustFlagsAndWidth(leftOperand);
				this.leftOperand = leftOperand;
			}
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			if (rightOperand != null)
			{
				base.AdjustFlagsAndWidth(rightOperand);
				this.rightOperand = rightOperand;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06005252 RID: 21074 RVA: 0x0014A1BE File Offset: 0x001483BE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax LeftOperand
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.leftOperand;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06005253 RID: 21075 RVA: 0x0014A1C6 File Offset: 0x001483C6
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06005254 RID: 21076 RVA: 0x0014A1CE File Offset: 0x001483CE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax RightOperand
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.rightOperand;
			}
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0014A1D8 File Offset: 0x001483D8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.leftOperand;
				break;
			case 1:
				greenNode = this.operatorToken;
				break;
			case 2:
				greenNode = this.rightOperand;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0014A217 File Offset: 0x00148417
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RangeExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0014A221 File Offset: 0x00148421
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRangeExpression(this);
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0014A22A File Offset: 0x0014842A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRangeExpression(this);
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x0014A234 File Offset: 0x00148434
		public RangeExpressionSyntax Update(ExpressionSyntax leftOperand, SyntaxToken operatorToken, ExpressionSyntax rightOperand)
		{
			if (leftOperand != this.LeftOperand || operatorToken != this.OperatorToken || rightOperand != this.RightOperand)
			{
				RangeExpressionSyntax rangeExpressionSyntax = SyntaxFactory.RangeExpression(leftOperand, operatorToken, rightOperand);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					rangeExpressionSyntax = rangeExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					rangeExpressionSyntax = rangeExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return rangeExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0014A294 File Offset: 0x00148494
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RangeExpressionSyntax(base.Kind, this.leftOperand, this.operatorToken, this.rightOperand, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0014A2BA File Offset: 0x001484BA
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RangeExpressionSyntax(base.Kind, this.leftOperand, this.operatorToken, this.rightOperand, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x0014A2E0 File Offset: 0x001484E0
		internal RangeExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.leftOperand = expressionSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax2 != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax2);
				this.rightOperand = expressionSyntax2;
			}
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0014A34F File Offset: 0x0014854F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.leftOperand);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.rightOperand);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0014A37C File Offset: 0x0014857C
		static RangeExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RangeExpressionSyntax), (ObjectReader r) => new RangeExpressionSyntax(r));
		}

		// Token: 0x0400161B RID: 5659
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax leftOperand;

		// Token: 0x0400161C RID: 5660
		internal readonly SyntaxToken operatorToken;

		// Token: 0x0400161D RID: 5661
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax rightOperand;
	}
}
