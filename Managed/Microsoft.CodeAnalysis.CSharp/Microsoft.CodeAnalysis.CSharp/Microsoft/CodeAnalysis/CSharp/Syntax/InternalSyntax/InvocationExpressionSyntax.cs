using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A7 RID: 935
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InvocationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005345 RID: 21317 RVA: 0x0014CAE9 File Offset: 0x0014ACE9
		internal InvocationExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0014CB19 File Offset: 0x0014AD19
		internal InvocationExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, ArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0014CB4D File Offset: 0x0014AD4D
		internal InvocationExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, ArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x0014CB79 File Offset: 0x0014AD79
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x0014CB81 File Offset: 0x0014AD81
		public ArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0014CB8C File Offset: 0x0014AD8C
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
					greenNode = this.argumentList;
				}
			}
			else
			{
				greenNode = this.expression;
			}
			return greenNode;
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0014CBB7 File Offset: 0x0014ADB7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InvocationExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x0014CBC1 File Offset: 0x0014ADC1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInvocationExpression(this);
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0014CBCA File Offset: 0x0014ADCA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInvocationExpression(this);
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x0014CBD4 File Offset: 0x0014ADD4
		public InvocationExpressionSyntax Update(ExpressionSyntax expression, ArgumentListSyntax argumentList)
		{
			if (expression != this.Expression || argumentList != this.ArgumentList)
			{
				InvocationExpressionSyntax invocationExpressionSyntax = SyntaxFactory.InvocationExpression(expression, argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					invocationExpressionSyntax = invocationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					invocationExpressionSyntax = invocationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return invocationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0014CC2A File Offset: 0x0014AE2A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InvocationExpressionSyntax(base.Kind, this.expression, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0014CC4A File Offset: 0x0014AE4A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InvocationExpressionSyntax(base.Kind, this.expression, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0014CC6C File Offset: 0x0014AE6C
		internal InvocationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(argumentListSyntax);
			this.argumentList = argumentListSyntax;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0014CCBB File Offset: 0x0014AEBB
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0014CCDC File Offset: 0x0014AEDC
		static InvocationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InvocationExpressionSyntax), (ObjectReader r) => new InvocationExpressionSyntax(r));
		}

		// Token: 0x0400164B RID: 5707
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400164C RID: 5708
		internal readonly ArgumentListSyntax argumentList;
	}
}
