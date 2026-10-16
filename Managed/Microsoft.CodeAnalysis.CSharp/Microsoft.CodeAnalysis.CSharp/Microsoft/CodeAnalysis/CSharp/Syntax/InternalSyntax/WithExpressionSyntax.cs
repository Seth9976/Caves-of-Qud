using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003BA RID: 954
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class WithExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600544F RID: 21583 RVA: 0x0014F81B File Offset: 0x0014DA1B
		internal WithExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(withKeyword);
			this.withKeyword = withKeyword;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0014F85C File Offset: 0x0014DA5C
		internal WithExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(withKeyword);
			this.withKeyword = withKeyword;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0014F8AB File Offset: 0x0014DAAB
		internal WithExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(withKeyword);
			this.withKeyword = withKeyword;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06005452 RID: 21586 RVA: 0x0014F8E7 File Offset: 0x0014DAE7
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x0014F8EF File Offset: 0x0014DAEF
		public SyntaxToken WithKeyword
		{
			get
			{
				return this.withKeyword;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06005454 RID: 21588 RVA: 0x0014F8F7 File Offset: 0x0014DAF7
		public InitializerExpressionSyntax Initializer
		{
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0014F900 File Offset: 0x0014DB00
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
				greenNode = this.withKeyword;
				break;
			case 2:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0014F93F File Offset: 0x0014DB3F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new WithExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0014F949 File Offset: 0x0014DB49
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWithExpression(this);
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0014F952 File Offset: 0x0014DB52
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWithExpression(this);
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0014F95C File Offset: 0x0014DB5C
		public WithExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
		{
			if (expression != this.Expression || withKeyword != this.WithKeyword || initializer != this.Initializer)
			{
				WithExpressionSyntax withExpressionSyntax = SyntaxFactory.WithExpression(expression, withKeyword, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					withExpressionSyntax = withExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					withExpressionSyntax = withExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return withExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0014F9BC File Offset: 0x0014DBBC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new WithExpressionSyntax(base.Kind, this.expression, this.withKeyword, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0014F9E2 File Offset: 0x0014DBE2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new WithExpressionSyntax(base.Kind, this.expression, this.withKeyword, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0014FA08 File Offset: 0x0014DC08
		internal WithExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.withKeyword = syntaxToken;
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(initializerExpressionSyntax);
			this.initializer = initializerExpressionSyntax;
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0014FA71 File Offset: 0x0014DC71
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.withKeyword);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0014FA9E File Offset: 0x0014DC9E
		static WithExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(WithExpressionSyntax), (ObjectReader r) => new WithExpressionSyntax(r));
		}

		// Token: 0x0400167D RID: 5757
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400167E RID: 5758
		internal readonly SyntaxToken withKeyword;

		// Token: 0x0400167F RID: 5759
		internal readonly InitializerExpressionSyntax initializer;
	}
}
