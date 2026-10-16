using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003CB RID: 971
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OrderingSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600554E RID: 21838 RVA: 0x00152654 File Offset: 0x00150854
		internal OrderingSyntax(SyntaxKind kind, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken ascendingOrDescendingKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (ascendingOrDescendingKeyword != null)
			{
				base.AdjustFlagsAndWidth(ascendingOrDescendingKeyword);
				this.ascendingOrDescendingKeyword = ascendingOrDescendingKeyword;
			}
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x00152687 File Offset: 0x00150887
		internal OrderingSyntax(SyntaxKind kind, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken ascendingOrDescendingKeyword, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (ascendingOrDescendingKeyword != null)
			{
				base.AdjustFlagsAndWidth(ascendingOrDescendingKeyword);
				this.ascendingOrDescendingKeyword = ascendingOrDescendingKeyword;
			}
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x001526BE File Offset: 0x001508BE
		internal OrderingSyntax(SyntaxKind kind, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken ascendingOrDescendingKeyword)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (ascendingOrDescendingKeyword != null)
			{
				base.AdjustFlagsAndWidth(ascendingOrDescendingKeyword);
				this.ascendingOrDescendingKeyword = ascendingOrDescendingKeyword;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x001526ED File Offset: 0x001508ED
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x001526F5 File Offset: 0x001508F5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken AscendingOrDescendingKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ascendingOrDescendingKeyword;
			}
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x00152700 File Offset: 0x00150900
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
					greenNode = this.ascendingOrDescendingKeyword;
				}
			}
			else
			{
				greenNode = this.expression;
			}
			return greenNode;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0015272B File Offset: 0x0015092B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new OrderingSyntax(this, parent, position);
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x00152735 File Offset: 0x00150935
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOrdering(this);
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x0015273E File Offset: 0x0015093E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOrdering(this);
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x00152748 File Offset: 0x00150948
		public OrderingSyntax Update(ExpressionSyntax expression, SyntaxToken ascendingOrDescendingKeyword)
		{
			if (expression != this.Expression || ascendingOrDescendingKeyword != this.AscendingOrDescendingKeyword)
			{
				OrderingSyntax orderingSyntax = SyntaxFactory.Ordering(base.Kind, expression, ascendingOrDescendingKeyword);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					orderingSyntax = orderingSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					orderingSyntax = orderingSyntax.WithAnnotationsGreen(annotations);
				}
				return orderingSyntax;
			}
			return this;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x001527A4 File Offset: 0x001509A4
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new OrderingSyntax(base.Kind, this.expression, this.ascendingOrDescendingKeyword, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x001527C4 File Offset: 0x001509C4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new OrderingSyntax(base.Kind, this.expression, this.ascendingOrDescendingKeyword, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x001527E4 File Offset: 0x001509E4
		internal OrderingSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.ascendingOrDescendingKeyword = syntaxToken;
			}
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x00152836 File Offset: 0x00150A36
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.ascendingOrDescendingKeyword);
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x00152857 File Offset: 0x00150A57
		static OrderingSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(OrderingSyntax), (ObjectReader r) => new OrderingSyntax(r));
		}

		// Token: 0x040016B3 RID: 5811
		internal readonly ExpressionSyntax expression;

		// Token: 0x040016B4 RID: 5812
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken ascendingOrDescendingKeyword;
	}
}
