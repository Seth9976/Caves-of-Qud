using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E7 RID: 999
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class WhileLoopOperation : BaseLoopOperation, IWhileLoopOperation, ILoopOperation, IOperation
	{
		// Token: 0x06002649 RID: 9801 RVA: 0x00073E0D File Offset: 0x0007200D
		[NullableContext(1)]
		internal WhileLoopOperation([Nullable(2)] IOperation condition, bool conditionIsTop, bool conditionIsUntil, [Nullable(2)] IOperation ignoredCondition, IOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, ILabelSymbol continueLabel, ILabelSymbol exitLabel, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(body, locals, continueLabel, exitLabel, semanticModel, syntax, isImplicit)
		{
			this.Condition = Operation.SetParentOperation<IOperation>(condition, this);
			this.ConditionIsTop = conditionIsTop;
			this.ConditionIsUntil = conditionIsUntil;
			this.IgnoredCondition = Operation.SetParentOperation<IOperation>(ignoredCondition, this);
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x00073E4C File Offset: 0x0007204C
		public IOperation Condition { get; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x00073E54 File Offset: 0x00072054
		public bool ConditionIsTop { get; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x00073E5C File Offset: 0x0007205C
		public bool ConditionIsUntil { get; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x00073E64 File Offset: 0x00072064
		public IOperation IgnoredCondition { get; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00073E6C File Offset: 0x0007206C
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x00073E6F File Offset: 0x0007206F
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x00073E72 File Offset: 0x00072072
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Loop;
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00073E75 File Offset: 0x00072075
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitWhileLoop(this);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00073E7E File Offset: 0x0007207E
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitWhileLoop(this, argument);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00073E88 File Offset: 0x00072088
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			WhileLoopOperation.<>c__DisplayClass21_0 CS$<>8__locals1;
			CS$<>8__locals1.slot = slot;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.index = index;
			if (!this.ConditionIsTop)
			{
				return this.<GetCurrent>g__getCurrentSwitchBottom|21_1(ref CS$<>8__locals1);
			}
			return this.<GetCurrent>g__getCurrentSwitchTop|21_0(ref CS$<>8__locals1);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00073EC8 File Offset: 0x000720C8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			WhileLoopOperation.<>c__DisplayClass22_0 CS$<>8__locals1;
			CS$<>8__locals1.previousSlot = previousSlot;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.previousIndex = previousIndex;
			if (!this.ConditionIsTop)
			{
				return this.<MoveNext>g__moveNextConditionIsBottom|22_1(ref CS$<>8__locals1);
			}
			return this.<MoveNext>g__moveNextConditionIsTop|22_0(ref CS$<>8__locals1);
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00073F06 File Offset: 0x00072106
		public override LoopKind LoopKind
		{
			get
			{
				return LoopKind.While;
			}
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00073F0C File Offset: 0x0007210C
		[NullableContext(1)]
		[CompilerGenerated]
		private IOperation <GetCurrent>g__getCurrentSwitchTop|21_0(ref WhileLoopOperation.<>c__DisplayClass21_0 A_1)
		{
			switch (A_1.slot)
			{
			case 0:
				if (this.Condition != null)
				{
					return this.Condition;
				}
				break;
			case 1:
				if (base.Body != null)
				{
					return base.Body;
				}
				break;
			case 2:
				if (this.IgnoredCondition != null)
				{
					return this.IgnoredCondition;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(A_1.slot, A_1.index));
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00073F84 File Offset: 0x00072184
		[NullableContext(1)]
		[CompilerGenerated]
		private IOperation <GetCurrent>g__getCurrentSwitchBottom|21_1(ref WhileLoopOperation.<>c__DisplayClass21_0 A_1)
		{
			switch (A_1.slot)
			{
			case 0:
				if (base.Body != null)
				{
					return base.Body;
				}
				break;
			case 1:
				if (this.Condition != null)
				{
					return this.Condition;
				}
				break;
			case 2:
				if (this.IgnoredCondition != null)
				{
					return this.IgnoredCondition;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(A_1.slot, A_1.index));
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00073FFC File Offset: 0x000721FC
		[NullableContext(0)]
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		private ValueTuple<bool, int, int> <MoveNext>g__moveNextConditionIsTop|22_0(ref WhileLoopOperation.<>c__DisplayClass22_0 A_1)
		{
			switch (A_1.previousSlot)
			{
			case -1:
				if (this.Condition != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0047;
			case 2:
			case 3:
				goto IL_0058;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(A_1.previousSlot, A_1.previousIndex));
			}
			if (base.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0047:
			if (this.IgnoredCondition != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0058:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00074088 File Offset: 0x00072288
		[NullableContext(0)]
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		private ValueTuple<bool, int, int> <MoveNext>g__moveNextConditionIsBottom|22_1(ref WhileLoopOperation.<>c__DisplayClass22_0 A_1)
		{
			switch (A_1.previousSlot)
			{
			case -1:
				if (base.Body != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0047;
			case 2:
			case 3:
				goto IL_0058;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(A_1.previousSlot, A_1.previousIndex));
			}
			if (this.Condition != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0047:
			if (this.IgnoredCondition != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0058:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}
	}
}
