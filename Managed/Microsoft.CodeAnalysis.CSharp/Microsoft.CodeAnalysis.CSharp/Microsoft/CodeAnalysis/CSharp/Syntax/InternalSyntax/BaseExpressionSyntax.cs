using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200039E RID: 926
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BaseExpressionSyntax : InstanceExpressionSyntax
	{
		// Token: 0x060052B0 RID: 21168 RVA: 0x0014AFE7 File Offset: 0x001491E7
		internal BaseExpressionSyntax(SyntaxKind kind, SyntaxToken token, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0014B008 File Offset: 0x00149208
		internal BaseExpressionSyntax(SyntaxKind kind, SyntaxToken token, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0014B02D File Offset: 0x0014922D
		internal BaseExpressionSyntax(SyntaxKind kind, SyntaxToken token)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x0014B04B File Offset: 0x0014924B
		public SyntaxToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0014B053 File Offset: 0x00149253
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.token;
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0014B060 File Offset: 0x00149260
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BaseExpressionSyntax(this, parent, position);
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0014B06A File Offset: 0x0014926A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBaseExpression(this);
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0014B073 File Offset: 0x00149273
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBaseExpression(this);
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0014B07C File Offset: 0x0014927C
		public BaseExpressionSyntax Update(SyntaxToken token)
		{
			if (token != this.Token)
			{
				BaseExpressionSyntax baseExpressionSyntax = SyntaxFactory.BaseExpression(token);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					baseExpressionSyntax = baseExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					baseExpressionSyntax = baseExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return baseExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0014B0C8 File Offset: 0x001492C8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BaseExpressionSyntax(base.Kind, this.token, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0014B0E2 File Offset: 0x001492E2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BaseExpressionSyntax(base.Kind, this.token, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x0014B0FC File Offset: 0x001492FC
		internal BaseExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.token = syntaxToken;
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0014B131 File Offset: 0x00149331
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.token);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0014B146 File Offset: 0x00149346
		static BaseExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BaseExpressionSyntax), (ObjectReader r) => new BaseExpressionSyntax(r));
		}

		// Token: 0x0400162B RID: 5675
		internal readonly SyntaxToken token;
	}
}
