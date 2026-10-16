using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A1 RID: 929
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RefTypeExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060052DD RID: 21213 RVA: 0x0014B630 File Offset: 0x00149830
		internal RefTypeExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0014B68C File Offset: 0x0014988C
		internal RefTypeExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0014B6EC File Offset: 0x001498EC
		internal RefTypeExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x060052E0 RID: 21216 RVA: 0x0014B743 File Offset: 0x00149943
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x0014B74B File Offset: 0x0014994B
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x060052E2 RID: 21218 RVA: 0x0014B753 File Offset: 0x00149953
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x0014B75B File Offset: 0x0014995B
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0014B764 File Offset: 0x00149964
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.keyword;
				break;
			case 1:
				greenNode = this.openParenToken;
				break;
			case 2:
				greenNode = this.expression;
				break;
			case 3:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0014B7B0 File Offset: 0x001499B0
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RefTypeExpressionSyntax(this, parent, position);
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0014B7BA File Offset: 0x001499BA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefTypeExpression(this);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0014B7C3 File Offset: 0x001499C3
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefTypeExpression(this);
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0014B7CC File Offset: 0x001499CC
		public RefTypeExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || expression != this.Expression || closeParenToken != this.CloseParenToken)
			{
				RefTypeExpressionSyntax refTypeExpressionSyntax = SyntaxFactory.RefTypeExpression(keyword, openParenToken, expression, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					refTypeExpressionSyntax = refTypeExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					refTypeExpressionSyntax = refTypeExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return refTypeExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0014B838 File Offset: 0x00149A38
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RefTypeExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0014B864 File Offset: 0x00149A64
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RefTypeExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0014B890 File Offset: 0x00149A90
		internal RefTypeExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0014B913 File Offset: 0x00149B13
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0014B94C File Offset: 0x00149B4C
		static RefTypeExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RefTypeExpressionSyntax), (ObjectReader r) => new RefTypeExpressionSyntax(r));
		}

		// Token: 0x04001631 RID: 5681
		internal readonly SyntaxToken keyword;

		// Token: 0x04001632 RID: 5682
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001633 RID: 5683
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001634 RID: 5684
		internal readonly SyntaxToken closeParenToken;
	}
}
