using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000346 RID: 838
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ElifDirectiveTriviaSyntax : ConditionalDirectiveTriviaSyntax
	{
		// Token: 0x060046DC RID: 18140 RVA: 0x0011905D File Offset: 0x0011725D
		internal ElifDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x00119068 File Offset: 0x00117268
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((ElifDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x00119087 File Offset: 0x00117287
		public SyntaxToken ElifKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ElifDirectiveTriviaSyntax)base.Green).elifKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x001190AD File Offset: 0x001172AD
		public override ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 2);
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x001190BC File Offset: 0x001172BC
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((ElifDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x001190E2 File Offset: 0x001172E2
		public override bool IsActive
		{
			get
			{
				return ((ElifDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x001190F4 File Offset: 0x001172F4
		public override bool BranchTaken
		{
			get
			{
				return ((ElifDirectiveTriviaSyntax)base.Green).BranchTaken;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x00119106 File Offset: 0x00117306
		public override bool ConditionValue
		{
			get
			{
				return ((ElifDirectiveTriviaSyntax)base.Green).ConditionValue;
			}
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x00119118 File Offset: 0x00117318
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.condition, 2);
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x0011912D File Offset: 0x0011732D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.condition;
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x0011913B File Offset: 0x0011733B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElifDirectiveTrivia(this);
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x00119144 File Offset: 0x00117344
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElifDirectiveTrivia(this);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x00119150 File Offset: 0x00117350
		public ElifDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			if (!(hashToken != this.HashToken) && !(elifKeyword != this.ElifKeyword) && condition == this.Condition && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			ElifDirectiveTriviaSyntax elifDirectiveTriviaSyntax = SyntaxFactory.ElifDirectiveTrivia(hashToken, elifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return elifDirectiveTriviaSyntax;
			}
			return elifDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x001191BB File Offset: 0x001173BB
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x001191C4 File Offset: 0x001173C4
		public new ElifDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.ElifKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x001191F1 File Offset: 0x001173F1
		public ElifDirectiveTriviaSyntax WithElifKeyword(SyntaxToken elifKeyword)
		{
			return this.Update(this.HashToken, elifKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x0011921E File Offset: 0x0011741E
		internal override ConditionalDirectiveTriviaSyntax WithConditionCore(ExpressionSyntax condition)
		{
			return this.WithCondition(condition);
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00119227 File Offset: 0x00117427
		public new ElifDirectiveTriviaSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.HashToken, this.ElifKeyword, condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00119254 File Offset: 0x00117454
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x0011925D File Offset: 0x0011745D
		public new ElifDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.ElifKeyword, this.Condition, endOfDirectiveToken, this.IsActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x0011928A File Offset: 0x0011748A
		public ElifDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.ElifKeyword, this.Condition, this.EndOfDirectiveToken, isActive, this.BranchTaken, this.ConditionValue);
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x001192B7 File Offset: 0x001174B7
		public ElifDirectiveTriviaSyntax WithBranchTaken(bool branchTaken)
		{
			return this.Update(this.HashToken, this.ElifKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, branchTaken, this.ConditionValue);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x001192E4 File Offset: 0x001174E4
		public ElifDirectiveTriviaSyntax WithConditionValue(bool conditionValue)
		{
			return this.Update(this.HashToken, this.ElifKeyword, this.Condition, this.EndOfDirectiveToken, this.IsActive, this.BranchTaken, conditionValue);
		}

		// Token: 0x0400152B RID: 5419
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;
	}
}
