using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E6 RID: 998
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ForToLoopOperation : BaseLoopOperation, IForToLoopOperation, ILoopOperation, IOperation
	{
		// Token: 0x06002639 RID: 9785 RVA: 0x00073BA4 File Offset: 0x00071DA4
		internal ForToLoopOperation(IOperation loopControlVariable, IOperation initialValue, IOperation limitValue, IOperation stepValue, bool isChecked, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> nextVariables, [TupleElementNames(new string[] { "LoopObject", "UserDefinedInfo" })] [Nullable(new byte[] { 0, 1, 1 })] ValueTuple<ILocalSymbol, ForToLoopOperationUserDefinedInfo> info, IOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, ILabelSymbol continueLabel, ILabelSymbol exitLabel, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(body, locals, continueLabel, exitLabel, semanticModel, syntax, isImplicit)
		{
			this.LoopControlVariable = Operation.SetParentOperation<IOperation>(loopControlVariable, this);
			this.InitialValue = Operation.SetParentOperation<IOperation>(initialValue, this);
			this.LimitValue = Operation.SetParentOperation<IOperation>(limitValue, this);
			this.StepValue = Operation.SetParentOperation<IOperation>(stepValue, this);
			this.IsChecked = isChecked;
			this.NextVariables = Operation.SetParentOperation<IOperation>(nextVariables, this);
			this.Info = info;
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x00073C18 File Offset: 0x00071E18
		public IOperation LoopControlVariable { get; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00073C20 File Offset: 0x00071E20
		public IOperation InitialValue { get; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00073C28 File Offset: 0x00071E28
		public IOperation LimitValue { get; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x00073C30 File Offset: 0x00071E30
		public IOperation StepValue { get; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00073C38 File Offset: 0x00071E38
		public bool IsChecked { get; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00073C40 File Offset: 0x00071E40
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> NextVariables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00073C48 File Offset: 0x00071E48
		[TupleElementNames(new string[] { "LoopObject", "UserDefinedInfo" })]
		[Nullable(new byte[] { 0, 1, 1 })]
		public ValueTuple<ILocalSymbol, ForToLoopOperationUserDefinedInfo> Info
		{
			[return: TupleElementNames(new string[] { "LoopObject", "UserDefinedInfo" })]
			[return: Nullable(new byte[] { 0, 1, 1 })]
			get;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00073C50 File Offset: 0x00071E50
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.LoopControlVariable != null)
				{
					return this.LoopControlVariable;
				}
				break;
			case 1:
				if (this.InitialValue != null)
				{
					return this.InitialValue;
				}
				break;
			case 2:
				if (this.LimitValue != null)
				{
					return this.LimitValue;
				}
				break;
			case 3:
				if (this.StepValue != null)
				{
					return this.StepValue;
				}
				break;
			case 4:
				if (base.Body != null)
				{
					return base.Body;
				}
				break;
			case 5:
				if (index < this.NextVariables.Length)
				{
					return this.NextVariables[index];
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00073D0C File Offset: 0x00071F0C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.LoopControlVariable != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0051;
			case 2:
				goto IL_0062;
			case 3:
				goto IL_0073;
			case 4:
				goto IL_0084;
			case 5:
				if (previousIndex + 1 < this.NextVariables.Length)
				{
					return new ValueTuple<bool, int, int>(true, 5, previousIndex + 1);
				}
				goto IL_00BB;
			case 6:
				goto IL_00BB;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.InitialValue != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0051:
			if (this.LimitValue != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0062:
			if (this.StepValue != null)
			{
				return new ValueTuple<bool, int, int>(true, 3, 0);
			}
			IL_0073:
			if (base.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 4, 0);
			}
			IL_0084:
			if (!this.NextVariables.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 5, 0);
			}
			IL_00BB:
			return new ValueTuple<bool, int, int>(false, 6, 0);
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00073DEE File Offset: 0x00071FEE
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x00073DF1 File Offset: 0x00071FF1
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00073DF4 File Offset: 0x00071FF4
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Loop;
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00073DF7 File Offset: 0x00071FF7
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitForToLoop(this);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00073E00 File Offset: 0x00072000
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitForToLoop(this, argument);
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x00073E0A File Offset: 0x0007200A
		public override LoopKind LoopKind
		{
			get
			{
				return LoopKind.ForTo;
			}
		}
	}
}
