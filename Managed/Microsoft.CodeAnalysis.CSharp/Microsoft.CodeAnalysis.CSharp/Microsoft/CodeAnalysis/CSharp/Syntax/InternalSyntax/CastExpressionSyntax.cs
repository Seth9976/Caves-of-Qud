using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003AF RID: 943
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CastExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060053B5 RID: 21429 RVA: 0x0014DBE4 File Offset: 0x0014BDE4
		internal CastExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0014DC40 File Offset: 0x0014BE40
		internal CastExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0014DCA0 File Offset: 0x0014BEA0
		internal CastExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x0014DCF7 File Offset: 0x0014BEF7
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x060053B9 RID: 21433 RVA: 0x0014DCFF File Offset: 0x0014BEFF
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x060053BA RID: 21434 RVA: 0x0014DD07 File Offset: 0x0014BF07
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x0014DD0F File Offset: 0x0014BF0F
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0014DD18 File Offset: 0x0014BF18
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
				greenNode = this.type;
				break;
			case 2:
				greenNode = this.closeParenToken;
				break;
			case 3:
				greenNode = this.expression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0014DD64 File Offset: 0x0014BF64
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CastExpressionSyntax(this, parent, position);
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x0014DD6E File Offset: 0x0014BF6E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCastExpression(this);
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0014DD77 File Offset: 0x0014BF77
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCastExpression(this);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0014DD80 File Offset: 0x0014BF80
		public CastExpressionSyntax Update(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
		{
			if (openParenToken != this.OpenParenToken || type != this.Type || closeParenToken != this.CloseParenToken || expression != this.Expression)
			{
				CastExpressionSyntax castExpressionSyntax = SyntaxFactory.CastExpression(openParenToken, type, closeParenToken, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					castExpressionSyntax = castExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					castExpressionSyntax = castExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return castExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0014DDEC File Offset: 0x0014BFEC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CastExpressionSyntax(base.Kind, this.openParenToken, this.type, this.closeParenToken, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0014DE18 File Offset: 0x0014C018
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CastExpressionSyntax(base.Kind, this.openParenToken, this.type, this.closeParenToken, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0014DE44 File Offset: 0x0014C044
		internal CastExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0014DEC7 File Offset: 0x0014C0C7
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.type);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.expression);
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0014DF00 File Offset: 0x0014C100
		static CastExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CastExpressionSyntax), (ObjectReader r) => new CastExpressionSyntax(r));
		}

		// Token: 0x0400165C RID: 5724
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400165D RID: 5725
		internal readonly TypeSyntax type;

		// Token: 0x0400165E RID: 5726
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x0400165F RID: 5727
		internal readonly ExpressionSyntax expression;
	}
}
