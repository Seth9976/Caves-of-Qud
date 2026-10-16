using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003FF RID: 1023
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class UnaryOperation : Operation, IUnaryOperation, IOperation
	{
		// Token: 0x0600273E RID: 10046 RVA: 0x000753D0 File Offset: 0x000735D0
		internal UnaryOperation(UnaryOperatorKind operatorKind, [Nullable(1)] IOperation operand, bool isLifted, bool isChecked, IMethodSymbol operatorMethod, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.OperatorKind = operatorKind;
			this.Operand = Operation.SetParentOperation<IOperation>(operand, this);
			this.IsLifted = isLifted;
			this.IsChecked = isChecked;
			this.OperatorMethod = operatorMethod;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x00075424 File Offset: 0x00073624
		public UnaryOperatorKind OperatorKind { get; }

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0007542C File Offset: 0x0007362C
		[Nullable(1)]
		public IOperation Operand
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x00075434 File Offset: 0x00073634
		public bool IsLifted { get; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x0007543C File Offset: 0x0007363C
		public bool IsChecked { get; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x00075444 File Offset: 0x00073644
		public IMethodSymbol OperatorMethod { get; }

		// Token: 0x06002744 RID: 10052 RVA: 0x0007544C File Offset: 0x0007364C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operand != null)
			{
				return this.Operand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00075480 File Offset: 0x00073680
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
			else if (this.Operand != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000754B7 File Offset: 0x000736B7
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000754BF File Offset: 0x000736BF
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000754C7 File Offset: 0x000736C7
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Unary;
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000754CB File Offset: 0x000736CB
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitUnaryOperator(this);
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000754D4 File Offset: 0x000736D4
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitUnaryOperator(this, argument);
		}
	}
}
