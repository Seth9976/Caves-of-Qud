using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E6 RID: 742
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ParenthesizedPatternSyntax : PatternSyntax
	{
		// Token: 0x0600419D RID: 16797 RVA: 0x0010FC23 File Offset: 0x0010DE23
		internal ParenthesizedPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x0010FC2E File Offset: 0x0010DE2E
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedPatternSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x0010FC4D File Offset: 0x0010DE4D
		public PatternSyntax Pattern
		{
			get
			{
				return base.GetRed<PatternSyntax>(ref this.pattern, 1);
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x0010FC5C File Offset: 0x0010DE5C
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParenthesizedPatternSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x0010FC82 File Offset: 0x0010DE82
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<PatternSyntax>(ref this.pattern, 1);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x0010FC97 File Offset: 0x0010DE97
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.pattern;
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x0010FCA5 File Offset: 0x0010DEA5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedPattern(this);
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x0010FCAE File Offset: 0x0010DEAE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedPattern(this);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x0010FCB8 File Offset: 0x0010DEB8
		public ParenthesizedPatternSyntax Update(SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && pattern == this.Pattern && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			ParenthesizedPatternSyntax parenthesizedPatternSyntax = SyntaxFactory.ParenthesizedPattern(openParenToken, pattern, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return parenthesizedPatternSyntax;
			}
			return parenthesizedPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x0010FD0C File Offset: 0x0010DF0C
		public ParenthesizedPatternSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Pattern, this.CloseParenToken);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x0010FD21 File Offset: 0x0010DF21
		public ParenthesizedPatternSyntax WithPattern(PatternSyntax pattern)
		{
			return this.Update(this.OpenParenToken, pattern, this.CloseParenToken);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x0010FD36 File Offset: 0x0010DF36
		public ParenthesizedPatternSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Pattern, closeParenToken);
		}

		// Token: 0x040014B5 RID: 5301
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax pattern;
	}
}
