using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E5 RID: 997
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ForLoopOperation : BaseLoopOperation, IForLoopOperation, ILoopOperation, IOperation
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x00073990 File Offset: 0x00071B90
		[NullableContext(1)]
		internal ForLoopOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> before, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> conditionLocals, [Nullable(2)] IOperation condition, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> atLoopBottom, IOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, ILabelSymbol continueLabel, ILabelSymbol exitLabel, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(body, locals, continueLabel, exitLabel, semanticModel, syntax, isImplicit)
		{
			this.Before = Operation.SetParentOperation<IOperation>(before, this);
			this.ConditionLocals = conditionLocals;
			this.Condition = Operation.SetParentOperation<IOperation>(condition, this);
			this.AtLoopBottom = Operation.SetParentOperation<IOperation>(atLoopBottom, this);
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000739E0 File Offset: 0x00071BE0
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Before
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000739E8 File Offset: 0x00071BE8
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> ConditionLocals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000739F0 File Offset: 0x00071BF0
		public IOperation Condition { get; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x000739F8 File Offset: 0x00071BF8
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> AtLoopBottom
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00073A00 File Offset: 0x00071C00
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (index < this.Before.Length)
				{
					return this.Before[index];
				}
				break;
			case 1:
				if (this.Condition != null)
				{
					return this.Condition;
				}
				break;
			case 2:
				if (base.Body != null)
				{
					return base.Body;
				}
				break;
			case 3:
				if (index < this.AtLoopBottom.Length)
				{
					return this.AtLoopBottom[index];
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00073AA4 File Offset: 0x00071CA4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Before.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Before.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				IL_006F:
				if (base.Body != null)
				{
					return new ValueTuple<bool, int, int>(true, 2, 0);
				}
				goto IL_0080;
			case 2:
				goto IL_0080;
			case 3:
				if (previousIndex + 1 < this.AtLoopBottom.Length)
				{
					return new ValueTuple<bool, int, int>(true, 3, previousIndex + 1);
				}
				goto IL_00B7;
			case 4:
				goto IL_00B7;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Condition != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			goto IL_006F;
			IL_0080:
			if (!this.AtLoopBottom.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 3, 0);
			}
			IL_00B7:
			return new ValueTuple<bool, int, int>(false, 4, 0);
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00073B82 File Offset: 0x00071D82
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00073B85 File Offset: 0x00071D85
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x00073B88 File Offset: 0x00071D88
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Loop;
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00073B8B File Offset: 0x00071D8B
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitForLoop(this);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00073B94 File Offset: 0x00071D94
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitForLoop(this, argument);
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x00073B9E File Offset: 0x00071D9E
		public override LoopKind LoopKind
		{
			get
			{
				return LoopKind.For;
			}
		}
	}
}
