using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003DC RID: 988
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConstantPatternSyntax : PatternSyntax
	{
		// Token: 0x06005648 RID: 22088 RVA: 0x00154C94 File Offset: 0x00152E94
		internal ConstantPatternSyntax(SyntaxKind kind, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x00154CB5 File Offset: 0x00152EB5
		internal ConstantPatternSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x00154CDA File Offset: 0x00152EDA
		internal ConstantPatternSyntax(SyntaxKind kind, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x0600564B RID: 22091 RVA: 0x00154CF8 File Offset: 0x00152EF8
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x00154D00 File Offset: 0x00152F00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x0600564D RID: 22093 RVA: 0x00154D0D File Offset: 0x00152F0D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConstantPatternSyntax(this, parent, position);
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x00154D17 File Offset: 0x00152F17
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstantPattern(this);
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x00154D20 File Offset: 0x00152F20
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstantPattern(this);
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x00154D2C File Offset: 0x00152F2C
		public ConstantPatternSyntax Update(ExpressionSyntax expression)
		{
			if (expression != this.Expression)
			{
				ConstantPatternSyntax constantPatternSyntax = SyntaxFactory.ConstantPattern(expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					constantPatternSyntax = constantPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					constantPatternSyntax = constantPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return constantPatternSyntax;
			}
			return this;
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x00154D78 File Offset: 0x00152F78
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConstantPatternSyntax(base.Kind, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x00154D92 File Offset: 0x00152F92
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConstantPatternSyntax(base.Kind, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x00154DAC File Offset: 0x00152FAC
		internal ConstantPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x00154DE1 File Offset: 0x00152FE1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x00154DF6 File Offset: 0x00152FF6
		static ConstantPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConstantPatternSyntax), (ObjectReader r) => new ConstantPatternSyntax(r));
		}

		// Token: 0x040016DA RID: 5850
		internal readonly ExpressionSyntax expression;
	}
}
