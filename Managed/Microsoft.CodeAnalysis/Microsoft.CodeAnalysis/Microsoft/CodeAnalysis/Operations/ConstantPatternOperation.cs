using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000439 RID: 1081
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ConstantPatternOperation : BasePatternOperation, IConstantPatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002969 RID: 10601 RVA: 0x000785D8 File Offset: 0x000767D8
		internal ConstantPatternOperation(IOperation value, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x000785F5 File Offset: 0x000767F5
		public IOperation Value { get; }

		// Token: 0x0600296B RID: 10603 RVA: 0x00078600 File Offset: 0x00076800
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00078634 File Offset: 0x00076834
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

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0007866B File Offset: 0x0007686B
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0007866E File Offset: 0x0007686E
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x00078671 File Offset: 0x00076871
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ConstantPattern;
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00078675 File Offset: 0x00076875
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitConstantPattern(this);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0007867E File Offset: 0x0007687E
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitConstantPattern(this, argument);
		}
	}
}
