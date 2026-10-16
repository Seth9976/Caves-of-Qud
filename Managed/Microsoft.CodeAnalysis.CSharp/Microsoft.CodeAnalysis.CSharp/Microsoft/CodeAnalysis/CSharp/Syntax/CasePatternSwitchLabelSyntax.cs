using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002FC RID: 764
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CasePatternSwitchLabelSyntax : SwitchLabelSyntax
	{
		// Token: 0x06004279 RID: 17017 RVA: 0x00110E63 File Offset: 0x0010F063
		internal CasePatternSwitchLabelSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x00110E6E File Offset: 0x0010F06E
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((CasePatternSwitchLabelSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x00110E8D File Offset: 0x0010F08D
		public PatternSyntax Pattern
		{
			get
			{
				return base.GetRed<PatternSyntax>(ref this.pattern, 1);
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x00110E9C File Offset: 0x0010F09C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public WhenClauseSyntax WhenClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<WhenClauseSyntax>(ref this.whenClause, 2);
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x00110EAB File Offset: 0x0010F0AB
		public override SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((CasePatternSwitchLabelSyntax)base.Green).colonToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x00110ED4 File Offset: 0x0010F0D4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<WhenClauseSyntax>(ref this.whenClause, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRed<PatternSyntax>(ref this.pattern, 1);
			}
			return syntaxNode;
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x00110F10 File Offset: 0x0010F110
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.whenClause;
				}
			}
			else
			{
				syntaxNode = this.pattern;
			}
			return syntaxNode;
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x00110F3C File Offset: 0x0010F13C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCasePatternSwitchLabel(this);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x00110F45 File Offset: 0x0010F145
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCasePatternSwitchLabel(this);
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x00110F50 File Offset: 0x0010F150
		public CasePatternSwitchLabelSyntax Update(SyntaxToken keyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken colonToken)
		{
			if (!(keyword != this.Keyword) && pattern == this.Pattern && whenClause == this.WhenClause && !(colonToken != this.ColonToken))
			{
				return this;
			}
			CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = SyntaxFactory.CasePatternSwitchLabel(keyword, pattern, whenClause, colonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return casePatternSwitchLabelSyntax;
			}
			return casePatternSwitchLabelSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x00110FB0 File Offset: 0x0010F1B0
		internal override SwitchLabelSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x00110FB9 File Offset: 0x0010F1B9
		public new CasePatternSwitchLabelSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.Pattern, this.WhenClause, this.ColonToken);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x00110FD4 File Offset: 0x0010F1D4
		public CasePatternSwitchLabelSyntax WithPattern(PatternSyntax pattern)
		{
			return this.Update(this.Keyword, pattern, this.WhenClause, this.ColonToken);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x00110FEF File Offset: 0x0010F1EF
		public CasePatternSwitchLabelSyntax WithWhenClause([global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause)
		{
			return this.Update(this.Keyword, this.Pattern, whenClause, this.ColonToken);
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x0011100A File Offset: 0x0010F20A
		internal override SwitchLabelSyntax WithColonTokenCore(SyntaxToken colonToken)
		{
			return this.WithColonToken(colonToken);
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x00111013 File Offset: 0x0010F213
		public new CasePatternSwitchLabelSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Keyword, this.Pattern, this.WhenClause, colonToken);
		}

		// Token: 0x040014C8 RID: 5320
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax pattern;

		// Token: 0x040014C9 RID: 5321
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private WhenClauseSyntax whenClause;
	}
}
