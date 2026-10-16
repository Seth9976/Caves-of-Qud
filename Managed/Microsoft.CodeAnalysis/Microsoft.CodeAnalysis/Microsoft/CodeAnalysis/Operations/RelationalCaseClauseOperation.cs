using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000433 RID: 1075
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RelationalCaseClauseOperation : BaseCaseClauseOperation, IRelationalCaseClauseOperation, ICaseClauseOperation, IOperation
	{
		// Token: 0x0600293C RID: 10556 RVA: 0x00078237 File Offset: 0x00076437
		internal RelationalCaseClauseOperation(IOperation value, BinaryOperatorKind relation, [Nullable(2)] ILabelSymbol label, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(label, semanticModel, syntax, isImplicit)
		{
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.Relation = relation;
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x0007825A File Offset: 0x0007645A
		public IOperation Value { get; }

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x00078262 File Offset: 0x00076462
		public BinaryOperatorKind Relation { get; }

		// Token: 0x0600293F RID: 10559 RVA: 0x0007826C File Offset: 0x0007646C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000782A0 File Offset: 0x000764A0
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

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000782D7 File Offset: 0x000764D7
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x000782DA File Offset: 0x000764DA
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000782DD File Offset: 0x000764DD
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CaseClause;
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000782E1 File Offset: 0x000764E1
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitRelationalCaseClause(this);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000782EA File Offset: 0x000764EA
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitRelationalCaseClause(this, argument);
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000782F4 File Offset: 0x000764F4
		public override CaseKind CaseKind
		{
			get
			{
				return CaseKind.Relational;
			}
		}
	}
}
