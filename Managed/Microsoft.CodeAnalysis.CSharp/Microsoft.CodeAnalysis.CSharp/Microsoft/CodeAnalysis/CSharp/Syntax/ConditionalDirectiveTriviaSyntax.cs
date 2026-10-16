using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000344 RID: 836
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class ConditionalDirectiveTriviaSyntax : BranchingDirectiveTriviaSyntax
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x00118D95 File Offset: 0x00116F95
		internal ConditionalDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x060046C1 RID: 18113
		public abstract ExpressionSyntax Condition { get; }

		// Token: 0x060046C2 RID: 18114 RVA: 0x00118DA0 File Offset: 0x00116FA0
		public ConditionalDirectiveTriviaSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.WithConditionCore(condition);
		}

		// Token: 0x060046C3 RID: 18115
		internal abstract ConditionalDirectiveTriviaSyntax WithConditionCore(ExpressionSyntax condition);

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x060046C4 RID: 18116
		public abstract bool ConditionValue { get; }
	}
}
