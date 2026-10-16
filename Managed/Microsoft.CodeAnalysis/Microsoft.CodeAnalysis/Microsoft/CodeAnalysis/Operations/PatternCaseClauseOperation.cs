using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000431 RID: 1073
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PatternCaseClauseOperation : BaseCaseClauseOperation, IPatternCaseClauseOperation, ICaseClauseOperation, IOperation
	{
		// Token: 0x06002925 RID: 10533 RVA: 0x00078017 File Offset: 0x00076217
		internal PatternCaseClauseOperation(ILabelSymbol label, IPatternOperation pattern, [Nullable(2)] IOperation guard, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(label, semanticModel, syntax, isImplicit)
		{
			this.Pattern = Operation.SetParentOperation<IPatternOperation>(pattern, this);
			this.Guard = Operation.SetParentOperation<IOperation>(guard, this);
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x00078040 File Offset: 0x00076240
		public new ILabelSymbol Label
		{
			get
			{
				return base.Label;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x00078048 File Offset: 0x00076248
		public IPatternOperation Pattern { get; }

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00078050 File Offset: 0x00076250
		[Nullable(2)]
		public IOperation Guard
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x00078058 File Offset: 0x00076258
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Guard != null)
					{
						return this.Guard;
					}
				}
			}
			else if (this.Pattern != null)
			{
				return this.Pattern;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000780A8 File Offset: 0x000762A8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Pattern != null)
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
			if (this.Guard != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x0007810B File Offset: 0x0007630B
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x0007810E File Offset: 0x0007630E
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x00078111 File Offset: 0x00076311
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CaseClause;
			}
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x00078115 File Offset: 0x00076315
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitPatternCaseClause(this);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0007811E File Offset: 0x0007631E
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitPatternCaseClause(this, argument);
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x00078128 File Offset: 0x00076328
		public override CaseKind CaseKind
		{
			get
			{
				return CaseKind.Pattern;
			}
		}
	}
}
