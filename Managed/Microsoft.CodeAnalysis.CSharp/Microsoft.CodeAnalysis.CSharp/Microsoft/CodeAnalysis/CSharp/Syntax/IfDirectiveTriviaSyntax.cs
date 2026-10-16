using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000345 RID: 837
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IfDirectiveTriviaSyntax : ConditionalDirectiveTriviaSyntax
	{
		// Token: 0x060046C5 RID: 18117 RVA: 0x00118DA9 File Offset: 0x00116FA9
		internal IfDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x00118DB4 File Offset: 0x00116FB4
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((IfDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x00118DD3 File Offset: 0x00116FD3
		public SyntaxToken IfKeyword
		{
			get
			{
				return new SyntaxToken(this, ((IfDirectiveTriviaSyntax)base.Green).ifKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x00118DF9 File Offset: 0x00116FF9
		public override ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 2);
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x00118E08 File Offset: 0x00117008
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((IfDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x00118E2E File Offset: 0x0011702E
		public override bool IsActive
		{
			get
			{
				return ((IfDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x060046CB RID: 18123 RVA: 0x00118E40 File Offset: 0x00117040
		public override bool BranchTaken
		{
			get
			{
				return ((IfDirectiveTriviaSyntax)base.Green).BranchTaken;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x060046CC RID: 18124 RVA: 0x00118E52 File Offset: 0x00117052
		public override bool ConditionValue
		{
			get
			{
				return ((IfDirectiveTriviaSyntax)base.Green).ConditionValue;
			}
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00118E64 File Offset: 0x00117064
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.condition, 2);
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x00118E79 File Offset: 0x00117079
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.condition;
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00118E87 File Offset: 0x00117087
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIfDirectiveTrivia(this);
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00118E90 File Offset: 0x00117090
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIfDirectiveTrivia(this);
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00118E9C File Offset: 0x0011709C
		public IfDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			if (!(hashToken != this.HashToken) && !(ifKeyword != this.IfKeyword) && condition == this.Condition && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			IfDirectiveTriviaSyntax ifDirectiveTriviaSyntax = SyntaxFactory.IfDirectiveTrivia(hashToken, ifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return ifDirectiveTriviaSyntax;
			}
			return ifDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x00118F07 File Offset: 0x00117107
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00118F10 File Offset: 0x00117110
		public new IfDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.IfKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00118F3D File Offset: 0x0011713D
		public IfDirectiveTriviaSyntax WithIfKeyword(SyntaxToken ifKeyword)
		{
			return this.Update(this.HashToken, ifKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x00118F6A File Offset: 0x0011716A
		internal override ConditionalDirectiveTriviaSyntax WithConditionCore(ExpressionSyntax condition)
		{
			return this.WithCondition(condition);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00118F73 File Offset: 0x00117173
		public new IfDirectiveTriviaSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.HashToken, this.IfKeyword, condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x00118FA0 File Offset: 0x001171A0
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x00118FA9 File Offset: 0x001171A9
		public new IfDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.IfKeyword, this.Condition, endOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00118FD6 File Offset: 0x001171D6
		public IfDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.IfKeyword, this.Condition, this.EndOfDirectiveToken, isActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x00119003 File Offset: 0x00117203
		public IfDirectiveTriviaSyntax WithBranchTaken(bool branchTaken)
		{
			return this.Update(this.HashToken, this.IfKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, branchTaken, this.ConditionValue);
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00119030 File Offset: 0x00117230
		public IfDirectiveTriviaSyntax WithConditionValue(bool conditionValue)
		{
			return this.Update(this.HashToken, this.IfKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, conditionValue);
		}

		// Token: 0x0400152A RID: 5418
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;
	}
}
