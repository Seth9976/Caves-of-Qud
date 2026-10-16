using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A3 RID: 931
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CheckedExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005301 RID: 21249 RVA: 0x0014BDE4 File Offset: 0x00149FE4
		internal CheckedExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005302 RID: 21250 RVA: 0x0014BE40 File Offset: 0x0014A040
		internal CheckedExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxFactoryContext context)
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

		// Token: 0x06005303 RID: 21251 RVA: 0x0014BEA0 File Offset: 0x0014A0A0
		internal CheckedExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
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

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0014BEF7 File Offset: 0x0014A0F7
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x0014BEFF File Offset: 0x0014A0FF
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06005306 RID: 21254 RVA: 0x0014BF07 File Offset: 0x0014A107
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x0014BF0F File Offset: 0x0014A10F
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0014BF18 File Offset: 0x0014A118
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

		// Token: 0x06005309 RID: 21257 RVA: 0x0014BF64 File Offset: 0x0014A164
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CheckedExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x0014BF6E File Offset: 0x0014A16E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCheckedExpression(this);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0014BF77 File Offset: 0x0014A177
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCheckedExpression(this);
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0014BF80 File Offset: 0x0014A180
		public CheckedExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || expression != this.Expression || closeParenToken != this.CloseParenToken)
			{
				CheckedExpressionSyntax checkedExpressionSyntax = SyntaxFactory.CheckedExpression(base.Kind, keyword, openParenToken, expression, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					checkedExpressionSyntax = checkedExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					checkedExpressionSyntax = checkedExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return checkedExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0014BFF2 File Offset: 0x0014A1F2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CheckedExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0014C01E File Offset: 0x0014A21E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CheckedExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x0014C04C File Offset: 0x0014A24C
		internal CheckedExpressionSyntax(ObjectReader reader)
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

		// Token: 0x06005310 RID: 21264 RVA: 0x0014C0CF File Offset: 0x0014A2CF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0014C108 File Offset: 0x0014A308
		static CheckedExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CheckedExpressionSyntax), (ObjectReader r) => new CheckedExpressionSyntax(r));
		}

		// Token: 0x0400163B RID: 5691
		internal readonly SyntaxToken keyword;

		// Token: 0x0400163C RID: 5692
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400163D RID: 5693
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400163E RID: 5694
		internal readonly SyntaxToken closeParenToken;
	}
}
