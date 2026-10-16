using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E4 RID: 740
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SubpatternSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600418B RID: 16779 RVA: 0x0010FA96 File Offset: 0x0010DC96
		internal SubpatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x0010FAA1 File Offset: 0x0010DCA1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameColonSyntax NameColon
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRedAtZero<NameColonSyntax>(ref this.nameColon);
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x0010FAAF File Offset: 0x0010DCAF
		public PatternSyntax Pattern
		{
			get
			{
				return base.GetRed<PatternSyntax>(ref this.pattern, 1);
			}
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0010FAC0 File Offset: 0x0010DCC0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<PatternSyntax>(ref this.pattern, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<NameColonSyntax>(ref this.nameColon);
			}
			return syntaxNode;
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0010FAF8 File Offset: 0x0010DCF8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.pattern;
				}
			}
			else
			{
				syntaxNode = this.nameColon;
			}
			return syntaxNode;
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x0010FB23 File Offset: 0x0010DD23
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSubpattern(this);
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x0010FB2C File Offset: 0x0010DD2C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSubpattern(this);
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x0010FB38 File Offset: 0x0010DD38
		public SubpatternSyntax Update([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, PatternSyntax pattern)
		{
			if (nameColon == this.NameColon && pattern == this.Pattern)
			{
				return this;
			}
			SubpatternSyntax subpatternSyntax = SyntaxFactory.Subpattern(nameColon, pattern);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return subpatternSyntax;
			}
			return subpatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x0010FB78 File Offset: 0x0010DD78
		public SubpatternSyntax WithNameColon([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon)
		{
			return this.Update(nameColon, this.Pattern);
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x0010FB87 File Offset: 0x0010DD87
		public SubpatternSyntax WithPattern(PatternSyntax pattern)
		{
			return this.Update(this.NameColon, pattern);
		}

		// Token: 0x040014B2 RID: 5298
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameColonSyntax nameColon;

		// Token: 0x040014B3 RID: 5299
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax pattern;
	}
}
