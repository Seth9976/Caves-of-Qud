using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E4 RID: 996
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ForEachLoopOperation : BaseLoopOperation, IForEachLoopOperation, ILoopOperation, IOperation
	{
		// Token: 0x0600261E RID: 9758 RVA: 0x000737AC File Offset: 0x000719AC
		internal ForEachLoopOperation(IOperation loopControlVariable, IOperation collection, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> nextVariables, [Nullable(2)] ForEachLoopOperationInfo info, bool isAsynchronous, IOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, ILabelSymbol continueLabel, ILabelSymbol exitLabel, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(body, locals, continueLabel, exitLabel, semanticModel, syntax, isImplicit)
		{
			this.LoopControlVariable = Operation.SetParentOperation<IOperation>(loopControlVariable, this);
			this.Collection = Operation.SetParentOperation<IOperation>(collection, this);
			this.NextVariables = Operation.SetParentOperation<IOperation>(nextVariables, this);
			this.Info = info;
			this.IsAsynchronous = isAsynchronous;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x00073804 File Offset: 0x00071A04
		public IOperation LoopControlVariable { get; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x0007380C File Offset: 0x00071A0C
		public IOperation Collection { get; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x00073814 File Offset: 0x00071A14
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> NextVariables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x0007381C File Offset: 0x00071A1C
		[Nullable(2)]
		public ForEachLoopOperationInfo Info
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x00073824 File Offset: 0x00071A24
		public bool IsAsynchronous { get; }

		// Token: 0x06002624 RID: 9764 RVA: 0x0007382C File Offset: 0x00071A2C
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.Collection != null)
				{
					return this.Collection;
				}
				break;
			case 1:
				if (this.LoopControlVariable != null)
				{
					return this.LoopControlVariable;
				}
				break;
			case 2:
				if (base.Body != null)
				{
					return base.Body;
				}
				break;
			case 3:
				if (index < this.NextVariables.Length)
				{
					return this.NextVariables[index];
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000738BC File Offset: 0x00071ABC
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Collection != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0046;
			case 2:
				goto IL_0057;
			case 3:
				if (previousIndex + 1 < this.NextVariables.Length)
				{
					return new ValueTuple<bool, int, int>(true, 3, previousIndex + 1);
				}
				goto IL_008E;
			case 4:
				goto IL_008E;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.LoopControlVariable != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0046:
			if (base.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0057:
			if (!this.NextVariables.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 3, 0);
			}
			IL_008E:
			return new ValueTuple<bool, int, int>(false, 4, 0);
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x00073971 File Offset: 0x00071B71
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x00073974 File Offset: 0x00071B74
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00073977 File Offset: 0x00071B77
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Loop;
			}
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0007397A File Offset: 0x00071B7A
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitForEachLoop(this);
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00073983 File Offset: 0x00071B83
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitForEachLoop(this, argument);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x0007398D File Offset: 0x00071B8D
		public override LoopKind LoopKind
		{
			get
			{
				return LoopKind.ForEach;
			}
		}
	}
}
