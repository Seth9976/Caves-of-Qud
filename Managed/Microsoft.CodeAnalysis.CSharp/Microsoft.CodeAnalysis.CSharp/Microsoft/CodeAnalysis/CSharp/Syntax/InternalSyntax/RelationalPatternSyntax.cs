using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003DE RID: 990
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RelationalPatternSyntax : PatternSyntax
	{
		// Token: 0x06005666 RID: 22118 RVA: 0x001550BB File Offset: 0x001532BB
		internal RelationalPatternSyntax(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x001550EB File Offset: 0x001532EB
		internal RelationalPatternSyntax(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0015511F File Offset: 0x0015331F
		internal RelationalPatternSyntax(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06005669 RID: 22121 RVA: 0x0015514B File Offset: 0x0015334B
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x0600566A RID: 22122 RVA: 0x00155153 File Offset: 0x00153353
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0015515C File Offset: 0x0015335C
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
				greenNode = this.operatorToken;
			}
			return greenNode;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x00155187 File Offset: 0x00153387
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RelationalPatternSyntax(this, parent, position);
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x00155191 File Offset: 0x00153391
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRelationalPattern(this);
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x0015519A File Offset: 0x0015339A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRelationalPattern(this);
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x001551A4 File Offset: 0x001533A4
		public RelationalPatternSyntax Update(SyntaxToken operatorToken, ExpressionSyntax expression)
		{
			if (operatorToken != this.OperatorToken || expression != this.Expression)
			{
				RelationalPatternSyntax relationalPatternSyntax = SyntaxFactory.RelationalPattern(operatorToken, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					relationalPatternSyntax = relationalPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					relationalPatternSyntax = relationalPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return relationalPatternSyntax;
			}
			return this;
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x001551FA File Offset: 0x001533FA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RelationalPatternSyntax(base.Kind, this.operatorToken, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x0015521A File Offset: 0x0015341A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RelationalPatternSyntax(base.Kind, this.operatorToken, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x0015523C File Offset: 0x0015343C
		internal RelationalPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x0015528B File Offset: 0x0015348B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x001552AC File Offset: 0x001534AC
		static RelationalPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RelationalPatternSyntax), (ObjectReader r) => new RelationalPatternSyntax(r));
		}

		// Token: 0x040016DE RID: 5854
		internal readonly SyntaxToken operatorToken;

		// Token: 0x040016DF RID: 5855
		internal readonly ExpressionSyntax expression;
	}
}
