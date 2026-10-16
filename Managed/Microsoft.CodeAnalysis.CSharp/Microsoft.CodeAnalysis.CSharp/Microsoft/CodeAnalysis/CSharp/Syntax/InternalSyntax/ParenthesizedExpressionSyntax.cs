using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200038E RID: 910
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060051C5 RID: 20933 RVA: 0x00148C20 File Offset: 0x00146E20
		internal ParenthesizedExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00148C60 File Offset: 0x00146E60
		internal ParenthesizedExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x00148CAF File Offset: 0x00146EAF
		internal ParenthesizedExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x060051C8 RID: 20936 RVA: 0x00148CEB File Offset: 0x00146EEB
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x00148CF3 File Offset: 0x00146EF3
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x060051CA RID: 20938 RVA: 0x00148CFB File Offset: 0x00146EFB
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00148D04 File Offset: 0x00146F04
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openParenToken;
				break;
			case 1:
				greenNode = this.expression;
				break;
			case 2:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00148D43 File Offset: 0x00146F43
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ParenthesizedExpressionSyntax(this, parent, position);
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x00148D4D File Offset: 0x00146F4D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedExpression(this);
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00148D56 File Offset: 0x00146F56
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedExpression(this);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00148D60 File Offset: 0x00146F60
		public ParenthesizedExpressionSyntax Update(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (openParenToken != this.OpenParenToken || expression != this.Expression || closeParenToken != this.CloseParenToken)
			{
				ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = SyntaxFactory.ParenthesizedExpression(openParenToken, expression, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					parenthesizedExpressionSyntax = parenthesizedExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					parenthesizedExpressionSyntax = parenthesizedExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return parenthesizedExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00148DC0 File Offset: 0x00146FC0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ParenthesizedExpressionSyntax(base.Kind, this.openParenToken, this.expression, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x00148DE6 File Offset: 0x00146FE6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ParenthesizedExpressionSyntax(base.Kind, this.openParenToken, this.expression, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00148E0C File Offset: 0x0014700C
		internal ParenthesizedExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00148E75 File Offset: 0x00147075
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x00148EA2 File Offset: 0x001470A2
		static ParenthesizedExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ParenthesizedExpressionSyntax), (ObjectReader r) => new ParenthesizedExpressionSyntax(r));
		}

		// Token: 0x04001606 RID: 5638
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001607 RID: 5639
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001608 RID: 5640
		internal readonly SyntaxToken closeParenToken;
	}
}
