using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A0 RID: 928
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class MakeRefExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060052CC RID: 21196 RVA: 0x0014B2F0 File Offset: 0x001494F0
		internal MakeRefExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x060052CD RID: 21197 RVA: 0x0014B34C File Offset: 0x0014954C
		internal MakeRefExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxFactoryContext context)
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

		// Token: 0x060052CE RID: 21198 RVA: 0x0014B3AC File Offset: 0x001495AC
		internal MakeRefExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
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

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x060052CF RID: 21199 RVA: 0x0014B403 File Offset: 0x00149603
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x060052D0 RID: 21200 RVA: 0x0014B40B File Offset: 0x0014960B
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x060052D1 RID: 21201 RVA: 0x0014B413 File Offset: 0x00149613
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x0014B41B File Offset: 0x0014961B
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0014B424 File Offset: 0x00149624
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

		// Token: 0x060052D4 RID: 21204 RVA: 0x0014B470 File Offset: 0x00149670
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new MakeRefExpressionSyntax(this, parent, position);
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0014B47A File Offset: 0x0014967A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMakeRefExpression(this);
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0014B483 File Offset: 0x00149683
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMakeRefExpression(this);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0014B48C File Offset: 0x0014968C
		public MakeRefExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || expression != this.Expression || closeParenToken != this.CloseParenToken)
			{
				MakeRefExpressionSyntax makeRefExpressionSyntax = SyntaxFactory.MakeRefExpression(keyword, openParenToken, expression, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					makeRefExpressionSyntax = makeRefExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					makeRefExpressionSyntax = makeRefExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return makeRefExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0014B4F8 File Offset: 0x001496F8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new MakeRefExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0014B524 File Offset: 0x00149724
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new MakeRefExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0014B550 File Offset: 0x00149750
		internal MakeRefExpressionSyntax(ObjectReader reader)
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

		// Token: 0x060052DB RID: 21211 RVA: 0x0014B5D3 File Offset: 0x001497D3
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0014B60C File Offset: 0x0014980C
		static MakeRefExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(MakeRefExpressionSyntax), (ObjectReader r) => new MakeRefExpressionSyntax(r));
		}

		// Token: 0x0400162D RID: 5677
		internal readonly SyntaxToken keyword;

		// Token: 0x0400162E RID: 5678
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400162F RID: 5679
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001630 RID: 5680
		internal readonly SyntaxToken closeParenToken;
	}
}
