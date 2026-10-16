using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002EA RID: 746
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class UnaryPatternSyntax : PatternSyntax
	{
		// Token: 0x060041C7 RID: 16839 RVA: 0x00110008 File Offset: 0x0010E208
		internal UnaryPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x00110013 File Offset: 0x0010E213
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((UnaryPatternSyntax)base.Green).operatorToken, base.Position, 0);
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x00110032 File Offset: 0x0010E232
		public PatternSyntax Pattern
		{
			get
			{
				return base.GetRed<PatternSyntax>(ref this.pattern, 1);
			}
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x00110041 File Offset: 0x0010E241
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<PatternSyntax>(ref this.pattern, 1);
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x00110056 File Offset: 0x0010E256
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.pattern;
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00110064 File Offset: 0x0010E264
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUnaryPattern(this);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x0011006D File Offset: 0x0010E26D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUnaryPattern(this);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x00110078 File Offset: 0x0010E278
		public UnaryPatternSyntax Update(SyntaxToken operatorToken, PatternSyntax pattern)
		{
			if (!(operatorToken != this.OperatorToken) && pattern == this.Pattern)
			{
				return this;
			}
			UnaryPatternSyntax unaryPatternSyntax = SyntaxFactory.UnaryPattern(operatorToken, pattern);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return unaryPatternSyntax;
			}
			return unaryPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x001100BD File Offset: 0x0010E2BD
		public UnaryPatternSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(operatorToken, this.Pattern);
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x001100CC File Offset: 0x0010E2CC
		public UnaryPatternSyntax WithPattern(PatternSyntax pattern)
		{
			return this.Update(this.OperatorToken, pattern);
		}

		// Token: 0x040014BA RID: 5306
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax pattern;
	}
}
