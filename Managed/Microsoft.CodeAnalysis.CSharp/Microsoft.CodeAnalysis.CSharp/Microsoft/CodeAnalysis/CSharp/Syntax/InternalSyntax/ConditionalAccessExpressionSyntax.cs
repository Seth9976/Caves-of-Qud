using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000394 RID: 916
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConditionalAccessExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005222 RID: 21026 RVA: 0x00149A8B File Offset: 0x00147C8B
		internal ConditionalAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(whenNotNull);
			this.whenNotNull = whenNotNull;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00149ACC File Offset: 0x00147CCC
		internal ConditionalAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(whenNotNull);
			this.whenNotNull = whenNotNull;
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x00149B1B File Offset: 0x00147D1B
		internal ConditionalAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(whenNotNull);
			this.whenNotNull = whenNotNull;
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x00149B57 File Offset: 0x00147D57
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x00149B5F File Offset: 0x00147D5F
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x00149B67 File Offset: 0x00147D67
		public ExpressionSyntax WhenNotNull
		{
			get
			{
				return this.whenNotNull;
			}
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x00149B70 File Offset: 0x00147D70
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.expression;
				break;
			case 1:
				greenNode = this.operatorToken;
				break;
			case 2:
				greenNode = this.whenNotNull;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00149BAF File Offset: 0x00147DAF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConditionalAccessExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00149BB9 File Offset: 0x00147DB9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConditionalAccessExpression(this);
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x00149BC2 File Offset: 0x00147DC2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConditionalAccessExpression(this);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00149BCC File Offset: 0x00147DCC
		public ConditionalAccessExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
		{
			if (expression != this.Expression || operatorToken != this.OperatorToken || whenNotNull != this.WhenNotNull)
			{
				ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax = SyntaxFactory.ConditionalAccessExpression(expression, operatorToken, whenNotNull);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					conditionalAccessExpressionSyntax = conditionalAccessExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					conditionalAccessExpressionSyntax = conditionalAccessExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return conditionalAccessExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00149C2C File Offset: 0x00147E2C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConditionalAccessExpressionSyntax(base.Kind, this.expression, this.operatorToken, this.whenNotNull, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00149C52 File Offset: 0x00147E52
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConditionalAccessExpressionSyntax(base.Kind, this.expression, this.operatorToken, this.whenNotNull, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x00149C78 File Offset: 0x00147E78
		internal ConditionalAccessExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax2);
			this.whenNotNull = expressionSyntax2;
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x00149CE1 File Offset: 0x00147EE1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.whenNotNull);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x00149D0E File Offset: 0x00147F0E
		static ConditionalAccessExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConditionalAccessExpressionSyntax), (ObjectReader r) => new ConditionalAccessExpressionSyntax(r));
		}

		// Token: 0x04001615 RID: 5653
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001616 RID: 5654
		internal readonly SyntaxToken operatorToken;

		// Token: 0x04001617 RID: 5655
		internal readonly ExpressionSyntax whenNotNull;
	}
}
