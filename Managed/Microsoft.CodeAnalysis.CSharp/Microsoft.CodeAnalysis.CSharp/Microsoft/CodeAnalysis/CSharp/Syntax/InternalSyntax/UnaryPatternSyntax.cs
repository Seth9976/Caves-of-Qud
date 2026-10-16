using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E1 RID: 993
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UnaryPatternSyntax : PatternSyntax
	{
		// Token: 0x06005693 RID: 22163 RVA: 0x001556FB File Offset: 0x001538FB
		internal UnaryPatternSyntax(SyntaxKind kind, SyntaxToken operatorToken, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x0015572B File Offset: 0x0015392B
		internal UnaryPatternSyntax(SyntaxKind kind, SyntaxToken operatorToken, PatternSyntax pattern, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0015575F File Offset: 0x0015395F
		internal UnaryPatternSyntax(SyntaxKind kind, SyntaxToken operatorToken, PatternSyntax pattern)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x0015578B File Offset: 0x0015398B
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06005697 RID: 22167 RVA: 0x00155793 File Offset: 0x00153993
		public PatternSyntax Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0015579C File Offset: 0x0015399C
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
					greenNode = this.pattern;
				}
			}
			else
			{
				greenNode = this.operatorToken;
			}
			return greenNode;
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x001557C7 File Offset: 0x001539C7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new UnaryPatternSyntax(this, parent, position);
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x001557D1 File Offset: 0x001539D1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUnaryPattern(this);
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x001557DA File Offset: 0x001539DA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUnaryPattern(this);
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x001557E4 File Offset: 0x001539E4
		public UnaryPatternSyntax Update(SyntaxToken operatorToken, PatternSyntax pattern)
		{
			if (operatorToken != this.OperatorToken || pattern != this.Pattern)
			{
				UnaryPatternSyntax unaryPatternSyntax = SyntaxFactory.UnaryPattern(operatorToken, pattern);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					unaryPatternSyntax = unaryPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					unaryPatternSyntax = unaryPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return unaryPatternSyntax;
			}
			return this;
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x0015583A File Offset: 0x00153A3A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new UnaryPatternSyntax(base.Kind, this.operatorToken, this.pattern, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x0015585A File Offset: 0x00153A5A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new UnaryPatternSyntax(base.Kind, this.operatorToken, this.pattern, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x0015587C File Offset: 0x00153A7C
		internal UnaryPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.pattern = patternSyntax;
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x001558CB File Offset: 0x00153ACB
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.pattern);
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x001558EC File Offset: 0x00153AEC
		static UnaryPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(UnaryPatternSyntax), (ObjectReader r) => new UnaryPatternSyntax(r));
		}

		// Token: 0x040016E4 RID: 5860
		internal readonly SyntaxToken operatorToken;

		// Token: 0x040016E5 RID: 5861
		internal readonly PatternSyntax pattern;
	}
}
