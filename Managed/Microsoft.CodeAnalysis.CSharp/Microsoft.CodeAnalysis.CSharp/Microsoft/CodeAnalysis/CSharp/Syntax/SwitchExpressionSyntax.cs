using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002FF RID: 767
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SwitchExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060042A3 RID: 17059 RVA: 0x0011124E File Offset: 0x0010F44E
		internal SwitchExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x00111259 File Offset: 0x0010F459
		public ExpressionSyntax GoverningExpression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.governingExpression);
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x00111267 File Offset: 0x0010F467
		public SyntaxToken SwitchKeyword
		{
			get
			{
				return new SyntaxToken(this, ((SwitchExpressionSyntax)base.Green).switchKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x0011128D File Offset: 0x0010F48D
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((SwitchExpressionSyntax)base.Green).openBraceToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x001112B4 File Offset: 0x0010F4B4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<SwitchExpressionArmSyntax> Arms
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.arms, 3);
				if (red == null)
				{
					return default(SeparatedSyntaxList<SwitchExpressionArmSyntax>);
				}
				return new SeparatedSyntaxList<SwitchExpressionArmSyntax>(red, base.GetChildIndex(3));
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x001112E9 File Offset: 0x0010F4E9
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((SwitchExpressionSyntax)base.Green).closeBraceToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x00111310 File Offset: 0x0010F510
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed(ref this.arms, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<ExpressionSyntax>(ref this.governingExpression);
			}
			return syntaxNode;
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x00111348 File Offset: 0x0010F548
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.arms;
				}
			}
			else
			{
				syntaxNode = this.governingExpression;
			}
			return syntaxNode;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x00111373 File Offset: 0x0010F573
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchExpression(this);
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0011137C File Offset: 0x0010F57C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchExpression(this);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x00111388 File Offset: 0x0010F588
		public SwitchExpressionSyntax Update(ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
		{
			if (governingExpression == this.GoverningExpression && !(switchKeyword != this.SwitchKeyword) && !(openBraceToken != this.OpenBraceToken) && !(arms != this.Arms) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			SwitchExpressionSyntax switchExpressionSyntax = SyntaxFactory.SwitchExpression(governingExpression, switchKeyword, openBraceToken, arms, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return switchExpressionSyntax;
			}
			return switchExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x001113FE File Offset: 0x0010F5FE
		public SwitchExpressionSyntax WithGoverningExpression(ExpressionSyntax governingExpression)
		{
			return this.Update(governingExpression, this.SwitchKeyword, this.OpenBraceToken, this.Arms, this.CloseBraceToken);
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x0011141F File Offset: 0x0010F61F
		public SwitchExpressionSyntax WithSwitchKeyword(SyntaxToken switchKeyword)
		{
			return this.Update(this.GoverningExpression, switchKeyword, this.OpenBraceToken, this.Arms, this.CloseBraceToken);
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x00111440 File Offset: 0x0010F640
		public SwitchExpressionSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.GoverningExpression, this.SwitchKeyword, openBraceToken, this.Arms, this.CloseBraceToken);
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x00111461 File Offset: 0x0010F661
		public SwitchExpressionSyntax WithArms([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SwitchExpressionArmSyntax> arms)
		{
			return this.Update(this.GoverningExpression, this.SwitchKeyword, this.OpenBraceToken, arms, this.CloseBraceToken);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x00111482 File Offset: 0x0010F682
		public SwitchExpressionSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.GoverningExpression, this.SwitchKeyword, this.OpenBraceToken, this.Arms, closeBraceToken);
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x001114A4 File Offset: 0x0010F6A4
		public SwitchExpressionSyntax AddArms(params SwitchExpressionArmSyntax[] items)
		{
			return this.WithArms(this.Arms.AddRange(items));
		}

		// Token: 0x040014CB RID: 5323
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax governingExpression;

		// Token: 0x040014CC RID: 5324
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode arms;
	}
}
