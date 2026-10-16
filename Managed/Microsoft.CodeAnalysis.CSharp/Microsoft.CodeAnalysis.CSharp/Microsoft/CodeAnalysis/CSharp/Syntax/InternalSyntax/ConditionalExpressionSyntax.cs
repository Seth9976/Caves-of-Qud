using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200039B RID: 923
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConditionalExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600528D RID: 21133 RVA: 0x0014AA78 File Offset: 0x00148C78
		internal ConditionalExpressionSyntax(SyntaxKind kind, ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(questionToken);
			this.questionToken = questionToken;
			base.AdjustFlagsAndWidth(whenTrue);
			this.whenTrue = whenTrue;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(whenFalse);
			this.whenFalse = whenFalse;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0014AAE4 File Offset: 0x00148CE4
		internal ConditionalExpressionSyntax(SyntaxKind kind, ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(questionToken);
			this.questionToken = questionToken;
			base.AdjustFlagsAndWidth(whenTrue);
			this.whenTrue = whenTrue;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(whenFalse);
			this.whenFalse = whenFalse;
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x0014AB54 File Offset: 0x00148D54
		internal ConditionalExpressionSyntax(SyntaxKind kind, ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(questionToken);
			this.questionToken = questionToken;
			base.AdjustFlagsAndWidth(whenTrue);
			this.whenTrue = whenTrue;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(whenFalse);
			this.whenFalse = whenFalse;
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06005290 RID: 21136 RVA: 0x0014ABBB File Offset: 0x00148DBB
		public ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06005291 RID: 21137 RVA: 0x0014ABC3 File Offset: 0x00148DC3
		public SyntaxToken QuestionToken
		{
			get
			{
				return this.questionToken;
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06005292 RID: 21138 RVA: 0x0014ABCB File Offset: 0x00148DCB
		public ExpressionSyntax WhenTrue
		{
			get
			{
				return this.whenTrue;
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06005293 RID: 21139 RVA: 0x0014ABD3 File Offset: 0x00148DD3
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0014ABDB File Offset: 0x00148DDB
		public ExpressionSyntax WhenFalse
		{
			get
			{
				return this.whenFalse;
			}
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x0014ABE4 File Offset: 0x00148DE4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.condition;
				break;
			case 1:
				greenNode = this.questionToken;
				break;
			case 2:
				greenNode = this.whenTrue;
				break;
			case 3:
				greenNode = this.colonToken;
				break;
			case 4:
				greenNode = this.whenFalse;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0014AC3D File Offset: 0x00148E3D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConditionalExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0014AC47 File Offset: 0x00148E47
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConditionalExpression(this);
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0014AC50 File Offset: 0x00148E50
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConditionalExpression(this);
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0014AC5C File Offset: 0x00148E5C
		public ConditionalExpressionSyntax Update(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
		{
			if (condition != this.Condition || questionToken != this.QuestionToken || whenTrue != this.WhenTrue || colonToken != this.ColonToken || whenFalse != this.WhenFalse)
			{
				ConditionalExpressionSyntax conditionalExpressionSyntax = SyntaxFactory.ConditionalExpression(condition, questionToken, whenTrue, colonToken, whenFalse);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					conditionalExpressionSyntax = conditionalExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					conditionalExpressionSyntax = conditionalExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return conditionalExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x0014ACD4 File Offset: 0x00148ED4
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConditionalExpressionSyntax(base.Kind, this.condition, this.questionToken, this.whenTrue, this.colonToken, this.whenFalse, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0014AD06 File Offset: 0x00148F06
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConditionalExpressionSyntax(base.Kind, this.condition, this.questionToken, this.whenTrue, this.colonToken, this.whenFalse, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0014AD38 File Offset: 0x00148F38
		internal ConditionalExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.condition = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.questionToken = syntaxToken;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax2);
			this.whenTrue = expressionSyntax2;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
			ExpressionSyntax expressionSyntax3 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax3);
			this.whenFalse = expressionSyntax3;
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x0014ADD8 File Offset: 0x00148FD8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.questionToken);
			writer.WriteValue(this.whenTrue);
			writer.WriteValue(this.colonToken);
			writer.WriteValue(this.whenFalse);
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x0014AE28 File Offset: 0x00149028
		static ConditionalExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConditionalExpressionSyntax), (ObjectReader r) => new ConditionalExpressionSyntax(r));
		}

		// Token: 0x04001625 RID: 5669
		internal readonly ExpressionSyntax condition;

		// Token: 0x04001626 RID: 5670
		internal readonly SyntaxToken questionToken;

		// Token: 0x04001627 RID: 5671
		internal readonly ExpressionSyntax whenTrue;

		// Token: 0x04001628 RID: 5672
		internal readonly SyntaxToken colonToken;

		// Token: 0x04001629 RID: 5673
		internal readonly ExpressionSyntax whenFalse;
	}
}
