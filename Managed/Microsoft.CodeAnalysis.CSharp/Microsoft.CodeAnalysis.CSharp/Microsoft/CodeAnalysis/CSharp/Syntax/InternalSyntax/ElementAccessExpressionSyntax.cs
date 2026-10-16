using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A8 RID: 936
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ElementAccessExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005354 RID: 21332 RVA: 0x0014CCFD File Offset: 0x0014AEFD
		internal ElementAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, BracketedArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0014CD2D File Offset: 0x0014AF2D
		internal ElementAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, BracketedArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0014CD61 File Offset: 0x0014AF61
		internal ElementAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x0014CD8D File Offset: 0x0014AF8D
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06005358 RID: 21336 RVA: 0x0014CD95 File Offset: 0x0014AF95
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0014CDA0 File Offset: 0x0014AFA0
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

		// Token: 0x0600535A RID: 21338 RVA: 0x0014CDCB File Offset: 0x0014AFCB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ElementAccessExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0014CDD5 File Offset: 0x0014AFD5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElementAccessExpression(this);
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0014CDDE File Offset: 0x0014AFDE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElementAccessExpression(this);
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0014CDE8 File Offset: 0x0014AFE8
		public ElementAccessExpressionSyntax Update(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
		{
			if (expression != this.Expression || argumentList != this.ArgumentList)
			{
				ElementAccessExpressionSyntax elementAccessExpressionSyntax = SyntaxFactory.ElementAccessExpression(expression, argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					elementAccessExpressionSyntax = elementAccessExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					elementAccessExpressionSyntax = elementAccessExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return elementAccessExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0014CE3E File Offset: 0x0014B03E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ElementAccessExpressionSyntax(base.Kind, this.expression, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0014CE5E File Offset: 0x0014B05E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ElementAccessExpressionSyntax(base.Kind, this.expression, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0014CE80 File Offset: 0x0014B080
		internal ElementAccessExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(bracketedArgumentListSyntax);
			this.argumentList = bracketedArgumentListSyntax;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0014CECF File Offset: 0x0014B0CF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0014CEF0 File Offset: 0x0014B0F0
		static ElementAccessExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ElementAccessExpressionSyntax), (ObjectReader r) => new ElementAccessExpressionSyntax(r));
		}

		// Token: 0x0400164D RID: 5709
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400164E RID: 5710
		internal readonly BracketedArgumentListSyntax argumentList;
	}
}
