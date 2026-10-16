using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003EC RID: 1004
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LockOperation : Operation, ILockOperation, IOperation
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x00074338 File Offset: 0x00072538
		internal LockOperation(IOperation lockedValue, IOperation body, [Nullable(2)] ILocalSymbol lockTakenSymbol, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.LockedValue = Operation.SetParentOperation<IOperation>(lockedValue, this);
			this.Body = Operation.SetParentOperation<IOperation>(body, this);
			this.LockTakenSymbol = lockTakenSymbol;
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00074367 File Offset: 0x00072567
		public IOperation LockedValue { get; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x0007436F File Offset: 0x0007256F
		public IOperation Body { get; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x00074377 File Offset: 0x00072577
		[Nullable(2)]
		public ILocalSymbol LockTakenSymbol
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00074380 File Offset: 0x00072580
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Body != null)
					{
						return this.Body;
					}
				}
			}
			else if (this.LockedValue != null)
			{
				return this.LockedValue;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000743D0 File Offset: 0x000725D0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.LockedValue != null)
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
			if (this.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x00074433 File Offset: 0x00072633
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x00074436 File Offset: 0x00072636
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00074439 File Offset: 0x00072639
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Lock;
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0007443D File Offset: 0x0007263D
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitLock(this);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00074446 File Offset: 0x00072646
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitLock(this, argument);
		}
	}
}
