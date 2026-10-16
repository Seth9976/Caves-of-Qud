using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000434 RID: 1076
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SingleValueCaseClauseOperation : BaseCaseClauseOperation, ISingleValueCaseClauseOperation, ICaseClauseOperation, IOperation
	{
		// Token: 0x06002947 RID: 10567 RVA: 0x000782F7 File Offset: 0x000764F7
		internal SingleValueCaseClauseOperation(IOperation value, [Nullable(2)] ILabelSymbol label, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(label, semanticModel, syntax, isImplicit)
		{
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x00078312 File Offset: 0x00076512
		public IOperation Value { get; }

		// Token: 0x06002949 RID: 10569 RVA: 0x0007831C File Offset: 0x0007651C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00078350 File Offset: 0x00076550
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			if (previousSlot != -1)
			{
				if (previousSlot > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
				}
			}
			else if (this.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x00078387 File Offset: 0x00076587
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x0007838A File Offset: 0x0007658A
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x0007838D File Offset: 0x0007658D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CaseClause;
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x00078391 File Offset: 0x00076591
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSingleValueCaseClause(this);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0007839A File Offset: 0x0007659A
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSingleValueCaseClause(this, argument);
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000783A4 File Offset: 0x000765A4
		public override CaseKind CaseKind
		{
			get
			{
				return CaseKind.SingleValue;
			}
		}
	}
}
