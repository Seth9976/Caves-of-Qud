using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200039F RID: 927
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LiteralExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060052BE RID: 21182 RVA: 0x0014B167 File Offset: 0x00149367
		internal LiteralExpressionSyntax(SyntaxKind kind, SyntaxToken token, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0014B188 File Offset: 0x00149388
		internal LiteralExpressionSyntax(SyntaxKind kind, SyntaxToken token, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0014B1AD File Offset: 0x001493AD
		internal LiteralExpressionSyntax(SyntaxKind kind, SyntaxToken token)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x0014B1CB File Offset: 0x001493CB
		public SyntaxToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0014B1D3 File Offset: 0x001493D3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.token;
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0014B1E0 File Offset: 0x001493E0
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LiteralExpressionSyntax(this, parent, position);
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0014B1EA File Offset: 0x001493EA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLiteralExpression(this);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0014B1F3 File Offset: 0x001493F3
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLiteralExpression(this);
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0014B1FC File Offset: 0x001493FC
		public LiteralExpressionSyntax Update(SyntaxToken token)
		{
			if (token != this.Token)
			{
				LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(base.Kind, token);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					literalExpressionSyntax = literalExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					literalExpressionSyntax = literalExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return literalExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x0014B24E File Offset: 0x0014944E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LiteralExpressionSyntax(base.Kind, this.token, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0014B268 File Offset: 0x00149468
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LiteralExpressionSyntax(base.Kind, this.token, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x0014B284 File Offset: 0x00149484
		internal LiteralExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.token = syntaxToken;
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x0014B2B9 File Offset: 0x001494B9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.token);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x0014B2CE File Offset: 0x001494CE
		static LiteralExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LiteralExpressionSyntax), (ObjectReader r) => new LiteralExpressionSyntax(r));
		}

		// Token: 0x0400162C RID: 5676
		internal readonly SyntaxToken token;
	}
}
