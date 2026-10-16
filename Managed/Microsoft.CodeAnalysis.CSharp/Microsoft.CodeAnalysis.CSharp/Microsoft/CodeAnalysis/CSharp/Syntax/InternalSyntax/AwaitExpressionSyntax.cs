using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000391 RID: 913
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AwaitExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060051F4 RID: 20980 RVA: 0x001493B1 File Offset: 0x001475B1
		internal AwaitExpressionSyntax(SyntaxKind kind, SyntaxToken awaitKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(awaitKeyword);
			this.awaitKeyword = awaitKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x001493E1 File Offset: 0x001475E1
		internal AwaitExpressionSyntax(SyntaxKind kind, SyntaxToken awaitKeyword, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(awaitKeyword);
			this.awaitKeyword = awaitKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00149415 File Offset: 0x00147615
		internal AwaitExpressionSyntax(SyntaxKind kind, SyntaxToken awaitKeyword, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(awaitKeyword);
			this.awaitKeyword = awaitKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x060051F7 RID: 20983 RVA: 0x00149441 File Offset: 0x00147641
		public SyntaxToken AwaitKeyword
		{
			get
			{
				return this.awaitKeyword;
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x00149449 File Offset: 0x00147649
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00149454 File Offset: 0x00147654
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
				greenNode = this.awaitKeyword;
			}
			return greenNode;
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x0014947F File Offset: 0x0014767F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AwaitExpressionSyntax(this, parent, position);
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x00149489 File Offset: 0x00147689
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAwaitExpression(this);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00149492 File Offset: 0x00147692
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAwaitExpression(this);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0014949C File Offset: 0x0014769C
		public AwaitExpressionSyntax Update(SyntaxToken awaitKeyword, ExpressionSyntax expression)
		{
			if (awaitKeyword != this.AwaitKeyword || expression != this.Expression)
			{
				AwaitExpressionSyntax awaitExpressionSyntax = SyntaxFactory.AwaitExpression(awaitKeyword, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					awaitExpressionSyntax = awaitExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					awaitExpressionSyntax = awaitExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return awaitExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x001494F2 File Offset: 0x001476F2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AwaitExpressionSyntax(base.Kind, this.awaitKeyword, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x00149512 File Offset: 0x00147712
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AwaitExpressionSyntax(base.Kind, this.awaitKeyword, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00149534 File Offset: 0x00147734
		internal AwaitExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.awaitKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00149583 File Offset: 0x00147783
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.awaitKeyword);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x001495A4 File Offset: 0x001477A4
		static AwaitExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AwaitExpressionSyntax), (ObjectReader r) => new AwaitExpressionSyntax(r));
		}

		// Token: 0x0400160E RID: 5646
		internal readonly SyntaxToken awaitKeyword;

		// Token: 0x0400160F RID: 5647
		internal readonly ExpressionSyntax expression;
	}
}
