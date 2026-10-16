using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000392 RID: 914
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PostfixUnaryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005203 RID: 20995 RVA: 0x001495C5 File Offset: 0x001477C5
		internal PostfixUnaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operand);
			this.operand = operand;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x001495F5 File Offset: 0x001477F5
		internal PostfixUnaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operand);
			this.operand = operand;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00149629 File Offset: 0x00147829
		internal PostfixUnaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operand);
			this.operand = operand;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x00149655 File Offset: 0x00147855
		public ExpressionSyntax Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x0014965D File Offset: 0x0014785D
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x00149668 File Offset: 0x00147868
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
					greenNode = this.operatorToken;
				}
			}
			else
			{
				greenNode = this.operand;
			}
			return greenNode;
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00149693 File Offset: 0x00147893
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PostfixUnaryExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0014969D File Offset: 0x0014789D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPostfixUnaryExpression(this);
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x001496A6 File Offset: 0x001478A6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPostfixUnaryExpression(this);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x001496B0 File Offset: 0x001478B0
		public PostfixUnaryExpressionSyntax Update(ExpressionSyntax operand, SyntaxToken operatorToken)
		{
			if (operand != this.Operand || operatorToken != this.OperatorToken)
			{
				PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax = SyntaxFactory.PostfixUnaryExpression(base.Kind, operand, operatorToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					postfixUnaryExpressionSyntax = postfixUnaryExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					postfixUnaryExpressionSyntax = postfixUnaryExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return postfixUnaryExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0014970C File Offset: 0x0014790C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PostfixUnaryExpressionSyntax(base.Kind, this.operand, this.operatorToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0014972C File Offset: 0x0014792C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PostfixUnaryExpressionSyntax(base.Kind, this.operand, this.operatorToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0014974C File Offset: 0x0014794C
		internal PostfixUnaryExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.operand = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0014979B File Offset: 0x0014799B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.operand);
			writer.WriteValue(this.operatorToken);
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x001497BC File Offset: 0x001479BC
		static PostfixUnaryExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PostfixUnaryExpressionSyntax), (ObjectReader r) => new PostfixUnaryExpressionSyntax(r));
		}

		// Token: 0x04001610 RID: 5648
		internal readonly ExpressionSyntax operand;

		// Token: 0x04001611 RID: 5649
		internal readonly SyntaxToken operatorToken;
	}
}
