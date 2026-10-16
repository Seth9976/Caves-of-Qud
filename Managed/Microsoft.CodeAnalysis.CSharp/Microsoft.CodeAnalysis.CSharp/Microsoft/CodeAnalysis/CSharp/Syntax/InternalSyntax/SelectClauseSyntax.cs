using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003CC RID: 972
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SelectClauseSyntax : SelectOrGroupClauseSyntax
	{
		// Token: 0x0600555D RID: 21853 RVA: 0x00152878 File Offset: 0x00150A78
		internal SelectClauseSyntax(SyntaxKind kind, SyntaxToken selectKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(selectKeyword);
			this.selectKeyword = selectKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x001528A8 File Offset: 0x00150AA8
		internal SelectClauseSyntax(SyntaxKind kind, SyntaxToken selectKeyword, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(selectKeyword);
			this.selectKeyword = selectKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x001528DC File Offset: 0x00150ADC
		internal SelectClauseSyntax(SyntaxKind kind, SyntaxToken selectKeyword, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(selectKeyword);
			this.selectKeyword = selectKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06005560 RID: 21856 RVA: 0x00152908 File Offset: 0x00150B08
		public SyntaxToken SelectKeyword
		{
			get
			{
				return this.selectKeyword;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x00152910 File Offset: 0x00150B10
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x00152918 File Offset: 0x00150B18
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
				greenNode = this.selectKeyword;
			}
			return greenNode;
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x00152943 File Offset: 0x00150B43
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SelectClauseSyntax(this, parent, position);
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0015294D File Offset: 0x00150B4D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSelectClause(this);
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x00152956 File Offset: 0x00150B56
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSelectClause(this);
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x00152960 File Offset: 0x00150B60
		public SelectClauseSyntax Update(SyntaxToken selectKeyword, ExpressionSyntax expression)
		{
			if (selectKeyword != this.SelectKeyword || expression != this.Expression)
			{
				SelectClauseSyntax selectClauseSyntax = SyntaxFactory.SelectClause(selectKeyword, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					selectClauseSyntax = selectClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					selectClauseSyntax = selectClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return selectClauseSyntax;
			}
			return this;
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x001529B6 File Offset: 0x00150BB6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SelectClauseSyntax(base.Kind, this.selectKeyword, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x001529D6 File Offset: 0x00150BD6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SelectClauseSyntax(base.Kind, this.selectKeyword, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x001529F8 File Offset: 0x00150BF8
		internal SelectClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.selectKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x00152A47 File Offset: 0x00150C47
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.selectKeyword);
			writer.WriteValue(this.expression);
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x00152A68 File Offset: 0x00150C68
		static SelectClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SelectClauseSyntax), (ObjectReader r) => new SelectClauseSyntax(r));
		}

		// Token: 0x040016B5 RID: 5813
		internal readonly SyntaxToken selectKeyword;

		// Token: 0x040016B6 RID: 5814
		internal readonly ExpressionSyntax expression;
	}
}
