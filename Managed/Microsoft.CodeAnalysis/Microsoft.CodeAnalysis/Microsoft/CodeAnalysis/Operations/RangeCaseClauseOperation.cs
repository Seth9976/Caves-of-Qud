using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000432 RID: 1074
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RangeCaseClauseOperation : BaseCaseClauseOperation, IRangeCaseClauseOperation, ICaseClauseOperation, IOperation
	{
		// Token: 0x06002931 RID: 10545 RVA: 0x0007812B File Offset: 0x0007632B
		internal RangeCaseClauseOperation(IOperation minimumValue, IOperation maximumValue, [Nullable(2)] ILabelSymbol label, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(label, semanticModel, syntax, isImplicit)
		{
			this.MinimumValue = Operation.SetParentOperation<IOperation>(minimumValue, this);
			this.MaximumValue = Operation.SetParentOperation<IOperation>(maximumValue, this);
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x00078154 File Offset: 0x00076354
		public IOperation MinimumValue { get; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x0007815C File Offset: 0x0007635C
		public IOperation MaximumValue { get; }

		// Token: 0x06002934 RID: 10548 RVA: 0x00078164 File Offset: 0x00076364
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.MaximumValue != null)
					{
						return this.MaximumValue;
					}
				}
			}
			else if (this.MinimumValue != null)
			{
				return this.MinimumValue;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000781B4 File Offset: 0x000763B4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.MinimumValue != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.MaximumValue != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x00078217 File Offset: 0x00076417
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x0007821A File Offset: 0x0007641A
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x0007821D File Offset: 0x0007641D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CaseClause;
			}
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00078221 File Offset: 0x00076421
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitRangeCaseClause(this);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0007822A File Offset: 0x0007642A
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitRangeCaseClause(this, argument);
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x00078234 File Offset: 0x00076434
		public override CaseKind CaseKind
		{
			get
			{
				return CaseKind.Range;
			}
		}
	}
}
