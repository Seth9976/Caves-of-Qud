using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200044C RID: 1100
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SwitchExpressionArmOperation : Operation, ISwitchExpressionArmOperation, IOperation
	{
		// Token: 0x06002A1F RID: 10783 RVA: 0x000795B9 File Offset: 0x000777B9
		internal SwitchExpressionArmOperation(IPatternOperation pattern, [Nullable(2)] IOperation guard, IOperation value, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Pattern = Operation.SetParentOperation<IPatternOperation>(pattern, this);
			this.Guard = Operation.SetParentOperation<IOperation>(guard, this);
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.Locals = locals;
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000795F6 File Offset: 0x000777F6
		public IPatternOperation Pattern { get; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x000795FE File Offset: 0x000777FE
		[Nullable(2)]
		public IOperation Guard
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x00079606 File Offset: 0x00077806
		public IOperation Value { get; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x0007960E File Offset: 0x0007780E
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00079618 File Offset: 0x00077818
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.Pattern != null)
				{
					return this.Pattern;
				}
				break;
			case 1:
				if (this.Guard != null)
				{
					return this.Guard;
				}
				break;
			case 2:
				if (this.Value != null)
				{
					return this.Value;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00079684 File Offset: 0x00077884
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
				goto IL_0040;
			case 2:
			case 3:
				goto IL_0051;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Guard != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0040:
			if (this.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0051:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x000796FC File Offset: 0x000778FC
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000796FF File Offset: 0x000778FF
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x00079702 File Offset: 0x00077902
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.SwitchExpressionArm;
			}
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00079706 File Offset: 0x00077906
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSwitchExpressionArm(this);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x0007970F File Offset: 0x0007790F
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSwitchExpressionArm(this, argument);
		}
	}
}
