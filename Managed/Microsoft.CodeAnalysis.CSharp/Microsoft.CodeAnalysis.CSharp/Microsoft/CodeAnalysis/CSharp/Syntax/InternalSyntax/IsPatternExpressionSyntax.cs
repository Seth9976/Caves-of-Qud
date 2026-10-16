using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D1 RID: 977
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IsPatternExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060055AB RID: 21931 RVA: 0x001534BA File Offset: 0x001516BA
		internal IsPatternExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(isKeyword);
			this.isKeyword = isKeyword;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x001534FC File Offset: 0x001516FC
		internal IsPatternExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(isKeyword);
			this.isKeyword = isKeyword;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x0015354B File Offset: 0x0015174B
		internal IsPatternExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(isKeyword);
			this.isKeyword = isKeyword;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060055AE RID: 21934 RVA: 0x00153587 File Offset: 0x00151787
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060055AF RID: 21935 RVA: 0x0015358F File Offset: 0x0015178F
		public SyntaxToken IsKeyword
		{
			get
			{
				return this.isKeyword;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x060055B0 RID: 21936 RVA: 0x00153597 File Offset: 0x00151797
		public PatternSyntax Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x001535A0 File Offset: 0x001517A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.expression;
				break;
			case 1:
				greenNode = this.isKeyword;
				break;
			case 2:
				greenNode = this.pattern;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x001535DF File Offset: 0x001517DF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IsPatternExpressionSyntax(this, parent, position);
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x001535E9 File Offset: 0x001517E9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIsPatternExpression(this);
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x001535F2 File Offset: 0x001517F2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIsPatternExpression(this);
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x001535FC File Offset: 0x001517FC
		public IsPatternExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
		{
			if (expression != this.Expression || isKeyword != this.IsKeyword || pattern != this.Pattern)
			{
				IsPatternExpressionSyntax isPatternExpressionSyntax = SyntaxFactory.IsPatternExpression(expression, isKeyword, pattern);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					isPatternExpressionSyntax = isPatternExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					isPatternExpressionSyntax = isPatternExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return isPatternExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x0015365C File Offset: 0x0015185C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IsPatternExpressionSyntax(base.Kind, this.expression, this.isKeyword, this.pattern, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x00153682 File Offset: 0x00151882
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IsPatternExpressionSyntax(base.Kind, this.expression, this.isKeyword, this.pattern, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x001536A8 File Offset: 0x001518A8
		internal IsPatternExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.isKeyword = syntaxToken;
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.pattern = patternSyntax;
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x00153711 File Offset: 0x00151911
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.isKeyword);
			writer.WriteValue(this.pattern);
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x0015373E File Offset: 0x0015193E
		static IsPatternExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IsPatternExpressionSyntax), (ObjectReader r) => new IsPatternExpressionSyntax(r));
		}

		// Token: 0x040016C2 RID: 5826
		internal readonly ExpressionSyntax expression;

		// Token: 0x040016C3 RID: 5827
		internal readonly SyntaxToken isKeyword;

		// Token: 0x040016C4 RID: 5828
		internal readonly PatternSyntax pattern;
	}
}
