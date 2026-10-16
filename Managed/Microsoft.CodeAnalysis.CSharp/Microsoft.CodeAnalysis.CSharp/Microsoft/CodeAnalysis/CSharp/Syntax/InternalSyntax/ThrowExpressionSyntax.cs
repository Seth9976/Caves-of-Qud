using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D2 RID: 978
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ThrowExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060055BB RID: 21947 RVA: 0x0015375F File Offset: 0x0015195F
		internal ThrowExpressionSyntax(SyntaxKind kind, SyntaxToken throwKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(throwKeyword);
			this.throwKeyword = throwKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x0015378F File Offset: 0x0015198F
		internal ThrowExpressionSyntax(SyntaxKind kind, SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(throwKeyword);
			this.throwKeyword = throwKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x001537C3 File Offset: 0x001519C3
		internal ThrowExpressionSyntax(SyntaxKind kind, SyntaxToken throwKeyword, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(throwKeyword);
			this.throwKeyword = throwKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x001537EF File Offset: 0x001519EF
		public SyntaxToken ThrowKeyword
		{
			get
			{
				return this.throwKeyword;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x060055BF RID: 21951 RVA: 0x001537F7 File Offset: 0x001519F7
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00153800 File Offset: 0x00151A00
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
				greenNode = this.throwKeyword;
			}
			return greenNode;
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x0015382B File Offset: 0x00151A2B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ThrowExpressionSyntax(this, parent, position);
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x00153835 File Offset: 0x00151A35
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitThrowExpression(this);
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x0015383E File Offset: 0x00151A3E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitThrowExpression(this);
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00153848 File Offset: 0x00151A48
		public ThrowExpressionSyntax Update(SyntaxToken throwKeyword, ExpressionSyntax expression)
		{
			if (throwKeyword != this.ThrowKeyword || expression != this.Expression)
			{
				ThrowExpressionSyntax throwExpressionSyntax = SyntaxFactory.ThrowExpression(throwKeyword, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					throwExpressionSyntax = throwExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					throwExpressionSyntax = throwExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return throwExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0015389E File Offset: 0x00151A9E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ThrowExpressionSyntax(base.Kind, this.throwKeyword, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x001538BE File Offset: 0x00151ABE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ThrowExpressionSyntax(base.Kind, this.throwKeyword, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x001538E0 File Offset: 0x00151AE0
		internal ThrowExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.throwKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0015392F File Offset: 0x00151B2F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.throwKeyword);
			writer.WriteValue(this.expression);
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x00153950 File Offset: 0x00151B50
		static ThrowExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ThrowExpressionSyntax), (ObjectReader r) => new ThrowExpressionSyntax(r));
		}

		// Token: 0x040016C5 RID: 5829
		internal readonly SyntaxToken throwKeyword;

		// Token: 0x040016C6 RID: 5830
		internal readonly ExpressionSyntax expression;
	}
}
